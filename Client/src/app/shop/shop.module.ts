import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { PaginationComponent } from './pagination/pagination.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [ShopComponent, ProductItemComponent,PaginationComponent],
  imports: [
    CommonModule,
    NgbPaginationModule,
    FormsModule

],
  exports:[ShopComponent]
})
export class ShopModule { }
