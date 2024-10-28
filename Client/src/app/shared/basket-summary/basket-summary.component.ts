import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem } from '../models/IBasket';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.scss'
})
export class BasketSummaryComponent {
  basket$ = new Observable<IBasket|null>
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
