import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-menubar',
  templateUrl: './menubar.component.html',
  styleUrls: ['./menubar.component.scss']
})
export class MenubarComponent implements OnInit {
  title = 'material-responsive-sidenav';
  @ViewChild('sidenav') sidenav: MatSidenav;
  isCollapsed = true;
  navItems: any[] = [];
  selectedValue = 0;
  categoryList: any[] = [];


  isExpanded:boolean = true;
  showSubmenu: boolean = false;
  isShowing = false;
  showSubSubMenu: boolean = false;
  constructor(private observer: BreakpointObserver, private apiService: ApiService, private sanitizer: DomSanitizer, private router: Router) { }
  badgevisible = false;
  
  ngOnInit(): void {
    this.loadApi();
  }

  loadApi() {
    this.getNavItems();
    this.getCategories();
  }
  getNavItems() {
    this.apiService.getNavItems().subscribe({
      next: resp => {
        console.log(resp)
        if (resp.Success) {
          this.navItems = resp.Result;
        }
      },
      error: err => console.log(err)
    })
  }

  getCategories() {
    this.apiService.getCategories().subscribe(data => {
      console.log(data);
      if (data.Success) {
        this.categoryList = data.Result
      }
    })
  }

  badgevisibility() {
    this.badgevisible = true;
    this.getNavItems();
  }

  mouseenter() {
    if (!this.isExpanded) {
      this.isShowing = true;
    }
  }

  mouseleave() {
    if (!this.isExpanded) {
      this.isShowing = false;
    }
  }

}
