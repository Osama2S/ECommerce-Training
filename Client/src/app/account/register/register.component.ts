import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  standalone: false,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  registrationFrom!: FormGroup;
  emailError!: string;
  passwordError!: string;
  displayNameError!: string;
  constructor(private fb:FormBuilder,private account:AccountService,private router:Router){}
  ngOnInit(): void {
    this.createRegistrationForm();
  }
  createRegistrationForm()
  {
    this.registrationFrom = this.fb.group({
      displayName:[null,[Validators.required]],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
      password: [null,
        [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).{4,16}$')]]
    });
  }
  EmailErrors()
  {
    this.cheakEmail();
    this.validationExistEmail();
  }
  checkDisplayName() {
    if (this.registrationFrom.get('displayName')?.touched && this.registrationFrom.get('displayName')?.invalid) {
      if (this.registrationFrom.get('displayName')?.errors?.['required']) {
        this.displayNameError = "The display name is required";
        return true;
      }
    }
    return false;
  }
  cheakEmail()
  {
    if (this.registrationFrom.get('email')?.touched && this.registrationFrom.get('email')?.invalid)
    {
      if (this.registrationFrom.get('email')?.errors?.['required']) {
        this.emailError = "The email is required";
      }
      else if (this.registrationFrom.get('email')?.errors?.['pattern'])
      {
        this.emailError = "Enter the valid email";
      }
      return true
    }
    return false
  }
  validationExistEmail() {
    this.account.checkEmailExist(this.registrationFrom.get('email')?.value).subscribe({
      next: (value) => {
        console.log(value);
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log("Finished check");
      }
    })
  }
  cheakPassword()
  {

    if (this.registrationFrom.get('password')?.touched && this.registrationFrom.get('password')?.invalid)
      {
      if (this.registrationFrom.get('password')?.errors?.['required']) {
        this.passwordError = "The password is required";
      }
      else if (this.registrationFrom.get('password')?.errors?.['pattern']) {
        this.passwordError = "Enter the valid password";
      }
      return true;
    }
    return false;
  }
  OnSubmit()
  {
    this.account.register(this.registrationFrom.value).subscribe({
      next:()=> {
        console.log("user registered in")
      },
      error: (err) => {
        this.passwordError = err.errors;
        console.log("The Error is ",err)
      },
      complete:()=> {
        console.log("Finished")
        this.router.navigate(["/home"]);
      },
    })
  }
}
