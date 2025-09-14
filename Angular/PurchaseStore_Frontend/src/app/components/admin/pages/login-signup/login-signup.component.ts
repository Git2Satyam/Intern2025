import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserModel } from 'src/app/models/user-model';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-signup',
  templateUrl: './login-signup.component.html',
  styleUrls: ['./login-signup.component.scss']
})
export class LoginSignupComponent implements OnInit {

  userForm: FormGroup
  userML: UserModel = new UserModel();

  constructor(private fb: FormBuilder, private api: ApiService, private toastr: ToastrService, private authService: AuthService) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      password: ['', Validators.required]
    })
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

    this.api.getAllUser().subscribe({
    next: res => console.log(res),
    error: err => console.error(err)
   });
  }

  onSubmit(type: any){
    console.log(type);
    let obj = this.userForm.value;
    if(type == 'register'){
      if(!this.userForm.valid) this.toastr.error('Invalid details.', 'Error!');
      this.userML.FirstName = obj.firstName;
      this.userML.LastName = obj.lastName;
      this.userML.Email = obj.email;
      this.userML.Address = obj.address;
      this.userML.Password = obj.password;
      this.userML.PhoneNumber = obj.phoneNumber;
      console.log(this.userML);

      this.api.saveUser(this.userML).subscribe(data => {
        console.log(data);
        if(data.Success){
          this.toastr.success("User added successfully", 'Success!');
          this.userForm.reset();
        }
      })
    }
    else if(type == 'login'){
       this.api.verifyUser(obj.email, obj.password).subscribe(data => {
        console.log(data);
        if(data.Success){
          this.toastr.success("Login Successfully.", 'Sucess!');
          this.userForm.reset();
          this.authService.saveToken(data.Result);
        }
       })
    }
  }
}
