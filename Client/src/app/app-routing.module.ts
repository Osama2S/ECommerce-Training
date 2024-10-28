import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ErrorComponent } from './error/error.component';

const routes: Routes = [
  {path:'',redirectTo:'home',pathMatch:'full'},
  { path: 'home', component: HomeComponent, title: 'home' },
  { path: 'shop', loadChildren: () => import("./shop/shop.module").then(x => x.ShopModule) },
  { path: 'error', loadChildren: () => import("./core/core.module").then(x => x.CoreModule) },
  { path: 'checkout', loadChildren: () => import("./checkout/checkout.module").then(x => x.CheckoutModule) },
  { path: 'basket', loadChildren: () => import("./basket/basket.module").then(x => x.BasketModule) },
  { path: 'test-error',component:ErrorComponent,title:'Error' },
  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
