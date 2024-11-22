using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork UnitOfWork;

        public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfWork)
        {

            this.basketRepo=basketRepo;
            UnitOfWork=unitOfWork;
        }


        public async Task<Order> CreateOrderAsync(string buyEmail, int DeliveryMethod, int basketId, Address shippingAddress)
        {
            // get basket from the repo
            var basket = await basketRepo.GetBasketAsync(basketId);
            // get items from the product repo
            var items=new List<OrderItem>();
            foreach (var item in basket.basket)
            {
                var produtItem = await UnitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(produtItem.Id, produtItem.name, produtItem.pictureUrl!);
                var orderItem = new OrderItem(itemOrdered, produtItem.price, item.Quantity);
                items.Add(orderItem);
            }
            // get delivery method from repo
            var deliveryMethod = await UnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethod);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price*item.Quantity);

            // create order
            var order = new Order(items,buyEmail,shippingAddress,deliveryMethod,subtotal);
            UnitOfWork.Repository<Order>().Add(order);

            // save tp db
            var result = await UnitOfWork.Complete();
            if (result<=0) return null!;

            // delete basket
            await basketRepo.DeleteBasketAsync(basketId); ;
            // return order
            return order;
        }



        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await UnitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);
            return await UnitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);
            return await UnitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
