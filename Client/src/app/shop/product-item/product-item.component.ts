import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IProduct } from '../../shared/models/IProduct';
import { BasketService } from '../../basket/basket.service';


@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() product!: IProduct;
  @Output() getProductDetails = new EventEmitter();
  constructor(private basket: BasketService) { }
  addItemToBasket()
  {
    this.basket.addItemInBasket(this.product);
  }
  onGetProductDetails()
  {
    this.getProductDetails.emit();
  }

}
