import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { OrderTotalComponent } from './order-total/order-total.component';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';




@NgModule({
  declarations: [
    OrderTotalComponent,
    BasketSummaryComponent,

  ],
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
    BsDropdownModule.forRoot()
  ],
  exports:[OrderTotalComponent,BasketSummaryComponent,ReactiveFormsModule,BsDropdownModule]
})
export class SharedModule { }
