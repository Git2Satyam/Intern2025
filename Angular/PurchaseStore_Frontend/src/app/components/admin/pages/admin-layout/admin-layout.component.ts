import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  isExpanded: boolean = true;
  navItems: any[] = [];
  showSubMenu: boolean = false;
  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadApi();
  }

  loadApi(){
   this.getNavItems();
  }

  getNavItems(){
    this.apiService.getNavItems().subscribe({
      next: resp => {
        if(resp.Success){
           this.navItems = resp.Result;
           console.log(this.navItems);
        }
      },
      error: err => console.log(err)
    })
  }

  toggleSidebar(){
    this.isExpanded = !this.isExpanded;
  }

}
