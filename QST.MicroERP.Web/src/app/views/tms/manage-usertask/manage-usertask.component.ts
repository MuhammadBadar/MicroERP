import { Component, Injector, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { UserTaskVM } from '../Models/UserTaskVM';
import { CatalogService } from '../../catalog/catalog.service';
import { MatTableDataSource } from '@angular/material/table';
import { SecurityService } from '../../security/security.service';
import { Router } from '@angular/router';
import { Statuses } from '../../tms/models/Enums/Statuses';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../../enums/enumTypes';
import { TaskVM } from '../../tms/Models/task-vm';
import { TaskService } from '../../tms/task.service';
import { NgForm } from '@angular/forms';
import { UserVM } from '../../security/models/user-vm';
import { ScheduleService } from '../../schedule/schedule.serivce';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { Claims } from '../Models/Enums/Claims';
import { DatePipe } from '@angular/common';
import { ManageEnumLineComponent } from '../../catalog/manage-enum-line/manage-enum-line.component';
@Component({
  selector: 'app-manage-usertask',
  templateUrl: './manage-usertask.component.html',
  styleUrls: ['./manage_usertask.component.css']
})
export class ManageUsertaskComponent implements OnInit {
  displayedColumns: string[] = ['Task Title', 'Priority', 'RemainingSPs', 'Assign'];
  AddMode: boolean = true;
  EditMode: boolean = false;
  dataSource: any;
  dialogData: any;
  dialogRefe: MatDialogRef<any, any>;
  stalledTaskDialogRef: any
  taskDialogRef: any
  userTasks: UserTaskVM[] = [];
  selectedItems: UserTaskVM[] = [];
  userTask: UserTaskVM;
  selectedUsertask: TaskVM;
  task: TaskVM[];
  tasks: TaskVM[] = [];
  responseData: any;
  totalSP: number = 0;
  dueSps: number = 0;
  disableSubmitButton: boolean = true;
  sumOfSps: any
  reasons: SettingsVM[] = [];
  filteredOptions: SettingsVM[] = [];
  priorities: SettingsVM[]
  users: UserVM[]
  selectedTask: TaskVM
  openedToReAssign: boolean = false
  showData: boolean = true
  userId: string
  isDayEnded: boolean
  @ViewChild('taskForm', { static: true }) taskForm: NgForm;
  @ViewChild('stalledForm', { static: true }) stalledForm: NgForm;
  @ViewChild('stalledTaskDialog') stalledTaskDialog: TemplateRef<any>;
  @ViewChild('taskDialog') taskDialog: TemplateRef<any>;
  isLoading: boolean = false
  hasPerms: boolean = false
  showLoadBtn: boolean = false
  fomattedDate: string
  constructor(
    private datePipe: DatePipe,
    public secSvc: SecurityService,
    private injector: Injector,
    public tmsSvc: TaskService,
    private catSvc: CatalogService,
    private schSvc: ScheduleService,
    private storeSvc: StorageService,
    private route: Router,
    public dialog: MatDialog,
  ) {
    this.selectedUsertask = new TaskVM();
    this.selectedTask = new TaskVM
    this.dialogRefe = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    this.hasPerms = this.catSvc.isSuperOrClientAdmin()
    if (this.dialogData) {
      this.userId = this.dialogData.userId
      this.responseData = this.dialogData.data;
      this.openedToReAssign = this.dialogData.openedToReAssign
    }
    this.secSvc.GetUsers();
    this.GetReasons()
    this.GetPrioritiess()
    this.GetTaskByUserId(this.userId);
    this.selectedTask.userId = this.userId
    this.hasTodaysTargets()
  }
  //#region Get Methods
  GetReasons() {
    var Settings = new SettingsVM();
    Settings.enumTypeId = EnumTypes.Reasons;
    Settings.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchSettings(Settings).subscribe(
      (res: SettingsVM[]) => {
        this.reasons = res
        this.filteredOptions = res
      },
      (err: any) => {
        this.catSvc.ErrorMsgBar()
      }
    );
  }
  GetPrioritiess() {
    var Settings = new SettingsVM();
    Settings.enumTypeId = EnumTypes.Priorities;
    this.catSvc.SearchSettings(Settings).subscribe(
      (res: SettingsVM[]) => {
        this.priorities = res
        this.selectedTask.priorityId = this.priorities[0].id
      },
      (err: any) => {
        this.catSvc.ErrorMsgBar()
      }
    );
  }
  GetTaskByUserId(userId: string) {
    debugger
    this.isLoading = true
    this.schSvc.GetDueSps(userId).subscribe({
      next: (value: any) => {
        this.dueSps = parseFloat(value.toFixed(2));
        this.tmsSvc.GetTaskByUserId(userId).subscribe({
          next: (res: TaskVM[]) => {
            debugger
            this.isLoading = false
            this.userTasks = []
            console.warn(res)
            this.tasks = res;
            var date = new Date().getDate();
            var find = this.tasks.find(x => new Date(x.date).getDate() == date)
            // if (find == undefined && this.openedToReAssign != true) {
            var activeTasks = this.tasks.filter(x => x.statusId != Statuses.Stalled)
            this.sumOfSps = activeTasks.map(obj => obj.remainingSPs).reduce((sum, value) => sum + value, 0);
            if ((this.sumOfSps < this.dueSps) && this.sumOfSps > 0) {
              //this.disableSubmitButton = false
              activeTasks.forEach(element => {
                element.ischecked = true
                element.isDisabled = true
                this.toggleRow(element, true)
              });
            } else
              this.disableSubmitButton = true
            var _tasks = this.tasks.filter(x => x.statusId == Statuses.InProgress || x.statusId == Statuses.ReOpen)
            _tasks.forEach(x => {
              x.ischecked = true
              x.isDisabled = true
              this.toggleRow(x, true)
            })
            this.sortTasks()
            console.warn(this.tasks)
            this.dataSource = new MatTableDataSource(this.tasks);
            if (this.tasks != undefined && this.tasks.length > 0)
              this.fomattedDate = this.datePipe.transform(new Date, 'EEEE, MMMM d, yyyy');

          },
          error: (err) => {
            this.isLoading = false
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          }
        });
      },
      error: (err) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      }
    });
  }
  //#endregion
  //#region  Stalled Task Methods
  onSelectReason(event) {
    this.selectedUsertask.reason = event.option.value.name;
  }
  onClickReason(stng) {
    this.selectedUsertask.reason = stng.name
  }
  filterReason() {
    var filterValue;
    if (this.selectedUsertask.reason !== undefined) {
      filterValue = this.selectedUsertask.reason.toString().toLowerCase();
      this.filteredOptions = this.reasons.filter(x => x.name.toString().toLowerCase().includes(filterValue))
    } else this.filteredOptions = this.reasons
    //this.filteredOptions = this.options.filter(o => o.toLowerCase().includes(filterValue));
  }
  markTaskStatus() {
    this.showLoadBtn = true
    if (this.selectedUsertask.claimId! > 0)
      this.selectedUsertask.claimId == Claims.Claim_0Per
    this.selectedUsertask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.tmsSvc.MarkTaskAsStalled(this.selectedUsertask).subscribe({
      next: (res) => {
        this.showLoadBtn = false
        if (res == true) {
          this.userTasks = this.userTasks.filter(x => x.taskId == this.selectedUsertask.id)
          this.catSvc.SuccessfullyUpdateMsg()
          this.stalledTaskDialogRef.close()
        }
        else
          this.catSvc.ErrorMsgBar()
      }, error: (e) => {
        this.showLoadBtn = false
        console.warn(e)
        this.catSvc.ErrorMsgBar();
      }
    })
  }
  openStalledTaskDilaog(task) {
    this.selectedUsertask = task
    this.stalledTaskDialogRef = this.dialog.open(this.stalledTaskDialog, {
      width: '800px'
    });
    this.stalledTaskDialogRef.afterClosed().subscribe({
      next: () => {
        var userId;
        if (this.openedToReAssign)
          userId = this.selectedTask.userId
        else
          userId = this.userId
        this.GetTaskByUserId(userId)
        this.calTotalSps()
      }
    })
  }
  OpenReasonDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: { enumTypeId: EnumTypes.Reasons, isDialog: true, clientId: +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID) }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetReasons()
      }
    })
  }
  //#endregion
  //#region Create New Task Methods
  openTaskDilaog() {
    if (!this.openedToReAssign)
      this.selectedTask.userId = this.userId
    this.taskDialogRef = this.dialog.open(this.taskDialog, {
      width: '800px'
    });
    this.taskDialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetTaskByUserId(this.userId)
        this.calTotalSps()
      }
    })
  }
  submitTask() {
    this.showLoadBtn = true
    this.selectedTask.statusId = Statuses.Open
    this.selectedTask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.tmsSvc.SaveTask(this.selectedTask).subscribe({
      next: (res) => {
        this.showLoadBtn = false
        if (res) {
          var userId;
          if (this.openedToReAssign)
            userId = this.selectedTask.userId
          else
            userId = this.userId
          this.GetTaskByUserId(userId)
          this.selectedTask = new TaskVM
          this.selectedTask.priorityId = this.priorities[0].id
          this.selectedTask.userId = userId
          this.catSvc.SuccessfullyAddMsg()
        }
        else this.catSvc.ErrorMsgBar()
      }
      , error: () => {
        this.showLoadBtn = false
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  //#endregion
  //#region Save UserTask
  Savetask() {
    if (this.userTasks.length > 0) {
      const selectedTaskIds = this.userTasks.map(task => task.taskId);
      var markAtt = false;
      if (!this.openedToReAssign)
        markAtt = true
      else
        markAtt = false
      this.isLoading = true
      this.tmsSvc.SaveUsertasks(this.userTasks, markAtt).subscribe({
        next: (value) => {
          this.isLoading = false
          if (!this.openedToReAssign)
            this.catSvc.SuccessMsgBar('Day Successfully Started');
          else
            this.catSvc.SuccessMsgBar("Successfully Updated")
          if (this.dialogRefe) {
            this.dialogRefe.close({ markInTime: true });
            this.route.navigate(['/catalog/home']);
            if (this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_TOKEN) == undefined)
              this.setCredentials()
          }
          this.dataSource.data.forEach(task => {
            if (selectedTaskIds.includes(task.id)) {
              task.ischecked = true;
            }
          });
          this.userTasks = [];
          this.totalSP = 0;
          this.disableSubmitButton = true;
        },
        error: (err) => {
          this.isLoading = false
          console.error('Error saving user tasks:', err);
          this.catSvc.ErrorMsgBar('Error Occurred', 5000);
        }
      });
    } else this.catSvc.ErrorMsgBar("Please Select some Task to start your Day")
  }
  //#endregion
  //#region Others
  hasTodaysTargets() {
    debugger
    this.tmsSvc.HasTasks(this.selectedTask.userId).subscribe({
      next: (res) => {
        debugger
        if (this.openedToReAssign) {
          if (res == false) {
            this.disableSubmitButton = true
            this.showData = false
          }
          else {
            this.disableSubmitButton = false
            this.showData = true
          }
        }
      }, error: () => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  toggleRow(row, isChecked) {
    debugger
    var searchTask = this.userTasks.find(x => x.taskId == row.id)
    if (searchTask == undefined || searchTask == null) {
      if (isChecked) {
        debugger
        this.userTask = new UserTaskVM();
        this.userTask.taskId = row.id;
        this.userTask.remainingSPs = row.remainingSPs
        this.userTask.lastClaimId = row.claimId
        if (row.approvedClaim)
          this.userTask.lastClaimId = row.approvedClaimId
        this.userTask.statusId = Statuses.InProgress
        this.userTask.userId = row.userId;
        this.userTask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
        this.userTask.date = this.catSvc.setDate(new Date());
        this.userTask.sp = parseFloat(row.sp.toFixed(2));
        this.userTask.isChecked = true;
        this.userTasks.push(this.userTask);
        this.calTotalSps()
      } else {
        const index = this.userTasks.findIndex(task => task.taskId === row.id);
        if (index !== -1) {
          this.userTasks.splice(index, 1);
          this.calTotalSps()
        }
      }
      const roundedTotalSP = parseFloat(this.totalSP.toFixed(2));
      const roundedDueSps = parseFloat(this.dueSps.toFixed(2));
      debugger
      // if (this.sumOfSps < this.dueSps && this.sumOfSps > 0)
      //   this.disableSubmitButton = false
      // else
      this.disableSubmitButton = (roundedTotalSP < roundedDueSps) || (roundedTotalSP == 0);
    }
  }
  calTotalSps() {
    this.totalSP = parseFloat(this.userTasks.reduce((total, task) => total + task.remainingSPs, 0).toFixed(2)); // Round off the total
  }
  sortTasks() {

    this.tasks.sort((a, b) => (a.ischecked === b.ischecked ? 1 : a.ischecked ? -1 : 1));
    this.tasks.sort((a, b) => {
      if (a.statusId === Statuses.Stalled && b.statusId !== Statuses.Stalled) {
        return 1;
      } else if (a.statusId !== Statuses.Stalled && b.statusId === Statuses.Stalled) {
        return -1;
      } else {
        return 0;
      }
    });
  }
  setCredentials() {
    this.catSvc.SetProject(this.responseData)
    this.catSvc.setToken(this.responseData.token)
  }
  withoutDayStart() {
    this.setCredentials()
    this.route.navigate(['/catalog/home']);
    this.dialogRefe.close({ markInTime: true });
  }
  //#endregion
}
