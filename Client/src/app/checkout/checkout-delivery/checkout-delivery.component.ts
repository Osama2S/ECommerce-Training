import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.services';
import { IDeliveryMethod } from '../../shared/models/deliveryMethod';
import { BasketService } from '../../basket/basket.service';

@Component({
  standalone:false,
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss'
})
export class CheckoutDeliveryComponent implements OnInit{
  @Input() checkoutForm!: FormGroup;
  deliveryMethods: IDeliveryMethod[] = [];
  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe({
      next:(dm: IDeliveryMethod[]) => {
        this.deliveryMethods = dm;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }
  constructor(private checkoutService:CheckoutService,private basket:BasketService){}
  setShippingPrice(deliverMethod:IDeliveryMethod) {
    this.basket.setShippingPrice(deliverMethod);
  }
}
