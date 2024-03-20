import { SecurityService } from './../security.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LoginVM } from '../models/LoginVM';
import { CatalogService } from '../../catalog/catalog.service';
import { MatDialog } from '@angular/material/dialog';
import { ManageUsertaskComponent } from '../../tms/manage-usertask/manage-usertask.component';
import { AttendanceService } from '../../attendance/attendance.service';
import { ManageDayStatusComponent } from '../../tms/manage-day-status/manage-day-status.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isRequesting: boolean = false
  info = new LoginVM;
  hide = true;
  dialogRef: any
  login: LoginVM = new LoginVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  constructor(
    public secSvc: SecurityService,
    private route: Router,
    private catSvc: CatalogService,
    private attSvc: AttendanceService,
    public dialog: MatDialog,
  ) {
  }
  ngOnInit(): void {
  }
  OpenUserDialog(data) {
    this.dialogRef = this.dialog.open(ManageUsertaskComponent, {
      width: '1200px',
      height: '550px',
      data: { data: data, userId: data.id }
    });
    this.dialogRef.afterClosed().subscribe({
      next: (res) => {
        console.warn(res)
        if (res)
          if (res.markInTime)
            this.attSvc.MarkUserInTime()
      }
    })
  }
  Login() {
    if (!this.userForm.invalid) {
      this.isRequesting = true
      this.secSvc.Login(this.login).subscribe({
        next: (data: any) => {
          this.isRequesting = false
          if (data?.result?.succeeded == true) {
            this.catSvc.SetProject(data)
            if (data?.showDayStartDialog == true) {
              this.OpenUserDialog(data);
            }
            else if (data?.showDayEndDialog) {
              this.catSvc.ErrorMsgBar("Please End your Previous day to Continue... ", 6000)
              this.dialogRef = this.dialog.open(ManageDayStatusComponent, {
                width: '1200px',
                height: '500px',
                hasBackdrop: false
              });
            }
            else {
              this.catSvc.setToken(data.token)
              //if (data.shouldMarkAttendance)
              this.attSvc.MarkUserInTime()
            }
          }
          else {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Wrong credentials! ',
              footer: 'Invalid Password ',
              confirmButtonColor: "#000000"
            })
          }
        }, error: (err) => {
          this.isRequesting = false
          if (err.status == 400) {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Wrong credentials! ',
              footer: 'Invalid Email ',
              confirmButtonColor: "#000000"
            })
            console.warn(err)
          }
          else if (err.status == 0) {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Something went wrong! ',
              footer: 'Please Check your Connection',
              confirmButtonColor: "#000000"
            })
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Error Occurred ',
              confirmButtonColor: "#000000"
            })
          }
        }
      })
    }
  }
}
