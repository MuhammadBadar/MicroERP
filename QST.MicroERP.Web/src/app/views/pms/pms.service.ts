import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Globals } from 'src/app/globals';
import { DoctorVM } from './Models/DoctorVM';
import { StaffVM } from './Models/StaffVM';
import { PatientVM } from './Models/PatientVM';
import { AppointmentVM } from './Models/AppointmentVM';
import { PrescriptionVM } from './Models/PrescriptionVM';
import { ClientsVM } from '../catalog/Models/ClientsVM';
import { ItemVM } from '../key-accounting/Models/ItemVM';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class PMSService {
  public loggedInDoctorId: number = 0
  constructor(private http: HttpClient,
    private datePipe: DatePipe
  ) { }
  dateTranform(date): number {
    return +this.datePipe.transform(date, 'yyMMdd');
  }
  GetDoctor(): Observable<DoctorVM[]> {
    return this.http.get<DoctorVM[]>(Globals.BASE_API_URL + 'Doctor').pipe();
  }
  GetDoctorById(id: number): Observable<DoctorVM[]> {
    return this.http.get<DoctorVM[]>(Globals.BASE_API_URL + 'Doctor/' + id).pipe()
  }
  SaveDoctor(value: DoctorVM) {
    return this.http.post(Globals.BASE_API_URL + 'Doctor', value);
  }
  UpdateDoctor(value: DoctorVM) {
    return this.http.put(Globals.BASE_API_URL + 'Doctor', value);
  }
  DeleteDoctor(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Doctor/' + id);
  }
  SearchDoctor(value: DoctorVM): Observable<DoctorVM[]> {
    return this.http.post<DoctorVM[]>(Globals.BASE_API_URL + 'Doctor/Search', value).pipe();
  }


  GetStaff(): Observable<StaffVM[]> {
    return this.http.get<StaffVM[]>(Globals.BASE_API_URL + 'Staff').pipe();
  }
  GetStaffById(id: number): Observable<StaffVM[]> {
    return this.http.get<StaffVM[]>(Globals.BASE_API_URL + 'Staff/' + id).pipe()
  }
  SaveStaff(value: StaffVM) {
    return this.http.post(Globals.BASE_API_URL + 'Staff', value);
  }
  UpdateStaff(value: StaffVM) {
    return this.http.put(Globals.BASE_API_URL + 'Staff', value);
  }
  DeleteStaff(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Staff/' + id);
  }
  SearchStaff(value: StaffVM): Observable<StaffVM[]> {
    return this.http.post<StaffVM[]>(Globals.BASE_API_URL + 'Staff/Search', value).pipe();
  }


  GetAppointment(): Observable<AppointmentVM[]> {
    return this.http.get<AppointmentVM[]>(Globals.BASE_API_URL + 'Appointment').pipe();
  }
  GetAppointmentById(id: number): Observable<AppointmentVM[]> {
    return this.http.get<AppointmentVM[]>(Globals.BASE_API_URL + 'Appointment/' + id).pipe()
  }
  SaveAppointment(value: AppointmentVM) {
    return this.http.post(Globals.BASE_API_URL + 'Appointment', value);
  }
  UpdateAppointment(value: AppointmentVM) {
    return this.http.put(Globals.BASE_API_URL + 'Appointment', value);
  }
  DeleteAppointment(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Appointment/' + id);
  }
  SearchAppointment(value: AppointmentVM): Observable<AppointmentVM[]> {
    return this.http.post<AppointmentVM[]>(Globals.BASE_API_URL + 'Appointment/Search', value).pipe();
  }
  SearchNextAppt(value: AppointmentVM): Observable<AppointmentVM[]> {
    return this.http.post<AppointmentVM[]>(Globals.BASE_API_URL + 'Appointment/NextAppt', value).pipe();
  }
  GetMinTime(value: AppointmentVM) {
    return this.http.post<string>(Globals.BASE_API_URL + 'Appointment/ApptMinTime?', value)
  }
  GetTokenNo(value: AppointmentVM) {
    return this.http.post<string>(Globals.BASE_API_URL + 'Appointment/NextTknNo?', value)
  }


  GetPatient(): Observable<PatientVM[]> {
    return this.http.get<PatientVM[]>(Globals.BASE_API_URL + 'Patient').pipe();
  }
  GetPatientById(id: number): Observable<PatientVM[]> {
    return this.http.get<PatientVM[]>(Globals.BASE_API_URL + 'Patient/' + id).pipe()
  }
  SavePatient(value: PatientVM) {
    return this.http.post(Globals.BASE_API_URL + 'Patient', value);
  }
  UpdatePatient(value: PatientVM) {
    return this.http.put(Globals.BASE_API_URL + 'Patient', value);
  }
  DeletePatient(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Patient/' + id);
  }
  SearchPatient(value: PatientVM): Observable<PatientVM[]> {
    return this.http.post<PatientVM[]>(Globals.BASE_API_URL + 'Patient/Search', value).pipe();
  }


  UpdatePrescription(value: PrescriptionVM): Observable<PrescriptionVM> {
    return this.http.put<PrescriptionVM>(Globals.BASE_API_URL + 'Prescription', value);
  }
  GetPrescriptionById(id: number): Observable<PrescriptionVM[]> {
    return this.http.get<PrescriptionVM[]>(Globals.BASE_API_URL + 'Prescription/' + id).pipe()
  }
  SearchPrescription(value: PrescriptionVM): Observable<PrescriptionVM[]> {
    return this.http.post<PrescriptionVM[]>(Globals.BASE_API_URL + 'Prescription/Search', value).pipe();
  }
  GetPrescription(): Observable<PrescriptionVM[]> {
    return this.http.get<PrescriptionVM[]>(Globals.BASE_API_URL + 'Prescription').pipe();
  }
  SavePrescription(value: PrescriptionVM): Observable<PrescriptionVM> {
    return this.http.post<PrescriptionVM>(Globals.BASE_API_URL + 'Prescription', value);
  }
  DeletePrescription(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Prescription/' + id);
  }


  UpdateMedicine(value: ItemVM): Observable<ItemVM> {
    return this.http.put<ItemVM>(Globals.BASE_API_URL + 'Medicine', value);
  }
  GetMedicineById(id: number): Observable<ItemVM[]> {
    return this.http.get<ItemVM[]>(Globals.BASE_API_URL + 'Medicine/' + id).pipe()
  }
  SearchMedicine(value: ItemVM): Observable<ItemVM[]> {
    return this.http.post<ItemVM[]>(Globals.BASE_API_URL + 'Medicine/Search', value).pipe();
  }
  GetMedicine(): Observable<ItemVM[]> {
    return this.http.get<ItemVM[]>(Globals.BASE_API_URL + 'Medicine').pipe();
  }
  SaveMedicine(value: ItemVM): Observable<ItemVM> {
    return this.http.post<ItemVM>(Globals.BASE_API_URL + 'Medicine', value);
  }
  DeleteMedicine(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Medicine/' + id);
  }


  GetRxPdf(rx: PrescriptionVM): Observable<any> {
    console.warn(rx)
    return this.http.post<any>(Globals.SpPRING_BOOT_API_URL + "PatientVisit/Report", rx, {
      responseType: 'blob' as 'json',
      observe: 'response'
    });
  }
}
