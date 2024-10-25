import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr=inject(ToastrService);
  return next(req).pipe(
    catchError((error:HttpErrorResponse) => {
      if(error)
      {
        if (error.status === 500)
        {
          router.navigate(["error/server-error"])
        }
        if (error.status === 404)
        {
          router.navigate(["error/not-found"])


        }
        if (error.status === 400)
        {
          if (error.error.errors) {
            throw error.error;
          } else {
            toastr.error(error.error.errorMessage,error.error.statusCode);
          }
        }
      }
      return throwError(() => error);
    })
  )
};
