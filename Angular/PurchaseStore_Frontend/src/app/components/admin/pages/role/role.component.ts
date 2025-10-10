import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'view', 'edit', 'action'];
  dataSource:  MatTableDataSource<any>;

  roleForm: FormGroup;
  selectedValue: any = 'True';

  constructor(private apiService: ApiService, private fb: FormBuilder, private modalServie: NgbModal, private toastr: ToastrService, private auth: AuthService) {
     this.roleForm = this.fb.group({
       RoleName: ['', Validators.required],
       View: [this.selectedValue, Validators.required],
       Edit: [this.selectedValue, Validators.required],
     })
   }

  ngOnInit(): void {
   const role = this.auth.getRole();
   console.log(role);
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

  openModal(content: TemplateRef<any>){
    this.modalServie.open(content);
    this.roleForm.controls['View'].setValue('True');
    this.roleForm.controls['Edit'].setValue('True');
  }

  closeModal(){
    this.modalServie.dismissAll();
    this.roleForm.reset();
    this.roleForm.controls['View'].setValue('True');
    this.roleForm.controls['Edit'].setValue('True');
  }

  onSubmit(){
    console.log(this.roleForm.value);
    if(!this.roleForm.valid){
        this.toastr.error('Invalid input', 'Error!')
    }
    else{
      let obj = this.roleForm.value;
      this.apiService.saveRole(obj).subscribe({
        next: resp => {
          console.log(resp);
          if(resp.Success){
            this.toastr.success('Record saved successfully', 'Success!')
            this.roleForm.reset();
            this.modalServie.dismissAll();
            this.getRoles();
          }
        }
      })
    }
  }

  editRole(obj: any, content: TemplateRef<any>){
    this.openModal(content);
    console.log(obj);
    this.roleForm.patchValue({
       RoleName: obj.RoleName,
       View: obj.View,
       Edit: obj.Edit
    })
  }

  deleteRole(name: any){
    console.log(name);
    this.apiService.deleteRole(name).subscribe(data => {
      if(data){
        this.toastr.success('Record removed successfully', 'Success!');
        this.getRoles();
      }
    })
  }

}
