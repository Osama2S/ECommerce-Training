
export interface IBasket{
    id: number
    basket: IBasketItem[]
}
export class Basket implements IBasket{
  id = 1;
  basket :IBasketItem[]=[]

}
export interface IBasketItem {
  id: number
  productName: string
  price: number
  pictureUrl: string
  quantity: number
  brand: string
  type: string
}

export interface IBasketTotal{
  total: number,
  subtotal: number,
  shipping:number
}
