import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  isExpanded: boolean = true;
  constructor() { }

  ngOnInit(): void {
  }

  toggleSidebar(){
    this.isExpanded = !this.isExpanded;
  }

}
