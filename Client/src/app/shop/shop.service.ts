import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/IPagination';
import { IBrand } from '../shared/models/IBrand';
import { IType } from '../shared/models/IType';
import { map } from 'rxjs';
import { response } from 'express';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  constructor(private http: HttpClient) { }
  getProducts(shopParams?:ShopParams ) {

    let params = new HttpParams();
    if (shopParams?.BrandId)
    {
      params=params.append("BrandId", shopParams.BrandId.toString());
    }

    if (shopParams?.TypeId)
    {
      params=params.append("TypeId", shopParams.TypeId.toString());
    }
      params=params.append("sort",shopParams!.Sort)

      params=params.append("pageIndex",shopParams!.pageIndex.toString())

    if (shopParams?.Search)
    {
      params = params.append("Search", shopParams.Search);
    }
    return this.http.get<IPagination>("/api/Product",{'params':params})
      .pipe(
        map(response=>{return response})
      );
  }
  getBrands() {
    return this.http.get<IBrand[]>("/api/Product/GetBrands");
  }
  getTypes() {
    return this.http.get<IType[]>("/api/Product/GetTypes");
  }
}
