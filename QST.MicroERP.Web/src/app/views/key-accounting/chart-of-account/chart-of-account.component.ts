import { VoucherDetailsVM } from './../Models/VoucherVM';

import { Component, OnInit, ViewChild, Inject, Injector } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../../catalog/catalog.service';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { SettingsTypeVM } from '../../catalog/Models/SettingsTypeVM';
import { ManageSettingsTypeComponent } from '../../catalog/manage-settings-type/manage-settings-type.component';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { Levels } from '../Models/Enum/Levels';
import { Modules } from '../../catalog/Models/Enums/Modules';

@Component({
  selector: 'app-chart-of-account.component',
  templateUrl: './chart-of-account.component.html',
  styleUrls: ['./chart-of-account.component.css']
})
export class ChartOfAccountComponent implements OnInit {
  maxLength: number = 2
  Edit: boolean = false;
  Add: boolean = true;
  proccessing: boolean = false;
  levels!: SettingsTypeVM[]
  getSetttingsBiId?: SettingsVM[];
  settingsType?: SettingsTypeVM[];
  searchedStng?: SettingsVM[];
  Settings: SettingsVM[] = [];
  tAccounts!: SettingsVM[]
  selectedSettings = new SettingsVM();
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['name', 'keyCode', 'level', 'parent', 'isActive', 'actions'];
  dataSource: any;
  style = "background-image: linear-gradient(to bottom, rgb(2, 33, 58), rgb(7, 95, 122));color:white;font-weight:normal "
  code?: string
  levelKeyCode?: string;
  level!: SettingsVM
  dialogRef: any
  dialogData;
  constructor(
    private route: ActivatedRoute,
    public catSvc: CatalogService,
    private snack: MatSnackBar,
    private injector: Injector,
    public dialog: MatDialog,) {
    this.selectedSettings = new SettingsVM();
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedSettings.isActive = true
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.levelKeyCode = ""
    this.code = ""
    this.selectedSettings = new SettingsVM
    this.GetSettings();
    this.Getlevels()
    this.selectedSettings.isActive = true
    if (this.dialogData != null) {
      if (this.dialogData.accId != undefined) {
        this.GetSettingsForEdit(this.dialogData.accId)
      }
    }
  }
  numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
  Getlevels() {
    var val = new SettingsTypeVM
    val.istAccountLevel = true
    val.isActive = true
    console.warn(val)
    this.catSvc.SearchSettingsType(val).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.levels = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
      }
    })
  }
  GetSettings() {
    var value = new SettingsVM
    value.enumTypeId = EnumTypes.ChartofAccount
    value.moduleId = Modules.GL
    value.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchEnumLine(value).subscribe({
      next: (res: SettingsVM[]) => {
        // this.Settings = res;
        this.tAccounts = res
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  DeleteSettings(id: number) {
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
        this.catSvc.DeleteSettings(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetSettings();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occured!", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetSettingsForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedSettings = new SettingsVM;
    this.selectedSettings.id = id
    this.selectedSettings.clientId = +localStorage.getItem("ClientId")
    this.selectedSettings.moduleId = Modules.GL
    this.catSvc.SearchEnumLine(this.selectedSettings).subscribe({
      next: (getById: SettingsVM[]) => {
        // this.Settings = res;
        this.selectedSettings = getById[0]
        this.Edit = true;
        this.Add = false;
        var type = new SettingsTypeVM();
        type.id = getById[0].levelId
        this.catSvc.SearchSettingsType(type).subscribe({
          next: (res: SettingsTypeVM[]) => {
            var value = new SettingsVM
            value.levelId = res[0].parentId
            value.moduleId = Modules.GL
            value.clientId = +localStorage.getItem("ClientId")
            // this.SearchLevelCode(value)

            this.catSvc.SearchEnumLine(value).subscribe({
              next: (res: SettingsVM[]) => {
                debugger
                this.Settings = res;
                this.SetMaxLengthOfCode();
                var val = new SettingsVM
                val.id = getById[0].parentId
                val.moduleId = Modules.GL
                val.clientId = +localStorage.getItem("ClientId")
                if (getById[0].parentId != 0 && getById[0].parentId != undefined)
                  this.catSvc.SearchEnumLine(val).subscribe((res: SettingsVM[]) => {
                    this.levelKeyCode = res[0].accountCode
                    this.code = '-' + this.selectedSettings.keyCode
                  })
              }, error: (e) => {
                this.catSvc.ErrorMsgBar("Error Occured!", 5000)
                console.warn(e);
              }
            })
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occured!", 5000)
          }
        })
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  CheckCodeLength() {
    if (this.selectedSettings.levelId == Levels.Main)
      if (this.selectedSettings.keyCode?.length == 2) {
        this.SaveSettings();
      } else {
        this.catSvc.ErrorMsgBar("The MaxLength  of Code should be 2", 5000)
      }
    else if (this.selectedSettings.levelId == Levels.Sub)
      if (this.selectedSettings.keyCode?.length == 3) {
        this.SaveSettings();
      } else {
        this.catSvc.ErrorMsgBar("The MaxLength of Code should be 3", 5000)
      }
    else if (this.selectedSettings.levelId == Levels.Subsidiary)
      if (this.selectedSettings.keyCode?.length == 4) {
        this.SaveSettings();
      } else {
        this.catSvc.ErrorMsgBar("The MaxLength of Code should be 4", 5000)
      }
  }
  CheckValidation() {
    if (this.selectedSettings.levelId == 0 || this.selectedSettings.levelId == undefined)
      this.userForm.controls['levelId'].setErrors({ 'incorrect': true })
    if (this.selectedSettings.levelId !== Levels.Main && this.selectedSettings.levelId
      != undefined && this.selectedSettings.levelId != 0)
      if (this.selectedSettings.parentId == 0 || this.selectedSettings.parentId == undefined)
        this.userForm.controls['parentId'].setErrors({ 'incorrect': true })
  }
  SaveSettings() {
    this.CheckValidation()
    if (!this.userForm.invalid) {
      var values = this.tAccounts.filter(x => x.id != this.selectedSettings.id)
      var search = values.find(x => x.accountCode == this.selectedSettings.accountCode)
      if (search == undefined) {
        this.selectedSettings.clientId = +localStorage.getItem("ClientId")
        this.selectedSettings.moduleId = Modules.GL
        if (this.Edit)
          this.UpdateSettings()
        else {
          this.selectedSettings.enumTypeId = EnumTypes.ChartofAccount
          var stng = new SettingsVM();
          stng.enumTypeId = EnumTypes.ChartofAccount;
          this.catSvc.SearchEnumLine(stng).subscribe((res: SettingsVM[]) => {
            debugger
            if (res.length != 0) {
              this.searchedStng = res
              var value = this.searchedStng[this.searchedStng.length - 1]
              this.selectedSettings.id = value.id + 1
            }
            else {
              var id = this.selectedSettings.enumTypeId.toString() + "001"
              this.selectedSettings.id = +id
            }
            this.proccessing = true

            this.catSvc.SaveSettings(this.selectedSettings).subscribe({
              next: (res) => {
                this.catSvc.SuccessMsgBar("  Successfully Added!", 5000)
                this.Add = true;
                this.Edit = false;
                this.proccessing = false
                var levelId = this.selectedSettings.levelId
                this.ngOnInit();
                //this.SetList(levelId)
              }, error: (e) => {
                this.catSvc.ErrorMsgBar("Error Occured!", 5000)
                console.warn(e);
                this.proccessing = false
              }
            })

          });
        }
      } else {
        this.catSvc.ErrorMsgBar("This Code Already in use", 5000)
      }
      // } else {
      //   this.catSvc.ErrorMsgBar("Please Select Parent!", 5000)
      // }
    }
    else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }

  }
  // SetList(id: number) {
  //   var stng = new SettingsVM
  //   stng.levelId = id
  //   this.catSvc.SearchEnumLine(stng).subscribe({
  //     next(value) {
  //       this.catSvc.ErrorMsgBar("Error Occured!", 5000)
  //     }, error(err) {

  //     },
  //   })
  // }
  UpdateSettings() {
    this.proccessing = true
    this.catSvc.UpdateSettings(this.selectedSettings).subscribe({
      next: (res) => {
        this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
        this.Add = true;
        this.Edit = false;
        this.proccessing = false
        var levelId = this.selectedSettings.levelId
        this.ngOnInit();
        //this.SetList(levelId)
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
        this.proccessing = false
      }
    })
  }
  Refresh() {
    this.selectedSettings = new SettingsVM;
    this.Add = true;
    this.Edit = false;
    this.selectedSettings.isActive = true
  }
  SearchByLevel(val: SettingsTypeVM) {
    // if (this.selectedSettings.levelId == Levels.Main) {
    this.selectedSettings.accountCode = this.selectedSettings.keyCode
    this.code = ""
    this.levelKeyCode = ""
    // }
    this.SetMaxLengthOfCode();
    var value = new SettingsVM
    value.levelId = val.parentId
    value.isActive = true
    value.clientId = +localStorage.getItem("ClientId")
    value.moduleId = Modules.GL
    this.catSvc.SearchEnumLine(value).subscribe({
      next: (res: SettingsVM[]) => {
        this.Settings = res;
        var find = (this.Settings.find(x => x.id == this.selectedSettings.parentId))
        if (find == undefined)
          this.selectedSettings.parentId = 0
        // this.levelKeyCode = this.selectedSettings.keyCode
        // this.dataSource = new MatTableDataSource(this.Settings);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
    var stn = new SettingsVM
    stn.levelId = this.selectedSettings.levelId
    stn.clientId = +localStorage.getItem("ClientId")
    stn.moduleId = Modules.GL
    this.catSvc.SearchEnumLine(stn).subscribe({
      next: (res: SettingsVM[]) => {
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  SearchLevelCode(value: SettingsVM) {
    this.level = value;
    this.levelKeyCode = value.accountCode
    this.selectedSettings.accountCode = this.levelKeyCode
  }
  SetMaxLengthOfCode() {
    if (this.selectedSettings.levelId == Levels.Main)
      this.maxLength = 2
    else if (this.selectedSettings.levelId == Levels.Sub)
      this.maxLength = 3
    else if (this.selectedSettings.levelId == Levels.Subsidiary)
      this.maxLength = 4
  }
  TextChangeEvent(event: any) {
    debugger
    if (this.selectedSettings.levelId == Levels.Sub || this.selectedSettings.levelId == Levels.Subsidiary) {
      this.code = '-' + event.target.value
    } else
      this.code = event.target.value
    if (this.levelKeyCode != undefined)
      this.selectedSettings.accountCode = this.levelKeyCode + this.code
  }
  OnLevelChange(value: SettingsVM) {
    debugger
    this.code = '-' + this.selectedSettings.keyCode
    this.levelKeyCode = value.accountCode
    if (this.levelKeyCode != undefined)
      this.selectedSettings.accountCode = this.levelKeyCode + this.code
    var val = new SettingsVM
    val.parentId = value.id
    val.clientId = +localStorage.getItem("ClientId")
    val.moduleId = Modules.GL
    this.catSvc.SearchEnumLine(val).subscribe({
      next: (res: SettingsVM[]) => {
        debugger
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
}

