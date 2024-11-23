import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { OrderTotalComponent } from './order-total/order-total.component';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './stepper/stepper.component'



@NgModule({
  declarations: [
    OrderTotalComponent,
    BasketSummaryComponent,
    StepperComponent,

  ],
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    CdkStepperModule
  ],
  exports: [
    OrderTotalComponent,
    BasketSummaryComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    CdkStepperModule,
    StepperComponent]
})
export class SharedModule { }
