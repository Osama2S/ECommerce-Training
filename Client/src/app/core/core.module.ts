import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { BadRequestComponent } from './bad-request/bad-request.component';
import { ErrorComponent } from '../error/error.component';
import {ToastrModule} from 'ngx-toastr'


@NgModule({
  declarations: [NavBarComponent,ServerErrorComponent, NotFoundComponent, ErrorComponent,BadRequestComponent],
  imports: [
    CommonModule,
    CoreRoutingModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates:true
    }),

  ],
  exports:[NavBarComponent]
})
export class CoreModule { }
