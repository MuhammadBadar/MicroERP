import { Component, Injector, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AttendanceVM } from '../Models/AttendanceVM';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { UserVM } from '../../security/models/user-vm';
import { UserTaskVM } from '../../tms/Models/UserTaskVM';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { AttendanceService } from '../attendance.service';
import { CatalogService } from '../../catalog/catalog.service';

@Component({
  selector: 'app-manage-report-detail',
  templateUrl: './manage-report-detail.component.html',
  styleUrls: ['./manage-report-detail.css']
})
export class ManageReportDetailComponent implements OnInit {

  maxDate = new Date
  selectedAttendance: AttendanceVM;
  dataSource: any
  attData: AttendanceVM[];
  users: UserVM[]
  selectedUserTask: UserTaskVM
  claims: SettingsVM[]
  hasPerms: boolean = false;
  dialogData: any
  dialogRef: any;
  @ViewChild('approvedClaimDialog') approvedClaimDialog: TemplateRef<any>;
  constructor(
    public dialog: MatDialog,
    private injector: Injector,
    private attSvc: AttendanceService,
    private catSvc: CatalogService
  ) {
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedUserTask = new UserTaskVM
    this.selectedAttendance = new AttendanceVM
  }
  ngOnInit(): void {
    if (this.dialogData) {
      this.selectedAttendance = this.dialogData.attendance
      // this.SearchAttReport()
    }
  }
  closeDialog(): void {
    this.dialogRef.close();
  }
  SearchAttReport() {
    this.attSvc.getAttendanceRpt(this.selectedAttendance).subscribe({
      next: (res: AttendanceVM[]) => {
        console.warn(res)
        this.attData = res
        this.dataSource = res
      }, error: () => {
        debugger
        this.catSvc.ErrorMsgBar("Error Occurred")
      }
    })
  }
  convertToList(inputStr: string) {
    return inputStr ? inputStr.split(',').map(item => item.trim()) : [];
  }
  getFormattedTaskScore(task: any): string {
    return this.attSvc.getFormattedTaskScore(task)
  }

  getFormattedClaimedHours(task: any): string {
    return this.attSvc.getFormattedClaimedHours(task)
  }
}