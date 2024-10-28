import { Component } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'Client';

  constructor(private basketService: BasketService) { }
  ngOnInit() {
    setTimeout(() => {
      
      if (typeof window !== 'undefined' && localStorage.getItem("Basketid")) {
        let basketid = JSON.parse(localStorage.getItem("Basketid")!);
        this.basketService.getBasket(+basketid).subscribe();
      } else {
        console.warn("Local storage is not available or basketid is missing.");
      }
    }, 50)
  }
}
