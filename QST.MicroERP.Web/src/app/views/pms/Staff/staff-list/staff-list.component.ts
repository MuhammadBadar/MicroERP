import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { StaffVM } from '../../Models/StaffVM';
import { PMSService } from '../../pms.service';
import { CreateUserDialogComponent } from 'src/app/views/security/manage-user/manage-user-dialog/create-user-dialog/create-user-dialog.component';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { ManageStaffComponent } from '../manage-staff/manage-staff.component';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';
import { Roles } from 'src/app/views/catalog/Models/Enums/Roles';

@Component({
  selector: 'app-staff-list',
  templateUrl: './staff-list.component.html',
  styleUrls: ['./staff-list.component.css']
})
export class StaffListComponent implements OnInit {
  isReadOnly: boolean = false
  @ViewChild('Form', { static: true }) filterForm!: NgForm;
  displayedColumns: string[] = ['StaffName', 'email', 'gender', 'contactNo', 'city', 'area', 'houseNo', 'address', 'isActive', 'actions'];
  dataSource: any;
  dialogRef: any
  filterVal: StaffVM = new StaffVM();
  Staffs!: StaffVM[]
  constructor(private route: Router,
    public catSvc: CatalogService,
    private pmsSvc: PMSService,
    public dialog: MatDialog) {
    this.filterVal = new StaffVM()
  }
  ngOnInit() {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Staffs)
    this.GetStaff()
  }
  GetStaff() {
    var value = new StaffVM
    value.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchStaff(value).subscribe({
      next: (res: StaffVM[]) => {
        // this.Staff = res;
        this.Staffs = res
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  GetStaffForEdit(id: number) {
    this.dialogRef = this.dialog.open(ManageStaffComponent, {
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
    this.dialogRef = this.dialog.open(ManageStaffComponent, {
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
    this.GetStaff()
  }
  OpenUserDialog(row: StaffVM) {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '450px'
      , data: { staffId: row.id, isDialog: true, roleId:Roles.Staff , moduleId: Modules.PMS, dialogTitle: row.name }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
}

