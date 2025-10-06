import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

   userList: any;
   roles: any
   dropdownSettings = {};
   selectedItems: any[] = [];
   dropdownList: any[] = [];

   displayedColumns: string[] = ['position', 'firstName', 'lastName', 'email', 'number', 'address', 'action']
   dataSource: MatTableDataSource<any>;

   assignRoleForm: FormGroup;
   constructor(private apiService: ApiService, private modalService: NgbModal, private fb: FormBuilder, private toastr: ToastrService) {
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'Id',
      textField: 'Name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: false
    }
   }

  ngOnInit(): void {
    this.assignRoleForm = this.fb.group({
      RoleName: ['', Validators.required],
      Users: ['', Validators.required]

    })
    this.loadApi();
  }

  loadApi(){
    this.getUsers();
    this.getRoles();
  }

  getUsers(){
    this.apiService.getAllUser().subscribe(data => {
      //console.log(data);
      if(data.Success){
        this.userList = data.Result;
        console.log(this.userList);
        this.dataSource = new MatTableDataSource(this.userList);
        this.dropdownList = this.userList.map((obj: any) => ({Id: obj.Id, Name: obj.FirstName}))
        console.log(this.dropdownList);
      }
    })
  }

  getRoles(){
    this.apiService.getAllRoles().subscribe(data => {
      if(data.Success){
         this.roles = data.Result;
         console.log(this.roles);
      }
    })
  }

  openModal(content: TemplateRef<any>){
     const modalOptions: NgbModalOptions = {
      backdrop: 'static', 
      keyboard: false, 
    };
    this.modalService.open(content, modalOptions);
    this.assignRoleForm.controls['RoleName'].setValue(null);
  }

  closeModal(){
     this.modalService.dismissAll();
     this.assignRoleForm.reset();
  }

  
  onSubmit() {
    console.log(this.assignRoleForm.value);
    if (this.assignRoleForm.valid) {
      let ids = this.assignRoleForm.controls['Users'].value.map((x: any) => x.Id);
      let obj = {
        RoleName: this.assignRoleForm.controls['RoleName'].value,
        UserIds: ids
      }
      console.log(obj);
      this.apiService.assignRole(obj).subscribe({
        next: resp => {
          if (resp.Success) {
            this.toastr.success('Record updated successfully', 'Success');
            this.assignRoleForm.reset();
            this.modalService.dismissAll();
          }
          else {
            this.toastr.error('Something went wrong', 'Error!');
          }
        }
      })
    }
  }

  onItemSelect(evet: any){
    //  console.log(evet);
    //  console.log(this.assignRoleForm.value);
  }

  onSelectAll(event: any){
    console.log(event);
  }
}
