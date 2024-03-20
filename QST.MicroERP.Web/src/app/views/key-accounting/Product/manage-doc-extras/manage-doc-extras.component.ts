import { ChangeDetectorRef, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DocExtrasVM } from '../../Models/DocExtrasVM';
import { KeyAccountingService } from '../../key-accounting.service';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { ItemsService } from 'src/app/views/items/items.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';



@Component({
  selector: 'app-manage-doc-extras',
  templateUrl: './manage-doc-extras.component.html',
  styleUrls: ['./manage-doc-extras.component.css']
})
export class ManageDocExtrasComponent implements OnInit {
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  DocExtras: DocExtrasVM[] = [];
  selectedDocExtras: DocExtrasVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['docExtraType', 'docExtra', 'incDecType', 'formula', 'value', 'isActive', 'actions'];
  dataSource: any;
  dialogData;
  isDialog: boolean = false
  dialogref: any
  dialogRef: any
  formulas: SettingsVM[]
  docExtrasTypes: SettingsTypeVM[]
  documentExtras: SettingsVM[] = []
  incDecTypes: SettingsVM[]
  constructor(
    private injector: Injector,
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    private dialog: MatDialog,
    private cdRef: ChangeDetectorRef,
  ) {
    this.dialogref = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedDocExtras = new DocExtrasVM();
  }
  ngOnInit(): void {
    this.Refresh()
    this.GetDocExtras();
    this.GetDocExtraTyes()
    this.GetSettings(EnumTypes.Formulas)
    this.GetSettings(EnumTypes.DocExtrasIncDecType)
    this.selectedDocExtras.isActive = true
  }
  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }
  GetDocExtras() {
    var doc = new DocExtrasVM
    doc.isActive = true
    this.accSvc.SearchDocExtras(doc).subscribe({
      next: (res: DocExtrasVM[]) => {
        this.DocExtras = res;
        console.warn(res)
        this.dataSource = new MatTableDataSource(this.DocExtras);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetDocExtraTyes() {
    var type = new SettingsTypeVM()
    type.parentId = EnumTypes.DocExtrasTypes
    type.isActive = true
    this.catSvc.SearchSettingsType(type).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.docExtrasTypes = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        console.warn(e);
      }
    })
  }
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype == EnumTypes.Formulas)
          this.formulas = res;
        if (etype == EnumTypes.DocExtrasIncDecType) {
          this.incDecTypes = res;
        }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        console.warn(e);
      }
    })
  }
  SearchDocExtraValues(id) {
    var setting = new SettingsVM()
    setting.enumTypeId = id
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        this.documentExtras = res
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        console.warn(e);
      }
    })
  }
  DeleteDocExtras(val: DocExtrasVM) {
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
        this.accSvc.DeleteDocExtras(val.id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Successfuly Deleted.',
              'success'
            )
            this.Refresh();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetDocExtrasForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedDocExtras = new DocExtrasVM;
    this.selectedDocExtras.id = id
    this.accSvc.SearchDocExtras(this.selectedDocExtras).subscribe({
      next: (res: DocExtrasVM[]) => {
        this.DocExtras = res;
        this.selectedDocExtras = this.DocExtras[0]
        this.SearchDocExtraValues(this.selectedDocExtras.docExtraTypeId)
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  CheckValidation() {
    debugger
    if (this.selectedDocExtras.docExtraTypeId == 0 || this.selectedDocExtras.docExtraTypeId == undefined)
      this.userForm.controls['docExtraTypeId'].setErrors({ 'incorrect': true })
    if (this.documentExtras.length > 0)
      if (this.selectedDocExtras.docExtraId == 0 || this.selectedDocExtras.docExtraId == undefined)
        this.userForm.controls['docExtraId'].setErrors({ 'incorrect': true })
    if (this.selectedDocExtras.formulaId == 0 || this.selectedDocExtras.formulaId == undefined)
      this.userForm.controls['formulaId'].setErrors({ 'formulaId': true })
    if (this.selectedDocExtras.incDecTypeId == 0 || this.selectedDocExtras.incDecTypeId == undefined)
      this.userForm.controls['incDecTypeId'].setErrors({ 'incorrect': true })
  }
  SaveDocExtras() {
    this.CheckValidation()
    if (!this.userForm.invalid) {
      if (this.Edit)
        this.UpdateDocExtras()
      else {
        this.accSvc.SaveDocExtras(this.selectedDocExtras).subscribe({
          next: (res) => {
            this.catSvc.SuccessfullyAddMsg()
            this.Refresh();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    } else
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
  }
  UpdateDocExtras() {
    this.accSvc.UpdateDocExtras(this.selectedDocExtras).subscribe({
      next: (res) => {
        this.catSvc.SuccessfullyUpdateMsg()
        this.Refresh();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.documentExtras = []
    this.Add = true;
    this.Edit = false;
    this.selectedDocExtras = new DocExtrasVM
    this.GetDocExtras()
    this.selectedDocExtras.isActive = true
  }


}

