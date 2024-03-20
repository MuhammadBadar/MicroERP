import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, Injector, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ScheduleDayVM, ScheduleVM } from '../Models/ScheduleVM';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ScheduleService } from '../schedule.serivce';
import { CatalogService } from '../../catalog/catalog.service';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';

@Component({
  selector: 'app-schedule-history',
  templateUrl: './schedule-history.component.html',
  styleUrls: ['./schedule-history.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ScheduleHistoryComponent implements OnInit {
  curDate: Date = new Date
  schedules: ScheduleVM[] = []
  selectedRowIndex = -1;
  innerDisplayedColumns: string[] = ['day', 'workTime', 'scheduleDayEvents'];
  dataSource: any;
  columnsToDisplayWithExpand = ['expand', 'schType', 'isActive'];
  expandedElement: any;
  isActive: boolean
  dialogRef: any
  dialogData: any;
  isDialog: boolean = false;
  userId: string
  selectedRow: ScheduleVM
  user: string
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<ScheduleDayVM>> | undefined;
  constructor(
    private injector: Injector,
    public dialog: MatDialog,
    public schSvc: ScheduleService,
    public catSvc: CatalogService,
    private storeSvc: StorageService
  ) {
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedRow = new ScheduleVM
  }
  ngOnInit(): void {
    if (this.dialogData != null) {
      if (this.dialogData.userId)
        this.userId = this.dialogData.userId
      if (this.dialogData.user)
        this.user = this.dialogData.user
      this.GetScheduleByUser();
    }
  }
  highlight(row: ScheduleVM) {
    // window.scrollTo(0, 0)
    this.selectedRow = new ScheduleVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetScheduleByUser() {
    var sch = new ScheduleVM
    sch.userId = this.userId
    sch.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.schSvc.SearchSchedule(sch).subscribe({
      next: (res: ScheduleVM[]) => {
        this.schedules = res
        this.dataSource = new MatTableDataSource(this.schedules);
      }, error: () => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
}

