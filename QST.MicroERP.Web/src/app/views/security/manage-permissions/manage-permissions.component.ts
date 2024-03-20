import { Component, OnInit } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from '../../catalog/catalog.service';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { PermissionVM } from '../models/permission-vm';
import { User, UserVM } from '../models/user-vm';
import { SecurityService } from '../security.service';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { ClientsVM } from '../../catalog/Models/ClientsVM';
import { Roles } from '../../catalog/Models/Enums/Roles';

@Component({
  selector: 'app-manage-permissions',
  templateUrl: './manage-permissions.component.html',
  styleUrls: ['./manage-permissions.component.css']
})
export class ManagePermissionsComponent implements OnInit {
  isLoading: boolean = false
  isReadOnly: boolean = false
  users: UserVM[]
  roles: SettingsVM[]
  selectedpermission: PermissionVM
  permsTypes: SettingsVM[]
  routes: SettingsVM[]
  dataSource: any
  clients: ClientsVM[] = []
  permissions: PermissionVM[] = []
  displayedColumns: string[] = []
  coulumns: string[] = []
  constructor(
    public catSvc: CatalogService,
    public secSvc: SecurityService,
    private storeSvc: StorageService
  ) {
    this.selectedpermission = new PermissionVM
  }
  ngOnInit() {
    if (this.catSvc.isSuperAdmin())
      this.isReadOnly = this.catSvc.getPermission(RouteIds.ClientPermissions)
    else
      this.isReadOnly = this.catSvc.getPermission(RouteIds.Permissions)
    this.GetRoles()
    this.GetPermTypes()
    this.GetUsers();
    this.GetClients()
    this.selectedpermission = new PermissionVM
    this.selectedpermission.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    // if (this.selectedpermission.clientId > 0)
    //   this.GetPerm()
    //this.GetPermissions(new PermissionVM)
  }
  GetPerm() {
    var perm = new PermissionVM
    perm.clientId = this.selectedpermission.clientId
    this.GetPermissions(perm)
  }
  GetPermissions(perm: PermissionVM) {
    this.isLoading = true
    perm.clientId = this.selectedpermission.clientId
    this.secSvc.SearchPerms(perm).subscribe({
      next: (res: PermissionVM[]) => {
        this.permissions = res
        this.dataSource = new MatTableDataSource(this.permissions)
        if (this.permsTypes)
          this.displayedColumns = ['route', 'notSet', ...this.permsTypes.map(type => type.name)]
        this.isLoading = false
      }, error: () => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
      }
    })
  }
  radioChange(event: MatRadioChange, data) {
    debugger
    var obj = this.dataSource.filter(x => x.routeId == data)[0]
    obj.permissionId = event.value;
    this.dataSource.some(x => x.routeId == data)
    if (!this.dataSource.some(x => x.id == data)) {
      this.dataSource.push(obj);
    }
  }
  GetClients() {
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
  GetUsers() {
    var user = new UserVM
    user.cltId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.secSvc.SearchUser(user).subscribe({
      next: (res: UserVM[]) => {
        this.users = res
        if (this.catSvc.isSuperAdmin())
          this.users = this.users.filter(x => x.cltId == null || x.cltId == 0)
      }, error: (e) => {
        console.warn(e)
        this.catSvc.ErrorMsgBar("Error Occurred while getting Users", 4000)
      }
    })
  }
  GetRoles() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Roles
    stng.clientId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.roles = res
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred while getting Roles", 4000)
      }
    })
  }
  GetPermTypes() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.PermissionTypes
    stng.clientId = 0
    this.catSvc.SearchEnumLine(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.permsTypes = res
        this.GetPermissions(new PermissionVM);
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred while getting Roles", 4000)
      }
    })
  }
  SearchPermsByRole(role?: SettingsVM) {
    var perm = new PermissionVM
    this.selectedpermission.clientId = role.clientId
    if (this.catSvc.isSuperAdmin())
      perm.cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    else
      perm.cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    perm.roleId = this.selectedpermission.roleId
    this.GetPermissions(perm)
  }
  SearchPermsByUser(user?: UserVM) {
    var perm = new PermissionVM
    this.selectedpermission.clientId = user.clientId
    if (this.catSvc.isSuperAdmin())
      perm.cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    else
      perm.cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    perm.userId = this.selectedpermission.userId
    this.GetPermissions(perm)
  }
  Submit() {
    debugger
    if (this.selectedpermission.userId == '' && this.selectedpermission.roleId == 0)
      this.catSvc.ErrorMsgBar("Please Select User or Role", 4000)
    else {
      if (this.selectedpermission.roleId > 0)
        this.permissions = this.dataSource.data.map((item) => ({ ...item, roleId: this.selectedpermission.roleId, clientId: +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID) }))
      if (this.selectedpermission.userId != "default")
        this.permissions = this.dataSource.data.map((item) => ({ ...item, userId: this.selectedpermission.userId, clientId: +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID) }))
      this.secSvc.SavePermissions(this.permissions).subscribe({
        next: () => {
          this.catSvc.SuccessMsgBar("Successfully Saved", 4000)
          if (this.selectedpermission.roleId > 0)
            this.SearchPermsByRole()
          else if (this.selectedpermission.userId != "default")
            this.SearchPermsByUser()
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        }
      })
    }
  }
}
