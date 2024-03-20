import { ChangeDetectorRef, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { RoleVM } from '../models/role-vm';
import { SecurityService } from '../security.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { SettingsTypeVM } from '../../catalog/Models/SettingsTypeVM';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../../catalog/catalog.service';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
@Component({
  selector: 'app-manage-role',
  templateUrl: './manage-role.component.html',
  styleUrls: ['./manage-role.component.css']
})
export class ManageRoleComponent implements OnInit {
  isReadOnly: boolean = false
  Edit: boolean = false;
  Add: boolean = true;
  Roles: SettingsVM[] = [];
  selectedRole: SettingsVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['name', 'isActive', 'actions'];
  dataSource: any;
  dialogData;
  selectedEnum: SettingsTypeVM
  isDialog: boolean = false
  dialogref: any
  dialogRef: any
  enumTypeId
  moduleId: number = 0
  clientId: number = 0
  modules: SettingsVM[]
  constructor(
    private injector: Injector,
    private dialog: MatDialog,
    private aRoute: ActivatedRoute,
    private catSvc: CatalogService,
    private cdRef: ChangeDetectorRef,
    private storeSvc: StorageService
  ) {
    this.dialogref = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedRole = new SettingsVM();
    this.selectedEnum = new SettingsTypeVM
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ManageRoles)
    this.aRoute.queryParams.subscribe(params => {
      var type = params['type'];
      if (type > 0) {
        this.enumTypeId = type
        this.moduleId = 0
      }
      this.GetEnum()
      this.GetRole()
    });
    this.selectedRole.isActive = true
    //this.getModules()
  }
  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }
  getModules() {
    var stng = new SettingsVM
    stng.isActive = true
    stng.enumTypeId = EnumTypes.BackOffice
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        res = res.filter(x => x.isSystemDefined == false)
        this.modules = res
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
      }
    })
  }
  GetEnum() {
    var type = new SettingsTypeVM
    type.id = this.enumTypeId
    this.catSvc.SearchSettingsType(type).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.selectedEnum = res[0];
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetRole() {
    var stng = new SettingsVM
    stng.enumTypeId = this.enumTypeId
    stng.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchEnumLine(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Roles = res;
        this.dataSource = new MatTableDataSource(this.Roles);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetRoleForEdit(id: number) {
    window.scrollTo(0, 0)
    var stng = new SettingsVM;
    stng.id = id
    stng.enumTypeId = this.enumTypeId
    stng.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchEnumLine(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.selectedRole = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveRole() {
    // if (this.selectedRole.moduleId == 0 || this.selectedRole.moduleId == undefined)
    //   this.userForm.controls["moduleId"].setErrors({ "incorrect": true })
    if (!this.userForm.invalid) {
      this.selectedRole.enumTypeId = this.enumTypeId
      this.selectedRole.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      //this.selectedRole.isSystemDefined = true
      this.selectedRole.value = this.selectedRole.name
      this.selectedRole.keyCode = this.selectedRole.name.replaceAll(" ", "_")
      if (this.Edit)
        this.UpdateRole()
      else {
        this.catSvc.SaveSettings(this.selectedRole).subscribe({
          next: (res) => {
            this.catSvc.SuccessfullyAddMsg()
            this.Reset();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    } else
      this.catSvc.ErrorMsgBar("Please Fill required field!", 5000)
  }
  UpdateRole() {
    this.catSvc.UpdateSettings(this.selectedRole).subscribe({
      next: (res) => {
        this.catSvc.SuccessfullyUpdateMsg()
        this.Reset();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.Roles = []
    this.Add = true;
    this.Edit = false;
    this.selectedRole = new SettingsVM
    this.selectedRole.clientId = 0
    this.GetRole()
    this.selectedRole.isActive = true
  }
  Reset() {
    this.Add = true;
    this.Edit = false;
    this.selectedRole = new SettingsVM
    this.selectedRole.clientId = 0
    this.GetRole()
    this.selectedRole.isActive = true
  }
}
