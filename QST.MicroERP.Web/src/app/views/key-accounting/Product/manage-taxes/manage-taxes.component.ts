import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-manage-taxes',
  templateUrl: './manage-taxes.component.html',
  styleUrls: ['./manage-taxes.component.css']
})
export class ManageTaxesComponent implements OnInit {
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  Taxes: SettingsVM[] = [];
  selectedTaxes: SettingsVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['name', 'isActive', 'actions'];
  dataSource: any;
  dialogData;
  isDialog: boolean = false
  Groups: SettingsVM[]
  dialogref: any
  dialogRef: any
  constructor(
    private catSvc: CatalogService,
    private cdRef: ChangeDetectorRef,
  ) {
    this.selectedTaxes = new SettingsVM();
  }
  ngOnInit(): void {
    this.Refresh()
    this.GetTaxes();
    this.selectedTaxes.isActive = true
  }
  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }
  GetTaxes() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Taxes
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Taxes = res;
        this.dataSource = new MatTableDataSource(this.Taxes);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteTaxes(val: SettingsVM) {
    debugger
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.catSvc.DeleteSettings(val.id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Successfuly Deleted.',
              'success'
            )
            this.selectedTaxes.parentId = val.parentId
            this.Reset();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetTaxesForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedTaxes = new SettingsVM;
    this.selectedTaxes.id = id
    this.selectedTaxes.enumTypeId = EnumTypes.Taxes
    this.catSvc.SearchSettings(this.selectedTaxes).subscribe({
      next: (res: SettingsVM[]) => {
        this.selectedTaxes = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveTaxes() {
    if (!this.userForm.invalid) {
      this.selectedTaxes.enumTypeId = EnumTypes.Taxes
      var TaxesList = this.Taxes
      if (this.Edit)
        TaxesList = TaxesList.filter(X => X.id != this.selectedTaxes.id)
      var search = TaxesList.find(x => x.name.toLocaleLowerCase() == this.selectedTaxes.name.toLocaleLowerCase())
      if (search == undefined) {
        if (this.Edit)
          this.UpdateTaxes()
        else {
          this.catSvc.SaveSettings(this.selectedTaxes).subscribe({
            next: (res) => {
              this.catSvc.SuccessfullyAddMsg()
              this.Refresh();
            }, error: (e) => {
              this.catSvc.ErrorMsgBar("Error Occurred", 5000)
              console.warn(e);
            }
          })
        }
      } else
        this.catSvc.ErrorMsgBar("A Taxes with this name already added", 5000)
    } else
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
  }
  UpdateTaxes() {
    this.catSvc.UpdateSettings(this.selectedTaxes).subscribe({
      next: (res) => {
        this.catSvc.SuccessfullyUpdateMsg()
        this.Refresh();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.Taxes = []
    this.Add = true;
    this.Edit = false;
    this.selectedTaxes = new SettingsVM
    this.GetTaxes()
    this.selectedTaxes.isActive = true
  }
  Reset() {
    this.Add = true;
    this.Edit = false;
    this.selectedTaxes.description = null
    this.selectedTaxes.keyCode = null
    this.selectedTaxes.name = null
  }
}



