import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { IProduct } from '../../shared/models/IProduct';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ShopService } from '../shop.service';
import { IBasketItem } from '../../shared/models/IBasket';
import { BasketService } from '../../basket/basket.service';

@Component({
  standalone:false,
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit, OnDestroy{
  @ViewChild("inputQuantity") quantity!: ElementRef;
  product: IProduct={} as IProduct;
  unsubscribe$!: Subscription;
  constructor(private _activeRout: ActivatedRoute,
    private _shopService: ShopService,
  private basketService:BasketService) { }
  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  ngOnInit() {
    this.getProductId();
    this.getProduct();

  }
  getProduct()
  {

    this._shopService.getProductSingle(this.product.id).subscribe({
      next: (value) => this.product = value,
      error: (err) => {
        console.log(err)
      },
      complete: () => {
        console.log("Complete get single product")
      }
    })
  }
  getProductId() {

    this.unsubscribe$ = this._activeRout.params.subscribe(param => {
      this.product.id = +param['id'];
    })
  }
  incrementItemBasket()
  {
    this.quantity.nativeElement.value = (+this.quantity.nativeElement.value+1).toString();
  }
  decrementItemBasket()
  {
    if (this.quantity.nativeElement.value > 1)
      this.quantity.nativeElement.value -= 1;
  }
  onBlur()
  {
    if (this.quantity.nativeElement.value < 1)
    {
      this.quantity.nativeElement.value = 1;

    }
  }
  onAddToCart()
  {
    this.basketService.addItemInBasket(this.product, +this.quantity.nativeElement.value);

  }
}
