import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendanceRptComponent } from './attendance-rpt/attendance-rpt.component';
import { AttendanceDetailRptComponent } from './attendance-detail-rpt/attendance-detail-rpt.component';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';
import { AuthorizationCheck } from '../security/AuthorizationCheck';

const routes: Routes = [
  {
    path: "attReport",
    component: AttendanceRptComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.AttSummaryReport, ''] },
    pathMatch: "full"
  },
  {
    path: "attDetReport",
    component: AttendanceDetailRptComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.AttDetailReport, ''] },
    pathMatch: "full"
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AttendanceRoutingModule {

}
