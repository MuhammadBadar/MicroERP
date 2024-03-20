import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AttendanceVM } from '../Models/AttendanceVM';
import { MatDialog } from '@angular/material/dialog';
import { SecurityService } from '../../security/security.service';
import { CatalogService } from '../../catalog/catalog.service';
import { UserVM } from '../../security/models/user-vm';
import { AttendanceService } from '../attendance.service';
import { UserTaskVM } from '../../tms/Models/UserTaskVM';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../../enums/enumTypes';
import { ManageTaskComponent } from '../../tms/manage-task/manage-task.component';
import { TaskVM } from '../../lms/Models/TaskVM';
import { TaskService } from '../../tms/task.service';
import { Statuses } from '../../tms/models/Enums/Statuses';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-attendance-rpt',
  templateUrl: './attendance-detail-rpt.component.html',
  styleUrls: ['./attendance-detail-rpt.component.css']
})
export class AttendanceDetailRptComponent implements OnInit {
  displayedColumns: string[] = ['day', 'userName', 'dayEndandStart',
    'inTimeOutTime', 'scheduleTime', 'late', 'dueSps', 'todaysTargets',
    'claimedSPs', 'TaskScore', 'approvedClaim', 'claimErrPer', 'finalScore', 'extraTime'];
  maxDate = new Date
  selectedAttendance: AttendanceVM;
  dataSource: any
  attData: AttendanceVM[] = [];
  users: UserVM[] = []
  selectedUserTask: UserTaskVM
  dialogRef: any;
  claims: SettingsVM[]
  isReadOnly: boolean = false;
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
    this.isReadOnly = this.catSvc.getPermission(RouteIds.AttDetailReport)
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
    this.selectedUserTask.reviewedBy = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
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
  openTaskDialog(uTask) {
    var task = new TaskVM
    task.id = uTask.taskId
    task.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
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
  isStalled(task: UserTaskVM) {
    if (task.statusId == Statuses.Stalled)
      return true;
    else return false;
  }
}


