import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksListComponent } from './tasks-list/tasks-list/tasks-list.component';
import { ManageTaskComponent } from './manage-task/manage-task.component';
import { ManageNotificationTemplateComponent } from './manage-notification-template/manage-notification-template.component';
import { CreateNotificationTemplateComponent } from './manage-notification-template/create-notification-template/create-notification-template.component';
import { TaskActivityReportComponent } from './task-activity-report/task-activity-report.component';
import { ManageUsertaskComponent } from './manage-usertask/manage-usertask.component';
import { ManageDayStatusComponent } from './manage-day-status/manage-day-status.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';

const routes: Routes = [
  {
    path: "daystatus",
    component: ManageDayStatusComponent,
    pathMatch: "full"
  },
  {
    path: "managetask",
    component: ManageTaskComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.ManageTask, ''] },
    pathMatch: "full"
  },
  {
    path: "usertask",
    component: ManageUsertaskComponent,
    pathMatch: "full"
  },
  {
    path: "activityRpt",
    component: TaskActivityReportComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.TaskActivity, ''] },
    pathMatch: "full"
  },
  {
    path: "notificationtemplate",
    component: CreateNotificationTemplateComponent,
    pathMatch: "full",
  },
  {
    path: "managenotification",
    component: ManageNotificationTemplateComponent,
    pathMatch: "full",
  },
  {
    path: "taskList",
    component: TasksListComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.ManageTask, ''] },
    pathMatch: "full"
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskRoutingModule { }
