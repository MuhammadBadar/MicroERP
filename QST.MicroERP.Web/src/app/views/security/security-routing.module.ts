import { CreateUserDialogComponent } from './manage-user/manage-user-dialog/create-user-dialog/create-user-dialog.component';
import { AssignRoleToUserComponent } from './assign-role-to-user/assign-role-to-user.component';
import { ManageRoleComponent } from "./manage-role/manage-role.component";
import { ManageUserComponent } from "./manage-user/manage-user.component";
import { Component } from "@angular/core";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthorizationCheck } from './AuthorizationCheck';
import { TokenCheck } from './TokenCheck';
import { LoginComponent } from './login/login.component';
import { ManagePermissionsComponent } from './manage-permissions/manage-permissions.component';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';

const routes: Routes = [

  {
    path: '',
    data: {
      title: 'KeyAccounting'
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'sLogin'
      },
      {
        path: "sLogin",
        component: LoginComponent,
        canActivate: [TokenCheck],
        pathMatch: "full"
      },
      {
        path: "userRegistration",
        component: CreateUserDialogComponent,
        canActivate: [AuthorizationCheck],
        pathMatch: "full"
      },
      {
        path: "roles",
        component: ManageRoleComponent,
        canActivate: [AuthorizationCheck],
        data: { RouteId: [RouteIds.ManageRoles, ''] },
        pathMatch: "full"
      },
      {
        path: "users",
        component: ManageUserComponent,
        canActivate: [AuthorizationCheck],
        data: { RouteId: [RouteIds.ManageUser, ''] },
        pathMatch: "full"
      },
      {
        path: "perms",
        component: ManagePermissionsComponent,
        pathMatch: "full", data: { RouteId: [RouteIds.Permissions, ''] },
        canActivate: [AuthorizationCheck]
      },
      {
        path: "cltPerms",
        component: ManagePermissionsComponent,
        pathMatch: "full", data: { RouteId: [RouteIds.ClientPermissions, ''] },
        canActivate: [AuthorizationCheck]
      },
      {
        path: "userrole",
        component: AssignRoleToUserComponent,
        pathMatch: "full",
        canActivate: [AuthorizationCheck]
      },
    ]
  }




];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

