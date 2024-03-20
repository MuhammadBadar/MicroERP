import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { DoctorVM } from '../../Models/DoctorVM';
import { PMSService } from '../../pms.service';
import { DoctorComponent } from '../doctor/doctor.component';
import { CreateUserDialogComponent } from 'src/app/views/security/manage-user/manage-user-dialog/create-user-dialog/create-user-dialog.component';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';
import { Roles } from 'src/app/views/catalog/Models/Enums/Roles';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {
  isReadOnly: boolean = false
  @ViewChild('Form', { static: true }) filterForm!: NgForm;
  displayedColumns: string[] = ['doctorName', 'email', 'startTime', 'defApptDur', 'gender', 'contactNo', 'city', 'area', 'houseNo', 'address', 'isActive', 'actions'];
  dataSource: any;
  dialogRef: any
  filterVal: DoctorVM = new DoctorVM();
  doctors!: DoctorVM[]
  constructor(private route: Router,
    public catSvc: CatalogService,
    private pmsSvc: PMSService,
    public dialog: MatDialog) {
    this.filterVal = new DoctorVM()
  }
  ngOnInit() {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Doctors)
    this.GetDoctor()
  }
  GetDoctor() {
    var value = new DoctorVM
    value.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchDoctor(value).subscribe({
      next: (res: DoctorVM[]) => {
        // this.Doctor = res;
        this.doctors = res
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  GetDoctorForEdit(id: number) {
    this.dialogRef = this.dialog.open(DoctorComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '500px'
      , data: { docId: id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(DoctorComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '500px'
      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  ResetGrid() {
    this.filterForm.reset()
    this.GetDoctor()
  }
  OpenUserDialog(row: DoctorVM) {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '500px'
      , data: { docId: row.id, isDialog: true, roleId: Roles.Doctor, moduleId: Modules.PMS, dialogTitle: row.doctorName }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
}

