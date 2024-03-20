
import { ChangeDetectorRef, Component, NgZone, OnInit, QueryList, ViewChild, ViewChildren, Injector } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ThisReceiver } from '@angular/compiler';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { PrescriptionVM, RxMedicineVM } from '../Models/PrescriptionVM';
import { PMSService } from '../pms.service';
import { Location } from '@angular/common';
import { PatientVM } from '../Models/PatientVM';
import * as moment from 'moment';
import { PrescriptionComponent } from '../Prescriptions/prescription/prescription.component';
@Component({
  selector: 'app-patient-history',
  templateUrl: './patient-history.component.html',
  styleUrls: ['./patient-history.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class PatientHistoryComponent implements OnInit {
  curDate: Date = new Date
  patients: PatientVM[]
  selectedRx: PrescriptionVM
  Prescription: PrescriptionVM[] = [];
  selectedRowIndex = -1;
  selectedRow: PrescriptionVM
  UnPosted: number
  Edit: boolean = true;
  isPosted: boolean = false
  selectedPatient: PatientVM
  innerDisplayedColumns: string[] = ['medicine', 'amQty', 'noonQty', 'eveQty', 'mr', 'days', 'remarks'];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'time', 'doctor', 'Action'];
  style = "background-image: linear-gradient(to bottom, rgb(2, 33, 58), rgb(7, 95, 122));color:white;font-weight:normal "
  columnsToDisplayWithExpand = ['expand', 'tokenNo', 'date', 'time', 'doctor', 'patient', 'amount', 'temperature', 'bp', 'weight', 'ecg', 'remarks'];
  rptDisplayedCol: string[] = ['date', 'category', 'actions']
  patId: number
  extraColumns: string[] = []
  expandedElement: any;
  isActive: boolean
  dialogRef: any
  visitDialog: any
  dialogData: any;
  isDialog: boolean = false;
  doctorId: number
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<RxMedicineVM>> | undefined;
  fields: SettingsTypeVM[]
  constructor(
    private injector: Injector,
    private route: Router,
    private aRoute: ActivatedRoute,
    public dialog: MatDialog,
    public pmsSvc: PMSService,
    public catSvc: CatalogService,
    private zone: NgZone,
    private cdRef: ChangeDetectorRef,
    private _loc: Location,
  ) {
    this.selectedRx = new PrescriptionVM
    this.selectedPatient = new PatientVM
    this.selectedRow = new PrescriptionVM;
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);

  }
  ngOnInit(): void {
    this.selectedRx = new PrescriptionVM
    this.extraColumns = []
    this.columnsToDisplayWithExpand = ['expand', 'date', 'time', 'doctor', 'Action'];
    this.GetField()
    this.GetPatientById();
    this.GetPrescriptionByPatient();
    this.aRoute.queryParams.subscribe(params => {
      this.patId = params['patId'];
    });
    if (this.patId > 0) {
      this.GetPatientById();
      this.GetPrescriptionByPatient();
      //this.onBlur()
    }
    if (this.dialogData != null) {
      if (this.dialogData.patId)
        this.patId = this.dialogData.patId
      if (this.dialogData.docId)
        this.doctorId = this.dialogData.docId
      this.GetPatientById();
      this.GetPrescriptionByPatient();
    }
  }
  highlight(row: PrescriptionVM) {
    // window.scrollTo(0, 0)
    this.selectedRow = new PrescriptionVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetPatientById() {
    var pat = new PatientVM
    pat.id = this.patId
    pat.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchPatient(pat).subscribe({
      next: (res: PatientVM[]) => {
        this.selectedPatient = res[0]
        this.selectedPatient.age = this.catSvc.GetAge(res[0].dateOfBirth)
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
      }
    })
  }
  GetField() {
    var stng = new SettingsTypeVM
    stng.parentId = EnumTypes.RxExtraFields
    stng.isActive = true
    this.catSvc.SearchSettingsType(stng).subscribe((res: SettingsTypeVM[]) => {
      this.fields = res;
      res.forEach(field => {
        if (!this.extraColumns.includes(field.name)) {
          this.extraColumns.push(field.name);
        }
      });
      // this.columnsToDisplayWithExpand = [...this.columnsToDisplayWithExpand, ... this.extraColumns, 'Action']
      this.dataSource = new MatTableDataSource(this.Prescription);
    });
  }
  GetPrescriptionByPatient() {
    if (this.patId > 0) {
      this.selectedRow = new PrescriptionVM;
      var pres = new PrescriptionVM
      pres.patientId = this.patId
      if (this.doctorId > 0)
        pres.doctorId = this.doctorId
      pres.clientId = +localStorage.getItem("ClientId")
      this.pmsSvc.SearchPrescription(pres).subscribe({
        next: (res: PrescriptionVM[]) => {
          this.Prescription = res;
          this.dataSource = new MatTableDataSource(this.Prescription);
          if (res)
            if (res.length > 0) {
            } else
              this.catSvc.ErrorMsgBar("This Patient has no History!", 4000)
        }, error: (e) => {
          this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
          console.warn(e);
        }
      })
    }
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
    this.dialogRef.close()
  }
  // Back() {
  //   this._loc.back();
  // }
  onBlur() {
    var rx = new PrescriptionVM
    if (this.selectedRx.from != null && this.selectedRx.from != undefined) {
      this.selectedRx.from = moment(this.selectedRx.from).toDate()
      rx.from = new Date(Date.UTC(this.selectedRx.from.getFullYear(), this.selectedRx.from.getMonth(), this.selectedRx.from.getDate()))
    }
    if (this.selectedRx.to != null && this.selectedRx.to != undefined) {
      this.selectedRx.to = moment(this.selectedRx.to).toDate()
      rx.to = new Date(Date.UTC(this.selectedRx.to.getFullYear(), this.selectedRx.to.getMonth(), this.selectedRx.to.getDate()))
    }
    rx.patientId = this.patId
    if (this.doctorId > 0)
      rx.doctorId = this.doctorId
    this.pmsSvc.SearchPrescription(rx).subscribe({
      next: (res: PrescriptionVM[]) => {
        // if (res)
        //   if (res.length > 0)
        this.Prescription = res;
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  ViewRx(Id: number) {
    this.visitDialog = this.dialog.open(PrescriptionComponent, {
      width: '1200px', height: '850px',
      data: { rxId: Id, isView: true }
    })
    this.visitDialog.afterClosed()
      .subscribe((res: any) => {
        this.ngOnInit()
      }
      );
  }
  openReportInNewPage(base64Path: string) {
    this.catSvc.generatePDF(base64Path)
  }
}

