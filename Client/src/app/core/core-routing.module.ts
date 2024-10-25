import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { BadRequestComponent } from './bad-request/bad-request.component';

const routes: Routes = [
  {path:'server-error',component:ServerErrorComponent,title:'error server'},
  {path:'not-found',component:NotFoundComponent,title:'not found'},
  {path:'bad-request',component:BadRequestComponent,title:'bad-request'},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
