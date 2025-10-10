import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NgForm, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { delay, map, Observable, of } from 'rxjs';
import { UserModel } from 'src/app/models/user-model';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-signup',
  templateUrl: './login-signup.component.html',
  styleUrls: ['./login-signup.component.scss']
})
export class LoginSignupComponent implements OnInit {

  userForm: FormGroup;
  userML: UserModel = new UserModel();
  emailList: any[] = [];

  constructor(private fb: FormBuilder, private apiService: ApiService, private toastr: ToastrService, private authService: AuthService) {
    this.userForm = this.fb.group({
      firstName: ['', [Validators.required, this.noSpaceValidators.bind(this)]],
      lastName: ['', [Validators.required, this.noSpaceValidators.bind(this)]],
      email: ['', [Validators.required, Validators.email], [this.emailTaken.bind(this)]],
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      
    }, { validators: this.passwordMatchValidator.bind(this)})
  }

  ngOnInit(): void {
    const container = document.querySelector('.container');
    const LoginLink = document.querySelector('.SignInLink');
    const RegisterLink = document.querySelector('.SignUpLink');

    if (RegisterLink && LoginLink && container) {
      RegisterLink.addEventListener('click', (event) => {
        event.preventDefault();
        container.classList.add('active');
      });

      LoginLink.addEventListener('click', (event) => {
        event.preventDefault();
        container.classList.remove('active');
      });
    }

    this.getUsersEmails();
  }

  getUsersEmails() {
    this.apiService.getAllUser().subscribe({
      next: resp => {
        if (resp.Success) {
          this.emailList = resp.Result.map((x: any) => x.Email)
          console.log(this.emailList)
        }
      },
      error: err => {
        console.error('Api error', err)
      },
      complete: () => {
        console.log('Completed');
      }
    })
  }

  onSubmit(type: string) {
    console.log(type);
    //console.log(this.userForm.value);
    let obj = this.userForm.value;
    if (type == 'register') {
      if (!this.userForm.valid) {
        this.toastr.error('Invalid details.', 'Error!');
      }
      else {
        this.userML.FirstName = obj.firstName;
        this.userML.LastName = obj.lastName;
        this.userML.Email = obj.email;
        this.userML.Address = obj.address;
        this.userML.PhoneNumber = obj.phoneNumber;
        this.userML.Password = obj.password;
        console.log(this.userML);
        this.apiService.saveUser(this.userML).subscribe(data => {
          console.log(data);
          if (data.Success) {
            this.toastr.success('User added successfully.', 'Success!');
            this.userForm.reset();
          }
          else {
            this.toastr.error('Something went wrong.', 'Error!');
          }

        })
      }

    }
    else if (type == 'login') {
      console.log(this.userForm.value);
      this.apiService.verifyUser(obj.email, obj.password).subscribe(data => {
        console.log(data);
        if (data.Success) {
          this.toastr.success('Login successfully.', 'Sucess!');
          this.userForm.reset();
          this.authService.saveToken(data.Result);
        }
      })
    }
  }

  get f() {
    return this.userForm.controls;
  }

  // custom validators
  emailTaken(control: AbstractControl): Observable<{ emailTaken: boolean } | null> {
    return of(this.emailList.includes(control.value)).pipe(   // boolean true
      delay(1000),
      map(isTaken => (isTaken ? { emailTaken: true } : null))
    )
  }

  noSpaceValidators(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (value && value.indexOf(' ') >= 0) {
      return { noSpace: true };  // invalid
    }
    else {
      return null; // valid
    }
  }

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    let pass = control.get('password')?.value;
    let confPass = control.get('confirmPassword')?.value;
    if (!pass || !confPass) {
      return null;
    }
    return pass === confPass ? null : { passwordMisMatch: true };
  }
}
