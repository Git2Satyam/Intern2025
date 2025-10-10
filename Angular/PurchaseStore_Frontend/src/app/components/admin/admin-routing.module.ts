import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLayoutComponent } from './pages/admin-layout/admin-layout.component';
import { LoginSignupComponent } from './pages/login-signup/login-signup.component';
import { RoleComponent } from './pages/role/role.component';
import { UsersComponent } from './pages/users/users.component';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { RoleGuard } from 'src/app/guards/role.guard';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      {
         path: '',
         component: DashboardComponent
      },
      {
        path: 'login',
        component: LoginSignupComponent,
      },
      {
        path: 'role',
        component: RoleComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: {role: ['Super Admin']}
      },
      {
        path: 'users',
        component: UsersComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: {role: ['Super Admin', 'Admin']}
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
