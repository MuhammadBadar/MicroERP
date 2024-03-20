import { ChangeDetectorRef, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SettingsVM } from '../Models/SettingsVM';
import { NgForm } from '@angular/forms';
import { CatalogService } from '../catalog.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SettingsTypeVM } from '../Models/SettingsTypeVM';
import { Modules } from '../Models/Enums/Modules';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manage-enum-line',
  templateUrl: './manage-enum-line.component.html',
  styleUrls: ['./manage-enum-line.component.css']
})
export class ManageEnumLineComponent implements OnInit {
  Edit: boolean = false;
  Add: boolean = true;
  EnumLine: SettingsVM[] = [];
  selectedEnumLine: SettingsVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['name', 'isActive', 'actions'];
  dataSource: any;
  dialogData;
  selectedEnum: SettingsTypeVM
  isDialog: boolean = false
  dialogref: any
  dialogRef: any
  enumTypeId
  moduleId: number = 0
  clientId: number = 0
  constructor(
    private injector: Injector,
    private dialog: MatDialog,
    private aRoute: ActivatedRoute,
    private catSvc: CatalogService,
    private cdRef: ChangeDetectorRef,
  ) {
    this.dialogref = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedEnumLine = new SettingsVM();
    this.selectedEnum = new SettingsTypeVM
  }
  ngOnInit(): void {
    //this.Refresh()
    if (this.dialogData != null) {
      this.isDialog = this.dialogData.isDialog;
      if (this.isDialog)
        if (this.dialogData.moduleId != undefined) {
          this.moduleId = this.dialogData.moduleId
          this.selectedEnumLine.moduleId = this.dialogData.moduleId
        }
      if (this.dialogData.clientId != undefined) {
        this.clientId = this.dialogData.clientId
        this.selectedEnumLine.clientId = this.dialogData.clientId
      }
      if (this.dialogData.enumTypeId != undefined) {
        this.enumTypeId = this.dialogData.enumTypeId
        this.GetEnum()
        this.GetEnumLine()
      }
    }
    if (!this.isDialog)
      this.aRoute.queryParams.subscribe(params => {
        var type = params['type'];
        if (type > 0) {
          this.enumTypeId = type
          this.moduleId = 0
        }
        this.moduleId = params['module'];
        this.clientId = +localStorage.getItem("ClientId")
        this.selectedEnumLine.clientId = this.clientId
        this.GetEnum()
        this.GetEnumLine()
      });
    this.selectedEnumLine.isActive = true
  }
  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }
  GetEnum() {
    var type = new SettingsTypeVM
    type.id = this.enumTypeId
    // type.moduleId = this.moduleId
    this.catSvc.SearchSettingsType(type).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.selectedEnum = res[0];
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetEnumLine() {
    debugger
    var stng = new SettingsVM
    stng.enumTypeId = this.enumTypeId
    stng.moduleId = this.moduleId
    stng.clientId = this.clientId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.EnumLine = res;
        this.dataSource = new MatTableDataSource(this.EnumLine);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetEnumLineForEdit(id: number) {
    window.scrollTo(0, 0)
    var stng = new SettingsVM;
    stng.id = id
    stng.enumTypeId = this.enumTypeId
    stng.moduleId = this.moduleId
    stng.clientId = this.clientId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.selectedEnumLine = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveEnumLine() {
    if (!this.userForm.invalid) {
      this.selectedEnumLine.enumTypeId = this.enumTypeId
      this.selectedEnumLine.moduleId = this.moduleId
      this.selectedEnumLine.clientId = this.clientId
      this.selectedEnumLine.value = this.selectedEnumLine.name
      this.selectedEnumLine.keyCode = this.selectedEnumLine.name.replaceAll(" ", "_")
      if (this.Edit)
        this.UpdateEnumLine()
      else {
        this.catSvc.SaveSettings(this.selectedEnumLine).subscribe({
          next: (res) => {
            this.catSvc.SuccessfullyAddMsg()
            this.Reset();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    } else
      this.catSvc.ErrorMsgBar("Please Fill required field!", 5000)
  }
  UpdateEnumLine() {
    this.catSvc.UpdateSettings(this.selectedEnumLine).subscribe({
      next: (res) => {
        this.catSvc.SuccessfullyUpdateMsg()
        this.Reset();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.EnumLine = []
    this.Add = true;
    this.Edit = false;
    this.selectedEnumLine = new SettingsVM
    this.selectedEnumLine.moduleId = this.moduleId
    this.selectedEnumLine.clientId = this.clientId
    this.GetEnumLine()
    this.selectedEnumLine.isActive = true
  }
  Reset() {
    this.Add = true;
    this.Edit = false;
    this.selectedEnumLine = new SettingsVM
    this.selectedEnumLine.moduleId = this.moduleId
    this.selectedEnumLine.clientId = this.clientId
    this.GetEnumLine()
    this.selectedEnumLine.isActive = true
  }
}


