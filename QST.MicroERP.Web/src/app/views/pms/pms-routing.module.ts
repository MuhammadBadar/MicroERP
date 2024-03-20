import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentListComponent } from './Appointments/appointment-list/appointment-list.component';
import { AppointmentComponent } from './Appointments/appointment/appointment.component';
import { DoctorListComponent } from './Doctors/doctor-list/doctor-list.component';
import { StaffListComponent } from './Staff/staff-list/staff-list.component';
import { DoctorComponent } from './Doctors/doctor/doctor.component';
import { PatientHistoryComponent } from './patient-history/patient-history.component';
import { PatientFieldListComponent } from './Patients/patient-field-list/patient-field-list.component';
import { PatientFieldsComponent } from './Patients/patient-fields/patient-fields.component';
import { PatientListComponent } from './Patients/patient-list/patient-list.component';
import { PatientComponent } from './Patients/patient/patient.component';
import { PrescriptionComponent } from './Prescriptions/prescription/prescription.component';
import { RxListComponent } from './Prescriptions/rx-list/rx-list.component';
import { RxMedExtraFieldsListComponent } from './Prescriptions/rx-med-extra-fields-list/rx-med-extra-fields-list.component';
import { ManageMedicineComponent } from './manage-medicine/manage-medicine.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';
const routes: Routes = [{
  path: "appointment",
  component: AppointmentComponent,
  canActivate: [AuthorizationCheck],
  pathMatch: "full"
},
{
  path: "appmntList",
  component: AppointmentListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.Appointments, ''] },
  pathMatch: "full"
},
{
  path: "patFields",
  component: PatientFieldsComponent,
  canActivate: [AuthorizationCheck],
  pathMatch: "full"
},
{
  path: "patFieldList",
  component: PatientFieldListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.PatientParameters, ''] },
  pathMatch: "full"
},
{
  path: "patient",
  component: PatientComponent,
  canActivate: [AuthorizationCheck],
  pathMatch: "full"
},
{
  path: "patientList",
  component: PatientListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.Patients, ''] },
  pathMatch: "full"
},
{
  path: "rxList",
  component: RxListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.Rx, ''] },
  pathMatch: "full"
},
{
  path: "prescription",
  component: PrescriptionComponent,
  canActivate: [AuthorizationCheck],
  pathMatch: "full"
},
{
  path: "rxMedExtraFieldsList",
  component: RxMedExtraFieldsListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.RxParameters, ''] },
  pathMatch: "full"
},
{
  path: "doctorList",
  component: DoctorListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.Doctors, ''] },
  pathMatch: "full"
},
{
  path: "patHistory",
  component: PatientHistoryComponent,
  canActivate: [AuthorizationCheck],
  pathMatch: "full"
},
{
  path: "doctor",
  component: DoctorComponent,
  canActivate: [AuthorizationCheck],
  pathMatch: "full"
},
{
  path: "staffList",
  component: StaffListComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.Staffs, ''] },
  pathMatch: "full"
},
{
  path: "manageMedicine",
  component: ManageMedicineComponent,
  canActivate: [AuthorizationCheck],
  data: { RouteId: [RouteIds.ManageMedicine, ''] },
  pathMatch: "full"
},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PMSRoutingModule { }
