import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { IBasket, IBasketItem } from '../shared/models/IBasket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent {
  basket$!: Observable<IBasket|null>;
  constructor(private basketService: BasketService) { }
  ngOnInit()
  {
    this.basket$ = this.basketService.basket$;
  }
  removeItemBasket(item:IBasketItem)
  {
    this.basketService.removeItemFromBasket(item);
  }
  incrementItemBasket(item: IBasketItem)
  {
    this.basketService.incrementItemQuantity(item);
  }
  decrementItemBasket(item: IBasketItem)
  {
    this.basketService.decrementItemQuantity(item);
  }
}
