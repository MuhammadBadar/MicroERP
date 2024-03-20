
import { NotificationTemplateVM } from './Models/NotificationTemplateVM';
import { TaskVM, AttachmentsVM } from './Models/task-vm';
import { EnumValueVM } from './Models/EnumValueVM';
import { TaskCommentVM } from './Models/taskcomment-vm';
import { NotificationVM } from './Models/NotificationVM';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Globals } from 'src/app/globals';
import { UserTaskVM } from './Models/UserTaskVM';
import { Statuses } from './models/Enums/Statuses';
import { DatePipe } from '@angular/common';
import { UserVM } from '../security/models/user-vm';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';
@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient, public date: DatePipe, private storeSvc: StorageService) { }

  selectedTemplateData: NotificationTemplateVM
  selectedMail: NotificationVM
  selectedTask: TaskVM;
  file: AttachmentsVM
  selectedTaskComment: TaskCommentVM;
  isStalled(status) {
    if (status == Statuses.Stalled)
      return true;
    else return false;
  }
  getFormattedTask(task: any): string {
    return "TaskId: <b>" + task.id +
      " </b>" + "<b>(</b>" + task.priority + "<b>)</b>" + " - <b>(</b>" +
      task.sp +
      "Hr" +
      "<b>)</b> - " +
      task.title;
  }
  getFormattedDayTask(task: any): string {
    return "<b>TaskId: " + task.id +
      " </b>" + "- <b>(</b>" +
      task.sp +
      "Hr" +
      (task.claimPercent > 0
        ? " - (<b>" + task.claimPercent + "% Already Completed</b>) "
        : "") +
      "<b>)</b> - " +
      task.title;
  }
  getFormattedActivitiy(task: UserTaskVM): string {
    return `<b>${this.date.transform(task.date, 'dd/MM/yyyy')}</b>, 
      <b>${task.user}</b>, 
     <b> ${task.status} </b>` +
      (task.statusId == Statuses.Stalled && task.stalledReason ? (`(<b>Reason:</b> ${task.stalledReason})`) : "") +
      (task.isDayEnded ?
        ` ,Worked Time:<b>${task.workTime ?? "0"} Hr</b>,
      Percentage Completion:${task.lastClaim ?? "0"}%=><b> ${task.approvedClaim ?? task.claimPercent ?? "0"}% </b> 
     `: '') +
      (task.reviewComments != null && task.reviewComments != '' ? (`(<b>Supervisor Comments:</b>${task.reviewComments})`) : "");
  }
  getTasksList() {
    return this.http.get<TaskVM[]>(Globals.BASE_API_URL + 'Task/').pipe();
  }
  UpdateTask(task: TaskVM) {
    return this.http.put(Globals.BASE_API_URL + 'Task', task);
  }
  UpdateTemplate(tem: NotificationTemplateVM) {
    return this.http.put(Globals.BASE_API_URL + 'NotificationTemplate', tem);
  }
  getTaskbyId(id): Observable<TaskVM> {
    return this.http.get<TaskVM>(Globals.BASE_API_URL + 'Task/' + id).pipe();
  }
  GetTaskById(id): Observable<TaskVM> {
    return this.http.get<TaskVM>(Globals.BASE_API_URL + 'Task/' + id).pipe();
  }
  SearchTask(Task): Observable<TaskVM[]> {
    return this.http.post<TaskVM[]>(Globals.BASE_API_URL + 'Task/Search', Task).pipe();
  }
  SearchUserTask(Task): Observable<TaskVM[]> {
    return this.http.post<TaskVM[]>(Globals.BASE_API_URL + 'Task/Tasks', Task).pipe();
  }
  SearchTemplate(tem): Observable<NotificationTemplateVM[]> {
    return this.http.post<NotificationTemplateVM[]>(Globals.BASE_API_URL + 'NotificationTemplate/Search', tem).pipe();
  }
  getEnumValues(type): Observable<EnumValueVM[]> {
    return this.http.get<EnumValueVM[]>(Globals.BASE_API_URL + 'EnumValues/' + type).pipe();
  }
  getTaskComments(id): Observable<TaskCommentVM[]> {
    return this.http.get<TaskCommentVM[]>(Globals.BASE_API_URL + 'TaskComment/' + id).pipe();
  }
  GetTaskActivities(Task): Observable<TaskVM[]> {
    return this.http.post<TaskVM[]>(Globals.BASE_API_URL + 'UserTask/TaskActivities', Task).pipe();
  }
  GetTaskList(Task): Observable<TaskVM[]> {
    return this.http.post<TaskVM[]>(Globals.BASE_API_URL + 'Task/Tasks', Task).pipe();
  }
  SaveTask(Task): Observable<TaskVM> {
    debugger;
    return this.http.post<TaskVM>(Globals.BASE_API_URL + 'Task', Task).pipe();
  }
  SaveTemplate(tem): Observable<NotificationTemplateVM> {
    return this.http.post<NotificationTemplateVM>(Globals.BASE_API_URL + 'NotificationTemplate', tem).pipe();
  }
  SendMail(Mail: NotificationVM) {
    return this.http.post(Globals.BASE_API_URL + 'Notification', Mail);
  }
  SaveTaskComment(Task: TaskCommentVM) {
    debugger
    return this.http.post(Globals.BASE_API_URL + 'TaskComment', Task);
  }
  SearchTaskComment(Task: TaskCommentVM) {
    debugger
    return this.http.post(Globals.BASE_API_URL + 'TaskComment/Search', Task);
  }
  deleteTasks(id) {
    return this.http.delete(Globals.BASE_API_URL + 'Task/' + id);
  }
  DeleteTemplate(id) {
    return this.http.delete(Globals.BASE_API_URL + 'NotificationTemplate/' + id);
  }


  GetTaskByUserId(userId: string): Observable<TaskVM[]> {
    var uTask = new UserTaskVM
    uTask.userId = userId
    uTask.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    return this.http.post<TaskVM[]>(Globals.BASE_API_URL + 'Task/GetTasksByUserId', uTask).pipe()
  }
  GetTask(): Observable<TaskVM[]> {
    return this.http.get<TaskVM[]>(Globals.BASE_API_URL + 'Task').pipe();
  }
  GetTaskId(userId: string): Observable<TaskVM[]> {
    debugger;
    return this.http.get<TaskVM[]>(Globals.BASE_API_URL + 'Task/' + userId).pipe()
  }
  DeleteTask(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Task/' + id);
  }

  GetUsertask(): Observable<UserTaskVM[]> {
    debugger;
    return this.http.get<UserTaskVM[]>(Globals.BASE_API_URL + 'UserTask').pipe();
  }
  SaveUsertask(task: UserTaskVM) {
    return this.http.post(Globals.BASE_API_URL + 'UserTask', task)
  }
  SaveUsertasks(tasks: UserTaskVM[], markAttendance: boolean) {
    const params = new HttpParams().set('markAttendance', markAttendance.toString());
    return this.http.post<UserTaskVM[]>(Globals.BASE_API_URL + 'UserTask', tasks, { params: params })
  }
  IsDayStarted(userId: any) {
    var user = new UserVM
    user.id = userId
    user.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    return this.http.post<any>(Globals.BASE_API_URL + 'UserTask/IsDayStarted', user)
  }
  HasTasks(userId: any) {
    var user = new UserVM
    user.id = userId
    user.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    return this.http.post<any>(Globals.BASE_API_URL + 'UserTask/HasTodaysTasks', user)
  }
  UpdateUsertask(value: UserTaskVM) {
    return this.http.put(Globals.BASE_API_URL + 'UserTask', value)
  }
  UpdateUsertasks(tasks: UserTaskVM[], markDayEnd) {
    const params = new HttpParams().set('markDayEnd', markDayEnd.toString());
    return this.http.put<UserTaskVM[]>(Globals.BASE_API_URL + 'UserTask', tasks, { params: params })
  }
  DeleteUsertask(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'UserTask/' + id)
  }
  SearchUsertask(value: UserTaskVM): Observable<UserTaskVM[]> {
    return this.http.post<UserTaskVM[]>(Globals.BASE_API_URL + 'UserTask/Search', value).pipe();
  }
  MarkTaskAsStalled(task: TaskVM) {
    return this.http.put<any>(Globals.BASE_API_URL + 'UserTask/MarkStatus', task)
  }


}
