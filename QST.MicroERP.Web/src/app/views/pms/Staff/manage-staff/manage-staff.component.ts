import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import * as moment from 'moment';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import Swal from 'sweetalert2';
import { StaffVM } from '../../Models/StaffVM';
import { PMSService } from '../../pms.service';
import { ManageEnumLineComponent } from 'src/app/views/catalog/manage-enum-line/manage-enum-line.component';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { ManageEnumLineWithParentComponent } from 'src/app/views/catalog/manage-enum-line-with-parent/manage-enum-line-with-parent.component';

@Component({
  selector: 'app-manage-staff',
  templateUrl: './manage-staff.component.html',
  styleUrls: ['./manage-staff.component.css']
})
export class ManageStaffComponent implements OnInit {
  currDate: Date = new Date
  hide = true;
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  Staff: StaffVM[] | undefined;
  selectedStaff: StaffVM
  @ViewChild('StaffForm', { static: true }) StaffForm!: NgForm;
  displayedColumns: string[] = ['StaffName', 'dateOfBirth', 'gender', 'contactNo', 'city', 'area', 'houseNo', 'address', 'isActive', 'actions'];
  dataSource: any;
  genders: SettingsVM[]
  cities: SettingsVM[]
  areas: SettingsVM[]
  dialogRef: any
  dialogData: any;
  countries: SettingsVM[]
  isDialog: boolean = false;
  constructor(
    public pmsSvc: PMSService,
    private injector: Injector,
    private catSvc: CatalogService,
    private dialog: MatDialog
  ) {
    this.selectedStaff = new StaffVM();
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedStaff = new StaffVM
    this.selectedStaff.clientId = +localStorage.getItem("ClientId")
    this.GetStaff()
    this.GetSettings(EnumTypes.Country);
    this.GetSettings(EnumTypes.City);
    this.GetSettings(EnumTypes.Areas);
    this.GetGenders()
    this.selectedStaff.isActive = true;
    debugger
    if (this.dialogData != null) {
      if (this.dialogData.docId != undefined) {
        this.GetStaffForEdit(this.dialogData.docId)
      }
    }
  }
  GetGenders() {
    var stng = new SettingsVM
    stng.isActive = true;
    stng.enumTypeId = EnumTypes.Gender
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.genders = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  GetSettings(enumType: EnumTypes) {
    var stng = new SettingsVM
    stng.isActive = true;
    stng.enumTypeId = enumType
    stng.clientId = +localStorage.getItem("ClientId")
    stng.moduleId = Modules.PMS
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        if (enumType == EnumTypes.City)
          this.cities = res
        else if (enumType == EnumTypes.Country)
          this.countries = res
        else if (enumType == EnumTypes.Areas)
          this.areas = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  GetStaff() {
    var doc = new StaffVM
    doc.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchStaff(doc).subscribe({
      next: (res: StaffVM[]) => {
        this.Staff = res;
        this.dataSource = new MatTableDataSource(this.Staff);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteStaff(id: number) {
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
        this.pmsSvc.DeleteStaff(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Staff has been deleted.',
              'success'
            )
            this.ngOnInit();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetStaffForEdit(id: number) {
    var doc = new StaffVM;
    doc.id = id
    doc.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchStaff(doc).subscribe({
      next: (res: StaffVM[]) => {
        this.Staff = res;
        this.selectedStaff = this.Staff[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveStaff() {
    this.selectedStaff.clientId = +localStorage.getItem("ClientId")
    this.selectedStaff.dateOfBirth = moment(this.selectedStaff.dateOfBirth).toDate()
    this.selectedStaff.dateOfBirth = new Date(Date.UTC(this.selectedStaff.dateOfBirth.getFullYear(), this.selectedStaff.dateOfBirth.getMonth(), this.selectedStaff.dateOfBirth.getDate()))
    if (!this.StaffForm.invalid) {
      if (this.Edit)
        this.UpdateStaff()
      else {
        this.pmsSvc.SaveStaff(this.selectedStaff).subscribe({
          next: (res) => {
            this.catSvc.SuccessMsgBar("Staff Successfully Added!", 5000)
            this.Add = true;
            this.Edit = false;
            this.proccessing = false
            this.ngOnInit();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
            this.proccessing = false
          }
        })
      }
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
  }
  UpdateStaff() {
    debugger
    this.pmsSvc.UpdateStaff(this.selectedStaff).subscribe({
      next: (res) => {
        this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
        this.Add = true;
        this.Edit = false;
        this.proccessing = false
        this.ngOnInit();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.proccessing = false
      }
    })
    this.proccessing = false
  }
  Refresh() {
    this.Add = true;
    this.Edit = false;
  }
  OpenCountryDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Country, isDialog: true,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId"),
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Country)
      }
    })
  }
  OpenCityDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineWithParentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.City,
        isDialog: true,
        parentType: EnumTypes.Country,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId"),
        parentId: this.selectedStaff.countryId
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.City)
      }
    })
  }
  OpenAreaDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineWithParentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Areas, isDialog: true,
        parentType: EnumTypes.City,
        parentId: this.selectedStaff.cityId,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId"),
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Areas)
      }
    })
  }
  SearchCities() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.City
    stng.moduleId = Modules.PMS
    stng.clientId = +localStorage.getItem("ClientId")
    stng.parentId = this.selectedStaff.countryId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.cities = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SearchArea() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Areas
    stng.moduleId = Modules.PMS
    stng.clientId = +localStorage.getItem("ClientId")
    stng.parentId = this.selectedStaff.cityId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.areas = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
}

