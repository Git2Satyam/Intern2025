import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  userList: any;

  displayedColumns: string[] = ['count', 'firstName', 'lastName', 'email', 'number', 'address', 'action'];
  dataSource: MatTableDataSource<any>;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadApi();
  }

  loadApi(){
     this.getUser();
  }

  getUser() {
    this.apiService.getAllUser().subscribe(data => {
      //console.log(data);
      if (data.Success) {
        this.userList = data.Result;
        console.log(this.userList);
        this.dataSource = new MatTableDataSource(this.userList);
      }
    })
  }

  editUser(obj: any){

  }

  deleteUser(obj: any){

  }

}
