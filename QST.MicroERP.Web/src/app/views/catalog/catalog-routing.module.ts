import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotificationTemplateComponent } from './notification-template/notification-template.component';
import { ManageSettingsTypeComponent } from './manage-settings-type/manage-settings-type.component';
import { ManageSettingsComponent } from './manage-settings/manage-settings.component';
import { ClientListComponent } from './Client/client-list/client-list.component';
import { ManageClientsComponent } from './Client//manage-clients/manage-clients.component';
import { HomeComponent } from './home/home.component';
import { ManageEnumLineComponent } from './manage-enum-line/manage-enum-line.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { RouteIds } from './Models/Enums/RouteIds';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Items'
    },
    children: [

      {
        path: "nTem",
        component: NotificationTemplateComponent,
        canActivate: [AuthorizationCheck],
        pathMatch: "full"
      },
      {
        path: "enumLine",
        component: ManageEnumLineComponent,
        canActivate: [AuthorizationCheck],
        pathMatch: "full"
      },
      {
        path: "manageSetting",
        component: ManageSettingsComponent,
        canActivate: [AuthorizationCheck],
        data: { RouteId: [RouteIds.Settings, ''] },
        pathMatch: "full"
      },
      {
        path: "home",
        component: HomeComponent,
        canActivate: [AuthorizationCheck],
        pathMatch: "full"
      },

      {
        path: "manageSettingType",
        component: ManageSettingsTypeComponent,
        canActivate: [AuthorizationCheck],
        pathMatch: "full"
      },
      {
        path: "cltList",
        component: ClientListComponent,
        canActivate: [AuthorizationCheck],
        data: { RouteId: [RouteIds.Clients, ''] },
        pathMatch: "full"
      },

      {
        path: "clients",
        component: ManageClientsComponent,
        canActivate: [AuthorizationCheck],
        pathMatch: "full"
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CatalogRoutingModule { }
