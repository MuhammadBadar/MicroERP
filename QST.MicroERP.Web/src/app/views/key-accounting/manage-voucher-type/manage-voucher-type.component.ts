import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from '../../catalog/catalog.service';
import { Modules } from '../../catalog/Models/Enums/Modules';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { ChartOfAccountComponent } from '../chart-of-account/chart-of-account.component';
import { KeyAccountingService } from '../key-accounting.service';
import { VoucherTypeVM } from '../Models/VoucherTypeVM';

@Component({
  selector: 'app-manage-voucher-type',
  templateUrl: './manage-voucher-type.component.html',
  styleUrls: ['./manage-voucher-type.component.css']
})
export class ManageVoucherTypeComponent implements OnInit {
  isReadOnly: boolean = false
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['name', 'defaultDrCrFirst', 'keyCode', 'defaultDrCr', 'isActive', 'actions'];
  dataSource: any;
  accounts: SettingsVM[]
  VoucherType?: VoucherTypeVM[] | undefined
  selectedVoucherType: VoucherTypeVM
  VoucherTypeId: number;
  getvoucherById: VoucherTypeVM[];
  dialogRef: any
  dialogData;
  isDialog: boolean = false
  constructor(
    private CatSvc: CatalogService,
    private dialog: MatDialog,
    private injector: Injector,
    private accSvc: KeyAccountingService
  ) {
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedVoucherType = new VoucherTypeVM();
  }
  ngOnInit() {
    this.isReadOnly = this.CatSvc.getPermission(RouteIds.VoucherTypes)
    this.Add = true;
    this.Edit = false;
    this.selectedVoucherType = new VoucherTypeVM
    this.GetVoucherType();
    this.GetDefaultDrCr();
    this.selectedVoucherType.isActive = true
    if (this.dialogData != null) {
      if (this.dialogData.isDialog != undefined)
        this.isDialog = this.dialogData.isDialog
    }
  }
  Check() {
    this.validFields = true;
  }
  GetVoucherType() {
    var type = new VoucherTypeVM
    type.clientId = +localStorage.getItem("ClientId")
    this.accSvc.SearchVoucherType(type).subscribe({
      next: (value: VoucherTypeVM[]) => {
        this.VoucherType = value;
        this.dataSource = new MatTableDataSource(this.VoucherType)
      }, error: (err) => {
        this.CatSvc.ErrorMsgBar("Error Occured", 500)
      },
    })
  }
  GetDefaultDrCr() {

    var stng = new SettingsVM
    stng.clientId = +localStorage.getItem("ClientId")
    stng.levelId = EnumTypes.SubSidiary
    stng.moduleId = Modules.GL
    stng.isActive = true
    this.CatSvc.SearchEnumLine(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.accounts = res;
      }, error: (e) => {
        this.CatSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetVoucherTypeForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedVoucherType = new VoucherTypeVM;
    this.selectedVoucherType.id = id
    this.selectedVoucherType.clientId = +localStorage.getItem("ClientId")
    this.accSvc.SearchVoucherType(this.selectedVoucherType).subscribe({
      next: (res: VoucherTypeVM[]) => {
        this.VoucherType = res;
        this.selectedVoucherType = this.VoucherType[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.CatSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveVoucherType() {
    if (this.selectedVoucherType.defaultDrCrSecondId == 0 || this.selectedVoucherType.defaultDrCrSecondId == undefined)
      this.userForm.controls['defaultDrCrSecondId'].setErrors({ incorrect: true })
    if (!this.userForm.invalid) {
      var vchType = new VoucherTypeVM
      vchType.keyCode = this.selectedVoucherType.keyCode
      vchType.clientId = +localStorage.getItem("ClientId")
      this.accSvc.SearchVoucherType(vchType).subscribe({
        next: (res: VoucherTypeVM[]) => {
          if (this.Edit)
            res = res.filter(x => x.id != this.selectedVoucherType.id)
          if (res.length > 0)
            this.CatSvc.ErrorMsgBar("This Key Code already in use", 5000)
          else {
            this.selectedVoucherType.clientId = +localStorage.getItem("ClientId")
            if (this.Edit)
              this.UpdateVoucherType()
            else
              this.accSvc.SaveVoucherType(this.selectedVoucherType).subscribe({
                next: (res) => {
                  this.CatSvc.SuccessfullyAddMsg()
                  this.Add = true;
                  this.Edit = false;
                  this.ngOnInit();
                }, error: (e) => {
                  this.CatSvc.ErrorMsgBar("Error Occurred", 5000)
                  console.warn(e);
                }
              })
          }
        }, error: () => {

        }
      })
    } else
      this.CatSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
  }
  UpdateVoucherType() {
    this.validFields = true;
    if (!this.userForm.invalid) {
      this.accSvc.UpdateVoucherType(this.selectedVoucherType).subscribe({
        next: (res) => {
          this.CatSvc.SuccessfullyUpdateMsg()
          this.Add = true;
          this.Edit = false;
          this.ngOnInit();
        }, error: (e) => {
          this.CatSvc.ErrorMsgBar("Error Occurred", 5000)
          console.warn(e);
        }
      })
    }
    else
      this.CatSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
  }
  Refresh() {
    this.selectedVoucherType = new VoucherTypeVM
    this.Add = true;
    this.Edit = false;
    this.selectedVoucherType.isActive = true;
  }
  AccountsDialog() {
    var dialogRef = this.dialog.open(ChartOfAccountComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px'
      , data: {}
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetDefaultDrCr()
      });
  }
  getKeyCode() {
    debugger
    this.selectedVoucherType.keyCode = this.CatSvc.getCapitalLettersAsString(this.selectedVoucherType.name)
  }
}


