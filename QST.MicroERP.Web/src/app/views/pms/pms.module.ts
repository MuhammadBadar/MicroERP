
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatStepperModule } from '@angular/material/stepper';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { MatTableModule } from '@angular/material/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import {
  AccordionModule,
  BadgeModule,
  BreadcrumbModule,
  ButtonModule,
  CardModule,
  CarouselModule,
  CollapseModule,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ModalModule,
  NavModule,
  PaginationModule,
  PlaceholderModule,
  PopoverModule,
  ProgressModule,
  SharedModule,
  SpinnerModule,
  TableModule,
  TabsModule,
  TooltipModule,
  UtilitiesModule,


} from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { MatTooltip, MatTooltipModule } from '@angular/material/tooltip';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTabsModule } from '@angular/material/tabs';
import { PatientComponent } from './Patients/patient/patient.component';
import { DoctorComponent } from './Doctors/doctor/doctor.component';
import { AppointmentComponent } from './Appointments/appointment/appointment.component';
import { PMSService } from './pms.service';
import { PMSRoutingModule } from './pms-routing.module';
import { AppointmentListComponent } from './Appointments/appointment-list/appointment-list.component';
import { NgxMatTimepickerModule } from 'ngx-mat-timepicker';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule
} from '@angular-material-components/datetime-picker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { PatientListComponent } from './Patients/patient-list/patient-list.component';
import { DoctorListComponent } from './Doctors/doctor-list/doctor-list.component';
import { PatientFieldsComponent } from './Patients/patient-fields/patient-fields.component';
import { PatientFieldListComponent } from './Patients/patient-field-list/patient-field-list.component';
import { KeysPipe } from './keys.pipe';
import { RxMedExtraFieldsComponent } from './Prescriptions/rx-med-extra-fields/rx-med-extra-fields.component';
import { RxMedExtraFieldsListComponent } from './Prescriptions/rx-med-extra-fields-list/rx-med-extra-fields-list.component';
import { PrescriptionComponent } from './Prescriptions/prescription/prescription.component';
import { RxListComponent } from './Prescriptions/rx-list/rx-list.component';
import { PatientHistoryComponent } from './patient-history/patient-history.component';
import { DoctorAppointmentsComponent } from './Appointments/doctor-appointments/doctor-appointments.component';
import { ManageMedicineComponent } from './manage-medicine/manage-medicine.component';
import { StaffListComponent } from './Staff/staff-list/staff-list.component';
import { ManageStaffComponent } from './Staff/manage-staff/manage-staff.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
// import { ReadOnlyFirstSixNumDirective } from './read-only-first-six-num.directive';
@NgModule({
  declarations: [
    PatientComponent,
    DoctorComponent,
    AppointmentComponent,
    AppointmentListComponent,
    PatientListComponent,
    DoctorListComponent,
    PatientFieldsComponent,
    PatientFieldListComponent,
    KeysPipe,
    RxMedExtraFieldsComponent,
    RxMedExtraFieldsListComponent,
    PrescriptionComponent,
    RxListComponent,
    PatientHistoryComponent,
    DoctorAppointmentsComponent,
    ManageMedicineComponent,
    StaffListComponent,
    ManageStaffComponent,
    // ReadOnlyFirstSixNumDirective,
  ],
  imports: [
    MatMenuModule ,
    MatButtonToggleModule, 
    MatSortModule,
    NgxMatSelectSearchModule,
    NgxMatDatetimePickerModule,
    MatMomentDateModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    CommonModule,
    MatTabsModule,
    PMSRoutingModule,
    MatCardModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    FormsModule,
    FlexLayoutModule,
    FormsModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    HttpClientModule,
    MatTooltipModule,
    MatInputModule,
    MatListModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressBarModule,
    MatRadioModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatStepperModule,
    MatTableModule,
    MatPaginatorModule,
    AccordionModule,
    BadgeModule,
    BreadcrumbModule,
    ButtonModule,
    CardModule,
    CollapseModule,
    GridModule,
    UtilitiesModule,
    SharedModule,
    ListGroupModule,
    IconModule,
    ListGroupModule,
    PlaceholderModule,
    ProgressModule,
    SpinnerModule,
    TabsModule,
    NavModule,
    TooltipModule,
    CarouselModule,
    FormModule,
    DropdownModule,
    PaginationModule,
    PopoverModule,
    ModalModule,
    TableModule,
    MatTooltipModule,
    IconModule,
    MatTooltipModule,
    HttpClientModule,
    FlexLayoutModule,
    NgxMaterialTimepickerModule,

  ],
  providers: [DatePipe, PMSService, AuthorizationCheck,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class PMSModule { }
