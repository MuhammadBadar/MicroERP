import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { UserVM } from '../models/user-vm';
import { SecurityService } from '../security.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CreateUserDialogComponent } from './manage-user-dialog/create-user-dialog/create-user-dialog.component';
import Swal from 'sweetalert2';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CatalogService } from '../../catalog/catalog.service';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css']
})
export class ManageUserComponent implements OnInit {
  dataSource: any;
  dialogRef: any
  displayedColumns = ['userName', 'role', 'email', 'supervisor', 'phoneNumber',
    'action'];
  users?: UserVM[];
  supervisorId: string = null
  selectedValue: UserVM
  supervisors: UserVM[] = []
  isReadOnly: boolean = false
  isLoading: boolean = false
  @ViewChild('supervisorDialog') supervisorDialog: TemplateRef<any>;
  constructor(
    private secSvc: SecurityService,
    private snack: MatSnackBar,
    private aRoute: ActivatedRoute,
    private route: Router,
    private storeSvc: StorageService,
    private catSvc: CatalogService,
    public dialog: MatDialog) {
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ManageUser)
    this.GetUsers()
  }
  GetUsers() {
    this.isLoading = true
    var user = new UserVM
    user.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    //user.id = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    //user.includeSubordinatesData = true
    this.secSvc.SearchUser(user).subscribe({
      next: (res: UserVM[]) => {
        this.users = res;
        this.dataSource = new MatTableDataSource(this.users);
        this.isLoading = false
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        this.isLoading = false
      }
    })
  }
  GetSupervisors() {
    this.isLoading = true
    var user = new UserVM
    user.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.secSvc.SearchUser(user).subscribe({
      next: (res: UserVM[]) => {
        this.supervisors = res;
        this.supervisors = this.supervisors.filter(x => x.id != this.selectedValue.id)
        this.isLoading = false
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        this.isLoading = false
      }
    })
  }
  EditUser(user: any) {
    this.route.navigate(['/security/userRegistration'], {
      queryParams: {
        id: user.id
      }
    });
  }
  DeleteUser(id: any) {
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
        this.secSvc.DeleteUser(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'User has been deleted.',
              'success'
            )
            this.ngOnInit();
          }, error: (e) => {
            this.snack.open('Error Occured!', 'OK', { duration: 4000 })
          }
        })
      }
    })
  }
  UserEditDialog(user: any) {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '460px'
      , data: { id: user.id, isDialog: true }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  UserCreateDialog() {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '460px'
      , data: { isDialog: true }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  AssignSupervisor() {
    this.isLoading = true
    this.selectedValue.supervisorId = this.supervisorId
    this.secSvc.UpdateUser(this.selectedValue).subscribe({
      next: () => {
        this.isLoading = false
        this.supervisorId = null
        this.catSvc.SuccessMsgBar("Supervisor Successfully Assigned")
        this.dialogRef.close();
      }, error: () => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  SupervisorDilaog(user) {
    if (user.supervisorId)
      this.supervisorId = user.supervisorId
    this.dialogRef = this.dialog.open(this.supervisorDialog, {
      width: '700px'
    });
    this.selectedValue = user
    this.GetSupervisors()
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
}