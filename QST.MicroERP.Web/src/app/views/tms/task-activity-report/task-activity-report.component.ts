import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { UserVM } from '../../security/models/user-vm';
import { SecurityService } from '../../security/security.service';
import { TaskVM } from '../Models/task-vm';
import { TaskService } from '../task.service';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
@Component({
  selector: 'app-task-activity-report',
  templateUrl: './task-activity-report.component.html',
  styleUrls: ['./task-activity-report.component.css']
})
export class TaskActivityReportComponent implements OnInit {
  maxDate = new Date
  isLoading: boolean = false
  length: number;
  freeSearch: string;
  deleteItem: boolean = false;
  public items: any[];
  tasks: TaskVM[] = [];
  taskList: TaskVM[] = []
  searchtask: TaskVM;
  dataSource;
  users: UserVM[] = []
  displayedColumns = ['title', 'extraTime', 'activity'];
  hasPerms: boolean = false
  selectedTask: TaskVM
  taskSearchValue?: any
  taskFilterData: any
  constructor(
    public taskSvc: TaskService,
    private catSvc: CatalogService,
    public secSvc: SecurityService,
    private storeSvc: StorageService,
    public dialog: MatDialog) {
    this.selectedTask = new TaskVM;
  }
  ngOnInit(): void {
    this.GetTasks();
    this.secSvc.GetUsers()
    this.GetTaskList()
  }
  updateFilter(event) {
    if (this.tasks.length > 0) {
      const val = event.target.value.toLowerCase();
      var columns = Object.keys(this.tasks[0]);
      columns.splice(columns.length - 1);
      if (!columns.length)
        return;
      const rows = this.tasks.filter(function (d) {
        for (let i = 0; i <= columns.length; i++) {
          let column = columns[i];
          if (d[column] && d[column].toString().toLowerCase().indexOf(val) > -1) {
            return true;
          }
        }
        return false;
      });
      this.dataSource = new MatTableDataSource(rows);
      this.length = this.dataSource.filteredData.length;
    }
  }
  GetTaskList() {
    var task = new TaskVM
    task.priorityId = 0
    task.statusId = 0
    task.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    task.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    task.includeSubordinatesData = true
    this.taskSvc.GetTaskList(task).subscribe({
      next: (res: TaskVM[]) => {
        this.taskList = res;
        this.taskFilterData = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
  GetTasks() {
    this.isLoading = true
    var task = new TaskVM
    task.fromDate = this.catSvc.setDate(this.selectedTask.fromDate)
    task.toDate = this.catSvc.setDate(this.selectedTask.toDate)
    task.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    task.id = this.selectedTask.id
    if (this.selectedTask.userId == null || this.selectedTask.userId == "") {
      task.includeSubordinatesData = true
      task.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    } else
      task.userId = this.selectedTask.userId
    this.taskSvc.GetTaskActivities(task).subscribe({
      next: (res: TaskVM[]) => {
        this.tasks = res;
        this.dataSource = new MatTableDataSource(this.tasks);
        this.length = this.dataSource.filteredData.length;
        this.isLoading = false
      }, error: (e) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.selectedTask = new TaskVM
    this.GetTasks();
  }
  TaskAutoCompleteSearch(evet: any) {
    this.taskFilterData = this.taskList.filter((Task) =>
      this.TaskMatchesSearch(Task, evet)
    );
  }
  TaskMatchesSearch(Task: TaskVM, evet): boolean {
    const searchLower = evet.toLowerCase();
    const searchCriteria = searchLower.split(' ');
    return searchCriteria.every((criteria) => {
      if (!criteria.trim()) return true;
      const criteriaMatches = [
        Task.id.toString(),
        Task.title,
        Task.priority,
        Task.user,
        Task.sp.toString(),
      ].some((field) => field && field.toLowerCase().includes(criteria));
      return criteriaMatches;
    });
  }
}
