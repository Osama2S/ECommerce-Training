import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/models/IBasket';
import { BasketService } from '../../basket/basket.service';
import { IUser } from '../../shared/models/user';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  basket$!: Observable<IBasket | null>;
  currentUser$!: Observable<IUser|null>;
  constructor(private basketService: BasketService,private account:AccountService) { }
  ngOnInit()
  {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.account.currentUser$;
  }
  logout() {
    this.account.logout();
  }
}
