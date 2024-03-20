import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { ClientsVM } from 'src/app/views/catalog/Models/ClientsVM';
import { StorageData } from 'src/app/views/catalog/Models/StorageData';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { ManageSMTPCredsComponent } from 'src/app/views/catalog/SMTPCredentials/manage-smtpcreds/manage-smtpcreds.component';
import { MatDialog } from '@angular/material/dialog';
import { CreateUserDialogComponent } from 'src/app/views/security/manage-user/manage-user-dialog/create-user-dialog/create-user-dialog.component';
import { Roles } from 'src/app/views/catalog/Models/Enums/Roles';
import { AttendanceService } from 'src/app/views/attendance/attendance.service';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { SharedService } from 'src/app/shared/shared.service';
import { TaskService } from 'src/app/views/tms/task.service';
import { ManageUsertaskComponent } from 'src/app/views/tms/manage-usertask/manage-usertask.component';
import { ManageDayStatusComponent } from 'src/app/views/tms/manage-day-status/manage-day-status.component';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
})
export class DefaultHeaderComponent extends HeaderComponent implements OnInit {
  userName: string = ""
  dialogRef: any
  Client: string = ""
  roleName: string = ""
  roleId: number
  clients: ClientsVM[]
  superAdminId: string = ""
  isDayStarted: boolean = false
  @Input() sidebarId: string = "sidebar";

  public newMessages = new Array(4)
  public newTasks = new Array(5)
  public newNotifications = new Array(5)

  constructor(private classToggler: ClassToggleService,
    public catSvc: CatalogService,
    private tmsSvc: TaskService,
    private attSvc: AttendanceService,
    private dialog: MatDialog,
    private sharedSvc: SharedService,
    private storeSvc: StorageService,
    private cdr: ChangeDetectorRef,) {
    super();
  }
  LogOut() {
    this.attSvc.MarkUserOutTime();
    localStorage.clear()
    this.catSvc.triggerRefresh()
  }
  ngOnInit(): void {
    this.IsDayStarted()
    this.userName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_NAME)
    this.Client = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT)
    this.roleName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE)
    this.roleId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID)
    var id = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID)
    if (id != undefined)
      this.superAdminId = id
    this.catSvc.refresh$.subscribe(() => {
      debugger
      // Implement your logic to refresh the content here
      this.userName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_NAME)
      this.Client = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT)
      this.roleName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE)
      //this.getClients()
      this.IsDayStarted()
    });
    this.getClients()
  }
  getClients() {
    var clt = new ClientsVM
    clt.isActive = true
    this.catSvc.SearchClients(clt).subscribe({
      next: (res: ClientsVM[]) => {
        this.clients = res
      }, error: (err) => {
        console.warn(err)
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
      }
    })
  }
  async openCltData(clt: ClientsVM) {
    var _data = new StorageData()
    _data.role = await this.sharedSvc.GetRole(clt.userId)
    _data.roleId = Roles.ClientAdmin
    _data.id = clt.userId
    _data.token = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_TOKEN)
    _data.moduleIds = clt.moduleIds
    _data.clientId = clt.id
    _data.client = clt.clientName
    _data.userName = clt.user
    _data.superAdminId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID)
    _data.superAdminName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_NAME)
    this.catSvc.SetProject(_data)
    this.catSvc.setToken(_data.token)
    this.catSvc.triggerRefresh()
  }
  async reset() {
    var _data = new StorageData()
    _data.role = await this.sharedSvc.GetRole(this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID))
    _data.roleId = Roles.SuperAdmin
    _data.id = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID)
    _data.token = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_TOKEN)
    _data.moduleIds = ""
    _data.clientId = 0
    _data.client = ""
    _data.cltId=this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    _data.userName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_NAME)
    this.catSvc.SetProject(_data)
    this.catSvc.setToken(_data.token)
    this.catSvc.triggerRefresh()
  }
  refresh() {
    this.cdr.detectChanges();
  }
  Refresh() {
    this.catSvc.triggerRefresh()
  }
  OpenSMPTCredsDialog() {
    this.dialogRef = this.dialog.open(ManageSMTPCredsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '900px', minHeight: '330px', maxHeight: "550px"
      , data: { cltId: +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID) }
    });
    // this.dialogRef.afterClosed()
    //   .subscribe((res) => {
    //     this.ngOnInit()
    //   });
  }
  OpenUserDialog() {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '450px'
      , data: { id: this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID), isDialog: true, }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.Refresh()
      });
  }
  DayEnd() {
    this.OpenDayEndDialog()
  }
  OpenDayEndDialog() {
    this.dialogRef = this.dialog.open(ManageDayStatusComponent, {
      width: '1400px',
      height: '550px',
      hasBackdrop: false
    });
  }
  IsDayStarted() {
    this.tmsSvc.IsDayStarted(this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)).subscribe({
      next: (res) => {
        console.warn(res)
        this.isDayStarted = res
      }, error: (err) => {
        console.warn(err)
      }
    })
  }
  ReAssignTask() {
    this.dialogRef = this.dialog.open(ManageUsertaskComponent, {
      width: '1200px',
      height: '550px',
      data: { openedToReAssign: true, userId: this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID) }
    });
    this.dialogRef.afterClosed().subscribe({
      next: (res) => {

      }
    })
  }
}
