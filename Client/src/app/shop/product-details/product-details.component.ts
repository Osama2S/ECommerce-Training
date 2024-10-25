import { Component, OnDestroy, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/IProduct';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit,OnDestroy{
  product: IProduct={} as IProduct;
  unsubscribe$!: Subscription;
  constructor(private _activeRout: ActivatedRoute,private _shopService:ShopService) { }
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
}
