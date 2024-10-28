import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/models/IBasket';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  basket$!: Observable<IBasket|null>;
  constructor(private basketService: BasketService) { }
  ngOnInit()
  {
    this.basket$ = this.basketService.basket$;
  }
}
