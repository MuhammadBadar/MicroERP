import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageCoursescheduleComponent } from './Schedule/manage-courseschedule/manage-courseschedule.component';
import { ScheduleListComponent } from './Schedule/schedule-list/schedule-list.component';
import { ManageScheduleComponent } from './manage-schedule/manage-schedule.component';
import { ManageScheduleListComponent } from './manage-schedule-list/manage-schedule-list.component';
import { ManageScheduleDayEventComponent } from './manage-schedule-day-event/manage-schedule-day-event.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';
const routes: Routes = [
  // {
  //   path: "login",
  //   component: LoginComponent,
  //   pathMatch: "full",
  //  // data: { title: 'Manage Task', breadcrumb: 'Manage Task' },
  // },
  {
    path: "schedule",
    component: ManageCoursescheduleComponent,
    pathMatch: "full"
  },
  {
    path: "schList",
    component: ScheduleListComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.Schedule, ''] },
    pathMatch: "full"
  },
  {
    path: "Schedule",
    component: ManageScheduleComponent,
    pathMatch: "full"
  },
  {
    path: "manageschedulelist",
    component: ManageScheduleListComponent,
    pathMatch: "full"
  },
  {
    path: "scheduledayevent",
    component: ManageScheduleDayEventComponent,
    pathMatch: "full"
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ScheduleRoutingModule { }
