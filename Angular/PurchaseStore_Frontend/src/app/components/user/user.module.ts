import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserLayoutComponent } from './pages/user-layout/user-layout.component';
import { UserHeaderComponent } from './shared/user-header/user-header.component';
import { UserFooterComponent } from './shared/user-footer/user-footer.component';
import { MenubarComponent } from './pages/menubar/menubar.component';
import { MaterialModule } from 'src/app/material.module';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    UserLayoutComponent,
    UserHeaderComponent,
    UserFooterComponent,
    MenubarComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    MaterialModule,
    NgbDropdownModule
  ]
})
export class UserModule { }
