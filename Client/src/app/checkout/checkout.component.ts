import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';

@Component({
  standalone:false,
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit{
  checkoutForm!: FormGroup;
  constructor(private fb:FormBuilder,private account:AccountService,private basketService: BasketService){}
  ngOnInit(): void {
    this.createCheckoutForm();
    this.getAddressFormValues();
    this.getDeliveryMethodValue();
  }
  createCheckoutForm()
  {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        street: [null, Validators.required],
        city: [null, Validators.required],
        zipCode: [null, Validators.required]
      }),
      deliveryForm: this.fb.group({
        deliveryMethod: [null, Validators.required]
      }),
      paymentFrom: this.fb.group({
        nameOnCard: [null, Validators.required]
      })
    });
  }
  getAddressFormValues()
  {

    this.account.getUserAddress().subscribe({
      next:(address) => {
        if (address)
        {
          this.checkoutForm.get('addressForm')?.patchValue(address)
        }
      },
      error: (err) => {
        console.log(err);
      }
    })
  }
  getDeliveryMethodValue()
  {
    const basket = this.basketService.getCurrentBasketValue();
    if (basket && basket.id) {
      this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')
        ?.patchValue(basket.id);
    }
  }

}
