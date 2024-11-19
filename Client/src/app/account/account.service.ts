import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { IUser } from '../shared/models/user';
import { HttpClient, HttpContext, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient, private rout: Router) { }
  loadCurrentUser(token:string)
  {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<IUser>("api/account", { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem("token", user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }
  getCurrentUser() {
    return this.currentUserSource.value;
  }
  login(value: { email: string, password: string }) {

    return this.http.post<IUser>("api/account/login", value).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem("token", user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }
  register(value: any) {
    debugger;
    return this.http.post<IUser>("api/account/register", value).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem("token", user.token);
        }
      })
    )
  }
  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.rout.navigateByUrl('/home');
   }
  checkEmailExist(email: string) {
    return this.http.get("api/account/existEmail?email=" + email);
  }
}
