import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-header',
  templateUrl: './user-header.component.html',
  styleUrls: ['./user-header.component.scss']
})
export class UserHeaderComponent implements OnInit {

  constructor() { }

  categories: Category[] = [
    { value: '0', viewValue: 'All' },
    { value: '1', viewValue: 'Men Cloth' },
    { value: '2', viewValue: 'Women Cloth' },
  ];
  ngOnInit(): void {
  }

  

}
interface Category {
  value: string;
  viewValue: string;
}