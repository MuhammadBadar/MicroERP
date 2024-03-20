
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LogEventVM } from '../catalog/Models/LogEventVM'
import { CatalogService } from '../catalog/catalog.service';
import { AttendanceVM } from './Models/AttendanceVM';
import { Globals } from '../../globals';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';
@Injectable({
    providedIn: 'root'
})
export class AttendanceService {
    selectedScheduleId: number;
    selectedScheduleDayId: number;
    constructor(private http: HttpClient, private catSvc: CatalogService, private storeSvc: StorageService) { }

    getFormattedTask(task: any): string {
        return "<b>TaskId: " + task.taskId +
            " </b>" + "<b>(</b>" + task.priority + "<b>)</b>" + " - <b>(</b>" +
            task.sp +
            "" +
            (task.lastClaim > 0
                ? " - (<b>" + task.lastClaim + "% Already Completed</b>) "
                : "") +
            "<b>)</b> - " +
            task.title;
    }
    getFormattedTaskScore(task: any): string {
        return `<b>${task.taskId}: </b>` +
            (task.isDayEnded ?
                `${task.taskScore ?? "0"}-> ${task.workSP ?? "0"}/${task.sp} <b> (</b>${task.lastClaim ?? "0"}%-> ${task.claimPercent ?? "0"}% <b>)</b>` :
                'N/A');
    }
    getFormattedSummaryRptScore(task: any): string {
        return this.getFormattedFinalScore(task)
    }
    getFormattedFinalScore(task: any): string {
        return `<b>${task.taskId}: </b>` +
            (task.isDayEnded ?
                `${task.finalScore ?? "0"}/${task.sp} <b>(</b>${task.approvedClaim > 0 ? task.approvedClaim : task.claimPercent ?? "0"}%<b>)</b>` :
                'N/A');
    }
    getFormattedClaimedHours(task: any): string {
        return `<b>${task.taskId}: (</b>${task.sp} <b>) </b>` +
            (task.isDayEnded ?
                `${task.claimWorkTime ?? "0"}` :
                'N/A');
    }
    getFormattedExtraTime(task: any): string {
        return `<b>${task.taskId}: (</b>${task.sp} <b>) </b>` +
            (task.isDayEnded ?
                ` => ${task.extraTime}` :
                'N/A');
    }
    // getFormattedTaskScore(task: any): string {
    //   return "<b>" + task.taskId + ": </b>" + (task.claimId > 0 ? task.workSP + "->" + task.taskScore +
    //     "/" + task.sp + "<b>  (</b>" + task.claimPercent + "% ->" + task.taskComPer + " <b>)</b>" : " N/A")
    // }
    //Attendance lms services
    GetAttendance(): Observable<AttendanceVM[]> {
        return this.http.get<AttendanceVM[]>(Globals.BASE_API_URL + 'Attendance').pipe();
    }
    GetAttendanceById(userId: string): Observable<AttendanceVM[]> {
        return this.http.get<AttendanceVM[]>(Globals.BASE_API_URL + 'Attendance/' + userId).pipe();
    }

    SaveAttendance(value: AttendanceVM) {
        return this.http.post(Globals.BASE_API_URL + 'Attendance', value);
    }
    UpdateAttendance(value: AttendanceVM) {
        return this.http.put(Globals.BASE_API_URL + 'Attendance', value);
    }
    DeleteAttendance(id: number) {
        return this.http.delete(Globals.BASE_API_URL + 'Attendance/' + id);
    }
    SearchAttendance(value: AttendanceVM): Observable<AttendanceVM[]> {
        return this.http.post<AttendanceVM[]>(Globals.BASE_API_URL + 'Attendance/Search', value).pipe();
    }
    GetLastAttendance(userId: string): Observable<AttendanceVM> {
        return this.http.get<AttendanceVM>(Globals.BASE_API_URL + 'Attendance/GetLastAttendance/' + userId).pipe();
    }
    MarkInTime(userId): Observable<any> {
        var event = new LogEventVM
        event.userId = userId
        event.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
        //event.clientId=
        return this.http.post<any>(Globals.BASE_API_URL + 'LogEvent/MarkInTime', event).pipe();
    }
    MarkOutTime(userId: string): Observable<any> {
        var event = new LogEventVM
        event.userId = userId
        event.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
        return this.http.post<any>(Globals.BASE_API_URL + 'LogEvent/MarkOutTime', event).pipe();
    }
    MarkUserInTime() {
        this.MarkInTime(this.catSvc.getLoggedInUserId()).subscribe({
            next: (res) => { console.warn(res) },
            error: (err) => {
                this.catSvc.ErrorMsgBar("Error Occurred while marking User's InTime", 4000)
                console.warn(err)
            }
        })
    }
    MarkUserOutTime() {
        const userId = this.catSvc.getLoggedInUserId()
        if (userId) {
            this.MarkOutTime(userId).subscribe({
                next: (res) => {
                    console.warn(res)
                },
                error: (err) => {
                    console.warn(err)
                    this.catSvc.ErrorMsgBar("Error Ouucrred while marking  User's OutTime", 4000)
                },
            });
        } else {
            console.error('Invalid userId:', userId);
        }
    }
    getAttendanceRpt(value: AttendanceVM): Observable<AttendanceVM[]> {
        return this.http.post<AttendanceVM[]>(Globals.BASE_API_URL + 'Attendance/getAttendanceRpt', value).pipe();
    }
}
