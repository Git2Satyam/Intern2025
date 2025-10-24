import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  products: any[] = [];
  editModal: boolean = false;
  categoryList: any[] = [];
  subCategoryList: any[] = [];
  productForm: FormGroup;
  file: File;
  productImage: any;

  displayedColumns: string[] = ['position', 'name', 'description', 'qty', 'price', 'image', 'action']
  dataSource: MatTableDataSource<any>;

  constructor(private api: ApiService, private toastr: ToastrService, private modalService: NgbModal, private fb: FormBuilder) {
    this.productForm = this.fb.group({
      id: [0],
      productName: ['', [Validators.required, Validators.maxLength(15), Validators.minLength(5)]],
      productDescription: ['', [Validators.required, Validators.maxLength(100), Validators.minLength(10)]],
      quantity: ['', [Validators.required, Validators.max(15), Validators.min(5)]],
      unitPrice: ['', [Validators.required, Validators.min(100)]],
      categoryId: ['', Validators.required],
      subCatId: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.loadApi()
  }

  loadApi() {
    this.getProducts();
    this.getAllCategories();
  }

  getProducts() {
    this.api.getProducts().subscribe(data => {
      if (data.Success) {
        this.products = data.Result;
        console.log(this.products);
        this.dataSource = new MatTableDataSource(this.products)
      }
    })
  }

  getAllCategories() {
    this.api.getCategories().subscribe(data => {
      if (data.Success) {
        this.categoryList = data.Result;
        console.log(this.categoryList);
      }
    })
  }

  get f() {
    return this.productForm.controls;
  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content);
    this.productForm.reset();
  }

  closeModal() {
    this.modalService.dismissAll();
  }

 editProduct(product: any, content: TemplateRef<any>){
    this.editModal = true;
     console.log(product)
     this.modalService.open(content);
     this.productForm.patchValue({
      id: product.Id,
      productName: product.ProductName,
      productDescription: product.ProductDescription,
      quantity: product.Quantity,
      unitPrice: product.UnitPrice,
      categoryId: product.CategoryId,
      subCatId: product.SubCatId,
     })

     console.log(this.productForm.value);
     this.getSubCategory()
  }
  onSubmit(){
    let obj = this.productForm.value;
    console.log(obj);
    if(!this.productForm.valid) this.toastr.error('Invalid Fields', 'Error')
      else{
         let productObj = {
          Id: obj.id ? obj.id : 0,
          ProductName: obj.productName,
          ProductDescription: obj.productDescription,
          Quantity: obj.quantity,
          UnitPrice: obj.unitPrice,
          SubCatId: obj.subCatId
        }
        console.log(productObj);

        this.api.saveProduct(productObj).subscribe({
          next: resp => {
            if(resp.Success){
              this.toastr.success('Product saved successfully', 'Success!');
              this.closeModal();
              this.getProducts();
            }
            else{
              this.toastr.error('Something went wrong', 'Error!')
            }
          },
          error: err => {
            console.log(err);
            this.toastr.error('Something went wrong', 'Error!')
          }
        })
    }

  }

  getSubCategory() {
    let id = this.productForm.controls['categoryId'].value
    console.log(id);
    let selectedCategory = this.categoryList.find(x => x.Id == id);
    this.subCategoryList = selectedCategory ? selectedCategory.SubCategory : [];
    console.log(this.subCategoryList);
  }

  deleteProduct(id: any){
    console.log(id);
    this.api.deleteProduct(id).subscribe(data => {
      console.log(data);
      if(data.Success){
        this.toastr.success('Record removed successfully', 'Success!');
        this.getProducts();
      }
    })
  }


  // Image Section
  openImageModal(content: TemplateRef<any>, id: any){
    this.modalService.open(content);
    console.log(id);
  }

  closeImageModal(){
    this.modalService.dismissAll();
  }

  readImageFile(event: any){
    let reader = new FileReader();
    this.file = event.target.files[0];
    reader.readAsDataURL(this.file);
    console.log(this.file)
    reader.onload = () => {
       this.productImage = reader.result;
    }
  }

  uploadImage(){

  }
}
