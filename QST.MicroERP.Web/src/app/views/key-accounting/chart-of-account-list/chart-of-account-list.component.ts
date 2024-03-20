import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CatalogService } from '../../catalog/catalog.service';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { ChartOfAccountComponent } from '../chart-of-account/chart-of-account.component';
import { Modules } from '../../catalog/Models/Enums/Modules';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-chart-of-account-list',
  templateUrl: './chart-of-account-list.component.html',
  styleUrls: ['./chart-of-account-list.component.css']
})
export class ChartOfAccountListComponent implements OnInit {
  isReadOnly: boolean = false
  @ViewChild('Form', { static: true }) filterForm!: NgForm;
  displayedColumns: string[] = ['keyCode', 'name', 'level', 'parent', 'isActive', 'actions'];
  dataSource: any;
  dialogRef: any
  filterVal: SettingsVM = new SettingsVM();
  style = "background-image: linear-gradient(to bottom, rgb(2, 33, 58), rgb(7, 95, 122));color:white;font-weight:normal "
  tAccounts!: SettingsVM[]
  constructor(private route: Router,
    public catSvc: CatalogService,
    public dialog: MatDialog,) {
    this.filterVal = new SettingsVM()
  }
  ngOnInit() {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ChartOfAccount)
    this.GetSettings()
  }
  GetSettings() {
    var value = new SettingsVM
    value.enumTypeId = EnumTypes.ChartofAccount
    value.clientId = +localStorage.getItem("ClientId")
    value.moduleId = Modules.GL
    this.catSvc.SearchSettings(value).subscribe({
      next: (res: SettingsVM[]) => {
        // this.Settings = res;
        this.tAccounts = res
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
  EditAccount(acc: SettingsVM) {
    this.dialogRef = this.dialog.open(ChartOfAccountComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px'
      , data: { accId: acc.id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(ChartOfAccountComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px'
      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  SearchInChartOfAccount(field) {
    var text;
    var account = this.filterVal
    this.SearchAccount(account)
  }
  ResetGrid() {
    this.filterForm.reset()
    this.GetSettings()
  }
  SearchAccount(value: SettingsVM) {
    value.enumTypeId = EnumTypes.ChartofAccount
    value.clientId = +localStorage.getItem("ClientId")
    value.moduleId = Modules.GL
    this.catSvc.SearchAccounts(value).subscribe({
      next: (res: SettingsVM[]) => {
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
}


