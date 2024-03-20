import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { CatalogService } from '../../catalog.service';
import { SMTPCredsVM } from '../../Models/SMTPCredsVM';

@Component({
  selector: 'app-manage-smtpcreds',
  templateUrl: './manage-smtpcreds.component.html',
  styleUrls: ['./manage-smtpcreds.component.css']
})
export class ManageSMTPCredsComponent implements OnInit {
  displayedColumns: string[] = ['id', 'patientname', 'date', 'time', 'age', 'gender', 'actions'];
  AddMode: boolean = true
  isReadonly: boolean = false
  EditMode: boolean = false
  dataSource: any
  selectedSMTPCreds: SMTPCredsVM
  SMTPCredss?: SMTPCredsVM[]
  dialogref: any
  dialogRef: any
  dialogData: any;
  disableClose: any
  isDialog: boolean = false;
  proccessing: boolean = false;
  DisabledType: boolean = false;
  hide = true;
  filteredData: any;
  searchValue?: any
  docSearchValue?: any
  filteredDocData: any;
  clientId: number
  @ViewChild('SMTPCredsForm', { static: true }) SMTPCredsForm: NgForm;
  constructor(
    private injector: Injector,
    private catSvc: CatalogService,
    private dialog: MatDialog) {
    this.dialogref = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedSMTPCreds = new SMTPCredsVM();
    this.selectedSMTPCreds.isActive = true
  }
  ngOnInit(): void {
    this.EditMode = false
    this.AddMode = true
    this.selectedSMTPCreds = new SMTPCredsVM
    this.GetSMTPCreds();
    this.selectedSMTPCreds.clientId = +localStorage.getItem("ClientId")
    if (this.dialogData != null) {
      if (this.dialogData.cltId != undefined) {
        this.clientId = this.dialogData.cltId
        this.SearchSMPTByClient(this.dialogData.cltId)
      }
    }
  }

  GetSMTPCreds() {
    var appt = new SMTPCredsVM
    this.catSvc.SearchSMTPCreds(appt).subscribe({
      next: (res: SMTPCredsVM[]) => {
        this.SMTPCredss = res
        this.dataSource = new MatTableDataSource(this.SMTPCredss)
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  SaveSMTPCreds() {
    if (this.SMTPCredsForm.invalid) {
      const controls = this.SMTPCredsForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          this.catSvc.ErrorMsgBar(` ${name} is required field`, 6000)
          break;
        }
      }
    } else {
      this.proccessing = true;
      if (this.EditMode) {
        this.UpdateSMTPCreds();
      } else {
        this.catSvc.SaveSMTPCreds(this.selectedSMTPCreds).subscribe({
          next: (res) => {
            this.catSvc.SuccessMsgBar("Successfully Added!", 6000);
            this.ngOnInit();
            //this.Refresh();
            window.scrollTo(0, 0);
            this.proccessing = false;
          },
          error: (e) => {
            console.warn(e);
            this.catSvc.ErrorMsgBar("Error Occurred!", 6000);
            this.proccessing = false;
          }
        });
      }
    }
  }
  SearchSMPTByClient(id) {
    this.selectedSMTPCreds = new SMTPCredsVM;
    this.selectedSMTPCreds.clientId = this.clientId
    this.catSvc.SearchSMTPCreds(this.selectedSMTPCreds).subscribe({
      next: (res: SMTPCredsVM[]) => {
        if (res.length > 0) {
          this.selectedSMTPCreds = res[0]
          this.EditMode = true;
          this.AddMode = false;
        }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  EditSMTPCreds(id) {
    this.selectedSMTPCreds = new SMTPCredsVM;
    this.selectedSMTPCreds.id = id
    this.catSvc.SearchSMTPCreds(this.selectedSMTPCreds).subscribe({
      next: (res: SMTPCredsVM[]) => {
        this.selectedSMTPCreds = res[0]
        this.EditMode = true;
        this.AddMode = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  UpdateSMTPCreds() {
    this.catSvc.UpdateSMTPCreds(this.selectedSMTPCreds).subscribe({
      next: (value) => {
        this.catSvc.SuccessMsgBar("Successfully Updated", 5000);
        this.ngOnInit();
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000);
      }
    });
  }
  Refresh() {
    this.ngOnInit()
  }


}
