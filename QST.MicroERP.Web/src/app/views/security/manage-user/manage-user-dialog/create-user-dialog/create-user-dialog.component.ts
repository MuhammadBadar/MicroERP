

import { Component, Inject, Injector, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
//import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators'
import Swal from 'sweetalert2';
import { SecurityService } from '../../../security.service';
import { UserVM } from '../../../models/user-vm';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { RoleVM } from '../../../models/role-vm';
import { Location } from '@angular/common';
import { DoctorVM } from 'src/app/views/pms/Models/DoctorVM';
import { PMSService } from 'src/app/views/pms/pms.service';
import { ClientsVM } from 'src/app/views/catalog/Models/ClientsVM';
import { StaffVM } from 'src/app/views/pms/Models/StaffVM';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';

@Component({
  selector: 'app-create-user-dialog',
  templateUrl: './create-user-dialog.component.html',
  styleUrls: ['./create-user-dialog.component.css']
})
export class CreateUserDialogComponent implements OnInit {
  @ViewChild('userForm', { static: true }) userForm?: NgForm;
  isReadonly: boolean = false
  messagebox: boolean = false;
  message: any;
  success?: boolean;
  error: string = '';
  submitted: boolean = false;
  user: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  UserId: string = '';
  durationInSeconds = 3;
  proccessing: boolean | undefined;
  hide = true;
  cPHide = true
  users?: UserVM[];
  selectedUser: UserVM;
  getbyIduser?: UserVM;
  dialogTitle: string;
  action?: string;
  Roles?: SettingsVM[]
  dialogData: any;
  _dialogRef: any
  isDialog: boolean = false
  selectedDoctor: DoctorVM
  docId: number
  cltId: number = 0
  roleId: number = 0
  moduleId: number
  selectedClient: ClientsVM
  selectedStaff: StaffVM
  staffId: number
  isLoading: boolean = false
  constructor(
    private location: Location,
    private catSvc: CatalogService,
    private pmsSvc: PMSService,
    private injector: Injector,
    private storeSvc: StorageService,
    public securityService: SecurityService,
    private route: ActivatedRoute,
  ) {
    this._dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedUser = new UserVM();
    this.selectedDoctor = new DoctorVM
    this.selectedClient = new ClientsVM
  }
  ngOnInit() {
    this.isLoading = true
    this.selectedUser = new UserVM;
    this.route.queryParams.subscribe(params => {
      this.UserId = params['id'];
    });

    if (this.dialogData != null) {
      if (this.dialogData.id != undefined)
        this.UserId = this.dialogData.id
      if (this.dialogData.clt != undefined) {
        this.selectedClient = this.dialogData.clt
        // this.isReadonly =true
      }
      if (this.dialogData.docId != undefined) {
        this.docId = this.dialogData.docId
        this.GetDoctorById()
      }
      if (this.dialogData.staffId != undefined) {
        this.staffId = this.dialogData.staffId
        this.GetStaffById()
      }
      if (this.dialogData.isDialog != undefined)
        this.isDialog = this.dialogData.isDialog
      if (this.dialogData.dialogTitle != undefined)
        this.dialogTitle = this.dialogData.dialogTitle
      if (this.dialogData.roleId != null)
        this.roleId = this.dialogData.roleId
    }
    this.GetRole();
    if (this.UserId != null) {
      this.Edit = true;
      this.Add = false;
      this.GetUserById();
    }
    this.isLoading = false
  }
  GetRole() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Roles
    if (this.roleId == 0)
      stng.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Roles = res;
        if (this.roleId > 0) {
          var _role = this.Roles.find(x => x.id == this.roleId)
          if (_role) {
            this.selectedUser.roleId = _role.id
            this.isReadonly = true
          }
        }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetDoctorById() {
    if (this.docId > 0) {
      var doc = new DoctorVM
      doc.id = this.docId
      doc.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      this.pmsSvc.SearchDoctor(doc).subscribe({
        next: (res: DoctorVM[]) => {
          if (res)
            if (res.length > 0) {
              this.selectedDoctor = res[0]
              if (this.selectedDoctor.userId != null && this.selectedDoctor.userId != "") {
                this.UserId = this.selectedDoctor.userId
                this.GetUserById()
              }
            }
        }, error: (e) => {
          this.catSvc.ErrorMsgBar()
        }
      })
    }
  }
  GetStaffById() {
    if (this.staffId > 0) {
      var staff = new StaffVM
      staff.id = this.staffId
      staff.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      this.pmsSvc.SearchStaff(staff).subscribe({
        next: (res: StaffVM[]) => {
          if (res)
            if (res.length > 0) {
              this.selectedStaff = res[0]
              if (this.selectedStaff.userId != null && this.selectedStaff.userId != "") {
                this.UserId = this.selectedStaff.userId
                this.GetUserById()
              }
            }
        }, error: (e) => {
          this.catSvc.ErrorMsgBar()
        }
      })
    }
  }
  ConfirmPassword() {
    this.userForm.controls['confirmPassword'].setErrors(null);
    if (this.selectedUser.userPassword && this.selectedUser.confirmPassword)
      if (this.selectedUser.userPassword != this.selectedUser.confirmPassword)
        this.userForm.controls['confirmPassword'].setErrors({ "passwordMismatch": true });
  }
  OnSelectRole(role: SettingsVM) {
    this.selectedUser.role = role.name
  }
  UpdateDoctor() {
    this.isLoading = true
    this.pmsSvc.UpdateDoctor(this.selectedDoctor).subscribe({
      next: (res: DoctorVM) => {
        this.isLoading = false
      }, error: (e) => {
        console.warn(e)
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  UpdateSatff() {
    this.isLoading = true
    this.pmsSvc.UpdateStaff(this.selectedStaff).subscribe({
      next: (res: StaffVM) => {
        this.isLoading = false
      }, error: (e) => {
        console.warn(e)
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  UpdateClient() {
    this.isLoading = true
    this.catSvc.UpdateClients(this.selectedClient).subscribe({
      next: (res: ClientsVM) => {
        this.isLoading = false
      }, error: (e) => {
        console.warn(e)
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  GetUserById() {
    this.selectedUser.id = this.UserId
    // var cltId = +localStorage.getItem("ClientId")
    // if (cltId > 0)
    //   this.selectedUser.clientId = cltId
    this.securityService.SearchUserById(this.UserId).subscribe({
      next: (res: UserVM) => {
        this.getbyIduser = res;
        this.selectedUser = this.getbyIduser
        this.selectedUser.confirmPassword = this.selectedUser.userPassword
        this.Edit = true
        this.Add = false
      }, error: (e) => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  GetUserByClt() {
    var user = new UserVM
    user.clientId = this.selectedUser.clientId
    this.securityService.SearchUser(user).subscribe({
      next: (res: UserVM[]) => {
        if (res)
          if (res.length > 0) {
            this.Edit = true
            this.Add = false
            this.selectedUser = res[0]
          }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  SaveUser() {
    if (this.selectedUser.userPassword === this.selectedUser.confirmPassword) {
      if (this.selectedUser.roleId == undefined || this.selectedUser.roleId == 0)
        this.userForm.controls["Role"].setErrors({ "incorrect": true })
      const controls = this.userForm.controls;
      if (this.cltId == 0)
        this.selectedUser.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      this.selectedUser.moduleId = this.moduleId
      if (this.userForm.invalid) {
        for (const name in controls) {
          if (controls[name].invalid) {
            this.catSvc.ErrorMsgBar(`  ${name} is Required`, 6000)
            break
          }
        }
      } else {
        this.isLoading = true
        if (this.selectedUser.moduleId == null)
          this.selectedUser.moduleId = 0
        this.proccessing = true
        this.selectedUser.passwordHash = this.selectedUser.userPassword
        // this.selectedUser.userName = this.selectedUser.email
        if (this.Edit) {
          this.PutUser();
        } else {
          this.securityService.SaveUser(this.selectedUser).subscribe({
            next: async (data: any) => {
              this.isLoading = false
              if (data.result.succeeded == true) {
                this.messagebox = false;
                Swal.fire({
                  icon: 'success',
                  position: 'center',
                  text: 'User Registered Successfully',
                  background: "#FFFFFF",
                  confirmButtonColor: "#000000"
                })
                if (this.docId > 0) {
                  this.selectedDoctor.userId = data.userId
                  this.UpdateDoctor()
                }
                if (this.staffId > 0) {
                  this.selectedStaff.userId = data.userId
                  this.UpdateSatff()
                }
                if (this.selectedClient.id > 0) {
                  this.selectedClient.userId = data.userId
                  this.UpdateClient()
                }
                if (this.dialogTitle) {
                  this.Edit = true
                  this.Add = false
                }
                else
                  this.ngOnInit();
              }
              else {
                this.messagebox = true;
                this.message = data.result.errors
              }
            }, error: (err) => {
              this.isLoading = false
              console.warn(err)
              if (err.status == 0) {
                Swal.fire({
                  icon: 'error',
                  title: 'Oops...',
                  text: 'Something went wrong!',
                  footer: 'Please check your Internet Connection'
                })
              }
              else {
                Swal.fire({
                  icon: 'warning',
                  title: 'Oops...',
                  text: 'Something went wrong!',
                })
              }
            }
          })
        }
      }
    } else {
      this.userForm.controls['confirmPassword'].setErrors({ "passwordMismatch": true });
      this.isLoading = false
    }
  }
  PutUser() {
    this.securityService.UpdateUser(this.selectedUser).subscribe({
      next: (data: any) => {
        this.isLoading = false
        if (data.result.succeeded == true) {
          this.messagebox = false;
          Swal.fire({
            icon: 'success',
            position: 'center',
            text: 'User Successfully Updated',
            background: "#FFFFFF",
            confirmButtonColor: "#000000"
          })
          if (this.docId > 0) {
            this.selectedDoctor.userId = data.userId
            this.UpdateDoctor()
          }
          if (this.staffId > 0) {
            this.selectedStaff.userId = data.userId
            this.UpdateSatff()
          }
          if (this.selectedClient.id > 0) {
            this.selectedClient.userId = data.userId
            this.UpdateClient()
          }
        }
        else {
          this.messagebox = true;
          this.message = data.result.errors
        }
      }, error: (err) => {
        this.isLoading = false
        if (err.status == 0) {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: 'Please check your Internet Connection'
          })
        }
        else {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
          })
        }
      }
    })
  }
  Back() {
    this.location.back()
  }
}
