import { ChangeDetectorRef, Injector, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { PMSService } from '../../pms.service';
import { MatTableDataSource } from '@angular/material/table';
import { AppointmentVM } from '../../Models/AppointmentVM';
import * as moment from 'moment';
import { DoctorVM } from '../../Models/DoctorVM';
import { AppointmentComponent } from '../appointment/appointment.component';
import { AppStatus } from 'src/app/views/catalog/Models/Enums/AppStatus';

@Component({
  selector: 'app-doctor-appointments',
  templateUrl: './doctor-appointments.component.html',
  styleUrls: ['./doctor-appointments.component.css']
})
export class DoctorAppointmentsComponent implements OnInit {
  isView: boolean = false
  selectedRowIndex = -1;
  apptDate: Date = new Date
  displayedColumns: string[] = ['id', 'date', 'time', 'patientname', 'age', 'gender'];
  DataSource: any
  dialogRef: any
  appointments: AppointmentVM[] = []
  DoctorId: number
  docName: string
  dialogref: any
  dialogData: any;
  selectedAppointment: AppointmentVM
  selectedDoctor: DoctorVM
  constructor(
    private injector: Injector,
    private cdref: ChangeDetectorRef,
    public pmsSvc: PMSService,
    private catSvc: CatalogService,
    public dialog: MatDialog,
    public isdialogRef: MatDialogRef<DoctorAppointmentsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.selectedAppointment = new AppointmentVM();
    this.dialogref = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.DataSource = new MatTableDataSource(this.appointments)
  }
  ngOnInit(): void {
    if (this.data != null) {
      if (this.data.doctorId != undefined) {
        this.DoctorId = this.data.doctorId
        this.Appointments()
        this.getDocById()
      }
      if (this.data.viewMode != undefined)
        this.isView = this.data.viewMode
    }
  }
  getDocById() {
    var doc = new DoctorVM
    doc.id = this.DoctorId
    doc.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchDoctor(doc).subscribe({
      next: (res: DoctorVM[]) => {
        this.selectedDoctor = res[0]
        this.docName = res[0].doctorName
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  Appointments() {
    var appt = new AppointmentVM
    appt.doctorId = this.DoctorId
    appt.statusId = AppStatus.Waiting
    appt.clientId = +localStorage.getItem("ClientId")
    if (this.apptDate != null && this.apptDate != undefined) {
      this.apptDate = new Date
      this.apptDate = new Date(Date.UTC(this.apptDate.getFullYear(), this.apptDate.getMonth(), this.apptDate.getDate()))
      appt.apptDate = this.apptDate
    }
    this.pmsSvc.SearchAppointment(appt).subscribe({
      next: (res: AppointmentVM[]) => {
        this.appointments = res
        this.DataSource = new MatTableDataSource(res)
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
    this.cdref.markForCheck();
  }
  openAppt(row: AppointmentVM) {
    debugger
    if (!this.isView)
      this.isdialogRef.close({
        selectedAppt: row
      });
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(AppointmentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px'
      , data: { doctorId: this.DoctorId, minGaps: this.selectedDoctor.defApptDur }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
}

