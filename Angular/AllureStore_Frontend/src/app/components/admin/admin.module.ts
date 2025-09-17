import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminLayoutComponent } from './pages/admin-layout/admin-layout.component';
import { AdminHeaderComponent } from './shared/admin-header/admin-header.component';
import { AdminFooterComponent } from './shared/admin-footer/admin-footer.component';
import { MaterialModule } from 'src/app/material/material.module';
import { LoginSignupComponent } from './pages/login-signup/login-signup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoleComponent } from './pages/role/role.component';


@NgModule({
  declarations: [
    AdminLayoutComponent,
    AdminHeaderComponent,
    AdminFooterComponent,
    LoginSignupComponent,
    RoleComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AdminModule { }
