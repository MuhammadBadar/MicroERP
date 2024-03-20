import { Component, Injector, ViewChild } from '@angular/core';
import { Input, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AttachmentsVM, TaskVM } from '../Models/task-vm';
import { TaskCommentVM } from '../Models/taskcomment-vm';
import Swal from 'sweetalert2'
import { NotificationVM } from '../Models/NotificationVM';
import { NotificationLogVM } from '../../notifications/NotificationLogVM';
import { UserVM } from '../../security/models/user-vm';
import { TaskService } from '../task.service';
import { NotificationLogService } from '../../notifications/NotificationLogService';
import { SecurityService } from '../../security/security.service';
import { CatalogService } from '../../catalog/catalog.service';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from '../../../enums/enumTypes';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';
import { Priorities } from '../Models/Enums/Priorities';
import { Statuses } from '../models/Enums/Statuses';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
//import { QuillModule } from 'ngx-quill'

@Component({
  selector: 'app-manage-task',
  templateUrl: './manage-task.component.html',
  styleUrls: ['./manage-task.component.css']
})
export class ManageTaskComponent {
  imageId;
  statusId: number;
  status: string;
  showCount = false;
  previewImage = false;
  showMask = false;
  currentLightBoxImage;
  currentIndex = 0;
  controls = true;
  totalImageCount = 0
  Data;
  modules: SettingsVM[];
  User: UserVM[]
  commentedUser: UserVM[];
  commentedUserName: string;
  Status: SettingsVM[];
  Priority: SettingsVM[];
  TaskId: number;
  Time;
  CreatedOn = new Date;;
  Name: string;
  Size;
  DocPath;
  Id: number = null;
  Edit: boolean = false;
  Add: boolean = true;
  FileDetails = [];
  UpdateDetails = [];
  comments: TaskCommentVM[];
  gettaskById: TaskVM;
  selectedTaskComment: TaskCommentVM
  selectedTask: TaskVM
  selectedMail: NotificationVM
  notificationLog: NotificationLogVM
  isLoading: boolean = false
  dialogData: any
  dialogRef: any;
  isDialog: boolean = false
  @ViewChild('taskForm', { static: true }) taskForm: NgForm;
  isReadOnly: boolean = false
  constructor(
    private injector: Injector,
    public nLogSVC: NotificationLogService,
    public secSvc: SecurityService,
    private catSvc: CatalogService,
    private storeSvc: StorageService,
    private route: ActivatedRoute, private catSVC: CatalogService,
    public taskService: TaskService,
  ) {
    this.notificationLog = new NotificationLogVM;
    this.selectedTask = new TaskVM;
    this.selectedTaskComment = new TaskCommentVM
    this.selectedMail = new NotificationVM
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ManageTask)
    this.selectedTask = new TaskVM
    this.selectedTaskComment = new TaskCommentVM
    this.selectedTask.priorityId = Priorities.P0
    this.selectedTask.statusId = Statuses.Open
    this.route.queryParams.subscribe(params => {
      this.TaskId = params['id'];
    });
    if (this.dialogData) {
      if (this.dialogData.isDialog) {
        this.isDialog = true
        this.TaskId = this.dialogData.taskId
      }
    }
    this.secSvc.GetUsers();
    this.GetEnumValues(EnumTypes.Priorities);
    this.GetEnumValues(EnumTypes.TaskStatuses);
    this.selectedTask.userId = localStorage.getItem("userId")
    if (this.TaskId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetTaskById();
      this.GetTaskcomments();
    }
  }
  onPreviewImage(index: number): void {
    this.imageId = index;
    this.showCount = true;
    this.showMask = true;
    this.previewImage = true;
    this.currentIndex = index;
    this.totalImageCount = this.FileDetails.length;
    this.currentLightBoxImage = this.FileDetails[index].base64File
  }
  onClosePreview() {
    this.previewImage = false;
    this.showMask = false;
  }
  next(): void {
    debugger
    this.currentIndex = this.currentIndex + 1
    if (this.currentIndex > this.FileDetails.length - 1) {
      this.currentIndex = 0
    }
    this.currentLightBoxImage = this.FileDetails[this.currentIndex].base64File;
  }
  prev(): void {
    debugger
    this.currentIndex = this.currentIndex - 1
    if (this.currentIndex < 0) {
      this.currentIndex = this.FileDetails.length - 1
    }
    this.currentLightBoxImage = this.FileDetails[this.currentIndex].base64File;

  }
  Delete() {
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
        debugger
        console.warn(this.currentIndex)
        this.UpdateDetails = this.FileDetails.filter(i => i !== this.FileDetails[this.currentIndex]);
        this.totalImageCount = this.UpdateDetails?.length;
        if (this.FileDetails[this.currentIndex].id != 0) {
          this.selectedTask.attachments[0].id = this.FileDetails[this.currentIndex]?.id
          this.selectedTask.attachments[0].dBoperation = 3;
          this.selectedTask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
          this.taskService.UpdateTask(this.selectedTask).subscribe((data) => {
          },
            (err: any) => {
            });
        }
        Swal.fire(
          'Deleted!',
          'Image has been deleted.',
          'success'
        )
        this.next();
        this.FileDetails = this.UpdateDetails
        if (this.FileDetails.length == 0) {
          this.showMask = false;
          this.showCount = false;
          this.previewImage = false;
        }
      }

    })

    // console.warn(this.FileDetails[this.currentIndex].name)
    // console.warn(this.FileDetails[this.currentIndex].taskId)
    // console.warn(this.FileDetails[this.currentIndex].id)
    // console.warn(this.FileDetails[this.currentIndex])


  }
  GetEnumValues(etype: EnumTypes) {
    var Settings = new SettingsVM;
    Settings.enumTypeId = etype;
    this.catSVC.SearchSettings(Settings).subscribe((res: SettingsVM[]) => {
      if (etype == EnumTypes.TaskStatuses)
        this.Status = res;
      else if (etype == EnumTypes.Priorities)
        this.Priority = res;
      console.warn(this.Priority);
    },
      (err: any) => {
        alert('Error')
      });
  }
  GetTaskById() {
    debugger
    var task = new TaskVM
    task.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    task.id = this.TaskId
    this.taskService.GetTaskList(task).subscribe((res: TaskVM[]) => {
      this.gettaskById = res[0];
      this.selectedTask = this.gettaskById;
      this.statusId = this.gettaskById.statusId
      this.FileDetails = this.selectedTask.attachments;
    });
  }
  refreshPage() {
    this.selectedTask = new TaskVM();
  }
  SaveTask() {
    if (this.selectedTask.priorityId == 0)
      this.taskForm.controls['priorityId'].setErrors({ "incorrect": true })
    if (!this.taskForm.invalid) {
      this.isLoading = true
      this.selectedTask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      if (this.TaskId > 0) {
        this.UpdateTask();
      }
      else {
        this.selectedTask.attachments = this.FileDetails;
        console.warn(this.selectedTask)
        this.taskService.SaveTask(this.selectedTask).subscribe((res: TaskVM) => {
          this.isLoading = false
          if (res !== null) {
            if (!res.hasErrors) {
              this.ngOnInit()
              this.catSVC.SuccessfullyAddMsg();
              this.refreshPage();
              return;
            }
            else {
              this.catSVC.ErrorMsgBar('Unable to save Task!');
              return;
            }
          }
          else this.catSVC.ErrorMsgBar()
        },
          (err: any) => {
            this.isLoading = false
            this.catSVC.ErrorMsgBar()
          });
      }
    } else this.catSVC.ErrorMsgBar("Please fill all required Fields")
  }
  SaveNotificationLog(nLog) {
    console.warn(nLog)
    console.warn(this.notificationLog)
    this.nLogSVC.SaveNotificationLog(nLog).subscribe((res: NotificationLogVM) => { },
      (err: any) => {
        this.catSVC.ErrorMsgBar()
      });
  }
  UpdateTask() {
    this.UpdateDetails = this.FileDetails.filter(i => i.id == 0);
    this.selectedTask.attachments = this.UpdateDetails;
    this.taskService.UpdateTask(this.selectedTask).subscribe((data) => {
      this.catSVC.SuccessfullyUpdateMsg()
      // this.ngOnInit();
      this.isLoading = false
    },
      (err: any) => {
        this.isLoading = false
        this.catSVC.ErrorMsgBar()
      });
  }
  PutTask() {
    if (this.selectedTask.statusId !== this.statusId) {
      var loginUser = localStorage.getItem('TMSUserName')
      // this.selectedTask = this.gettaskById[0];
      if (loginUser == this.gettaskById[0].user || loginUser == this.gettaskById[0].directSupervisorName) {
        this.UpdateDetails = this.FileDetails.filter(i => i.id == 0);
        this.selectedTask.attachments = this.UpdateDetails;
        this.selectedTask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
        this.taskService.UpdateTask(this.selectedTask).subscribe((data) => {
          this.catSVC.SuccessfullyUpdateMsg()
          console.warn(this.selectedTask.statusId)
          if (this.selectedTask.statusId == 1010001) {
            this.status = "Open"
          } else if (this.selectedTask.statusId == 1010002) {
            this.status = "InProgress"
          } else if (this.selectedTask.statusId == 1010003) {
            this.status = "InTesting"
          } else if (this.selectedTask.statusId == 1010004) {
            this.status = "ReOpen"
          } else if (this.selectedTask.statusId == 1010005) {
            this.status = "Resolve"
          } else if (this.selectedTask.statusId == 1010006) {
            this.status = "Closed"
          }
          // else if (this.selectedTask.statusId == 1010007) {
          //   this.status = "RnD"
          // }

          this.ngOnInit();
        },
          (err: any) => {
            this.catSVC.ErrorMsgBar()
          });
      } else {
        this.catSVC.ErrorMsgBar('You Have no rights to update this Task!')
      }
    }
    // this.ngOnInit();
  }
  handleFileInput(e) {
    debugger
    for (let index = 0; index < e.target.files.length; index++) {
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[index]);
      this.Name = e.target.files[index].name;
      this.Size = e.target.files[index].size;
      reader.onload = (event: any) => {
        console.warn(event)
        this.Data = event.target.result;
        const newRow = {
          "id": 0,
          "isActive": true,
          "dBoperation": 1,
          "taskId": 0,
          "docPath": "string",
          "base64File": event.target.result,
          "name": e.target.files[index].name.toString(),
          "size": e.target.files[index].size.toString(),
          "createdOn": this.CreatedOn
        }
        this.FileDetails.push(newRow)
      };
    }
  }
  GetTaskcomments() {
    if (this.TaskId > 0) {
      var comnt = new TaskCommentVM
      comnt.taskId = this.TaskId
      comnt.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      this.taskService.SearchTaskComment(comnt).subscribe((res: TaskCommentVM[]) => {
        this.comments = res;
        console.warn(this.comments)
      });
    }
  }
  SaveTaskComment() {
    this.isLoading = true
    if (this.TaskId > 0) {
      this.selectedTaskComment.taskId = this.TaskId;
      this.selectedTaskComment.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
      this.selectedTaskComment.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      this.taskService.SaveTaskComment(this.selectedTaskComment).subscribe((data) => {
        this.isLoading = false
        this.selectedTaskComment.comment = '';
        this.catSVC.SuccessMsgBar('Comment Submitted!');
        this.GetTaskcomments()
        this.selectedTaskComment = new TaskCommentVM
        //this.loader.close();
      },
        (err: any) => {
          this.isLoading = false
          this.catSVC.ErrorMsgBar()
        });
    }
  }
}
