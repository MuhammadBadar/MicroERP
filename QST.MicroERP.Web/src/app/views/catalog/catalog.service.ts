import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, delay, Observable, of, Subject } from 'rxjs';
import { SettingsTypeVM } from './Models/SettingsTypeVM';
import { SettingsVM } from './Models/SettingsVM';
import { SMTPCredsVM } from './Models/SMTPCredsVM';
import { Globals } from 'src/app/globals';
import { NotificationTemplateVM } from './Models/NotificationTemplateVM';
import { NotificationVM } from './Models/NotificationVM';
import { ItemsService } from '../items/items.service';
import * as moment from 'moment';
import { ClientsVM } from './Models/ClientsVM';
import { Roles } from './Models/Enums/Roles';
import { Router } from '@angular/router';
import { StorageData } from './Models/StorageData';
import { MatDialog } from '@angular/material/dialog';
import { PermissionVM } from '../security/models/permission-vm';
import { AppConstants } from 'src/app/app.constants';
import { Credentials } from 'src/app/credentials';
import { StorageService } from 'src/app/storage.service';
@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  permissions: PermissionVM[] = []
  html: string
  constructor(
    private snack: MatSnackBar,
    private http: HttpClient,
    private route: Router,
    private storeSvc: StorageService
  ) {
    this.permissions = []
  }

  private refreshSubject = new Subject<void>();
  // Observable to listen for refresh events
  refresh$ = this.refreshSubject.asObservable();
  // Trigger a refresh event
  triggerRefresh() {
    this.refreshSubject.next();
  }
  getCurrentClientId() {
    return +localStorage.getItem("ClientId")
  }
  getLoggedInUserId() {
    return localStorage.getItem("UserId")
  }
  validateNo(e): boolean {
    const charCode = e.which ? e.which : e.keyCode;
    if (
      (charCode > 31 && (charCode < 48 || charCode > 57))
      && charCode !== 46
    )
      return false;
    return true
  }
  isSuperAdmin() {
    var roleId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID)
    var cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    if (roleId == Roles.SuperAdmin && cltId == 0)
      return true
    else return false
  }
  isClientAdmin() {
    var roleId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID)
    var cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    if (roleId == Roles.ClientAdmin && cltId == 0)
      return true
    else return false
  }
  isSuperOrClientAdmin() {
    var roleId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID)
    var cltId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    if ((roleId == Roles.SuperAdmin || roleId == Roles.ClientAdmin) && cltId == 0)
      return true
    else return false
  }
  calculateTimeSum(column: string, dataSource): string {
    let totalSeconds = dataSource.reduce((acc, row) => {
      const timeValue = row[column];
      if (!/^([01]\d|2[0-3]):([0-5]\d):([0-5]\d)$/.test(timeValue)) {
        return acc;
      }
      const [hours, minutes, seconds] = timeValue.split(':').map(Number);
      return acc + hours * 3600 + minutes * 60 + seconds;
    }, 0);
    const hours = Math.floor(totalSeconds / 3600);
    totalSeconds %= 3600;
    const minutes = Math.floor(totalSeconds / 60);
    const seconds = totalSeconds % 60;

    return `${hours}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  }
  calculateFractionSum(column: string, dataSource): string {
    let totalNumerator = 0;
    let totalDenominator = 0;

    dataSource.forEach(row => {
      const fractionValue = row[column];
      // Check if the fraction value is in the correct format (numerator/denominator)
      if (/^\d+(\.\d+)?\/\d+(\.\d+)?$/.test(fractionValue)) {
        const [numerator, denominator] = fractionValue.split('/').map(Number);
        totalNumerator += numerator;
        totalDenominator += denominator;
      }
    });

    return `${totalNumerator.toFixed(2)}/${totalDenominator.toFixed(2)}`;
  }
  calculatePercentageSum(column: string, dataSource): string {
    let totalPercentage = 0;
    let validCount = 0;

    dataSource.forEach(row => {
      const percentageValue = row[column];
      // Check if the percentage value is in the correct format (e.g., "46.11 %")
      if (/\d+(\.\d+)? ?%/.test(percentageValue)) {
        // Remove the percentage sign, trim extra spaces, and convert to a number
        const numericValue = parseFloat(percentageValue.replace('%', '').trim());
        totalPercentage += numericValue;
        validCount++;
      }
    });

    if (validCount === 0) {
      return 'N/A'; // No valid percentages to calculate average
    }

    const averagePercentage = totalPercentage / validCount;
    return `${averagePercentage.toFixed(2)} %`;
  }
  calculateNumericSum(column: string, dataSource): string {
    let total = 0;

    dataSource.forEach(row => {
      const numericValue = row[column];
      // Check if the value is a valid number
      if (!isNaN(parseFloat(numericValue)) && isFinite(numericValue)) {
        total += parseFloat(numericValue);
      }
    });

    return total.toFixed(2); // Adjust the number of decimal places as needed
  }

  GetCurrentTime() {
    const date = new Date();
    const hours = date.getHours();
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const ampm = hours >= 12 ? 'PM' : 'AM';
    const formattedHours = (hours % 12 || 12).toString().padStart(2, '0');
    return `${formattedHours}:${minutes} ${ampm}`;
  }
  formatDate(date: Date): string {
    date = moment(date).toDate()
    date = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()))

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Zero-padding for single-digit months
    const day = String(date.getDate()).padStart(2, '0'); // Zero-padding for single-digit days
    return `${year}-${month}-${day}`;
  }
  parseAgeString(ageString: string): string {
    // Extract numeric parts (years, months, days) from the age string
    const parts = ageString.match(/\d+/g);
    if (!parts || parts.length !== 3) {
      return '';
    }

    // Calculate age in years
    const years = parseInt(parts[0], 10);
    const months = parseInt(parts[1], 10);
    const days = parseInt(parts[2], 10);

    // Convert age to a standard format (e.g., "9 Years")
    return `${years} Years`;
  }
  openImageInNewPage(base64Path: string) {
    // Calculate the window size for the popup
    const popupWidth = screen.width; // 100% width
    const popupHeight = screen.height; // 100% height

    // Open a new window with the calculated size
    const newWindow = window.open('', '_blank', `width=${popupWidth},height=${popupHeight}`);

    if (newWindow) {
      // Write the HTML content for the PDF-like viewer
      newWindow.document.write(`
        <html>
          <head>
            <title>PDF-Like Image Viewer</title>
            <style>
              body {
                margin: 0;
                display: flex;
                justify-content: center;
                align-items: center;
                background-color: #f0f0f0; /* Background color of the viewer */
                height: 100vh;
                overflow: auto;  /* Hide scrollbars in the parent window */
              }

              .pdf-viewer {
                max-width: 100%;
                max-height: 100%;
                /* Enable scrollbars when image is zoomed */
                background-color: #fff; /* White background for the viewer */
                border: 1px solid #ccc; /* Border around the viewer */
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5); /* Box shadow for the viewer */
              }

              img {
                max-width: 100%;
                max-height: 100%;
                border: 1px solid #ccc; 
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5);
                cursor: zoom-in; /* Cursor style for zooming */
              }

              img:hover {
                cursor: zoom-out; /* Cursor style for zooming out */
              }
            </style>
          </head>
          <body>
            <div class="pdf-viewer">
              <img id="zoomable-image" src="${base64Path}" alt="Zoomable Image" />
            </div>
            <script>
              const zoomableImage = document.getElementById('zoomable-image');
              let zoomLevel = 1;

              zoomableImage.addEventListener('click', () => {
                if (zoomLevel === 1) {
                  zoomLevel = 1.5;
                } else {
                  zoomLevel = 1;
                }
                zoomableImage.style.transform = 'scale(' + zoomLevel + ')';
              });
            </script>
          </body>
        </html>
      `);
    }
  }
  convertImageToPdf(base64Path) {
    const imageElement = new Image();
    imageElement.src = base64Path;
    imageElement.onload = async () => {
      const imgWidth = 200; // A4 size
      const imgHeight = (imageElement.height * imgWidth) / imageElement.width;
      const { PDFDocument, rgb } = require('pdf-lib');
      const pdfDoc = await PDFDocument.create();
      const page = pdfDoc.addPage([imgWidth, imgHeight]);
      const { width, height } = page.getSize();
      const base64Data = base64Path.split(',')[1];
      const uint8Array = new Uint8Array(atob(base64Data).split('').map(char => char.charCodeAt(0)));

      // Embed the image from Uint8Array
      const pngImage = await pdfDoc.embedPng(uint8Array);
      page.drawImage(pngImage, {
        x: 0,
        y: 0,
        width: width,
        height: height,
      });
      // Serialize the PDF to bytes
      const pdfBytes = await pdfDoc.save();
      // Create a Blob from the PDF data
      const pdfBlob = new Blob([pdfBytes], { type: 'application/pdf' });
      // Download or display the PDF
      const pdfUrl = URL.createObjectURL(pdfBlob);
      window.open(pdfUrl, '_blank');
    }
  }
  generatePDF(base64Data) {
    // Check if it's a PDF or an image based on MIME type
    const mimeType = base64Data.split(':')[1].split(';')[0];
    if (mimeType.startsWith('image')) {
      const imageElement = new Image();
      imageElement.src = base64Data;
      imageElement.onload = async () => {
        const imgWidth = 200; // A4 size
        const imgHeight = (imageElement.height * imgWidth) / imageElement.width;
        const { PDFDocument, rgb } = require('pdf-lib');
        const pdfDoc = await PDFDocument.create();
        const page = pdfDoc.addPage([imgWidth, imgHeight]);
        const { width, height } = page.getSize();
        const _base64Data = base64Data.split(',')[1];
        const uint8Array = new Uint8Array(atob(_base64Data).split('').map(char => char.charCodeAt(0)));

        // Embed the image from Uint8Array
        const pngImage = await pdfDoc.embedPng(uint8Array);
        page.drawImage(pngImage, {
          x: 0,
          y: 0,
          width: width,
          height: height,
        });
        // Serialize the PDF to bytes
        const pdfBytes = await pdfDoc.save();
        // Create a Blob from the PDF data
        const pdfBlob = new Blob([pdfBytes], { type: 'application/pdf' });
        // Download or display the PDF
        const pdfUrl = URL.createObjectURL(pdfBlob);
        window.open(pdfUrl, '_blank');
      }
    } else if (mimeType === 'application/pdf') {
      // Create a Blob from the base64 data
      const byteCharacters = atob(base64Data.split(',')[1]);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);

      // Create a Blob based on MIME type
      const blob = new Blob([byteArray], { type: 'application/pdf' });

      // Create a URL for the Blob and open it accordingly
      const url = URL.createObjectURL(blob);

      // It's a PDF
      window.open(url, '_blank');
    } else {
      console.error('Unsupported data type.');
    }

  }
  calculateTimeDifference(time1, time2) {
    debugger
    const timeFormat = /^(1[0-2]|0?[1-9]):([0-5][0-9])\s(AM|PM)$/i;

    const match1 = time1.match(timeFormat);
    const match2 = time2.match(timeFormat);

    if (!match1 || !match2) {
      // Invalid time format
      return NaN;
    }

    const hours1 = parseInt(match1[1], 10);
    const minutes1 = parseInt(match1[2], 10);
    const ampm1 = match1[3].toUpperCase();

    const hours2 = parseInt(match2[1], 10);
    const minutes2 = parseInt(match2[2], 10);
    const ampm2 = match2[3].toUpperCase();

    if (ampm1 !== ampm2) {
      // Time formats don't match (e.g., AM vs. PM)
      return NaN;
    }

    const minutesDifference = (hours2 * 60 + minutes2) - (hours1 * 60 + minutes1);

    return minutesDifference >= 0 ? minutesDifference : 720 + minutesDifference; // 720 minutes in 12 hours

  }
  calculateDOB(years, months, days) {
    debugger
    const today = new Date();
    let birthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
    if (years)
      birthDate.setFullYear(today.getFullYear() - years);
    if (months)
      birthDate.setMonth(today.getMonth() - months);
    if (days)
      birthDate.setDate(today.getDate() - days);
    return birthDate;
  }
  calculateAge(date): string {
    //return moment().diff(date, 'years');
    const today = new Date();
    const birthDate = new Date(date);

    const timeDifference = today.getTime() - birthDate.getTime();
    const daysDifference = Math.floor(timeDifference / (1000 * 60 * 60 * 24));

    if (daysDifference < 30) {
      return `${daysDifference} day${daysDifference === 1 ? '' : 's'}`;
    } else if (daysDifference < 365) {
      const monthDiff = Math.floor(daysDifference / 30);
      return `${monthDiff} month${monthDiff === 1 ? '' : 's'}`;
    } else {
      let years = today.getFullYear() - birthDate.getFullYear();
      const monthDifference = today.getMonth() - birthDate.getMonth();
      if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < birthDate.getDate())) {
        years--;
      }
      return `${years} year${years === 1 ? '' : 's'}`;
    }
  }
  CalculateAge(date) {
    const today = new Date();
    const birthDate = new Date(date);
    const yearsDiff = today.getFullYear() - birthDate.getFullYear();
    const monthsDiff = today.getMonth() - birthDate.getMonth();
    const daysDiff = today.getDate() - birthDate.getDate();

    return {
      years: yearsDiff,
      months: monthsDiff < 0 ? 12 + monthsDiff : monthsDiff,
      days: daysDiff < 0 ? 30 + daysDiff : daysDiff, // Assuming 30 days per month for simplicity
    };
  }
  GetAge(date) {
    const today = new Date();
    const birthDate = new Date(date);
    const yearsDiff = today.getFullYear() - birthDate.getFullYear();
    const monthsDiff = today.getMonth() - birthDate.getMonth();
    const daysDiff = today.getDate() - birthDate.getDate();
    var year = yearsDiff;
    var months = monthsDiff < 0 ? 12 + monthsDiff : monthsDiff;
    var days = daysDiff < 0 ? 30 + daysDiff : daysDiff;
    var age = (year > 0 ? year + " Year(s)" : '').toString() + " " + (months > 0 ? months + " Month(s)" : "").toString() + " " + (days > 0 ? days + " Day(s)" : "").toString()
    //age = (year > 0 ? year : 0).toString()
    return age
  }
  GetAgeYear(date) {
    const today = new Date();
    const birthDate = new Date(date);
    const yearsDiff = today.getFullYear() - birthDate.getFullYear();
    const monthsDiff = today.getMonth() - birthDate.getMonth();
    const daysDiff = today.getDate() - birthDate.getDate();
    var year = yearsDiff;
    var months = monthsDiff < 0 ? 12 + monthsDiff : monthsDiff;
    var days = daysDiff < 0 ? 30 + daysDiff : daysDiff;
    var age = (year > 0 ? year + " Year(s)" : '').toString() + " " + (months > 0 ? months + " Month(s)" : "").toString() + " " + (days > 0 ? days + " Day(s)" : "").toString()
    age = (year > 0 ? year : 0).toString()
    return age;
  }
  getCapitalLettersAsString(inputString: string): string {
    const capitalLettersArray = inputString.match(/[A-Z]/g);
    if (!capitalLettersArray) {
      return "";
    }
    const capitalLettersString = capitalLettersArray.join("");
    return capitalLettersString;
  }
  setDate(date) {
    if (date != null) {
      date = moment(date).toDate()
      date = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()))
    }
    return date
  }

  SuccessfullyUpdateMsg() {
    this.SuccessMsgBar(" Successfully Updated!", 5000)
  }
  SuccessfullyAddMsg() {
    this.SuccessMsgBar(" Successfully Added!", 5000)
  }
  SuccessMsgBar(Message: string, Duration?: number) {
    if (Duration == undefined || Duration == null)
      Duration = 5000
    this.snack.open(Message, 'Ok', { duration: Duration, verticalPosition: 'bottom', panelClass: ['blue-snackbar'] });
  }
  InfoMsgBar(Message: string, Duration?: number) {
    if (Duration == undefined || Duration == null)
      Duration = 5000
    this.snack.open(Message, 'Ok', { duration: Duration, verticalPosition: 'bottom', panelClass: ['green-snackbar'] });
  }
  ErrorMsgBar(Message?: string, Duration?: number) {
    if (Duration == undefined)
      Duration = 5000
    if (Message == null)
      Message = "Error Occurred"
    this.snack.open(Message, 'Close', { duration: Duration, verticalPosition: 'bottom', panelClass: ['red-snackbar'] });
  }

  GetAgeByDOB(value): Observable<any> {
    console.warn(value)
    return this.http.post<any>(Globals.BASE_API_URL + 'Common/dob?', value);
  }

  UpdateSettings(value: SettingsVM) {
    return this.http.put(Globals.BASE_API_URL + 'Settings', value);
  }
  GetSettingsById(id: number): Observable<SettingsVM> {
    return this.http.get<SettingsVM>(Globals.BASE_API_URL + 'Settings/' + id).pipe()
  }
  SearchSettings(value: SettingsVM): Observable<SettingsVM[]> {
    return this.http.post<SettingsVM[]>(Globals.BASE_API_URL + 'Settings/Search', value).pipe();
  }
  SearchMenu(value: SettingsVM): Observable<SettingsVM[]> {
    return this.http.post<SettingsVM[]>(Globals.BASE_API_URL + 'Settings/Menu', value).pipe();
  }
  SearchActiveMenu(value): Observable<SettingsVM[]> {
    return this.http.post<SettingsVM[]>(Globals.BASE_API_URL + 'Settings/ActiveMenu', value).pipe();
  }
  SearchEnumLine(value: SettingsVM): Observable<SettingsVM[]> {
    return this.http.post<SettingsVM[]>(Globals.BASE_API_URL + 'Settings/EnumLine', value).pipe();
  }
  SearchAccounts(value: SettingsVM): Observable<SettingsVM[]> {
    return this.http.post<SettingsVM[]>(Globals.BASE_API_URL + 'Settings/Accounts', value).pipe();
  }
  GetSettings(): Observable<SettingsVM[]> {
    return this.http.get<SettingsVM[]>(Globals.BASE_API_URL + 'Settings').pipe();
  }
  SaveSettings(value: SettingsVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'Settings', value);
  }
  DeleteSettings(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Settings/' + id);
  }
  SearchStngByCode(Id: number, KeyCode: string): Observable<SettingsVM[]> {
    return this.http.get<SettingsVM[]>(Globals.BASE_API_URL + 'Settings/' + Id + "/" + KeyCode).pipe();
  }


  UpdateSettingsType(value: SettingsTypeVM): Observable<SettingsTypeVM> {
    return this.http.put<SettingsTypeVM>(Globals.BASE_API_URL + 'SettingsType', value);
  }
  GetSettingsTypeById(id: number): Observable<SettingsTypeVM> {
    return this.http.get<SettingsTypeVM>(Globals.BASE_API_URL + 'SettingsType/' + id).pipe()
  }
  SearchSettingsType(value: SettingsTypeVM): Observable<SettingsTypeVM[]> {
    return this.http.post<SettingsTypeVM[]>(Globals.BASE_API_URL + 'SettingsType/Search', value).pipe();
  }
  SearchEnums(value: SettingsTypeVM): Observable<SettingsTypeVM[]> {
    return this.http.post<SettingsTypeVM[]>(Globals.BASE_API_URL + 'SettingsType/Enum', value).pipe();
  }
  GetSettingsType(): Observable<SettingsTypeVM[]> {
    return this.http.get<SettingsTypeVM[]>(Globals.BASE_API_URL + 'SettingsType').pipe();
  }
  SaveSettingsType(value: SettingsTypeVM): Observable<SettingsTypeVM> {
    return this.http.post<SettingsTypeVM>(Globals.BASE_API_URL + 'SettingsType', value);
  }
  DeleteSettingsType(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'SettingsType/' + id);
  }


  UpdateNotificationTemplate(NotificationTemplate: NotificationTemplateVM) {
    return this.http.put(Globals.BASE_API_URL + 'NotificationTemplate', NotificationTemplate);
  }
  GetNotificationTemplateById(id: number): Observable<NotificationTemplateVM> {

    return this.http.get<NotificationTemplateVM>(Globals.BASE_API_URL + 'NotificationTemplate/' + id).pipe()
  }
  SearchNotificationTemplate(NotificationTemplate: NotificationTemplateVM): Observable<NotificationTemplateVM[]> {
    return this.http.post<NotificationTemplateVM[]>(Globals.BASE_API_URL + 'NotificationTemplate/Search', NotificationTemplate).pipe();
  }
  GetNotificationTemplate(): Observable<NotificationTemplateVM[]> {
    return this.http.get<NotificationTemplateVM[]>(Globals.BASE_API_URL + 'NotificationTemplate').pipe();
  }
  SaveNotificationTemplate(NotificationTemplate: NotificationTemplateVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'NotificationTemplate', NotificationTemplate);
  }
  DeleteNotificationTemplate(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'NotificationTemplate/' + id);
  }


  SendMail(Mail: NotificationVM) {
    return this.http.post(Globals.BASE_API_URL + 'Notification', Mail);
  }


  UpdateClients(value: ClientsVM): Observable<ClientsVM> {
    return this.http.put<ClientsVM>(Globals.BASE_API_URL + 'Client', value);
  }
  GetClientsById(id: number): Observable<ClientsVM[]> {
    return this.http.get<ClientsVM[]>(Globals.BASE_API_URL + 'Client/' + id).pipe()
  }
  SearchClients(value: ClientsVM): Observable<ClientsVM[]> {
    return this.http.post<ClientsVM[]>(Globals.BASE_API_URL + 'Client/Search', value).pipe();
  }
  GetClients(): Observable<ClientsVM[]> {
    return this.http.get<ClientsVM[]>(Globals.BASE_API_URL + 'Client').pipe();
  }
  SaveClients(value: ClientsVM): Observable<ClientsVM> {
    return this.http.post<ClientsVM>(Globals.BASE_API_URL + 'Client', value);
  }
  DeleteClients(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Client/' + id);
  }


  GetSMTPCreds(): Observable<SMTPCredsVM[]> {
    return this.http.get<SMTPCredsVM[]>(Globals.BASE_API_URL + 'SMTPCredentials').pipe();
  }
  GetSMTPCredsById(id: number): Observable<SMTPCredsVM[]> {
    return this.http.get<SMTPCredsVM[]>(Globals.BASE_API_URL + 'SMTPCredentials/' + id).pipe()
  }
  SaveSMTPCreds(value: SMTPCredsVM) {
    return this.http.post(Globals.BASE_API_URL + 'SMTPCredentials', value);
  }
  UpdateSMTPCreds(value: SMTPCredsVM) {
    return this.http.put(Globals.BASE_API_URL + 'SMTPCredentials', value);
  }
  DeleteSMTPCreds(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'SMTPCredentials/' + id);
  }
  SearchSMTPCreds(value: SMTPCredsVM): Observable<SMTPCredsVM[]> {
    return this.http.post<SMTPCredsVM[]>(Globals.BASE_API_URL + 'SMTPCredentials/Search', value).pipe();
  }


  SetProject(data) {
    console.warn(data)
    var perms = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_PERMISSIONS)
    localStorage.clear()
    if (data.permissions)
      localStorage.setItem(AppConstants.LOCAL_STORAGE_PERMISSIONS, JSON.stringify(data.permissions))
    else
      localStorage.setItem(AppConstants.LOCAL_STORAGE_PERMISSIONS, perms)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_ROLE, data.role)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_CLT_ID, data.cltId)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_MODULE_IDS, data.moduleIds)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_CLIENT, data.client)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_USER_NAME, data.userName)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_USER_ID, data.id)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_CLIENT_ID, data.clientId)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_ROLE_ID, data.roleId)
    debugger
    if (data.roleId == Roles.SuperAdmin && data.cltId == 0) {
      data.superAdminId = data.id
      data.superAdminName = data.userName
    }
    localStorage.setItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID, data.superAdminId)
    localStorage.setItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_NAME, data.superAdminName)
    if (data.doctorId > 0) {
      localStorage.setItem("DoctorId", data.doctorId)
      this.route.navigate(['/pms/rx/prescription'], {
        queryParams: {
          openOnLogin: true
        }
      });
    }
    // else if (data.roleId == Roles.Staff)
    //   this.route.navigate(['/pms/appt/appmntList']);
    else if (data.roleId == Roles.SuperAdmin && data.cltId == 0)
      this.route.navigate(['/client/List/cltList']);
    else
      this.route.navigate(['/catalog/home']);
  }
  setToken(token) {
    localStorage.setItem(AppConstants.LOCAL_STORAGE_TOKEN, token)
  }
  openCltData(clt: ClientsVM) {
    var _data = new StorageData()
    _data.role = "Client Admin"
    _data.roleId = Roles.ClientAdmin
    _data.id = clt.userId
    _data.token = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_TOKEN)
    _data.moduleIds = clt.moduleIds
    _data.clientId = clt.id
    _data.client = clt.clientName
    _data.name = clt.user
    _data.cltId = clt.id
    _data.superAdminId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID)
    _data.superAdminName = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_NAME)
    this.SetProject(_data)
  }
  CheckandSet() {
    var roleId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID)
    if (roleId == Roles.Doctor) {
      this.route.navigate(['/pms/rx/prescription'], {
        queryParams: {
          openOnLogin: true
        }
      });
    }
    else if (roleId == Roles.Staff)
      this.route.navigate(['/pms/appt/appmntList']);
    else if (roleId == Roles.SuperAdmin)
      this.route.navigate(['/client/List/cltList']);
    else this.route.navigate(['/catalog/home']);
  }
  getPermission(keyCode): boolean {
    var isReadOnly: boolean = null
    var perms = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_PERMISSIONS)
    if (perms != null)
      this.permissions = JSON.parse(perms)
    if (this.permissions != null && this.permissions.length > 0) {
      var perm = this.permissions.find(x => x.routeId == keyCode)
      if (perm != null)
        isReadOnly = perm.isReadOnly
    }
    return isReadOnly;
  }
  sendMailwithPdf(mail: NotificationVM): Observable<any> {
    const formData = new FormData();
    formData.append('attachment', mail.attachment, "myFile.pdf");
    formData.append('senderMail', mail.senderMail);
    formData.append('receiverMail', mail.receiverMail);
    formData.append('userName', mail.userName);
    formData.append('port', mail.port);
    formData.append('password', mail.password);
    formData.append('server', mail.server);
    formData.append('mailSubject', mail.mailSubject);
    formData.append('mailBody', mail.mailBody);
    return this.http.post(Globals.BASE_API_URL + 'Notification/Mail', formData)
  }
}