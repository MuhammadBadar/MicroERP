import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { stringify } from 'querystring';
import { MatSnackBar } from '@angular/material/snack-bar';
import Swal from 'sweetalert2'
import { Router } from '@angular/router';
import { SearchComponent } from '../search/search.component';
import { CommentsListComponent } from '../Comments-List/comments-list.component';
import { TaskVM } from '../../Models/task-vm';
import { TaskService } from '../../task.service';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';
@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent implements OnInit {
  isLoading: boolean = false
  length: number;
  freeSearch: string;
  deleteItem: boolean = false;
  public items: any[];
  private dialogref: MatDialogRef<SearchComponent>;
  private dialogRef: MatDialogRef<CommentsListComponent>;
  tasks: TaskVM[] = [];
  searchtask: TaskVM;
  dataSource;
  displayedColumns = ['id', 'title', 'user', 'priority', 'status', 'actualSP', 'description', 'actions'];
  isReadOnly: boolean = false
  constructor(
    private route: Router,
    private snack: MatSnackBar,
    public taskSvc: TaskService,
    private catSvc: CatalogService,
    private storeSvc: StorageService,
    public dialog: MatDialog) {
    this.taskSvc.selectedTask = new TaskVM;
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ManageTask)
    this.GetTask();
  }
  updateFilter(event) {
    if (this.tasks.length > 0) {
      const val = event.target.value.toLowerCase();
      var columns = Object.keys(this.tasks[0]);
      // Removes last "$$index" from "column"
      columns.splice(columns.length - 1);

      // console.log(columns);
      if (!columns.length)
        return;
      const rows = this.tasks.filter(function (d) {
        for (let i = 0; i <= columns.length; i++) {
          let column = columns[i];
          // console.log(d[column]);
          if (d[column] && d[column].toString().toLowerCase().indexOf(val) > -1) {
            return true;
          }
        }
        return false; // Add this line
      });
      this.dataSource = new MatTableDataSource(rows);
      this.length = this.dataSource.filteredData.length;
    }
  }
  Event() {
    this.dialogref = this.dialog.open(SearchComponent, {
      width: '800px'
    });
    this.dialogref.afterClosed()
      .subscribe((res) => {
        if (res) {
          this.isLoading = true
          this.searchtask = res.data;
          this.taskSvc.GetTaskList(this.searchtask).subscribe({
            next: (res: TaskVM[]) => {
              this.tasks = res;
              this.length = this.tasks.length;
              this.dataSource = this.tasks;
              this.isLoading = false
            }, error: (e) => {
              this.isLoading = false
              this.catSvc.ErrorMsgBar()
              console.warn(e);
            }
          })
        }
      });
  }
  public viewComments() {
    this.dialogRef = this.dialog.open(CommentsListComponent, {
      width: '800px'
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        if (!res) {
          return;
        }
      });
  }
  GetTask() {
    this.isLoading = true
    var task = new TaskVM
    task.priorityId = 0
    task.statusId = 0
    // if (!this.hasPerms)
    task.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    task.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    task.includeSubordinatesData = true
    this.taskSvc.GetTaskList(task).subscribe({
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
  DeleteTask(id) {
    debugger
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
        this.taskSvc.deleteTasks(id).subscribe((data) => {
          Swal.fire(
            'Deleted!',
            'Task has been deleted.',
            'success'
          )
          this.GetTask();
        })
      }
    })
  }
  EditTask(task: TaskVM) {
    this.route.navigate(['/task/task/managetask'], { queryParams: { id: task.id } });
  }

}
