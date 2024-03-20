import { SettingsVM } from './../Models/SettingsVM';
import { ManageSettingsTypeComponent } from './../manage-settings-type/manage-settings-type.component';
import { SettingsTypeVM } from './../Models/SettingsTypeVM';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../../catalog/catalog.service';
import { RouteIds } from '../Models/Enums/RouteIds';

@Component({
  selector: 'app-manage-settings',
  templateUrl: './manage-settings.component.html',
  styleUrls: ['./manage-settings.component.css']
})
export class ManageSettingsComponent implements OnInit {
  isReadOnly: boolean = false
  Edit: boolean = false;
  DisabledType: boolean = false;
  parentId?: number = 0
  typeId: number = 0
  typeKeyCode: string = "";
  settingName: string = "";
  Add: boolean = true;
  IsUnique: boolean = true;
  enumTypeId?: number;
  validFields: boolean = false;
  public date = new Date();
  selected = 200;
  values?: SettingsVM[];
  list?: SettingsVM[];
  searchedStng?: SettingsVM[];
  type: SettingsTypeVM = new SettingsTypeVM;
  getSetttingsBiId?: SettingsVM[];
  settingsType?: SettingsTypeVM[];
  Settings: SettingsVM[] | undefined;
  selectedSettings = new SettingsVM();
  private dialogRef?: MatDialogRef<ManageSettingsTypeComponent>;
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['settingType', 'name', 'keyCode', 'value', 'parent', 'description', 'isActive', 'actions'];
  style = "background-image: linear-gradient(to bottom, rgb(2, 33, 58), rgb(7, 95, 122));color:white;font-weight:normal "
  moduleId: number = 0
  dataSource: any;
  constructor(
    private route: ActivatedRoute,
    public catSvc: CatalogService,
    private snack: MatSnackBar,
    public dialog: MatDialog,) {
    this.selectedSettings = new SettingsVM();
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Settings)
    this.Add = true;
    this.Edit = false;
    this.typeKeyCode = ""
    this.settingName = ""
    this.selectedSettings = new SettingsVM;
    this.route.queryParams.subscribe(params => {
      var moduleId = + params['moduleId']
      if (moduleId > 0) {
        this.selectedSettings.moduleId = moduleId
        this.selectedSettings.typeModuleId = moduleId
        this.moduleId = moduleId
      }
      this.selectedSettings.moduleId = this.moduleId
      this.selectedSettings.typeModuleId = this.moduleId
      this.typeId = + params['type']
      if (this.typeId > 0) {
        // if (!Number.isNaN(this.typeId)) {
        //   var type = new SettingsTypeVM()
        //   type.id = params['type'];
        //   this.catSvc.SearchEnums(type).subscribe({
        //     next: (res: SettingsTypeVM[]) => {
        //       this.OnTypeChange(res[0])
        //       this.type.parentId = res[0].parentId;
        //       this.selectedSettings.enumTypeId = + params['type'];
        //       this.Search()
        //       this.DisabledType = true
        //     }, error: (e) => {
        //       this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
        //     }
        //   })

        // }
      }
      // else {
      this.GetSettings();
      this.GetSettingsType();
      this.Add = true;
      this.Edit = false;
      this.typeKeyCode = ""
      this.settingName = ""
      this.DisabledType = false
      //  }
    });
    this.selectedSettings.isActive = true
  }
  Check() {
    this.validFields = true;
  }
  GetValues() {
    var stng = new SettingsVM
    //if (this.moduleId > 0)
    stng.moduleId = this.moduleId
    stng.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.values = res;
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
      }
    })
  }
  GetSettings() {
    var stng = new SettingsVM
    stng.moduleId = this.moduleId
    stng.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(stng).subscribe((res: SettingsVM[]) => {
      var activeVals = res.filter(x => x.isActive == true)
      this.Settings = activeVals
      this.values = activeVals;
      this.dataSource = new MatTableDataSource(res);
    });
  }
  GetSettingsType() {
    var type = new SettingsTypeVM
    type.isActive = true
    type.moduleId = this.moduleId
    type.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchEnums(type).subscribe((res: SettingsTypeVM[]) => {
      this.settingsType = res;
    });
  }
  DeleteSettings(id: number) {
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
          next: () => {
            Swal.fire(
              'Deleted!',
              'success'
            )
            if (this.DisabledType == true)
              this.ngOnInit();
            else {
              this.Search();
              this.GetValues();
            }
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
          }
        })
      }
    })

  }
  GetSettingsForEdit(id: number) {
    window.scrollTo(0, 0)
    var type = new SettingsTypeVM();
    var setting = new SettingsVM()
    setting.id = id
    //if (this.moduleId > 0) {
    setting.moduleId = this.moduleId
    type.moduleId = this.moduleId
    //}
    type.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(setting).subscribe((res: SettingsVM[]) => {
      this.getSetttingsBiId = res;
      this.selectedSettings = this.getSetttingsBiId[0]
      this.Edit = true;
      this.Add = false;
      type.id = this.getSetttingsBiId[0]?.enumTypeId
      this.catSvc.SearchEnums(type).subscribe((res: SettingsTypeVM[]) => {
        this.type = res[0];
        this.typeKeyCode = res[0].keyCode
        this.settingName = '_' + this.selectedSettings.name
        this.Search()
      })
    })
  }
  SaveSettings() {
    debugger
    var stng = new SettingsVM();
    stng.enumTypeId = this.selectedSettings.enumTypeId;
    //if (this.moduleId > 0)
    stng.moduleId = this.moduleId
    stng.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(stng).subscribe((res: SettingsVM[]) => {
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
      this.validFields = true;
      if (!this.userForm.invalid && this.selectedSettings.enumTypeId != 0) {
        if (this.IsUnique == true) {
          //  if (this.moduleId > 0)
          this.selectedSettings.moduleId = this.moduleId
          this.catSvc.SaveSettings(this.selectedSettings).subscribe({
            next: () => {
              if (this.DisabledType == true)
                this.ngOnInit();
              else {
                this.GetValues();
                this.Search();
                this.typeKeyCode = ""
                this.settingName = ""
                this.selectedSettings = new SettingsVM
                this.selectedSettings.isActive = true
              }
              this.catSvc.SuccessMsgBar("Settings  successfully Added!", 5000)
            }, error: (e) => {
              this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
            }
          })
        }
        else
          this.catSvc.ErrorMsgBar('This Key Code is Already in Use!', 5000)
      }
      else
        this.catSvc.ErrorMsgBar("Please fill all required fields!", 5000)
    });
  }
  UpdateSettings() {
    this.validFields = true;
    if (!this.userForm.invalid && this.selectedSettings.enumTypeId != 0) {
      // if (this.IsUnique == true) {
      this.selectedSettings.clientId = +localStorage.getItem("ClientId")
      this.catSvc.UpdateSettings(this.selectedSettings).subscribe({
        next: () => {
          this.catSvc.SuccessMsgBar("Settings Successfully Updated!", 5000)
          if (this.DisabledType == true)
            this.ngOnInit();
          else {
            this.Add = true;
            this.Edit = false;
            this.Search();
            this.GetValues();
            this.typeKeyCode = ""
            this.settingName = ""
            this.selectedSettings = new SettingsVM
            this.selectedSettings.isActive = true
          }
        }, error: (e: any) => {
          this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
          console.warn(e);
        }
      })
      // }
      // else
      //   this.snack.open('This Key Code is Already in Use!', 'OK', { duration: 4000 })
    }
    else
      this.catSvc.ErrorMsgBar("Please fill all required fields!", 5000)
  }
  Refresh() {
    // this.ngOnInit();
    // // this.selectedSettings = new SettingsVM;
    // // this.Add = true;
    // // this.Edit = false;
    debugger
    this.GetSettings();
    this.GetSettingsType();
    this.selectedSettings.isActive = true
  }
  Search() {
    debugger
    // if(this.Edit==false)
    // this.settingName=""
    var setting = new SettingsVM();
    if (!this.Edit)
      this.selectedSettings.parentId = 0;
    // setting.enumTypeId = this.selectedSettings.enumTypeId;
    if (this.type.parentId != 0 && this.type.parentId != undefined) {
      setting.enumTypeId = this.type.parentId
      setting.isActive = true
      if (this.moduleId > 0)
        setting.moduleId = this.moduleId
      setting.clientId = +localStorage.getItem("ClientId")
      this.catSvc.SearchSettings(setting).subscribe({
        next: (res: SettingsVM[]) => {
          this.Settings = res;
        }, error: (e) => {
          this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
        }
      })
    }
    else {
      this.Settings = []
    }
    var stng = new SettingsVM();
    stng.enumTypeId = this.selectedSettings.enumTypeId
    //if (this.moduleId > 0)
    stng.moduleId = this.moduleId
    stng.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(stng).subscribe((res: SettingsVM[]) => {
      this.dataSource = new MatTableDataSource(res);
      if (this.DisabledType == true)
        this.values = res;
    });
  }
  OnTypeChange(value: SettingsTypeVM) {
    if (this.selectedSettings.isSystemDefined && this.Edit) {
    } else {
      this.type = value;
      this.typeKeyCode = value.keyCode
      this.selectedSettings.keyCode = this.typeKeyCode + this.settingName
    }
  }
  TextChangeEvent(event: any) {
    this.selectedSettings.value = this.selectedSettings.name
    if (this.selectedSettings.isSystemDefined && this.Edit) {

    } else {
      this.settingName = '_' + event.target.value.replaceAll(' ', '_')
      this.selectedSettings.keyCode = this.typeKeyCode + this.settingName
    }
  }
  onSearchChange() {
    this.IsUnique = true;
    if (this.Edit == true)
      this.values = this.values?.filter(x => x.id != this.selectedSettings.id)
    var value = this.values?.find(x => x.keyCode == this.selectedSettings.keyCode)
    if (value == undefined)
      this.IsUnique = true;
    else
      this.IsUnique = false;
  }
  SearchType() {
    debugger
    this.list = this.values?.filter(x => x.enumTypeId == this.selectedSettings.enumTypeId)
    this.dataSource = new MatTableDataSource(this.list);
  }
  SearchParent() {
    debugger
    var list = this.values?.filter(x => x.parentId == this.selectedSettings.parentId)
    this.dataSource = new MatTableDataSource(list);
  }
  SettingsTypeDialog() {
    this.dialogRef = this.dialog.open(ManageSettingsTypeComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1200px', height: '590px'
      , data: { moduleId: this.moduleId }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        if (this.DisabledType == true)
          this.ngOnInit();
        else {
          this.GetSettings()
          this.GetSettingsType()
          this.selectedSettings.keyCode = this.typeKeyCode
        }
      });
  }
}

