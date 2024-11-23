import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/models/IBasket';
import { BehaviorSubject, map } from 'rxjs';
import { IProduct } from '../shared/models/IProduct';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private basketSource = new BehaviorSubject<IBasket | null>(null);
  private basketTotalSource=new BehaviorSubject<IBasketTotal | null>(null);
  basket$ = this.basketSource.asObservable();
  basketTotal$ = this.basketTotalSource.asObservable();
  shipping = 0;
  constructor(private http: HttpClient) { }
  setShippingPrice(deliverMethod: IDeliveryMethod) {
    this.shipping = deliverMethod.price;
    this.calculateTotalBasket();
  }
  getBasket(id:number)
  {

    return this.http.get("api/Basket?id=" + id)
      .pipe(map((basket: any) => {

        this.basketSource.next(basket);
        this.calculateTotalBasket();
      }));
  }
  setBasket(basket: IBasket)
  {

    return this.http.post("api/Basket", basket).subscribe({
      next:(basketItem:any) => {
        this.basketSource.next(basketItem);
        this.calculateTotalBasket();
      },
        error:error => console.log(error)
    }
    )
  }
  getCurrentBasketValue()
  {
    return this.basketSource.value;
  }
  removeItemFromBasket(item:IBasketItem)
  {
    const basket = this.getCurrentBasketValue();
    if (basket?.basket.some(x => x.id === item.id))
    {
      basket.basket = basket.basket.filter(x => x.id !== item.id);
      if (basket.basket.length > 0)
      {
        this.setBasket(basket);
      }
      else
      {
        this.deleateBasket(basket);
        }
    }
  }
  deleateBasket(basket: IBasket)
  {
    this.http.delete<IBasket>("api/Basket?id=" + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem("Basketid");
    }

  )
  }
  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundIndex = basket!.basket.findIndex(x => x.id === item.id);
    basket!.basket[foundIndex].quantity++;
    this.setBasket(basket!);
  }
  decrementItemQuantity(item: IBasketItem)
  {
    const basket = this.getCurrentBasketValue();
    const foundIndex = basket!.basket.findIndex(x => x.id === item.id);
    if (basket!.basket[foundIndex].quantity > 1)
    {
      basket!.basket[foundIndex].quantity--;
      this.setBasket(basket!);
    }
    else
    {
      this.removeItemFromBasket(item);
      }
  }
  addItemInBasket(item: IProduct, quantity: number = 1)
  {

    const itemToAdd: IBasketItem = this.mapProductToBasket(item, quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.basket = this.addOrUpdateItem(basket.basket, itemToAdd, quantity);
    this.setBasket(basket);
  }
  private calculateTotalBasket()
  {
    const basket = this.getCurrentBasketValue();
    const shipping = this.shipping;
    const subtotal = basket!.basket.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({ shipping, subtotal, total });
  }
  private addOrUpdateItem(items :IBasketItem[], itemToAdd: IBasketItem, quantity: number)
    : IBasketItem[]{

    const index = items.findIndex(basket => basket.id === itemToAdd.id);
    if (index === -1)
    {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    else
    {
      items[index].quantity += quantity;
    }
    return items;
  }
  private createBasket():IBasket {
    let basket = new Basket();
    let getBasketId = this.getCurrentBasketValue();
    if (getBasketId !== null)
    {
      basket.id=getBasketId.id
    }
    localStorage.setItem("Basketid", JSON.stringify(basket.id));
    return basket;
  }
  private mapProductToBasket(item: IProduct, quantity: number):IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      brand: item.productBrand,
      type: item.productType,
      pictureUrl: item.pictureUrl,
      price: item.price,
      quantity:quantity
    };
  }
}
