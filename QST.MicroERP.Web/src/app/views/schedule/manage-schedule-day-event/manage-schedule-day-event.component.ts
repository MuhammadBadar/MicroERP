import { ScheduleDayEventVM } from '../Models/ScheduleDayEventVM';
import { Component, Injector, ViewChild } from '@angular/core';
import { ScheduleDayVM, ScheduleVM } from '../Models/ScheduleVM';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../../enums/enumTypes';
import { CatalogService } from '../../catalog/catalog.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { MatTableDataSource } from '@angular/material/table';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgForm } from '@angular/forms';
import { ScheduleService } from '../schedule.serivce';
import { error } from 'console';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';

@Component({
  selector: 'app-manage-schedule-day-event',
  templateUrl: './manage-schedule-day-event.component.html',
  styleUrls: ['./manage-schedule-day-event.component.css']
})
export class ManageScheduleDayEventComponent {
  minStartTime
  displayeScheduleColumns: string[] = ['startTime', 'endTime', 'sp', 'actions'];
  DayEventSource: any;
  proccessing: boolean | undefined;
  AddMode: boolean = true
  EditMode: boolean = false
  dataSource: any;
  schDayEvents: ScheduleDayEventVM[] = [];
  DayEvent: ScheduleDayVM[] | any;
  ScheduleDay: ScheduleDayVM[] = []
  selectedDayEvent = new ScheduleDayEventVM
  selectedSchedule: ScheduleDayEventVM;
  WeekDays: SettingsVM[];
  Location: SettingsVM[] = [];
  EventType: SettingsVM[] = [];
  Entities: SettingsVM[];
  ScheduleType: SettingsVM[];
  WorkingType: SettingsVM[];
  @ViewChild('dayeventForm', { static: true }) dayeventForm!: NgForm;
  dialogRefe: any;
  dialogData: any;
  isDialog: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = true
  schDay: ScheduleDayVM
  day: string
  timepicker: any;
  startTime: null;
  endTime: null;
  timeRangeInvalid: boolean = false;
  previousEndTime: string;
  minEndTime: string;
  constructor(private injector: Injector,
    private schSvc: ScheduleService,
    private route: ActivatedRoute,
    private catSvc: CatalogService,
    private storeSvc: StorageService
  ) {
    this.selectedSchedule = new ScheduleDayEventVM
    this.selectedDayEvent = new ScheduleDayEventVM()
    this.dialogRefe = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }

  ngOnInit(): void {
    if (this.dialogData != null) {
      this.isDialog = this.dialogData.isDialog;
      this.isDialog = true;
      if (this.dialogData.scheduleLine != undefined) {
        this.schDay = this.dialogData.scheduleLine
        this.day = this.schDay.day
        this.GetDayEvents()
      }
    }
    this.selectedSchedule.isActive = true;
  }
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype === EnumTypes.Entities) {
          this.Entities = res;
        } else if (etype === EnumTypes.ScheduleType) {
          this.ScheduleType = res;
        } else if (etype === EnumTypes.WeekDays) {
          this.WeekDays = res;
        } else if (etype === EnumTypes.WorkingType) {
          this.WorkingType = res;
        } else if (etype === EnumTypes.EventType) {
          this.EventType = res;
          if (this.EventType.length > 0)
            this.selectedDayEvent.eventTypeId = this.EventType[0].id;
        } else if (etype === EnumTypes.Locations) {
          this.Location = res;
          if (this.Location.length > 0)
            this.selectedDayEvent.locationId = this.Location[0].id;
        }
      }, error: (e) => {
        alert("t");
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
  GetDayEvents() {
    var evt = new ScheduleDayEventVM
    evt.schId = this.schDay.schId;
    evt.SchDayId = this.schDay.id;
    evt.clientId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.schSvc.SearchScheduleDayEvent(evt).subscribe({
      next: (value: ScheduleDayEventVM[]) => {
        if (value != undefined) {
          this.schDayEvents = value
          this.setStartTime()
          this.dataSource = new MatTableDataSource(this.schDayEvents)
        }
      }, error: (err) => {
        console.warn(err)
        this.catSvc.ErrorMsgBar()
      },
    })
  }
  EditScheduleDayEvents(event: ScheduleDayEventVM) {
    debugger
    var evt = new ScheduleDayEventVM
    evt.id = event.id
    evt.clientId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.schSvc.SearchScheduleDayEvent(evt).subscribe({
      next: (res) => {
        debugger
        this.selectedDayEvent = res[0]
        this.minStartTime = this.selectedSchedule.startTime
        this.EditMode = true
        this.AddMode = false
      }, error: () => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  Submit() {
    if (this.selectedDayEvent.startTime == null || this.selectedDayEvent.startTime == undefined) {
      this.catSvc.ErrorMsgBar("Please select Start Time.");
      return;
    }
    if (this.selectedDayEvent.endTime == null || this.selectedDayEvent.endTime == undefined) {
      this.catSvc.ErrorMsgBar("Please select End Time.");
      return;
    }
    this.selectedDayEvent.schId = this.schDay.schId;
    this.selectedDayEvent.SchDayId = this.schDay.id;
    this.selectedDayEvent.isActive = true;
    this.selectedDayEvent.clientId = + this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    if (this.selectedDayEvent.id > 0)
      this.UpdateScheduleDayEvent()
    else
      this.schSvc.SaveScheduleDayEvent(this.selectedDayEvent).subscribe({
        next: (value: ScheduleDayEventVM) => {
          if (value.hasErrors) {
            value.resultMessages.forEach(element => {
              this.catSvc.ErrorMsgBar(element.message);
            });
          }

          else {
            value.resultMessages.forEach(element => {
              this.catSvc.SuccessMsgBar(element.message);
            });
            this.GetDayEvents()
            this.Refresh();
          }
        },
        error: (err) => {
          this.catSvc.ErrorMsgBar();
        },
      });
  }
  UpdateScheduleDayEvent() {
    debugger;
    this.schSvc.UpdateScheduleDayEvent(this.selectedDayEvent).subscribe({
      next: (value: ScheduleDayEventVM) => {
        if (value.hasErrors) {
          value.resultMessages.forEach(element => {
            this.catSvc.ErrorMsgBar(element.message);
          });
        }

        else {
          value.resultMessages.forEach(element => {
            this.catSvc.SuccessMsgBar(element.message);
          });
          this.GetDayEvents()
          this.Refresh();
        }
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar()
        console.warn(e);
        this.ScheduleDay = []
        this.proccessing = false
        //this.Refresh();
      }
    })
  }
  DeleteScheduleDayEvents(id: number) {
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
        this.schSvc.DeleteScheduleDayEvent(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Schedule Day Event has been deleted.',
              'success'
            )
            this.Refresh();
            this.ngOnInit();
          }, error: (e) => {
            alert("e");
            this.catSvc.ErrorMsgBar()
            console.warn(e);
          }
        })
      }
    })
  }
  setStartTime() {
    if (this.schDayEvents.length > 0) {
      var lastEvent = this.schDayEvents[this.schDayEvents.length - 1];
      if (lastEvent.endTime != undefined) {
        this.selectedDayEvent.startTime = lastEvent.endTime
        this.minStartTime = lastEvent.endTime
      }
    }
  }
  Refresh() {
    // this.GetschDayEvents();
    this.selectedDayEvent = new ScheduleDayEventVM
    this.setStartTime()
    this.EditMode = false
    this.AddMode = true
  }
}
