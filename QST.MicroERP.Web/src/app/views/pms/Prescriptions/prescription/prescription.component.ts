
import { ItemsService } from './../../../items/items.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';

import { Component, ElementRef, Injector, OnInit, Renderer2, ViewChild } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { MatTableDataSource } from '@angular/material/table';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import * as moment from 'moment';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ChangeDetectorRef } from '@angular/core';
import { PatientVM } from '../../Models/PatientVM';
import { ItemVM } from 'src/app/views/key-accounting/Models/ItemVM';
import { DoctorVM } from '../../Models/DoctorVM';
import { PrescriptionVM, ReportsVM, RxMedicineVM } from '../../Models/PrescriptionVM';
import { PMSService } from '../../pms.service';
import { KeyAccountingService } from 'src/app/views/key-accounting/key-accounting.service';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { DoctorComponent } from '../../Doctors/doctor/doctor.component';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { PatientComponent } from '../../Patients/patient/patient.component';
import { MatSort } from '@angular/material/sort';
import jsPDF from 'jspdf';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import { PDFDocument } from 'pdf-lib';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { DoctorAppointmentsComponent } from '../../Appointments/doctor-appointments/doctor-appointments.component';
import { AppointmentVM } from '../../Models/AppointmentVM';
import { ManageEnumLineComponent } from 'src/app/views/catalog/manage-enum-line/manage-enum-line.component';
import { PatientHistoryComponent } from '../../patient-history/patient-history.component';
import { AppStatus } from 'src/app/views/catalog/Models/Enums/AppStatus';
import { NotificationVM } from 'src/app/views/catalog/Models/NotificationVM';
import { SMTPCredsVM } from 'src/app/views/catalog/Models/SMTPCredsVM';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';
@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.css']
})
export class PrescriptionComponent implements OnInit {
  isReadOnly: boolean = false
  isPrint: Boolean = false
  isSendMail: boolean = false
  isLoading: boolean = false
  docId: number
  minDate = new Date()
  isReadonly: boolean = false
  appId: number
  isDialog: boolean = false
  dialogref: any
  dialogRef: any
  extraColumns: string[] = []
  precautions: SettingsVM[]
  categories: SettingsVM[]
  medRemarks: SettingsVM[]
  TabIndex: number = 0
  rxMedailId = 0
  patId: number
  selectedPatient: PatientVM
  addButton = true
  isPatientDropdownDisabled: boolean = false;
  rptEditMode: boolean = false
  rptAddMode: boolean = true
  proccessing: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = true
  Edit: boolean = false;
  Add: boolean = true;
  medicines?: ItemVM[]
  patients?: PatientVM[]
  doctors?: DoctorVM[]
  mRelations?: SettingsVM[]
  bpStatuses: SettingsVM[]
  rxId!: number
  getVchById!: PrescriptionVM[];
  selectedPrescription: PrescriptionVM
  selectedRxMedicine = new RxMedicineVM
  selectedReport: ReportsVM
  RxMedicines: RxMedicineVM[] = []
  rxPrecaultList: string[] = []
  reports: ReportsVM[] = []

  @ViewChild('PrescriptionForm', { static: true }) PrescriptionForm!: NgForm;
  @ViewChild('medForm', { static: true }) RxMedicineForm!: NgForm;
  @ViewChild('ReportForm', { static: true }) ReportForm!: NgForm;
  displayedColumns: string[] = ['medicine', 'morQty', 'noonQty', 'eveQty', 'mr', 'days', 'remarks', 'actions'];
  dataSource: any
  rptDataSource: any
  precautsDS
  precautsCol: string[] = ['name']
  rptDisplayedCol: string[] = ['date', 'category', 'actions']
  outputArray = [];
  viewMode: boolean = false
  group!: FormGroup;
  fields: SettingsTypeVM[] = [];
  FieldData = [];
  filteredData: any;
  searchValue?: any
  filteredDocData: any;
  medSearchValue?: any
  filteredMedData: any;
  docSearchValue?: any
  dialogData: any;
  _dialogRef: any
  currentLightBoxImage: any
  previewImage = false;
  @ViewChild(MatSort) sort: MatSort;
  selectedAppt: AppointmentVM
  constructor(
    private location: Location,
    private renderer: Renderer2,
    private cdref: ChangeDetectorRef,
    public pmsSvc: PMSService,
    private dialog: MatDialog,
    private keySvc: KeyAccountingService,
    private route: ActivatedRoute,
    private _route: Router,
    private injector: Injector,
    private _location: Location,
    public catSvc: CatalogService,
    private cdRef: ChangeDetectorRef,) {
    this.selectedReport = new ReportsVM
    this.selectedPrescription = new PrescriptionVM();
    this.selectedAppt = new AppointmentVM
    this.selectedRxMedicine = new RxMedicineVM()
    this._dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    debugger
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Rx)
    this.viewMode = false
    this.GetField()
    this.rxMedailId = 0
    this.RxMedicines = []
    this.reports = []
    this.rxPrecaultList = []

    if (this.dialogData != null) {
      if (this.dialogData.rxId != undefined) {
        this.rxId = this.dialogData.rxId
        this.viewMode = true
        this.Edit = false
        this.Add = false
        this.GetPrescriptionById()
      }
    }
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.GetSettings(EnumTypes.MR)
    this.GetSettings(EnumTypes.ReportCategories)
    this.GetSettings(EnumTypes.Precautions)
    this.GetSettings(EnumTypes.Remarks)
    this.GetValues(EnumTypes.BPStatuses)
    this.GetPatient()
    this.GetDoctor()
    this.GetMedicine()
    this.selectedPrescription = new PrescriptionVM();
    this.selectedRxMedicine = new RxMedicineVM()
    this.selectedPrescription.isActive = true
    this.selectedPrescription.time = this.catSvc.GetCurrentTime()
    this.rptDataSource = new MatTableDataSource(this.reports);
    this.rptDataSource.sort = this.sort;
    this.selectedPrescription.clientId = +localStorage.getItem("ClientId")
    var role = localStorage.getItem("Role")
    if (role == "Doctor") {
      var userId = localStorage.getItem("UserId")
      var doc = new DoctorVM
      doc.clientId = +localStorage.getItem("ClientId")
      doc.userId = userId
      this.pmsSvc.SearchDoctor(doc).subscribe({
        next: (res: DoctorVM[]) => {
          this.isReadonly = true
          localStorage.setItem("DoctorId", res[0].id.toString())
          this.selectedPrescription.doctorId = res[0].id
          this.GetNextToken()
          this.route.queryParams.subscribe(params => {
            var isOpenOnLogin = params['openOnLogin'];
            if (isOpenOnLogin) {
              this.OpenDocAppointments()
            }
            if (this.selectedPrescription.doctorId > 0 && this.selectedPrescription.tokenNo)
              this.isPatientDropdownDisabled = true;
            else
              this.isPatientDropdownDisabled = false;
          })
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        }
      })
    }
    if (!this.viewMode)
      this.route.queryParams.subscribe(params => {
        this.rxId = params['id'];
        this.appId = params['appId']
        if (this.rxId > 0) {
          this.Edit = true;
          this.Add = false;
          this.GetPrescriptionById();
        }
        else {
          //this.Add = true;
          //this.Edit = false;
          this.dataSource = new MatTableDataSource(this.RxMedicines);
        }

      });
  }
  ngAfterViewInit() {
    this.rptDataSource = new MatTableDataSource(this.reports);
    this.rptDataSource.sort = this.sort;
  }
  //#region GetMethods
  GetField() {
    var stng = new SettingsTypeVM
    stng.parentId = EnumTypes.RxExtraFields
    stng.isActive = true
    stng.clientId = +localStorage.getItem("ClientId")
    stng.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(stng).subscribe((res: SettingsTypeVM[]) => {

      this.fields = res;
      if (this.fields.length > 0) {
        const formGroup = {};
        this.fields.forEach(formControl => {
          // this.group.addControl(formControl.name, new FormControl(''));
          formGroup[formControl.id] = new FormControl('');
        });
        this.group = new FormGroup(formGroup);
      } else this.fields = []
    });
  }
  GetMedicine() {
    var itm = new ItemVM
    itm.isActive = true
    itm.moduleId = Modules.PMS
    itm.clientId = +localStorage.getItem("ClientId")
    this.keySvc.SearchItem(itm).subscribe({
      next: (res: ItemVM[]) => {
        this.medicines = res;
        this.filteredMedData = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
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
          this.selectedPrescription.doctorId = this.doctors[0].id
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
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
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    setting.moduleId = Modules.PMS
    setting.clientId = +localStorage.getItem("ClientId")
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype == EnumTypes.MR)
          this.mRelations = res;
        else if (etype == EnumTypes.Precautions)
          this.precautions = res;
        else if (etype == EnumTypes.ReportCategories)
          this.categories = res;
        else if (etype == EnumTypes.Remarks)
          this.medRemarks = res;
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetValues(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype == EnumTypes.BPStatuses)
          this.bpStatuses = res;
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetPrescriptionById() {
    this.isLoading = true
    var rx = new PrescriptionVM
    rx.id = this.rxId
    rx.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchPrescription(rx).subscribe({
      next: (res: PrescriptionVM[]) => {
        this.isLoading = false
        this.selectedPrescription = res[0]
        if (this.selectedPrescription.precautions != null)
          this.rxPrecaultList = this.selectedPrescription.precautions.split(',').map(item => item.trim());
        this.RxMedicines = []
        this.selectedPrescription.rxMedicines?.forEach(element => {
          this.RxMedicines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.RxMedicines);
        this.reports = []
        this.reports = res[0].reports
        this.rptDataSource = new MatTableDataSource(res[0].reports)
        this.rptDataSource.sort = this.sort;
        if (this.group)
          for (let index = 0; index < this.selectedPrescription.rxmefData.length; index++) {
            debugger
            var FieldId = this.selectedPrescription.rxmefData[index].fieldId;
            if (FieldId != undefined) {
              var FieldValue = this.selectedPrescription.rxmefData[index].fieldValue
              if (this.selectedPrescription.rxmefData[index].type == "Check Box") {
                var ret = this.convertStringToBool(FieldValue)
                console.warn(ret)
                this.group.get(FieldId.toString())?.setValue(this.convertStringToBool(FieldValue));
                this.group.controls[FieldId].setValue(this.convertStringToBool(FieldValue))
              } else {
                this.group.get(FieldId.toString())?.setValue(FieldValue);
                this.group.controls[FieldId].setValue(FieldValue)
              }
            }
          }
        debugger
        if (this.minDate < this.selectedPrescription.nextVisitDate)
          console.warn("hiiiiiiii")
      }, error: (e) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred !", 6000)
        console.warn(e);
      }
    })
  }
  //#endregion
  //#region Crud Methods
  onBlurNextVisitDate() {
    if (this.selectedPrescription.nextVisitDate != null && this.selectedPrescription.nextVisitDate != undefined) {
      var app = new AppointmentVM
      app.apptDate = this.selectedPrescription.nextVisitDate
      app.patientId = this.selectedPrescription.patientId
      app.doctorId = this.selectedPrescription.doctorId
      this.pmsSvc.SearchAppointment(app).subscribe({
        next: (res: AppointmentVM[]) => {
          if (res.length > 0) {
            app.statusId = AppStatus.Due
            this.pmsSvc.UpdateAppointment(app).subscribe({
            })
          }
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        }
      })
    }
  }
  async Submit() {
    this.selectedPrescription.clientId = +localStorage.getItem("ClientId")
    this.SetDates()
    this.selectedPrescription.rxMedicines = this.RxMedicines
    this.selectedPrescription.reports = this.reports
    this.proccessing = true
    this.PrescriptionValidation();
    if (!this.PrescriptionForm.invalid) {
      if (this.group && this.group.invalid) {
        const controls = this.group.controls;
        for (const name in controls) {
          if (controls[name].hasError('required')) {
            this.group.controls[name].markAsTouched();
            const field = this.fields.find(x => x.id.toString() == name)
            this.catSvc.ErrorMsgBar(` ${field.name} is required field`, 6000)
            this.TabIndex = 0
            break
          }
        }
      } else {
        this.FieldData = []
        if (this.group) {
          Object.keys(this.group.controls).forEach(async (key: any) => {
            debugger
            const obstractControl = this.group.get(key);
            // this.Id = this.Id + 1
            var DbOperation;
            if (this.Edit)
              DbOperation = 2
            else
              DbOperation = 1
            const newRow = {
              "patient": 0, "fieldValue": obstractControl.value.toString(), "fieldId": key,
              "isActive": true
            }
            this.FieldData.push(newRow)
            this.selectedPrescription.rxmefData = this.FieldData
          })
        }
        if (this.selectedPrescription?.id > 0)
          await this.UpdatePrescription();
        else
          await this.SavePrescription();
      }
    } else {
      this.isLoading = false
      this.validateRxFields();
      this.TabIndex = 0
      // this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
    this.proccessing = false
  }
  SavePrescription() {
    this.isLoading = true
    this.selectedPrescription.tokenNo = +(this.selectedPrescription.tokenDate.toString() + this.selectedPrescription.tokenId)
    this.pmsSvc.SavePrescription(this.selectedPrescription).subscribe({
      next: (res: PrescriptionVM) => {
        var app = this.selectedAppt
        app.statusId = AppStatus.Closed
        this.pmsSvc.UpdateAppointment(app).subscribe({
          next: () => {
            this.isLoading = false
          }, error: () => {
            this.isLoading = false
            this.catSvc.ErrorMsgBar("Error Occurred while closing the Appointment", 5000)
          }
        })
        this.selectedPrescription = res
        if (this.isPrint)
          this.openPdf()
        if (this.isSendMail)
          this.sendMail()
        this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        this._route.navigate(['/pms/rx/prescription'], {
          queryParams: {
            id: res.id
          }
        });
        this.Refresh();
      }, error: (e: any) => {
        this.selectedPrescription.reports = []
        this.selectedPrescription.rxMedicines = []
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.RxMedicines = []
        this.reports = []
        this.isLoading = false
        this.proccessing = false
      }
    })
  }
  UpdatePrescription() {
    this.isLoading = true
    this.pmsSvc.UpdatePrescription(this.selectedPrescription).subscribe({
      next: (res: PrescriptionVM) => {
        this.isLoading = false
        if (this.Edit) {
          if (this.lineEditMode)
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
          else if (this.lineAddMode)
            this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
          else
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
        } else {
          if (this.lineEditMode)
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
          else
            this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        }
        this.selectedPrescription = res
        if (this.isPrint)
          this.openPdf()
        if (this.isSendMail)
          this.sendMail()
        this.Refresh();
      }, error: (e: any) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.RxMedicines = []
        this.reports = []
        this.proccessing = false
      }
    })
  }
  edit(rxMed: RxMedicineVM) {
    this.lineEditMode = true
    this.lineAddMode = false
    this.addButton = false
    this.selectedRxMedicine = rxMed
    this.selectedRxMedicine.editMode = true
  }
  delete(rxMed: RxMedicineVM) {
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
        if (rxMed.id == undefined || rxMed.id == 0) {
          Swal.fire(
            'Deleted!',
            'Successfully Deleted.',
            'success'
          )
        } else {
          var Prescription = new PrescriptionVM
          Prescription = this.selectedPrescription
          Prescription.rxMedicines = []
          Prescription.rxMedicines.push(rxMed)
          rxMed.dBoperation = 3
          this.pmsSvc.UpdatePrescription(Prescription).subscribe({
            next: (data: PrescriptionVM) => {
              Swal.fire(
                'Deleted!',
                'Successfully Deleted.',
                'success'
              )
              this.RxMedicines = []
              data.rxMedicines?.forEach(element => {
                this.RxMedicines.push(element)
              });
              this.dataSource = new MatTableDataSource(data.rxMedicines);
            }, error: (e) => {
              this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
              console.warn(e);
            }
          })
        }
      }
    })
  }
  async AddRxMedicinetoList() {
    debugger
    this.PrescriptionValidation();
    if (!this.PrescriptionForm.invalid) {
      if (this.selectedRxMedicine.medId == 0 || this.selectedRxMedicine.medId == undefined) {
        this.RxMedicineForm.controls['medId'].setErrors({ 'incorrect': true });
        this.catSvc.ErrorMsgBar("Please Select Medicine", 5000)
      }
      else {
        if (this.selectedRxMedicine.mrId == 0 || this.selectedRxMedicine.mrId == undefined) {
          this.RxMedicineForm.controls['mrId'].setErrors({ 'incorrect': true });
          this.catSvc.ErrorMsgBar("Please Select Meal Relation", 5000)
        }
        else {
          if (!this.RxMedicineForm.invalid) {
            if (this.lineEditMode)
              this.selectedRxMedicine.dBoperation = 2
            else
              this.selectedRxMedicine.dBoperation = 1
            if (this.selectedRxMedicine.dBoperation == 1) {
              this.lineAddMode = true
            }
            this.RxMedicines.push(this.selectedRxMedicine)

            this.selectedPrescription.rxMedicines = []
            this.selectedPrescription.rxMedicines?.push(this.selectedRxMedicine)

            this.Submit()
          }
          else
            this.validateMedFields()
          //this.catSvc.ErrorMsgBar("Please fill all required fields of Medicine Detail", 5000)
        }
      }
    } else {
      this.validateRxFields()
      //this.catSvc.ErrorMsgBar("Please fill all required fields of Patient", 5000)
      //this.TabIndex = 0
    }
  }
  //#endregion
  //#region Dialog Box
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
  OpenDocAppointments() {
    var isView = false;
    if (this.viewMode || this.Edit)
      isView = true
    let dialogRef = this.dialog.open(DoctorAppointmentsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: { doctorId: this.selectedPrescription.doctorId, viewMode: isView }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        debugger
        if (!this.Edit && !this.viewMode)
          if (res) {
            if (res.selectedAppt) {
              this.selectedAppt = res.selectedAppt
              this.selectedPrescription.patientId = res.selectedAppt.patientId
              this.selectedPrescription.tokenNo = res.selectedAppt.tokenNo
              this.SetToken()
            }
          } else {
          }
      }
    })
  }
  OpenPrecautDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Precautions, isDialog: true, moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId")
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Precautions)
      }
    })
  }
  OpenCatDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.ReportCategories, isDialog: true, moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId")
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.ReportCategories)
      }
    })
  }
  PatHistoryDialog() {
    this.dialogRef = this.dialog.open(PatientHistoryComponent, {
      width: '1200px', height: '590px'
      , data: { patId: this.selectedPrescription.patientId, docId: this.selectedPrescription.doctorId }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetField()
      });
  }
  OpenRemarksDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Remarks, isDialog: true, moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId")
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Remarks)
      }
    })
  }
  OpenMRDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.MR, isDialog: true, moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId")
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.MR)
      }
    })
  }
  //#endregion
  //#region Free Text Search
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
        // doctor.dateOfBirth ? this.catSvc.formatDate(doctor.dateOfBirth) : '',
      ].some((field) => field && field.toLowerCase().includes(criteria));
      return criteriaMatches;
    });
  }
  MedAutoCompleteSearch(evet: any) {
    this.filteredMedData = this.medicines.filter((med) =>
      this.medicineMatchesSearch(med, evet)
    );
  }
  medicineMatchesSearch(item: ItemVM, evet): boolean {
    const searchLower = evet.toLowerCase();
    const searchCriteria = searchLower.split(' ');
    return searchCriteria.every((criteria) => {
      if (!criteria.trim()) return true;
      const criteriaMatches = [
        item.name,
        item.category,
        item.manufacturer,
        item.formula,
      ].some((field) => field && field.toLowerCase().includes(criteria));
      return criteriaMatches;
    });
  }
  //#endregion
  //#region Others
  validateNo(e): boolean {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
      return false
    return true
  }
  validateRxFields() {
    if (this.PrescriptionForm.invalid) {
      const _controls = this.PrescriptionForm.controls;
      for (const name in _controls) {
        if (_controls[name].hasError('required')) {
          this.catSvc.ErrorMsgBar(` ${name} is required field`, 6000)
          this.PrescriptionForm.controls[`${name}`].setErrors({ 'incorrect': true });
          this.TabIndex = 0
          break
        }
      }
    } else { }
  }
  validateMedFields() {
    debugger
    if (this.RxMedicineForm.invalid) {
      const _controls = this.RxMedicineForm.controls;
      for (const name in _controls) {
        if (_controls[name].hasError('required')) {
          this.catSvc.ErrorMsgBar(` ${name} is required field`, 6000)
          this.RxMedicineForm.controls[`${name}`].setErrors({ 'incorrect': true });
          //this.TabIndex = 0
          break
        }
      }
    } else { }
  }
  validateRptFields() {
    if (this.ReportForm.invalid) {
      const _controls = this.ReportForm.controls;
      for (const name in _controls) {
        if (_controls[name].hasError('required')) {
          this.catSvc.ErrorMsgBar(` ${name} is required field`, 6000)
          this.ReportForm.controls[`${name}`].setErrors({ 'incorrect': true });
          //this.TabIndex = 0
          break
        }
      }
    } else { }
  }
  RefreshDetail() {
    this.lineAddMode = false;
    this.addButton = true
    this.lineEditMode = false;
    this.selectedRxMedicine = new RxMedicineVM
    if (this.rxId > 0)
      this.GetPrescriptionById()
  }
  Back() {
    this._route.navigate(['/pms/rx/rxList'])
  }
  PrescriptionValidation() {
    if (this.selectedPrescription.patientId == 0 || this.selectedPrescription.patientId == undefined)
      this.PrescriptionForm.controls['patientId'].setErrors({ 'incorrect': true });
    if (this.selectedPrescription.doctorId == 0 || this.selectedPrescription.doctorId == undefined)
      this.PrescriptionForm.controls['doctorId'].setErrors({ 'incorrect': true });
  }
  onBlur() {
    console.warn(this.selectedPrescription)
    if (this.selectedPrescription.id > 0) {
      this.Submit()
      // this.proccessing = true
      // this.PrescriptionValidation()
      // if (!this.PrescriptionForm.invalid) {
      //   this.SetDates()
      //   this.pmsSvc.UpdatePrescription(this.selectedPrescription).subscribe({
      //     next: (res: PrescriptionVM) => {
      //       this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
      //       this.selectedPrescription = res
      //       this.rxPrecaultList = this.selectedPrescription.precautions.split(',').map(item => item.trim());
      //       this.RxMedicines = []
      //       this.selectedPrescription.rxMedicines?.forEach(element => {
      //         this.RxMedicines.push(element)
      //       });
      //       this.dataSource = new MatTableDataSource(res.rxMedicines);
      //       this.RefreshDetail()
      //       this.refreshReportLine()
      //       this.reports = []
      //       this.reports = res.reports
      //       this.rptDataSource = new MatTableDataSource(res.reports)
      //       this.rptDataSource.sort = this.sort;
      //       this.proccessing = false
      //     }, error: (e: any) => {
      //       this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      //       console.warn(e);
      //       this.proccessing = false
      //     }
      //   })
      // } else {
      //   this.catSvc.ErrorMsgBar("Please fill all required fields", 5000)
      // }
    }
  }
  SetDates() {
    if (this.selectedPrescription.date != null && this.selectedPrescription.date != undefined) {
      this.selectedPrescription.date = moment(this.selectedPrescription.date).toDate()
      this.selectedPrescription.date = new Date(Date.UTC(this.selectedPrescription.date.getFullYear(), this.selectedPrescription.date.getMonth(), this.selectedPrescription.date.getDate()))
    }
    if (this.selectedPrescription.nextVisitDate != null && this.selectedPrescription.nextVisitDate != undefined) {
      this.selectedPrescription.nextVisitDate = moment(this.selectedPrescription.nextVisitDate).toDate()
      this.selectedPrescription.nextVisitDate = new Date(Date.UTC(this.selectedPrescription.nextVisitDate.getFullYear(), this.selectedPrescription.nextVisitDate.getMonth(), this.selectedPrescription.nextVisitDate.getDate()))
    }
  }
  convertStringToBool(value: string): boolean {
    return value === "true";
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
    this.cdref.markForCheck();
  }
  Refresh() {
    if (this.selectedPrescription.precautions != null)
      this.rxPrecaultList = this.selectedPrescription.precautions.split(',').map(item => item.trim());
    this.RxMedicines = []
    this.selectedPrescription.rxMedicines?.forEach(element => {
      this.RxMedicines.push(element)
    });
    this.dataSource = new MatTableDataSource(this.selectedPrescription.rxMedicines);
    this.RefreshDetail()
    this.refreshReportLine()
    this.reports = []
    this.reports = this.selectedPrescription.reports
    this.rptDataSource = new MatTableDataSource(this.selectedPrescription.reports)
    this.rptDataSource.sort = this.sort;
    this.proccessing = false
    this.Edit = true
    this.Add = false
  }
  utcToLocalDate(utcDate: Date): Date {
    if (utcDate)
      return new Date(utcDate);
    else
      return new Date
  }
  //#endregion
  //#region Report Methods
  refreshReportLine() {
    this.rptEditMode = false;
    this.rptAddMode = true;
    this.selectedReport = new ReportsVM
    if (this.rxId > 0)
      this.GetPrescriptionById()
  }
  handleFileInput = async (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      debugger
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[index]);
      reader.onload = (event: any) => {
        debugger
        this.selectedReport.reportBase64Path = event.target.result
        this.selectedReport.name = e.target.files[index].name.toString()
      };
    }
  }
  onPreviewImage(): void {
    this.previewImage = true
    this.currentLightBoxImage = this.selectedReport.reportBase64Path
  }
  onClosePreview() {
    this.previewImage = false;
  }
  async Upload() {
    this.PrescriptionValidation();
    if (!this.PrescriptionForm.invalid) {
      if (this.selectedReport.categoryId == 0 || this.selectedReport.categoryId == undefined) {
        this.ReportForm.controls['categoryId'].setErrors({ "incorrect": true })
        this.catSvc.ErrorMsgBar("Please Select Catagory", 5000)
      }
      else {
        if (!this.ReportForm.invalid) {
          if (this.selectedReport.date != null && this.selectedReport.date != undefined) {
            this.selectedReport.date = moment(this.selectedReport.date).toDate()
            this.selectedReport.date = new Date(Date.UTC(this.selectedReport.date.getFullYear(), this.selectedReport.date.getMonth(), this.selectedReport.date.getDate()))
          }
          if (this.selectedReport.reportBase64Path != null && this.selectedReport.reportBase64Path != '') {
            if (this.rptEditMode)
              this.selectedReport.dBoperation = 2
            else
              this.selectedReport.dBoperation = 1
            if (this.selectedReport.dBoperation == 1) {
              this.rptAddMode = true
            }
            this.reports.push(this.selectedReport)

            this.selectedPrescription.reports = []
            this.selectedPrescription.reports?.push(this.selectedReport)

            this.Submit()
          } else
            this.catSvc.ErrorMsgBar("Please Browse a report to Upload", 5000)
        }
        else
          this.validateRptFields();
        //this.catSvc.ErrorMsgBar("Please fill all required fields of Report ", 5000)
      }
    } else {
      this.validateRxFields();
      //this.catSvc.ErrorMsgBar("Please fill all required fields of Patient", 5000)
      //this.TabIndex = 0
    }
  }
  editRpt(rpt: ReportsVM) {
    this.rptEditMode = true;
    this.rptAddMode = false;
    this.selectedReport = rpt;
  }
  deleteRpt(rpt: ReportsVM) {
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
        if (rpt.id == undefined || rpt.id == 0) {
          Swal.fire(
            'Deleted!',
            'Successfully Deleted.',
            'success'
          );
        } else {
          var Prescription = new PrescriptionVM
          Prescription = this.selectedPrescription
          Prescription.reports = []
          rpt.dBoperation = 3
          Prescription.reports.push(rpt)
          this.pmsSvc.UpdatePrescription(Prescription).subscribe({
            next: (data: PrescriptionVM) => {
              Swal.fire(
                'Deleted!',
                'Successfully Deleted.',
                'success'
              );
              this.reports = []
              data.reports?.forEach(element => {
                this.reports.push(element)
              });
              this.rptDataSource = new MatTableDataSource(data.reports);
              this.rptDataSource.sort = this.sort;
            }, error: (e) => {
              this.catSvc.ErrorMsgBar("Error Occurred!", 4000);
              console.warn(e);
            }
          });
        }
      }
    });
  }
  openReportInNewPage(base64Path: string) {
    this.catSvc.generatePDF(base64Path);
  }
  //#endregion
  //#region Search Appointments
  GetNextToken() {
    if (this.selectedPrescription.doctorId != 0 && this.selectedPrescription.doctorId != undefined) {
      var appt = new AppointmentVM
      if (this.selectedPrescription.tokenId > 0 && this.selectedPrescription.tokenId != null) {
        this.selectedPrescription.tokenNo = +(this.selectedPrescription.tokenDate.toString() + this.selectedPrescription.tokenId)
        appt.tokenNo = this.selectedPrescription.tokenNo
      }
      appt.apptDate = new Date
      appt.apptDate = moment(appt.apptDate).toDate()
      appt.apptDate = new Date(Date.UTC(appt.apptDate.getFullYear(), appt.apptDate.getMonth(), appt.apptDate.getDate()))
      appt.clientId = +localStorage.getItem("ClientId")
      appt.adjacentType = "Next"
      appt.statusId = AppStatus.Waiting
      appt.doctorId = this.selectedPrescription.doctorId
      this.pmsSvc.SearchNextAppt(appt).subscribe({
        next: (res: AppointmentVM[]) => {
          if (res.length > 0) {
            // if (this.Edit) {
            //   this._route.navigate(['/pms/rxx/prescription'], {
            //     queryParams: {
            //       appId: res[0].id
            //     }
            //   });
            // }
            this.selectedAppt = res[0]
            this.selectedPrescription.patientId = res[0].patientId
            this.selectedPrescription.tokenNo = res[0].tokenNo
            this.SetToken()
            this.selectedPrescription.doctorId = res[0].doctorId
          }
          else
            this.catSvc.ErrorMsgBar("Having no next Appointment", 4000)
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Ocuurred", 5000)
        }
      });
    }
    else
      this.catSvc.ErrorMsgBar("Please Select Doctor ", 4000)
  }
  resetAppt() {
    this.selectedPrescription.tokenNo = undefined
    this.selectedPrescription.tokenId = undefined
    this.SetToken()
    this.selectedPrescription.patientId = 0
  }
  GetPreToken() {
    if (this.selectedPrescription.doctorId != 0 && this.selectedPrescription.doctorId != undefined) {
      var appt = new AppointmentVM
      if (this.selectedPrescription.tokenId > 0 && this.selectedPrescription.tokenId != null) {
        this.selectedPrescription.tokenNo = +(this.selectedPrescription.tokenDate.toString() + this.selectedPrescription.tokenId)
        appt.tokenNo = this.selectedPrescription.tokenNo
      }
      if (this.selectedPrescription.doctorId > 0)
        appt.doctorId = this.selectedPrescription.doctorId
      appt.apptDate = new Date
      appt.apptDate = moment(appt.apptDate).toDate()
      appt.apptDate = new Date(Date.UTC(appt.apptDate.getFullYear(), appt.apptDate.getMonth(), appt.apptDate.getDate()))
      appt.clientId = +localStorage.getItem("ClientId")
      appt.adjacentType = "Previous"
      appt.statusId = AppStatus.Waiting
      appt.doctorId = this.selectedPrescription.doctorId
      this.pmsSvc.SearchNextAppt(appt).subscribe({
        next: (res: AppointmentVM[]) => {
          if (res.length > 0) {
            this.selectedAppt = res[0]
            this.selectedPrescription.patientId = res[0].patientId
            this.selectedPrescription.tokenNo = res[0].tokenNo
            this.SetToken()
            this.selectedPrescription.doctorId = res[0].doctorId
          }
          else
            this.catSvc.ErrorMsgBar("Having no Previous Appointment", 4000)
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Ocuurred", 5000)
        }
      });
    }
    else
      this.catSvc.ErrorMsgBar("Please Select Doctor ", 4000)
  }
  GetApptById() {
    var appt = new AppointmentVM
    appt.id = this.appId
    appt.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchAppointment(appt).subscribe({
      next: (res: AppointmentVM[]) => {
        if (res.length > 0) {
          const queryParams = { ...this.route.snapshot.queryParams };
          delete queryParams['id'];
          this.selectedPrescription = new PrescriptionVM
          this.RxMedicines = []
          this.rxPrecaultList = []
          this.reports = []
          this.Edit = false
          this.Add = true
          this.selectedAppt = res[0]
          this.selectedPrescription.patientId = res[0].patientId
          this.selectedPrescription.tokenNo = res[0].tokenNo
          this.SetToken()
          this.selectedPrescription.doctorId = res[0].doctorId
        }
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Ocuurred", 5000)
      }
    });
  }
  SearchAppt() {
    debugger
    if (this.selectedPrescription.doctorId != 0 && this.selectedPrescription.doctorId != undefined) {
      if (this.selectedPrescription.tokenId > 0 && this.selectedPrescription.tokenId != null) {
        //this.SetToken()
        this.selectedPrescription.tokenNo = +(this.selectedPrescription.tokenDate.toString() + this.selectedPrescription.tokenId)
        if (this.selectedPrescription.doctorId == 0 || this.selectedPrescription.doctorId == undefined) {
          this.PrescriptionForm.controls['doctorId'].setErrors({ "incorrect": true })
          this.catSvc.ErrorMsgBar("Please Select a Doctor to get Appointmnet", 6000)
        } else {
          var appt = new AppointmentVM
          appt.statusId = AppStatus.Waiting
          appt.tokenNo = this.selectedPrescription.tokenNo
          appt.apptDate = new Date
          appt.apptDate = moment(appt.apptDate).toDate()
          appt.apptDate = new Date(Date.UTC(appt.apptDate.getFullYear(), appt.apptDate.getMonth(), appt.apptDate.getDate()))
          appt.doctorId = this.selectedPrescription.doctorId
          appt.clientId = +localStorage.getItem("ClientId")
          this.pmsSvc.SearchAppointment(appt).subscribe({
            next: (res: AppointmentVM[]) => {
              if (res)
                if (res.length > 0) {
                  this.selectedAppt = res[0]
                  this.selectedPrescription.patientId = res[0].patientId
                  this.selectedPrescription.tokenNo = res[0].tokenNo
                  this.SetToken()
                  this.selectedPrescription.doctorId = res[0].doctorId
                }
                else {
                  // this.selectedPrescription.doctorId = 0
                  this.selectedPrescription.patientId = 0
                  this.catSvc.ErrorMsgBar("This Doctor has no Appointment with this TokenNo for Today's Date", 5000)
                  this.resetAppt()
                  this.SetToken()
                }
            }, error: () => {
              this.catSvc.ErrorMsgBar("Error Occurred", 5000)
              this.selectedPrescription.doctorId = 0
              this.selectedPrescription.patientId = 0
            }
          });
        }
      }
      else this.resetAppt()
    }
    else
      this.catSvc.ErrorMsgBar("Please Select Doctor ", 5000)
  }
  GetAptByPatient() {
    if (this.selectedPrescription.doctorId != 0 && this.selectedPrescription.doctorId != undefined) {
      var appt = new AppointmentVM
      appt.patientId = this.selectedPrescription.patientId
      appt.doctorId = this.selectedPrescription.doctorId
      appt.apptDate = new Date
      appt.statusId = AppStatus.Waiting
      appt.apptDate = moment(appt.apptDate).toDate()
      appt.clientId = +localStorage.getItem("ClientId")
      if (this.selectedPrescription.tokenId > 0 && this.selectedPrescription.tokenId != null) {
        this.selectedPrescription.tokenNo = +(this.selectedPrescription.tokenDate.toString() + this.selectedPrescription.tokenId)
        appt.tokenNo = this.selectedPrescription.tokenNo
      }
      appt.apptDate = new Date(Date.UTC(appt.apptDate.getFullYear(), appt.apptDate.getMonth(), appt.apptDate.getDate()))
      this.pmsSvc.SearchAppointment(appt).subscribe({
        next: (res: AppointmentVM[]) => {
          if (res)
            if (res.length > 0) {
              this.selectedAppt = res[0]
              this.selectedPrescription.patientId = res[0].patientId
              this.selectedPrescription.tokenNo = res[0].tokenNo
              this.SetToken()
              this.selectedPrescription.doctorId = res[0].doctorId
            }
            else {
              //this.selectedPrescription.doctorId = 0
              this.selectedPrescription.tokenNo = undefined
              this.catSvc.ErrorMsgBar("This Patient has no Appointment for Today's Date with this Doctor", 5000)
            }
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          this.selectedPrescription.doctorId = 0
          this.selectedPrescription.patientId = 0
        }
      });
    }
    else
      this.catSvc.ErrorMsgBar("Please Select Doctor First", 5000)
  }
  SetToken() {
    // if (this.selectedPrescription.tokenId > 0)
    //   this.selectedPrescription.tokenNo = +(this.selectedPrescription.tokenDate.toString() + this.selectedPrescription.tokenId)
    if (this.selectedPrescription.tokenNo > 0) {
      const numericValue = this.selectedPrescription.tokenNo.toString().replace(/[^0-9]/g, '');
      this.selectedPrescription.tokenDate = Number(numericValue.substring(0, 6))
      this.selectedPrescription.tokenId = Number(numericValue.substring(6))
      return this.selectedPrescription.tokenDate
    }
    else {
      const currentDate = new Date();
      this.selectedPrescription.tokenDate = this.pmsSvc.dateTranform(currentDate)
      this.selectedPrescription.tokenNo = undefined
      return this.selectedPrescription.tokenDate
    }
  }
  getTokenPrefix() {
    if (this.selectedPrescription.tokenNo > 0) {
      const numericValue = this.selectedPrescription.tokenNo.toString().replace(/[^0-9]/g, '');
      this.selectedPrescription.tokenDate = Number(numericValue.substring(0, 6))
      return this.selectedPrescription.tokenDate
    }
    else {
      const currentDate = new Date();
      this.selectedPrescription.tokenDate = this.pmsSvc.dateTranform(currentDate)
      return this.selectedPrescription.tokenDate
    }
  }
  //#endregion
  //#region Pdf & Mail
  openPdf() {
    this.isPrint = false
    var rx = this.selectedPrescription
    this.isLoading = true
    this.pmsSvc.GetRxPdf(rx).subscribe({
      next: (response) => {
        this.isLoading = false
        const blob = new Blob([response.body], { type: 'application/pdf' });
        const url = URL.createObjectURL(blob);
        window.open(url);
      }, error: (e) => {
        this.isLoading = false
        console.warn(e)
        this.catSvc.ErrorMsgBar("Error Occurred while getting Report", 5000)
      }
    });
  }
  sendMail() {
    this.isSendMail = false
    var rx = this.selectedPrescription
    this.isLoading = true
    this.pmsSvc.GetRxPdf(rx).subscribe({
      next: (response) => {
        const blob = new Blob([response.body], { type: 'application/pdf' });
        var mail = new NotificationVM
        mail.senderMail = "bintameer212@gmail.com"
        mail.mailSubject = rx.patient
        mail.attachment = blob
        mail.mailBody = "Prescription PDF"
        mail.receiverMail = rx.patientEmail
        var smpt = new SMTPCredsVM
        smpt.clientId = +localStorage.getItem("ClientId")
        this.catSvc.SearchSMTPCreds(smpt).subscribe({
          next: (res: SMTPCredsVM[]) => {
            if (res.length > 0) {
              mail.server = res[0].server
              mail.password = res[0].password
              mail.userName = res[0].userName
              mail.port = res[0].port
              this.catSvc.sendMailwithPdf(mail).subscribe({
                next: () => {
                  this.catSvc.SuccessMsgBar("Success! Your request has been processed.", 4000)
                  this.isLoading = false
                }, error: (e) => {
                  console.warn(e)
                  this.isLoading = false
                  this.catSvc.ErrorMsgBar("Error Occurred while sending mail", 4000)
                }
              });
            } else this.catSvc.ErrorMsgBar("Can't send mail as SMTP Credentials not defined", 4000)
          }, error: () => {
            this.catSvc.ErrorMsgBar("Error Occurred while sending mail", 5000)
          }
        })
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred while getting Report", 5000)
        this.isLoading = false
      }
    })
  }
  //#endregion
}

