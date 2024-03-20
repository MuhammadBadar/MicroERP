import { Component, OnInit, ViewChild } from '@angular/core';
import { EnumValueVM } from '../../Models/EnumValueVM';
import { TaskVM } from '../../Models/task-vm';
import { UserVM } from 'src/app/views/security/models/user-vm';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TaskService } from '../../task.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SecurityService } from 'src/app/views/security/security.service';
import { EnumTypes } from '../../../../enums/enumTypes';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { NgForm } from '@angular/forms';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  Priority: SettingsVM[];
  task: TaskVM;
  modules: SettingsVM[];
  User: UserVM[];
  Status: SettingsVM[];
  hasPerms: boolean = false
  @ViewChild('taskForm', { static: true }) taskForm: NgForm;
  constructor(
    public dialogref: MatDialogRef<SearchComponent>,
    public dialog: MatDialog, private catSVC: CatalogService,
    public taskSvc: TaskService,
    public secSvc: SecurityService,
    private storeSvc: StorageService
  ) {
    this.task = new TaskVM;
    this.task.statusId = undefined;
    this.task.priorityId = undefined
  }

  ngOnInit(): void {
    this.hasPerms = this.catSVC.isSuperOrClientAdmin()
    this.task.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    // this.GetEnumValues(EnumType.Module);
    this.GetEnumValues(EnumTypes.Priorities);
    this.GetEnumValues(EnumTypes.TaskStatuses);
    this.secSvc.GetUsers();
  }
  GetEnumValues(etype: EnumTypes) {
    debugger;
    var Settings = new SettingsVM;
    Settings.enumTypeId = etype;
    this.catSVC.SearchSettings(Settings).subscribe((res: SettingsVM[]) => {
      if (etype == EnumTypes.TaskModules)
        this.modules = res;
      else if (etype == EnumTypes.TaskStatuses)
        this.Status = res;
      else if (etype == EnumTypes.Priorities)
        this.Priority = res;
    },
      (err: any) => {
        alert('Error')
      });
  }
  SearchTask() {
    const formControls = this.taskForm.form.controls;
    const isFormEmpty = Object.keys(formControls).every(controlName => !formControls[controlName].value);

    if (!isFormEmpty) {
      // if (!this.hasPerms)
      //   this.task.userId = localStorage.getItem("userId")
      if (this.task.userId == null || this.task.userId == "") {
        this.task.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
        this.task.includeSubordinatesData = true
      }
      this.task.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      if (this.task.title == '') {
        this.task.title = undefined
      }
      if (this.task.sp == null) {
        this.task.sp = undefined
      }
      if (this.task.id == null) {
        this.task.id = undefined
      }

      this.dialogref.close({ data: this.task });
    } else
      this.catSVC.ErrorMsgBar("Please Provide Some Input for Searching")
  }
}
