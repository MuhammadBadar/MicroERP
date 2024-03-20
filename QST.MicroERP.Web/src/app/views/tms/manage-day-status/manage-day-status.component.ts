import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { UserTaskVM } from '../../tms/Models/UserTaskVM';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from '../../catalog/catalog.service';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AttendanceService } from '../../attendance/attendance.service';
import { Claims } from '../../tms/Models/Enums/Claims'
import { EnumTypes } from '../../../enums/enumTypes';
import { ScheduleService } from '../../schedule/schedule.serivce';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { TaskService } from '../../tms/task.service';
import { TaskVM } from '../Models/task-vm';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-manage-day-status',
  templateUrl: './manage-day-status.component.html',
  styleUrls: ['./manage-day-status.component.css'],
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class ManageDayStatusComponent implements OnInit {
  displayedColumns: string[] = [
    'taskTitle',
    'workTime',
    'claim',
    'comments',
  ];
  fomattedDate: string
  dueSps: number = 0
  totalWorkTime: number
  selectedTask: UserTaskVM;
  hide = true;
  dataSource: any;
  pat: TaskVM[];
  _userTasks: UserTaskVM[];
  Claim: SettingsVM[] = [];
  proccessing: boolean;
  Add: boolean;
  Edit: boolean;
  @ViewChild('UserTaskForm', { static: true }) UserTaskForm: NgForm;
  isLoading: boolean = false
  dialogrefe: any;
  disableSubmitBtn: boolean = true
  constructor(
    private datePipe: DatePipe,
    private cdref: ChangeDetectorRef,
    private injector: Injector,
    public tmsSvc: TaskService,
    public attSvc: AttendanceService,
    private catSvc: CatalogService,
    private schSvc: ScheduleService,
    private storeSvc: StorageService,
    private route: Router,
  ) // public dialog: MatDialog,
  {
    this.selectedTask = new UserTaskVM();
    this.selectedTask.claimId = 0;
    // this.IsChecked = false;
    this.dialogrefe = this.injector.get(MatDialogRef, null);
  }
  ngOnInit(): void {
    this.GetUserTask();
    this.GetEnumValues(EnumTypes.TaskClaims);
    this.getDueSPs()
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  getDueSPs() {
    this.schSvc.GetDueSps(this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)).subscribe({
      next: (value: any) => {
        this.dueSps = parseFloat(value.toFixed(2));
      }, error: () => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  GetEnumValues(etype: EnumTypes) {
    var Settings = new SettingsVM();
    Settings.enumTypeId = etype;
    this.catSvc.SearchSettings(Settings).subscribe(
      (res: SettingsVM[]) => {
        this.Claim = res
      },
      (err: any) => {
        alert('Error');
      }
    );
  }
  EditUserTask(task: UserTaskVM) {
    this.selectedTask.selectedTask = false;
    task.selectedTask = true;
    this.selectedTask = task;
    this.selectedTask.isActive = true;
  }
  UpdateUserTask() {
    debugger;
    const controls = this.UserTaskForm.controls;
    console.warn(controls)
    // for (const name in controls) {
    //   if (controls[name].value == "")
    //     this.UserTaskForm.controls[name].setErrors({ "incorrect": true })
    // }
    if (!this.UserTaskForm.invalid) {
      this.isLoading = true
      this.proccessing = true;
      this._userTasks.forEach(element => {
        element.isDayEnded = true;
        element.dayEndTime = this.catSvc.setDate(new Date)
      });
      this.tmsSvc.UpdateUsertasks(this._userTasks, true).subscribe({
        next: (res) => {
          this.isLoading = false
          this.catSvc.SuccessMsgBar('Day Successfully Ended!', 5000);
          this.attSvc.MarkUserOutTime();
          this.dialogrefe.close();
          this.Add = true;
          this.Edit = false;
          this.proccessing = false;
          localStorage.clear();
          this.route.navigate(['/secLogin']);
        },
        error: (e) => {
          this.isLoading = false
          console.warn(e);
          this.catSvc.ErrorMsgBar('Error Occurred', 5000);
          this.proccessing = false;
        },
      });
    } else
      this.catSvc.ErrorMsgBar("Please fill all Required Fields!", 6000)
  }
  GetUserTask() {
    this.isLoading = true
    var uTasks = new UserTaskVM
    uTasks.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    uTasks.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    uTasks.isDayEnded = false
    this.tmsSvc.SearchUsertask(uTasks).subscribe({
      next: (value: UserTaskVM[]) => {
        this.isLoading = false
        this._userTasks = value;
        this.dataSource = new MatTableDataSource(this._userTasks);
        this._userTasks.forEach(element => {
          element.workTime = 0
          if (element.lastClaimId > 0)
            element.claimId = element.lastClaimId
          else
            element.claimId = Claims.Claim_0Per
        });
        if (this._userTasks != undefined && this._userTasks.length > 0) 
          this.fomattedDate = this.datePipe.transform(this._userTasks[0].date, 'EEEE, MMMM d, yyyy');
      },
      error: (err) => {
        this.isLoading = false
        console.warn(err)
        this.catSvc.ErrorMsgBar('Error Occurred', 5000);
      },
    });
  }
  Refresh() {
    this.GetUserTask();
    this.selectedTask = new UserTaskVM();
    this.selectedTask.isActive = true;
    this.Add = true;
    this.Edit = false;
  }
  getTotalWorkTime() {
    this.totalWorkTime = this._userTasks?.map(t => +t.workTime).reduce((acc, value) => acc + value, 0);
    if (this.totalWorkTime >= this.dueSps)
      this.disableSubmitBtn = false
    else
      this.disableSubmitBtn = true
    return this.totalWorkTime;
  }
}
