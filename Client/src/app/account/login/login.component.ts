import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone:false,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  loginFrom!: FormGroup;
  emailError!: string;
  passwordError!: string;
  returnUrl: string = '/shop';
  constructor(private account:AccountService,private router:Router,private activeRouter:ActivatedRoute){}
  ngOnInit(): void {
    this.returnUrl=this.activeRouter.snapshot.queryParams['returnUrl']||'/shop'
    this.createLoginForm();
  }
  createLoginForm()
  {
    this.loginFrom = new FormGroup({
      email: new FormControl('', [Validators.required,Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('', [Validators.required,Validators.pattern('^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).{4,16}$')])
    });
  }
  cheakEmail()
  {
    if (this.loginFrom.get('email')?.touched && this.loginFrom.get('email')?.invalid)
    {
      if (this.loginFrom.get('email')?.errors?.['required']) {
        this.emailError = "The email is required";
      }
      else if (this.loginFrom.get('email')?.errors?.['pattern'])
      {
        this.emailError = "Enter the valid email";
      }
      return true;
    }
    this.emailError = '';
    return false;
  }
  cheakPassword()
  {
    if (this.loginFrom.get('password')?.touched && this.loginFrom.get('password')?.invalid)
      {
      if (this.loginFrom.get('password')?.errors?.['required']) {
        this.passwordError = "The password is required";
      }
      else if (this.loginFrom.get('password')?.errors?.['pattern']) {
        this.passwordError = "Enter the valid password";
      }
      return true;
    }
    this.passwordError = '';
    return false;
  }
  OnSubmit()
  {
    
    if (!(this.emailError || this.passwordError))
    {
      this.account.login(this.loginFrom.value).subscribe({
        next:()=> {
          console.log("user logged in")
        },
        error:(err)=> {
          console.log("The Error is ",err)
        },
        complete:()=> {
          console.log("Finished")
          this.router.navigate([this.returnUrl]);
        },
      })
    }
  }
}
