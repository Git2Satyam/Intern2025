import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})

export class RoleComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'view', 'edit'];
  dataSource:  MatTableDataSource<any>;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadApi();
  }

  loadApi(){
    this.getRoles();
  }
  getRoles(){
    this.apiService.getAllRoles().subscribe({
      next: resp => {
         console.log(resp);
         this.dataSource = new MatTableDataSource(resp.Result);
      },
      error: err => console.log(err)
    })
    
  }

}
