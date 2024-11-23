import { Component } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  standalone:false,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'Client';

  constructor(private basketService: BasketService,private account:AccountService) { }
  loadBasketItem()
  {
    setTimeout(() => {

      if (typeof window !== 'undefined' && localStorage.getItem("Basketid")) {
        let basketid = JSON.parse(localStorage.getItem("Basketid")!);
        this.basketService.getBasket(+basketid).subscribe();
      } else {
        console.warn("Local storage is not available or basketid is missing.");
      }
    }, 100)
  }
  loadCurrentUser()
  {
    setTimeout(() => {
      if (typeof window !== 'undefined' &&localStorage.getItem("token")) {
        const token = localStorage.getItem("token");
        this.account.loadCurrentUser(token!).subscribe({
          next() {
            console.log("loaded user")
          },
          error(err) {
            console.log(err)
          }
        })
      } else {
        console.warn("Local storage is not available or token is missing.");
      }

    },100)
  }
  ngOnInit() {
    this.loadBasketItem();
    this.loadCurrentUser();
  }
}
