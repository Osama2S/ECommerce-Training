import { Component, Input } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { CheckoutService } from '../checkout.services';
import { NavigationExtras, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { IBasket } from '../../shared/models/IBasket';
import { IOrder, IOrderToCreate } from '../../shared/models/Order';
import { IAddress } from '../../shared/models/address';

@Component({
  standalone:false,
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrl: './checkout-payment.component.scss'
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;
  constructor(private basketService: BasketService, private checkoutService: CheckoutService,
    private router: Router) { }
  submitOrder()
  {
    
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket!)
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order: IOrder) => {
        this.basketService.deleateBasket(basket!)
        const navigationExtras:NavigationExtras={state:order}
        this.router.navigate(['checkout/success'],navigationExtras)
      }, error: (err) => {
        console.log(err);
      }
    })
  }
    // private async createOrder(basket: IBasket | null) {
    //   if (!basket) throw new Error('Basket is null');
    //   const orderToCreate = this.getOrderToCreate(basket);
    //   return firstValueFrom(this.checkoutService.createOrder(orderToCreate));
    // }

  private getOrderToCreate(basket: IBasket): IOrderToCreate {

      return {
        basketId: basket.id,
        deliveryMethodId: +this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value,
        shipToAddress: this.checkoutForm?.get('addressForm')?.value
      }
    }
}
