import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotal } from '../models/IBasket';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrl: './order-total.component.scss'
})
export class OrderTotalComponent {
  BasketTotal$ =new Observable<IBasketTotal|null>;
  constructor(private basket: BasketService) { }
  ngOnInit()
  {
    this.BasketTotal$ = this.basket.basketTotal$;
  }
}
