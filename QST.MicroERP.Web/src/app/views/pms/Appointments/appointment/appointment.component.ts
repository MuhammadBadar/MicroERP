import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import * as moment from 'moment';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import Swal from 'sweetalert2';
import { AppointmentVM } from '../../Models/AppointmentVM';
import { PatientVM } from '../../Models/PatientVM';
import { PatientComponent } from '../../Patients/patient/patient.component';
import { PMSService } from '../../pms.service';
import { DoctorVM } from '../../Models/DoctorVM';
import { DoctorComponent } from '../../Doctors/doctor/doctor.component';
import { MatSort } from '@angular/material/sort';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { DatePipe } from '@angular/common';
import { take, timer } from 'rxjs';
import { AppStatus } from 'src/app/views/catalog/Models/Enums/AppStatus';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';
@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  isReadOnly: boolean = false
  isLoading: boolean = false
  minAppDate = new Date()
  displayedColumns: string[] = ['id', 'patientname', 'date', 'time', 'age', 'gender', 'actions'];
  AddMode: boolean = true
  isReadonly: boolean = false
  EditMode: boolean = false
  dataSource: any
  selectedAppointment: AppointmentVM
  appointments?: AppointmentVM[]
  dialogref: any
  dialogRef: any
  dialogData: any;
  disableClose: any
  isDialog: boolean = false;
  proccessing: boolean = false;
  DisabledType: boolean = false;
  hide = true;
  patients?: PatientVM[];
  selectedPatient: PatientVM
  filteredData: any;
  searchValue?: any
  docSearchValue?: any
  filteredDocData: any;
  minutesGap: number = 5
  doctors?: DoctorVM[] = []
  appStatus: SettingsVM[]
  minTime
  @ViewChild('appointmentForm', { static: true }) appointmentForm: NgForm;
  constructor(
    private injector: Injector,
    private pmsSvc: PMSService,
    private catSvc: CatalogService,
    //  private datePipe: DatePipe,
    private dialog: MatDialog) {
    this.dialogref = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedAppointment = new AppointmentVM();
    this.selectedAppointment.isActive = true
    this.selectedAppointment.statusId = AppStatus.Waiting
    this.selectedPatient = new PatientVM
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Appointments)
    this.EditMode = false
    this.AddMode = true
    this.selectedAppointment = new AppointmentVM
    this.GetAppointment();
    this.GetPatient();
    this.GetDoctor()
    this.GetStatus()
    this.selectedAppointment.statusId = AppStatus.Waiting
    this.selectedAppointment.clientId = +localStorage.getItem("ClientId")
    if (this.dialogData.appId != undefined)
      this.EditAppointment(this.dialogData.appId)
    if (this.dialogData != null) {
      if (this.dialogData.doctorId != undefined) {
        this.minutesGap = this.dialogData.minGaps
        this.selectedAppointment.doctorId = this.dialogData.doctorId
        this.getTime(this.selectedAppointment.doctorId)
        this.getTknNo(this.selectedAppointment.doctorId)
        this.isReadonly = true
      }
    }
    var role = localStorage.getItem("Role")
    if (role == "Doctor") {
      var userId = localStorage.getItem("UserId")
      var doc = new DoctorVM
      doc.clientId = +localStorage.getItem("ClientId")
      doc.userId = userId
      this.pmsSvc.SearchDoctor(doc).subscribe({
        next: (res: DoctorVM[]) => {
          this.isReadonly = true
          this.selectedAppointment.doctorId = res[0].id
          this.minutesGap = res[0].defApptDur
          this.getTime(this.selectedAppointment.doctorId)
          this.getTknNo(this.selectedAppointment.doctorId)
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        }
      })
    }
  }
  getTknNo(id) {
    var app = new AppointmentVM
    app.doctorId = id
    app.clientId = +localStorage.getItem("ClientId")
    app.date = this.catSvc.setDate(this.selectedAppointment.date)
    this.pmsSvc.GetTokenNo(app).subscribe({
      next: (res) => {
        if (res)
          this.selectedAppointment.tokenId = + res
      }, error: (e) => {
        console.warn(e)
        this.catSvc.ErrorMsgBar("Error Occurred while getting TokenNo", 5000)
      }
    })
  }
  GetPatient() {
    var patient = new PatientVM
    patient.isActive = true
    patient.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchPatient(patient).subscribe({
      next: (res: PatientVM[]) => {
        this.patients = res
        this.filteredData = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  getTime(id) {
    debugger
    // this.minTime = doc.startTime
    var app = new AppointmentVM
    app.doctorId = id
    app.clientId = +localStorage.getItem("ClientId")
    app.date = this.catSvc.setDate(this.selectedAppointment.date)
    this.pmsSvc.GetMinTime(app).subscribe({
      next: (res) => {
        if (res)
          if (!this.EditMode) {
            this.minTime = res
            this.selectedAppointment.time = res
          }
      }, error: (e) => {
        console.warn(e)
        this.catSvc.ErrorMsgBar("Error Occurred while getting MinTime", 5000)
      }
    })
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
  validateNo(e): boolean {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
      return false
    return true
  }
  TokenPrefix() {
    debugger
    if (this.selectedAppointment.tokenNo > 0) {
      const numericValue = this.selectedAppointment.tokenNo.toString().replace(/[^0-9]/g, '');
      this.selectedAppointment.tokenDate = Number(numericValue.substring(0, 6))
      return this.selectedAppointment.tokenDate
    }
    else {
      const currentDate = new Date();
      this.selectedAppointment.tokenDate = this.pmsSvc.dateTranform(currentDate)
      return this.selectedAppointment.tokenDate
    }
  }
  GetAppointment() {
    var appt = new AppointmentVM
    appt.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchAppointment(appt).subscribe({
      next: (res: AppointmentVM[]) => {
        this.appointments = res
        this.dataSource = new MatTableDataSource(this.appointments)
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  GetDoctor() {
    var doc = new DoctorVM
    doc.isActive = true
    doc.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchDoctor(doc).subscribe({
      next: (res: DoctorVM[]) => {
        this.doctors = res;
        this.filteredDocData = res
        if (this.doctors.length == 1)
          this.selectedAppointment.doctorId = this.doctors[0].id
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
        doctor.city,
        doctor.area,
        doctor.houseNo,
        doctor.address,
        doctor.contactNo,
        // doctor.dateOfBirth ? this.catSvc.formatDate(doctor.dateOfBirth) : '',
        //doctor.dateOfBirth ? '' + doctor.dateOfBirth.getFullYear(): '',
      ].some((field) => field && field.toLowerCase().includes(criteria));
      return criteriaMatches;
    });
  }
  OpenDoctorDialog() {
    this.dialogRef = this.dialog.open(DoctorComponent, {
      width: '1200px', height: '950px',
      data: { isDialog: true, }
    })
    this.dialogRef.afterClosed()
      .subscribe((res: any) => {
        this.GetDoctor()
      }
      );
  }
  SaveAppointment() {
    this.isLoading = true
    this.selectedAppointment.date = this.catSvc.setDate(this.selectedAppointment.date)
    this.selectedAppointment.clientId = +localStorage.getItem("ClientId")

    if (this.selectedAppointment.patientId == 0 || this.selectedAppointment.patientId == undefined)
      this.appointmentForm.controls['Patient'].setErrors({ 'incorrect': true });
    if (this.selectedAppointment.doctorId == 0 || this.selectedAppointment.doctorId == undefined)
      this.appointmentForm.controls['Doctor'].setErrors({ 'incorrect': true });

    if (this.appointmentForm.invalid) {
      this.isLoading = false
      const controls = this.appointmentForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          this.catSvc.ErrorMsgBar(` ${name} is required field`, 6000)
          break;
        }
      }
    } else {
      var app = new AppointmentVM
      app.doctorId = this.selectedAppointment.doctorId
      app.apptDate = this.selectedAppointment.date
      app.time = this.selectedAppointment.time
      app.statusId = AppStatus.Waiting
      app.clientId = +localStorage.getItem("ClientId")
      this.pmsSvc.SearchAppointment(app).subscribe({
        next: (res: AppointmentVM[]) => {
          if (this.EditMode)
            res = res.filter(x => x.id != this.selectedAppointment.id)
          if (res.length > 0) {
            this.isLoading = false
            this.catSvc.ErrorMsgBar("This Doctor already has an Appointment in this Date at  " + "''" + res[0].time + "''", 5000);
          } else {
            if (!this.EditMode) {
              var app = new AppointmentVM
              app.patientId = this.selectedAppointment.patientId
              app.doctorId = this.selectedAppointment.doctorId
              app.apptDate = this.selectedAppointment.date
              app.clientId = +localStorage.getItem("ClientId")
              this.pmsSvc.SearchAppointment(app).subscribe({
                next: (res: AppointmentVM[]) => {
                  if (res.length > 0)
                    this.catSvc.InfoMsgBar("This Patient already has  Appointment with this doctor in this Date " + "''", 6000);
                }, error: (e) => {
                  this.isLoading = false
                  this.catSvc.ErrorMsgBar("Error Occurred!", 6000);
                  console.warn(e);
                }
              })
            }
            var app = new AppointmentVM
            app.tokenNo = +(this.selectedAppointment.tokenDate.toString() + this.selectedAppointment.tokenId)
            app.apptDate = this.selectedAppointment.date
            app.doctorId = this.selectedAppointment.doctorId
            app.clientId = +localStorage.getItem("ClientId")
            this.pmsSvc.SearchAppointment(app).subscribe({
              next: (res: AppointmentVM[]) => {
                console.warn(res)
                if (this.EditMode)
                  res = res.filter(x => x.id != this.selectedAppointment.id)
                if (res.length > 0 && this.selectedAppointment.tokenId > 0) {
                  this.isLoading = false
                  this.catSvc.ErrorMsgBar("This TokenNo Already Taken", 6000);
                }
                else {
                  debugger
                  this.proccessing = true;
                  if (this.selectedAppointment.tokenId > 0)
                    this.selectedAppointment.tokenNo = +(this.selectedAppointment.tokenDate.toString() + this.selectedAppointment.tokenId)
                  if (this.EditMode) {
                    this.UpdateAppointment();
                  } else {
                    this.pmsSvc.SaveAppointment(this.selectedAppointment).subscribe({
                      next: (res) => {
                        timer(5000)
                          .subscribe(() => {
                            this.isLoading = false
                            this.catSvc.SuccessMsgBar("Successfully Added!", 6000);
                            this.ngOnInit();
                            this.proccessing = false;
                          });
                      },
                      error: (e) => {
                        this.isLoading = false
                        console.warn(e);
                        this.catSvc.ErrorMsgBar("Error Occurred!", 6000);
                        this.proccessing = false;
                      }
                    });
                  }
                }
              }, error: (e) => {
                this.isLoading = false
                this.catSvc.ErrorMsgBar("Error Occurred!", 6000);
                console.warn(e);
              }
            })
          }
        }, error: () => {
          this.isLoading = false
        }
      })
    }
  }
  EditAppointment(id) {
    this.selectedAppointment = new AppointmentVM;
    this.selectedAppointment.id = id
    this.selectedAppointment.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchAppointment(this.selectedAppointment).subscribe({
      next: (res: AppointmentVM[]) => {
        this.selectedAppointment = res[0]
        this.selectedAppointment.tokenId = +this.selectedAppointment.tokenNo.toString().substring(6);
        this.minTime = this.selectedAppointment.time
        // this.getTime(this.selectedAppointment.doctorId)
        this.EditMode = true;
        this.AddMode = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  UpdateAppointment() {
    this.pmsSvc.UpdateAppointment(this.selectedAppointment).subscribe({
      next: (value) => {
        timer(5000)
          .pipe(take(1))
          .subscribe(() => {
            this.isLoading = false
            this.catSvc.SuccessMsgBar("Successfully Updated", 5000);
            this.ngOnInit();
            this.proccessing = false
          });
      }, error: (err) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred", 5000);
      }
    });
  }
  Refresh() {
    this.ngOnInit()
  }
  DeleteAppointment(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.pmsSvc.DeleteAppointment(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Appointment has been deleted.',
              'success'
            )
            this.Refresh();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  setDOB() {
    if (this.selectedPatient.dateOfBirth != null) {
      this.selectedPatient.dateOfBirth = moment(this.selectedPatient.dateOfBirth).toDate()
      this.selectedPatient.dateOfBirth = new Date(Date.UTC(this.selectedPatient.dateOfBirth.getFullYear(), this.selectedPatient.dateOfBirth.getMonth(), this.selectedPatient.dateOfBirth.getDate()))
    }
  }
  Search(val: PatientVM) {
    this.selectedPatient = val
    this.setDOB()
    //console.warn(val.dateOfBirth)
    this.catSvc.GetAgeByDOB(this.selectedPatient.dateOfBirth).subscribe({
      next: (age) => {
        console.warn(age)
        this.selectedAppointment.age = age
      }, error: (e) => {
        console.warn(e)
      }
    })
    this.selectedPatient = val
    this.selectedAppointment.gender = this.selectedPatient.gender
    this.selectedAppointment.genderId = this.selectedPatient.genderId
    this.selectedAppointment.dob = this.selectedPatient.dateOfBirth
    //this.selectedAppointment.age = this.selectedPatient.age
    // this.selectedAppointment.age = this.catSvc.GetAgeYear(val.dateOfBirth)

  }
  OpenPatientDialog() {
    this.dialogRef = this.dialog.open(PatientComponent, {
      width: '1200px', height: '850px',
      data: { isDialog: true, }
    })
    this.dialogRef.afterClosed()
      .subscribe((res: any) => {
        this.GetPatient()
      }
      );
  }
  // AutoCompleteSearch(evt: any) {
  //   console.warn(this.patients[0].dateOfBirth.getDate())
  //   evt = evt + "";
  //   if (!evt) this.filteredData = this.patients;
  //   else {
  //     /** uses both id and text fields for extensive filtering (case insensitive) . can be tailored for custom needs */
  //     this.filteredData = this.patients.filter(patient => (patient.id + "") === evt || patient.patientName.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.gender?.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.address?.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.area?.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.city?.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.houseNo?.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.dateOfBirth?.getDate().toString.toString().toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0
  //       || patient.contactNo?.toLocaleLowerCase().indexOf(evt.toLocaleLowerCase()) >= 0);
  //   }
  // }
  AutoCompleteSearch(evet: any) {
    this.filteredData = this.patients.filter((patient) =>
      this.patientMatchesSearch(patient, evet)
    );
  }
  patientMatchesSearch(patient: PatientVM, evet): boolean {
    // Convert the search term to lowercase for case-insensitive search
    const searchLower = evet.toLowerCase();

    // Split the search term into individual search criteria
    const searchCriteria = searchLower.split(' ');

    // Check if all specified criteria match the patient's fields
    return searchCriteria.every((criteria) => {
      // If the criteria is empty, skip it
      if (!criteria.trim()) return true;

      // Check if the criteria exists in any of the patient's fields
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
  utcToLocalDate(utcDate: Date): Date {
    if (utcDate)
      return new Date(utcDate);
    else
      return new Date
  }
}

