import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { timeStamp } from 'console';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { PatientVM } from '../../Models/PatientVM';
import { PMSService } from '../../pms.service';
import { PatientComponent } from '../patient/patient.component';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  isReadOnly: boolean = false
  @ViewChild('Form', { static: true }) filterForm!: NgForm;
  patColumns: string[] = ['patientName', 'dateOfBirth', 'gender', 'contactNo', 'city', 'area', 'houseNo', 'address', 'remarks'];
  extraColumns: string[] = []
  //patColumns: string[] = [];

  dataSource: any;
  dialogRef: any
  filterVal: PatientVM = new PatientVM();
  patients!: PatientVM[]
  fields: SettingsTypeVM[]
  constructor(private route: Router,
    public catSvc: CatalogService,
    private pmsSvc: PMSService,
    public dialog: MatDialog) {
    this.filterVal = new PatientVM()
  }
  ngOnInit() {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Patients)
    this.extraColumns = []
    this.patColumns = ['patientName', 'dateOfBirth', 'gender', 'contactNo', 'city', 'area', 'houseNo', 'address', 'remarks'];
    this.GetPatient()
    this.GetField()
  }
  GetPatient() {
    var value = new PatientVM
    value.clientId = +localStorage.getItem("ClientId")
    // value.isActive = true
    this.pmsSvc.SearchPatient(value).subscribe({
      next: (res: PatientVM[]) => {
        this.patients = res
        this.dataSource = new MatTableDataSource(this.patients);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  GetField() {
    var stng = new SettingsTypeVM
    stng.parentId = EnumTypes.PatientFields
    stng.isActive = true
    stng.clientId = +localStorage.getItem("ClientId")
    stng.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(stng).subscribe((res: SettingsTypeVM[]) => {
      this.fields = res;
      res.forEach(field => {
        if (!this.extraColumns.includes(field.name)) {
          this.extraColumns.push(field.name);
        }
      });
      this.patColumns = [...this.patColumns, ... this.extraColumns, 'isActive', 'actions']
      this.dataSource = new MatTableDataSource(this.patients);
    });
  }
  getCellValue(patient: PatientVM, columnName: string): string {
    const dynamicField = patient.ptFData.find(field => field.fieldName === columnName);
    return dynamicField ? dynamicField.fieldValue : '';
  }
  EditPatient(pat: PatientVM) {
    this.dialogRef = this.dialog.open(PatientComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '550px'
      , data: { patId: pat.id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(PatientComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '550px'
      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  ResetGrid() {
    this.filterForm.reset()
    this.GetPatient()
  }
}


