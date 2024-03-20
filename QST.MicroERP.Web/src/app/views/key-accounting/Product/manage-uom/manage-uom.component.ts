import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-manage-uom',
  templateUrl: './manage-uom.component.html',
  styleUrls: ['./manage-uom.component.css']
})
export class ManageUOMComponent implements OnInit {
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  UOM: SettingsVM[] = [];
  selectedUOM: SettingsVM
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
    this.selectedUOM = new SettingsVM();
  }
  ngOnInit(): void {
    this.Refresh()
    this.GetUOM();
    this.selectedUOM.isActive = true
  }
  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }
  GetUOM() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.UOM
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.UOM = res;
        this.dataSource = new MatTableDataSource(this.UOM);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteUOM(val: SettingsVM) {
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
            this.selectedUOM.parentId = val.parentId
            this.Reset();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetUOMForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedUOM = new SettingsVM;
    this.selectedUOM.id = id
    this.selectedUOM.enumTypeId = EnumTypes.UOM
    this.catSvc.SearchSettings(this.selectedUOM).subscribe({
      next: (res: SettingsVM[]) => {
        this.selectedUOM = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveUOM() {
    if (!this.userForm.invalid) {
      this.selectedUOM.enumTypeId = EnumTypes.UOM
      var uomList = this.UOM
      if (this.Edit)
        uomList = uomList.filter(X => X.id != this.selectedUOM.id)
      var search = uomList.find(x => x.name.toLocaleLowerCase() == this.selectedUOM.name.toLocaleLowerCase())
      if (search == undefined) {
        if (this.Edit)
          this.UpdateUOM()
        else {
          this.catSvc.SaveSettings(this.selectedUOM).subscribe({
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
        this.catSvc.ErrorMsgBar("A UOM with this name already added", 5000)
    } else
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
  }
  UpdateUOM() {
    this.catSvc.UpdateSettings(this.selectedUOM).subscribe({
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
    this.UOM = []
    this.Add = true;
    this.Edit = false;
    this.selectedUOM = new SettingsVM
    this.GetUOM()
    this.selectedUOM.isActive = true
  }
  Reset() {
    this.Add = true;
    this.Edit = false;
    this.selectedUOM.description = null
    this.selectedUOM.keyCode = null
    this.selectedUOM.name = null
  }
}


