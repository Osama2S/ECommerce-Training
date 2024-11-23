import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotal } from '../models/IBasket';
import { BasketService } from '../../basket/basket.service';

@Component({
  standalone:false,
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrl: './order-total.component.scss'
})
export class OrderTotalComponent {
  BasketTotal$ =new Observable<IBasketTotal|null>;
  constructor(private basket: BasketService) { }
  @Input() isBasket: boolean = true;
  ngOnInit()
  {
    this.BasketTotal$ = this.basket.basketTotal$;
  }
}
