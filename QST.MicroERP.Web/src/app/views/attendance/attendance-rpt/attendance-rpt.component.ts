import { Component, Injector, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AttendanceVM } from '../Models/AttendanceVM';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LMSService } from '../../lms/lms.service';
import { SecurityService } from '../../security/security.service';
import { CatalogService } from '../../catalog/catalog.service';
import { UserVM } from '../../security/models/user-vm';
import { AttendanceService } from '../attendance.service';
import { UserTaskVM } from '../../tms/Models/UserTaskVM';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../../enums/enumTypes';
import { ManageReportDetailComponent } from '../manage-report-detail/manage-report-detail.component';
import { MatTableDataSource } from '@angular/material/table';
import { ManageTaskComponent } from '../../tms/manage-task/manage-task.component';
import { TaskService } from '../../tms/task.service';
import { TaskVM } from '../../tms/Models/task-vm';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-attendance-rpt',
  templateUrl: './attendance-rpt.component.html',
  styleUrls: ['./attendance-rpt.component.css']
})
export class AttendanceRptComponent implements OnInit {
  displayedColumns: string[] = ['day', 'userName', 'dayEndandStart',
    'todaysTargets', 'TaskScore', 'extraTime', 'action'];
  maxDate = new Date
  selectedAttendance: AttendanceVM;
  dataSource: any
  attData: AttendanceVM[] = [];
  users: UserVM[] = []
  selectedUserTask: UserTaskVM
  dialogRef: any;
  claims: SettingsVM[]
  isReadOnly: boolean = false;
  detailDialogRef: any
  dialogData: any;
  isLoading: boolean = false
  @ViewChild('approvedClaimDialog') approvedClaimDialog: TemplateRef<any>;
  constructor(
    public tmsSvc: TaskService,
    public dialog: MatDialog,
    public secSvc: SecurityService,
    private catSvc: CatalogService,
    public attSvc: AttendanceService,
    private storeSvc: StorageService
  ) {
    this.selectedUserTask = new UserTaskVM
    this.selectedAttendance = new AttendanceVM
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.AttSummaryReport)
    if (this.dialogData)
      this.selectedAttendance = new AttendanceVM();
    this.secSvc.GetUsers();
    this.GetClaim()
    this.SearchAttReport()
  }
  GetClaim() {
    var Settings = new SettingsVM();
    Settings.enumTypeId = EnumTypes.TaskClaims;
    this.catSvc.SearchSettings(Settings).subscribe(
      (res: SettingsVM[]) => {
        this.claims = res
      },
      (err: any) => {
        this.catSvc.ErrorMsgBar()
      }
    );
  }
  SearchAttReport() {
    this.isLoading = true
    var att = new AttendanceVM
    att.fromDate = this.catSvc.setDate(this.selectedAttendance.fromDate)
    att.toDate = this.catSvc.setDate(this.selectedAttendance.toDate)
    att.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    if (this.selectedAttendance.userId == null || this.selectedAttendance.userId == "") {
      att.includeSubordinatesData = true
      att.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    } else
      att.userId = this.selectedAttendance.userId
    this.attSvc.getAttendanceRpt(att).subscribe({
      next: (res: AttendanceVM[]) => {
        this.isLoading = false
        console.warn(res)
        this.attData = res
        this.dataSource = res
      }, error: () => {
        this.isLoading = false
        debugger
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  Refresh() {
    this.selectedAttendance = new AttendanceVM
    this.SearchAttReport();
  }
  convertToList(inputStr: string) {
    return inputStr ? inputStr.split(',').map(item => item.trim()) : [];
  }
  calculateTimeSum(column) {
    if (this.dataSource != undefined)
      return this.catSvc.calculateTimeSum(column, this.dataSource)
    else
      return null
  }
  calculateFraction(column) {
    if (this.dataSource != undefined)
      return this.catSvc.calculateFractionSum(column, this.dataSource)
    else return null
  }
  calculatePercentageSum(column) {
    if (this.dataSource != undefined)
      return this.catSvc.calculatePercentageSum(column, this.dataSource)
    else return null
  }
  calculateNumericSum(column) {
    if (this.dataSource != undefined)
      return this.catSvc.calculateNumericSum(column, this.dataSource)
    else return null
  }
  GetTotalClaimError() {
    return this.attData?.map(t => +t.claimErrorPer).reduce((acc, value) => acc + value, 0);
  }
  submitApprovedClaim() {
    this.isLoading = true
    var tasks = new Array()
    tasks.push(this.selectedUserTask)
    this.tmsSvc.UpdateUsertasks(tasks, false).subscribe({
      next: () => {
        this.isLoading = false
        this.catSvc.SuccessfullyUpdateMsg();
        this.dialogRef.close()
      }, error: (e) => {
        this.isLoading = false
        console.warn(e)
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  approvedClaimDilaog(task) {
    this.selectedUserTask = task
    if (this.selectedUserTask.approvedClaimId > 0) { } else
      this.selectedUserTask.approvedClaimId = this.selectedUserTask.claimId
    this.dialogRef = this.dialog.open(this.approvedClaimDialog, {
      width: '700px'
    });
    this.dialogRef.afterClosed().subscribe({
      next: () => {
        this.SearchAttReport()
      }
    })
  }
  openTaskDialog(uTask) {
    var task = new TaskVM
    task.id = uTask.taskId
    this.tmsSvc.SearchTask(task).subscribe({
      next: (res) => {
        if (res && res.length > 0)
          task = res[0]
        this.dialogRef = this.dialog.open(ManageTaskComponent, {
          width: '1000px', height: '600px', data: { taskId: task.id, isDialog: true }
        });
        this.dialogRef.afterClosed().subscribe({
          next: () => {
            this.SearchAttReport()
          }
        })
      }, error: (err) => {
        console.warn(err)
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  viewDetails(row): void {
    this.detailDialogRef = this.dialog.open(ManageReportDetailComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '500px'
      , data: { attendance: row }
    });
    // this.dialogRef.afterClosed()
    //   .subscribe((res) => {
    //     this.ngOnInit()
    //   });
  }

  updateFilter(event) {
    if (this.attData.length > 0) {
      const val = event.target.value.toLowerCase();
      var columns = Object.keys(this.attData[0]);
      columns.splice(columns.length - 1);
      if (!columns.length)
        return;
      const rows = this.attData.filter(function (d) {
        for (let i = 0; i <= columns.length; i++) {
          let column = columns[i];
          if (d[column] && d[column].toString().toLowerCase().indexOf(val) > -1) {
            return true;
          }
        }
        return false;
      });
      this.dataSource = rows;
    }
  }
}