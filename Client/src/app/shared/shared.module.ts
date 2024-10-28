import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderTotalComponent } from './order-total/order-total.component';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterLink } from '@angular/router';



@NgModule({
  declarations: [
    OrderTotalComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    RouterLink
  ],
  exports:[OrderTotalComponent,BasketSummaryComponent]
})
export class SharedModule { }
