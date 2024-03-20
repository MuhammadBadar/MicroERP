import { Component, OnInit, ViewChild } from '@angular/core';
import { ItemVM, ProductAttribVm } from '../../key-accounting/Models/ItemVM';
import { KeyAccountingService } from '../../key-accounting/key-accounting.service';
import { CatalogService } from '../../catalog/catalog.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { ManageEnumLineComponent } from '../../catalog/manage-enum-line/manage-enum-line.component';
import { NgForm } from '@angular/forms';
import { PMSService } from '../pms.service';
import { MatTableDataSource } from '@angular/material/table';
import { Location } from '@angular/common';
import { Modules } from '../../catalog/Models/Enums/Modules';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
@Component({
  selector: 'app-manage-medicine',
  templateUrl: './manage-medicine.component.html',
  styleUrls: ['./manage-medicine.component.css']
})
export class ManageMedicineComponent implements OnInit {
  isReadOnly: boolean = false
  selectedMedicine: ItemVM;
  manufacturers: SettingsVM[]
  categories: SettingsVM[]
  displayedColumns: string[] = ['name', 'manufacturer', 'formula', 'category', 'isActive', 'actions'];
  dataSource: any;
  @ViewChild('medicineForm', { static: true }) medicineForm!: NgForm;
  Edit: boolean = false;
  Add: boolean = true;
  proccessing: boolean;
  medicines: ItemVM[]
  constructor(
    public accSvc: KeyAccountingService,
    public pmsSvc: PMSService,
    private catSvc: CatalogService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private location: Location,
  ) {
    this.selectedMedicine = new ItemVM();
    this.selectedMedicine.isActive = true
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ManageMedicine)
    this.Add = true;
    this.Edit = false;
    this.proccessing = false
    this.selectedMedicine = new ItemVM
    this.selectedMedicine.isActive = true;
    this.GetSettings(EnumTypes.MedicineCategories)
    this.GetSettings(EnumTypes.Manufacturers)
    this.GetMedicine()
  }
  GetMedicineForEdit(id: number) {
    window.scrollTo(0, 0)
    var itm = new ItemVM;
    itm.id = id
    itm.moduleId = Modules.PMS
    itm.clientId = +localStorage.getItem("ClientId")
    this.accSvc.SearchItem(itm).subscribe({
      next: (res: ItemVM[]) => {
        this.selectedMedicine = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetMedicine() {
    var itm = new ItemVM;
    itm.moduleId = Modules.PMS
    itm.clientId = +localStorage.getItem("ClientId")
    this.accSvc.SearchItem(itm).subscribe({
      next: (res: ItemVM[]) => {
        this.medicines = res
        this.dataSource = new MatTableDataSource(this.medicines)
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    setting.moduleId = Modules.PMS
    setting.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype == EnumTypes.MedicineCategories)
          this.categories = res;
        else if (etype == EnumTypes.Manufacturers)
          this.manufacturers = res;
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  OpenManufacturersDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Manufacturers, isDialog: true,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId")
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Manufacturers)
      }
    })
  }
  OpenCategoriesDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.MedicineCategories, isDialog: true,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId")
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.MedicineCategories)
      }
    })
  }
  SaveMedicine() {
    this.proccessing = true
    this.CheckValidation();
    if (!this.medicineForm.invalid) {
      this.selectedMedicine.moduleId = Modules.PMS
      this.selectedMedicine.clientId = +localStorage.getItem("ClientId")
      if (this.Edit)
        this.UpdateMedicine()
      else
        this.accSvc.SaveItem(this.selectedMedicine).subscribe({
          next: (res: ItemVM) => {
            this.catSvc.SuccessMsgBar("Product Successfully Added!", 5000)
            this.ngOnInit()
            //this.Refresh();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
            this.proccessing = false
          }
        })
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
  }
  CheckValidation() {
    if (this.selectedMedicine.categoryId == 0 || this.selectedMedicine.categoryId == undefined)
      this.medicineForm.controls['categoryId'].setErrors({ "incorrect": true })
    if (this.selectedMedicine.manufacturersId == 0 || this.selectedMedicine.manufacturersId == undefined)
      this.medicineForm.controls['manufacturersId'].setErrors({ "incorrect": true })
  }
  UpdateMedicine() {
    this.accSvc.UpdateItem(this.selectedMedicine).subscribe({
      next: (res: ItemVM) => {
        this.catSvc.SuccessMsgBar("Product Successfully Updated!", 5000)
        this.ngOnInit()
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.proccessing = false
      }
    })
  }

  // Back() {
  //   this.location.back();
  // }
}
