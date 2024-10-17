import { IProduct } from "./IProduct"

export interface IPagination {
  pageIndex: number
  pageSize: number
  count: number
  items: IProduct[]
}
