import { ScheduleService } from '../schedule.serivce';
import { Component, Injector, ViewChild, OnInit, TemplateRef } from '@angular/core';
// import { ScheduleService } from '../sch.service';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { CatalogService } from '../../catalog/catalog.service';
import { ScheduleDayVM, ScheduleVM } from '../Models/ScheduleVM';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';

import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { UserVM } from '../../security/models/user-vm';
import { SecurityService } from '../../security/security.service';
import { EnumTypes } from '../../../enums/enumTypes';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { ManageUserComponent } from '../../security/manage-user/manage-user.component';
import { RoleVM } from '../../security/models/role-vm';
import { ManageRoleComponent } from '../../security/manage-role/manage-role.component';
import { Entities } from '../models/Enums/Entities';
import { ScheduleTypes } from '../models/Enums/ScheduleTypes';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { ManageScheduleListComponent } from '../manage-schedule-list/manage-schedule-list.component';
import { ManageScheduleDayEventComponent } from '../manage-schedule-day-event/manage-schedule-day-event.component';
import { ScheduleHistoryComponent } from '../schedule-history/schedule-history.component';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
// import { ScheduleService } from '../sch.serivce';
@Component({
  selector: 'app-manage-schedule',
  templateUrl: './manage-schedule.component.html',
  styleUrls: ['./manage-schedule.component.css']
})
export class ManageScheduleComponent {
  @ViewChild('scheduleForm', { static: true }) scheduleForm!: NgForm;
  displayedColumns: string[] = [`day`, 'workTime', `scheduleDayEvents`, 'actions'];
  selectedSchedule: ScheduleVM;
  selectedScheduleDay: ScheduleDayVM
  users: UserVM[] | undefined;
  ScheduleDays: ScheduleDayVM[] = []
  dataSource: any;
  proccessing: boolean | undefined;
  AddMode: boolean = true
  EditMode: boolean = false
  user: number;
  role: number;
  FH: number;
  FWH: number;
  schId!: number
  getSchById!: ScheduleVM[];
  entities: SettingsVM[];
  schTypes: SettingsVM[];
  weekDays: SettingsVM[];
  workingTypes: SettingsVM[]
  roles: RoleVM[];
  lineAddMode: boolean = false
  lineEditMode: boolean = true
  UserId!: string
  roleDialogRef
  userDialogRef
  schEvtDialogRef
  dialogRef
  isLoading: boolean = false;
  showLoadBtn: boolean = false
  workTimeDialogRef: any
  workTime: string
  selectedScheduleType: number
  @ViewChild('workTimeDialog') workTimeDialog: TemplateRef<any>;
  selectedDaysOfWeek: number[] = [];
  isReadOnly: boolean = false
  constructor(private injector: Injector,
    private schSvc: ScheduleService,
    public catSvc: CatalogService,
    public dialog: MatDialog,
    public secSvc: SecurityService,
    private storeSvc: StorageService
  ) {
    this.selectedScheduleDay = new ScheduleDayVM
    this.selectedSchedule = new ScheduleVM
    this.user = Entities.user;
    this.role = Entities.role;
    this.FH = ScheduleTypes.FH;
    this.FWH = ScheduleTypes.FWH;
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Schedule)
    this.secSvc.GetUsers();
    this.GetSettings(EnumTypes.ScheduleType)
    this.GetSettings(EnumTypes.WorkingType)
    this.GetSettings(EnumTypes.WeekDays)
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.selectedSchedule.entityId = Entities.user
  }
  updateSelectedDays(day) {
    const index = this.selectedSchedule.dayIds.indexOf(day);
    if (index !== -1)
      this.selectedSchedule.dayIds.splice(index, 1);
    else
      this.selectedSchedule.dayIds.push(day);
  }
  GetScheduleById() {
    var sch = new ScheduleVM
    sch.id = this.schId
    sch.clientId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.schSvc.SearchSchedule(sch).subscribe({
      next: (res: ScheduleVM[]) => {
        this.getSchById = res;
        this.selectedSchedule = this.getSchById[0]
        this.selectedSchedule.scheduleDays?.forEach(element => {
          this.ScheduleDays.push(element)
        });
        this.dataSource = new MatTableDataSource(this.ScheduleDays);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
  RefreshDetail() {
    this.lineAddMode = false;
    this.lineEditMode = false;
  }
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype === EnumTypes.Entities) {
          this.entities = res;
          if (this.entities.length > 0)
            this.selectedSchedule.entityId = this.entities[0].id;
        } else if (etype === EnumTypes.ScheduleType) {
          this.schTypes = res;
          if (this.schTypes.length > 0)
            this.selectedSchedule.scheduleTypeId = this.schTypes[1].id;
        } else if (etype === EnumTypes.WeekDays) {
          this.weekDays = res;
        }
        else if (etype === EnumTypes.WorkingType) {
          this.workingTypes = res;
        }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
  OpenDayEventDialog(row: ScheduleDayVM) {
    this.schSvc.selectedScheduleDayId = row.id;
    this.schEvtDialogRef = this.dialog.open(ManageScheduleDayEventComponent, {
      width: '1200px',
      height: '550px',
      data: { isDialog: true, scheduleLine: row }
    });
    this.schEvtDialogRef.afterClosed().subscribe((res: any) => {
      this.getScheduleByUserId(this.selectedSchedule.userId);
    });
  }
  CheckScheduleTypeValidation() {
    if (this.selectedSchedule.scheduleTypeId === 0 || this.selectedSchedule.scheduleTypeId === undefined) {
      this.scheduleForm.form.controls['scheduleTypeId'].setErrors({ 'incorrect': true });
      return false;
    }
    return true;
  }
  validateFields() {
    if (this.selectedSchedule.userId == null || this.selectedSchedule.userId == undefined)
      this.scheduleForm.controls["User"].setErrors({ "required": true })
    if (this.selectedSchedule.scheduleTypeId <= 0)
      this.scheduleForm.controls["ScheduleType"].setErrors({ "required": true })
  }
  displaySchedule(sch) {
    this.selectedSchedule = sch
    this.AddMode = false
    this.EditMode = true
    this.ScheduleDays = this.selectedSchedule.scheduleDays;
    this.dataSource = new MatTableDataSource(this.ScheduleDays);
  }
  SubmitSchedule() {
    this.validateFields()
    const controls = this.scheduleForm.controls;
    if (this.scheduleForm.invalid) {
      for (const name in controls) {
        if (controls[name].hasError('required')) {
          this.catSvc.ErrorMsgBar(`Please select  ${name} `)
          return;
        }
      }
    } else {
      if (this.selectedSchedule.scheduleTypeId == ScheduleTypes.FWH && this.selectedSchedule.dayIds <= [])
        this.catSvc.ErrorMsgBar("Please select Day(s)")
      else {
        this.isLoading = true
        this.selectedSchedule.clientId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
        if (this.selectedSchedule.id > 0)
          this.UpdateSchedule()
        else
          this.schSvc.SaveSchedule(this.selectedSchedule).subscribe({
            next: (res: ScheduleVM) => {
              this.isLoading = false
              if (res.hasErrors) {
                res.resultMessages.forEach(element => {
                  this.catSvc.ErrorMsgBar(element.message);
                });
              }

              else {
                res.resultMessages.forEach(element => {
                  this.catSvc.SuccessMsgBar(element.message);
                });
                this.displaySchedule(res)
                this.proccessing = false
              }
            },
            error: (e: any) => {
              this.isLoading = false
              this.catSvc.ErrorMsgBar();
              console.warn(e);
              this.ScheduleDays = [];
              this.proccessing = false;
            },
          });
      }
    }
  }
  EditScheduleFH(schedule: ScheduleVM) {
    this.EditMode = true
    this.AddMode = false
    this.selectedSchedule = schedule
  }
  UpdateSchedule() {
    this.schSvc.UpdateSchedule(this.selectedSchedule).subscribe({
      next: (res: ScheduleVM) => {
        console.warn(res)
        this.isLoading = false
        if (res.hasErrors) {
          res.resultMessages.forEach(element => {
            this.catSvc.ErrorMsgBar(element.message);
          });
        }

        else {
          res.resultMessages.forEach(element => {
            this.catSvc.SuccessMsgBar(element.message);
          });
          this.displaySchedule(res)
          this.proccessing = false
        }
      }, error: (e: any) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar()
        console.warn(e);
        this.ScheduleDays = []
        this.proccessing = false
      }
    })
  }
  Refresh() {
    this.ngOnInit();
    // this.getScheduleByUserId;
    this.selectedSchedule = new ScheduleVM
    this.EditMode = false
    this.AddMode = true
    this.ScheduleDays = []
    this.dataSource = []
    this.selectedSchedule.isActive = true;
    this.selectedSchedule.entityId = this.user;
  }
  SetDates() {
    this.selectedSchedule.startDate = moment(this.selectedSchedule.startDate).toDate()
    this.selectedSchedule.startDate = new Date(Date.UTC(this.selectedSchedule.startDate.getFullYear(), this.selectedSchedule.startDate.getMonth(), this.selectedSchedule.startDate.getDate()))
  }
  getScheduleByUserId(val: any) {
    this.isLoading = true
    this.UserId = val;
    this.schSvc.GetScheduleByUserId(this.UserId).subscribe({
      next: (val: ScheduleVM) => {
        this.isLoading = false
        if (!val.hasErrors) {
          if (val.id > 0) {
            this.displaySchedule(val)
          } else {
            var userId = this.selectedSchedule.userId
            this.Refresh()
            this.selectedSchedule.userId = userId
            this.catSvc.InfoMsgBar("This User has no Active Schedule")
          }
        } else
          this.catSvc.ErrorMsgBar()
      }, error: (e) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
  DeleteScheduleEvents(id: number) {
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
        debugger;
        // First, delete the associated events
        // this.schSvc.DeleteScheduleDayEvent(id).subscribe({
        // next: (data) => {
        // Events deleted, now delete the schedule day
        // alert(id);
        this.schSvc.DeleteScheduleDayEvents(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Schedule Events has been deleted.',
              'success'
            );
            this.getScheduleByUserId(this.UserId);
            // this.Refresh();                          
            //this.ngOnInit();
          },
          error: (e) => {
            console.error(e);
            this.catSvc.ErrorMsgBar("Error Occurred while deleting the Schedule");
          }
        });
        // },
        error: (e) => {
          console.error(e);
          this.catSvc.ErrorMsgBar("Error Occurred while deleting Schedule Day Events");
        }
        // });
      }
    });
  }
  SchHistoryDialog() {
    this.dialogRef = this.dialog.open(ScheduleHistoryComponent, {
      width: '1200px', height: '590px'
      , data: { userId: this.selectedSchedule.userId, user: this.selectedSchedule.user }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {

      });
  }
  OpenWorkTimeDilaog(schDay) {
    this.selectedScheduleDay = schDay
    this.workTime = schDay.workTime
    this.workTimeDialogRef = this.dialog.open(this.workTimeDialog, {
      width: '800px'
    });
    this.workTimeDialogRef.afterClosed().subscribe({
      next: () => {
        this.getScheduleByUserId(this.selectedSchedule.userId);
      }
    })
  }
  UpdateScheduleDay() {
    this.selectedScheduleDay.workTime = this.workTime
    this.schSvc.UpdateScheduleDay(this.selectedScheduleDay).subscribe({
      next: () => {
        this.workTimeDialogRef.close()
        this.catSvc.SuccessfullyUpdateMsg()
      }, error: () => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
}
