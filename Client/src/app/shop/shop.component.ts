import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/IProduct';
import { IBrand } from '../shared/models/IBrand';
import { IType } from '../shared/models/IType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  @ViewChild("search",{static:true}) searchTerm!:ElementRef;
  Products: IProduct[]=[];
  Brands: IBrand[] = [];
  Types: IType[] = [];
  NumberOfPages: number = 10;
  shopParams = new ShopParams();
  sortOption = [
    { name: "Alphabetical", value: "name" },
    { name: "Price : Low To High", value: "priceAsc" },
    { name: "Price :High To Low", value: "priceDesc" }
  ];
  constructor(private _shopService: ShopService) { }
  ngOnInit(): void {
    this.getAllProducts();
    this.getAllBrands();
    this.getAllTypes();
  }
  getAllProducts()
  {

    return this._shopService.getProducts(this.shopParams).subscribe({
      next: (values) => {
        this.Products = values.items;
        this.shopParams.CountProd = values.count;
        this.shopParams.PageSize = values.pageSize
        this.NumberOfPages = Math.ceil(values.count / values.pageSize) * 10;
      },
      error(err) {
        console.log(err)
      },
      complete() {
        console.log("Successfull Get Product");
      },
    })
  }
  getAllBrands()
  {
    return this._shopService.getBrands().subscribe({
      next: (values) => this.Brands = [{id:0,name:"All"},...values],
      error(err) {
        console.log(err)
      },
      complete() {
        console.log("Successfull Get Brand");
      },
    })
  }
  getAllTypes()
  {
    return this._shopService.getTypes().subscribe({
      next: (values) => this.Types = [{id:0,name:"All"},...values],
      error(err) {
        console.log(err)
      },
      complete() {
        console.log("Successfull Get Brand");
      },
    })
  }
  onBrandSelected(brandid:number) {
    this.shopParams.BrandId = brandid;
    this.getAllProducts();
  }
  onTypeSelected(typeid: number)
  {
    this.shopParams.TypeId = typeid;
    this.getAllProducts();
  }
  onSortSelected(select: Event) {
    let sort = (select.target as HTMLSelectElement).value;
    this.shopParams.Sort = sort;
    this.getAllProducts();
  }
  onChangePage(Index:number)
  {

    this.shopParams.pageIndex = Index;
    this.getAllProducts();
  }
  onSearchProduct()
  {
    this.shopParams.Search = this.searchTerm.nativeElement.value;
    this.getAllProducts();
  }
  onResetValue()
  {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getAllProducts();
  }
}
