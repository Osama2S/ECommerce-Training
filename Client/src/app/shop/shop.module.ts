import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { PaginationComponent } from './pagination/pagination.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [ShopComponent, ProductItemComponent,PaginationComponent, ProductDetailsComponent],
  imports: [
    CommonModule,
    NgbPaginationModule,
    FormsModule,
    ShopRoutingModule
],
  exports:[ShopComponent]
})
export class ShopModule { }
