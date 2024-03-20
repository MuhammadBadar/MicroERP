
import { ChangeDetectorRef, Component, NgZone, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ThisReceiver } from '@angular/compiler';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { PrescriptionVM, RxMedicineVM } from '../../Models/PrescriptionVM';
import { PMSService } from '../../pms.service';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import * as moment from 'moment';
import { PatientVM } from '../../Models/PatientVM';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';
import { NotificationVM } from 'src/app/views/catalog/Models/NotificationVM';
import { SMTPCredsVM } from 'src/app/views/catalog/Models/SMTPCredsVM';

@Component({
  selector: 'app-rx-list',
  templateUrl: './rx-list.component.html',
  styleUrls: ['./rx-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class RxListComponent implements OnInit {
  isLoading: boolean = false
  isReadOnly: boolean = false
  curDate: Date = new Date
  Prescription: PrescriptionVM[] = [];;
  selectedRowIndex = -1;
  selectedRow: PrescriptionVM
  UnPosted: number
  Edit: boolean = true;
  isPosted: boolean = false
  // displayedColumns: string[] = ['expand', 'date', 'vchNo', 'status', 'vendor', 'salesman', 'invNo', 'godown', 'docNo', 'docDate', 'isPosted', 'Action'];
  innerDisplayedColumns: string[] = ['medicine', 'amQty', 'noonQty', 'eveQty', 'mr', 'days', 'remarks'];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'time', 'tokenNo', 'doctor', 'patient', 'amount', 'temperature', 'bp', 'weight', 'remarks', 'Action'];
  style = "background-image: linear-gradient(to bottom, rgb(2, 33, 58), rgb(7, 95, 122));color:white;font-weight:normal "
  columnsToDisplayWithExpand = ['expand', 'tokenNo', 'date', 'time', 'doctor', 'patient', 'amount', 'temperature', 'bp', 'weight', 'remarks'];
  rptDisplayedCol: string[] = ['date', 'category', 'actions']
  extraColumns: string[] = []
  expandedElement: any;
  isActive: boolean
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<RxMedicineVM>> | undefined;
  fields: SettingsTypeVM[]
  selectedRx: PrescriptionVM
  filteredData: any;
  searchValue?: any
  patients?: PatientVM[]
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public pmsSvc: PMSService,
    public catSvc: CatalogService,
    private zone: NgZone,
    private cdRef: ChangeDetectorRef,
    //  private _loc: Location,
  ) {
    this.selectedRx = new PrescriptionVM
    this.selectedRow = new PrescriptionVM;
    this.selectedRx.from = new Date
    this.selectedRx.to = new Date
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Rx)
    this.selectedRx.from = new Date
    this.selectedRx.to = new Date
    this.extraColumns = []
    this.columnsToDisplayWithExpand = ['expand', 'date', 'time', 'tokenNo', 'doctor', 'patient', 'amount', 'temperature', 'bp', 'weight', 'remarks'];
    //this.GetPrescription();
    this.SearchByDates()
    this.GetField()
    this.GetPatient()
  }
  highlight(row: PrescriptionVM) {
    // window.scrollTo(0, 0)
    this.selectedRow = new PrescriptionVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
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
  GetField() {
    var stng = new SettingsTypeVM
    stng.parentId = EnumTypes.RxExtraFields
    stng.isActive = true
    stng.clientId = +localStorage.getItem("ClientId")
    stng.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(stng).subscribe((res: SettingsTypeVM[]) => {
      this.fields = res;
      res.forEach(field => {
        if (!this.extraColumns.includes(field.name))
          this.extraColumns.push(field.name);
      });
      this.columnsToDisplayWithExpand = [...this.columnsToDisplayWithExpand, ... this.extraColumns, 'Action']
      this.dataSource = new MatTableDataSource(this.Prescription);
    });
  }
  GetPrescription() {
    this.selectedRow = new PrescriptionVM;
    var rx = new PrescriptionVM
    rx.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchPrescription(rx).subscribe({
      next: (res: PrescriptionVM[]) => {
        this.Prescription = res;
        this.dataSource = new MatTableDataSource(this.Prescription);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  SearchByDates() {
    this.isLoading = true
    var rx = new PrescriptionVM
    if (this.selectedRx.from != null && this.selectedRx.from != undefined) {
      this.selectedRx.from = moment(this.selectedRx.from).toDate()
      rx.from = new Date(Date.UTC(this.selectedRx.from.getFullYear(), this.selectedRx.from.getMonth(), this.selectedRx.from.getDate()))
    }
    if (this.selectedRx.to != null && this.selectedRx.to != undefined) {
      this.selectedRx.to = moment(this.selectedRx.to).toDate()
      rx.to = new Date(Date.UTC(this.selectedRx.to.getFullYear(), this.selectedRx.to.getMonth(), this.selectedRx.to.getDate()))
    }
    if (this.selectedRx.patientId != 0 && this.selectedRx.patientId != undefined)
      rx.patientId = this.selectedRx.patientId
    rx.clientId = +localStorage.getItem("ClientId")
    var role = localStorage.getItem("Role")
    if (role == "Doctor")
      rx.doctorId = +localStorage.getItem("DoctorId")
    this.pmsSvc.SearchPrescription(rx).subscribe({
      next: (res: PrescriptionVM[]) => {
        this.isLoading = false
        this.Prescription = []
        if (res)
          if (res.length > 0)
            this.Prescription = res;
        this.dataSource = new MatTableDataSource(this.Prescription);
      }, error: (e) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  getCellValue(pres: PrescriptionVM, columnName: string): string {
    const dynamicField = pres.rxmefData.find(field => field.fieldName === columnName);
    return dynamicField ? dynamicField.fieldValue : '';
  }
  EditPrescription(rx: PrescriptionVM) {
    this.route.navigate(['/pms/rx/prescription'], {
      queryParams: {
        id: rx.id
      }
    });
  }
  Back() {
    // this._loc.back();
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
  openReportInNewPage(base64Path: string) {
    this.catSvc.generatePDF(base64Path)
  }
  openPdf(rx: PrescriptionVM) {
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
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      }
    });
  }
  sendMail(rx: PrescriptionVM) {
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
            } else {
              this.catSvc.ErrorMsgBar("Can't send mail as SMTP Credentials not defined", 4000)
              this.isLoading = false
            }
          }, error: () => {
            this.catSvc.ErrorMsgBar("Error Occurred", 4000)
          }
        })
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        this.isLoading = false
      }
    })
  }
}

