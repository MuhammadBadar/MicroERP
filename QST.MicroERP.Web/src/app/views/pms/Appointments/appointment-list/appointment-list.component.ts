import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { AppointmentVM } from '../../Models/AppointmentVM';
import { PMSService } from '../../pms.service';
import { AppointmentComponent } from '../appointment/appointment.component';
import { PatientVM } from '../../Models/PatientVM';
import * as moment from 'moment';
import { DoctorVM } from '../../Models/DoctorVM';
import { MatSort } from '@angular/material/sort';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { AppStatus } from 'src/app/views/catalog/Models/Enums/AppStatus';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';


@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {
  isLoading: boolean = false
  isDDDReadOnly: boolean = false
  isReadOnly: boolean = false
  curDate: Date = new Date
  isView: boolean = false
  @ViewChild('Form', { static: true }) filterForm!: NgForm;
  //displayedColumns: string[] = ['id','date', 'time', 'doctor', 'patientname', 'age', 'gender', 'actions'];
  displayedColumns: string[] = ['id', 'status', 'date', 'time', 'doctor', 'patientname', 'ageTooltip', 'age', 'gender', 'actions'];
  dataSource: any;
  dialogRef: any
  apptDate: Date = new Date
  filterVal: AppointmentVM = new AppointmentVM();
  appointments!: AppointmentVM[]
  filteredData: any;
  DoctorId: number
  searchValue?: any
  patients?: PatientVM[]
  selectedApmnt: AppointmentVM
  docSearchValue?: any
  filteredDocData: any;
  doctors?: DoctorVM[]
  appStatus: SettingsVM[]
  @ViewChild(MatSort) sort: MatSort;
  constructor(private route: Router,
    public catSvc: CatalogService,
    private pmsSvc: PMSService,
    public dialog: MatDialog,) {
    this.selectedApmnt = new AppointmentVM
    this.selectedApmnt.from = new Date
    this.selectedApmnt.to = new Date
    this.filterVal = new AppointmentVM()
  }
  ngOnInit() {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Appointments)
    this.selectedApmnt = new AppointmentVM
    this.selectedApmnt.from = new Date
    this.selectedApmnt.to = new Date
    this.selectedApmnt.statusId = AppStatus.Waiting
    this.GetPatient()
    this.GetDoctor()
    this.GetStatus()
    this.SearchByDates()
    debugger
    var role = localStorage.getItem("Role")
    if (role == "Doctor") {
      this.selectedApmnt.doctorId = +localStorage.getItem("DoctorId")
      this.isDDDReadOnly = true
      this.SearchByDates()
    }
  }
  GetStatus() {
    var status = new SettingsVM
    status.enumTypeId = EnumTypes.AppStatus
    this.catSvc.SearchSettings(status).subscribe({
      next: (res: SettingsVM[]) => {
        this.appStatus = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  GetPatient() {
    var pat = new PatientVM
    pat.isActive = true
    pat.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchPatient(pat).subscribe({
      next: (res: PatientVM[]) => {
        this.patients = res;
        this.filteredData = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  AutoCompleteSearch(evet: any) {
    this.filteredData = this.patients.filter((patient) =>
      this.patientMatchesSearch(patient, evet)
    );
  }
  patientMatchesSearch(patient: PatientVM, evet): boolean {
    const searchLower = evet.toLowerCase();
    const searchCriteria = searchLower.split(' ');
    return searchCriteria.every((criteria) => {
      if (!criteria.trim()) return true;
      const criteriaMatches = [
        patient.patientName,
        patient.gender,
        patient.city,
        patient.area,
        patient.houseNo,
        patient.address,
        patient.contactNo,
        patient.age ? patient.age.replace(/\s/g, '') : "",
        patient.dateOfBirth ? this.catSvc.formatDate(patient.dateOfBirth) : '',
      ].some((field) => field && field.toLowerCase().includes(criteria));

      return criteriaMatches;

    });
  }
  GetDoctor() {
    var doc = new DoctorVM
    doc.isActive = true
    doc.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchDoctor(doc).subscribe({
      next: (res: DoctorVM[]) => {
        this.doctors = res;
        this.filteredDocData = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  DocAutoCompleteSearch(evet: any) {
    this.filteredDocData = this.doctors.filter((doctor) =>
      this.doctorMatchesSearch(doctor, evet)
    );
  }
  doctorMatchesSearch(doctor: DoctorVM, evet): boolean {
    const searchLower = evet.toLowerCase();
    const searchCriteria = searchLower.split(' ');
    return searchCriteria.every((criteria) => {
      if (!criteria.trim()) return true;
      const criteriaMatches = [
        doctor.doctorName,
        //doctor.gender,
        doctor.city,
        doctor.area,
        doctor.houseNo,
        doctor.address,
        doctor.contactNo,
        //doctor.dateOfBirth ? this.catSvc.formatDate(doctor.dateOfBirth) : '',
      ].some((field) => field && field.toLowerCase().includes(criteria));
      return criteriaMatches;
    });
  }
  SearchByDates() {
    this.isLoading = true
    var app = new AppointmentVM
    app.clientId = +localStorage.getItem("ClientId")
    if (this.selectedApmnt.from != null && this.selectedApmnt.from != undefined) {
      this.selectedApmnt.from = moment(this.selectedApmnt.from).toDate()
      app.from = new Date(Date.UTC(this.selectedApmnt.from.getFullYear(), this.selectedApmnt.from.getMonth(), this.selectedApmnt.from.getDate()))
    }
    if (this.selectedApmnt.to != null && this.selectedApmnt.to != undefined) {
      this.selectedApmnt.to = moment(this.selectedApmnt.to).toDate()
      app.to = new Date(Date.UTC(this.selectedApmnt.to.getFullYear(), this.selectedApmnt.to.getMonth(), this.selectedApmnt.to.getDate()))
    }
    if (this.selectedApmnt.patientId != 0 && this.selectedApmnt.patientId != undefined)
      app.patientId = this.selectedApmnt.patientId
    if (this.selectedApmnt.doctorId != 0 && this.selectedApmnt.doctorId != undefined)
      app.doctorId = this.selectedApmnt.doctorId
    if (this.selectedApmnt.statusId != 0 && this.selectedApmnt.statusId != undefined)
      app.statusId = this.selectedApmnt.statusId
    this.pmsSvc.SearchAppointment(app).subscribe({
      next: (res: AppointmentVM[]) => {
        this.isLoading = false
        this.appointments = []
        if (res)
          if (res.length > 0)
            this.appointments = res;
        this.dataSource = new MatTableDataSource(this.appointments);
        this.dataSource.sort = this.sort;
      }, error: (e) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  Appointments() {
    var appt = new AppointmentVM
    appt.doctorId = this.DoctorId
    appt.clientId = +localStorage.getItem("ClientId")
    if (this.apptDate != null && this.apptDate != undefined) {
      this.apptDate = new Date
      this.apptDate = new Date(Date.UTC(this.apptDate.getFullYear(), this.apptDate.getMonth(), this.apptDate.getDate()))
      appt.apptDate = this.apptDate
    }
    this.pmsSvc.SearchAppointment(appt).subscribe({
      next: (res: AppointmentVM[]) => {
        this.appointments = res
        this.dataSource = new MatTableDataSource(res)
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  openAppt(row: AppointmentVM) {
    if (!this.isView)
      this.dialogRef.close({
        selectedAppt: row
      });
  }
  GetAppointment() {
    var value = new AppointmentVM
    value.isActive = true
    value.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchAppointment(value).subscribe({
      next: (res: AppointmentVM[]) => {
        // this.Appointment = res;
        this.appointments = res
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  EditAppointment(app: AppointmentVM) {
    this.dialogRef = this.dialog.open(AppointmentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px'
      , data: { appId: app.id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(AppointmentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px'
      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  ResetGrid() {
    this.filterForm.reset()
    this.GetAppointment()
  }
}


