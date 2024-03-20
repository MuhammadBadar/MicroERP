import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import * as moment from 'moment';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import Swal from 'sweetalert2';
import { PMSService } from '../../pms.service';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';

@Component({
  selector: 'app-rx-med-extra-fields',
  templateUrl: './rx-med-extra-fields.component.html',
  styleUrls: ['./rx-med-extra-fields.component.css']
})
export class RxMedExtraFieldsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'actions'];
  default = "default"
  AddMode: boolean = true
  EditMode: boolean = false
  LineAddMode: boolean = true
  LineEditMode: boolean = false
  proccessing: boolean = false;
  dataSource: any
  hide = true;
  patFields: SettingsTypeVM[] = []
  selectedRxMedicineField: SettingsTypeVM
  selectedValues: SettingsVM
  selectedVal: SettingsVM
  valuesList: SettingsVM[] = []
  @ViewChild('RxMedicineFieldForm', { static: true }) RxMedicineFieldForm: NgForm;
  fieldTypes: SettingsVM[]
  dialogRef: any
  dialogData: any;
  inputTypes: SettingsVM[]
  constructor(
    private injector: Injector,
    private pmsSvc: PMSService,
    private catSvc: CatalogService) {
    this.selectedRxMedicineField = new SettingsTypeVM
    this.selectedRxMedicineField.keyCode = "default"
    this.selectedValues = new SettingsVM
    this.selectedVal = new SettingsVM
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    this.selectedRxMedicineField = new SettingsTypeVM
    this.selectedValues = new SettingsVM
    this.valuesList = []
    this.GetSettings(EnumTypes.FieldTypes);
    this.GetSettings(EnumTypes.InputTypes);
    this.selectedRxMedicineField.isActive = true;
    if (this.dialogData != null) {
      if (this.dialogData.fieldId != undefined) {
        this.EditRxMedicineField(this.dialogData.fieldId)
      }
    }
  }

  GetSettings(enumType: EnumTypes) {
    var stng = new SettingsVM
    stng.isActive = true;
    stng.enumTypeId = enumType
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        if (enumType == EnumTypes.FieldTypes)
          this.fieldTypes = res
        if (enumType == EnumTypes.InputTypes)
          this.inputTypes = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  AddPossibleValueToList() {
    if (this.selectedValues.name != null || this.selectedValues.name != "") {
      this.selectedValues.keyCode = this.selectedValues.name.replaceAll(' ', '_')
      this.selectedValues.value = this.selectedValues.name
      this.selectedValues.isActive = true
      if (this.LineEditMode && this.selectedValues.id > 0)
        this.selectedValues.dBoperation = 2
      else if (!this.LineEditMode) {
        this.selectedValues.dBoperation = 1
        this.valuesList.push(this.selectedValues)
      }
      this.dataSource = new MatTableDataSource(this.valuesList)
      this.selectedValues = new SettingsVM
      this.LineAddMode = true
      this.LineEditMode = false
    } else {
      this.catSvc.ErrorMsgBar("Please fill required field", 5000)
    }
  }
  RefreshDetail() {
    this.selectedValues = new SettingsVM
    this.LineAddMode = true
    this.LineEditMode = false
    //this.ngOnInit()
  }
  edit(val: SettingsVM) {

    this.selectedValues = val
    this.LineAddMode = false
    this.LineEditMode = true
  }
  delete(val: SettingsVM) {
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
        if (val.id > 0) {
          var type = new SettingsTypeVM
          type = this.selectedRxMedicineField
          type.settingList = []
          var stng = new SettingsVM
          stng = val
          stng.dBoperation = 3
          type.settingList.push(stng)
          this.catSvc.UpdateSettingsType(type).subscribe({
            next: (data: SettingsTypeVM) => {
              Swal.fire(
                'Deleted!',
                'Topic has been deleted.',
                'success'
              )
              this.valuesList = []
              this.selectedRxMedicineField = data
              this.valuesList = data.settingList
              this.dataSource = new MatTableDataSource(this.valuesList)
            }, error: (e) => {
              this.catSvc.ErrorMsgBar("Error Occurred", 5000)
              console.warn(e);
            }
          })
        } else {
          this.valuesList = this.valuesList.filter(x => x != val)
          this.dataSource = new MatTableDataSource(this.valuesList)
        }
      }
    })
  }
  SaveRxMedicineField() {
    if (this.selectedRxMedicineField.keyCode == "default" || this.selectedRxMedicineField.keyCode == null)
      this.RxMedicineFieldForm.controls["keyCode"].setErrors({ "incorrect": true })
    if (!this.RxMedicineFieldForm.invalid) {
      this.selectedRxMedicineField.parentId = EnumTypes.RxExtraFields
      this.selectedRxMedicineField.settingList = this.valuesList
      this.selectedRxMedicineField.clientId = +localStorage.getItem("ClientId")
      this.selectedRxMedicineField.moduleId = Modules.PMS
      if (this.EditMode)
        this.UpdateRxMedicineField()
      else {
        this.catSvc.SaveSettingsType(this.selectedRxMedicineField).subscribe({
          next: (value) => {
            this.catSvc.SuccessMsgBar("Successfully Added", 5000);
            this.Refresh();
          },
          error: (err) => {
            console.warn(err)
            this.catSvc.ErrorMsgBar("Error Occurred", 5000);
          },
        });
      }
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
  }
  EditRxMedicineField(id) {
    this.selectedRxMedicineField = new SettingsTypeVM;
    this.selectedRxMedicineField.id = id
    this.selectedRxMedicineField.parentId = EnumTypes.RxExtraFields
    this.selectedRxMedicineField.clientId = +localStorage.getItem("ClientId")
    this.selectedRxMedicineField.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(this.selectedRxMedicineField).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.selectedRxMedicineField = res[0]
        this.valuesList = res[0].settingList
        this.dataSource = new MatTableDataSource(this.valuesList)
        this.EditMode = true;
        this.AddMode = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  UpdateRxMedicineField() {
    this.proccessing = true;
    this.catSvc.UpdateSettingsType(this.selectedRxMedicineField).subscribe({
      next: (value) => {
        this.catSvc.SuccessMsgBar("Successfully Updated", 5000);
        this.Refresh()
      },
      error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000);
        console.warn(err);
        this.proccessing = false;
      }
    });
  }
  Refresh() {
    this.ngOnInit();
    this.proccessing = false
    this.AddMode = true;
    this.EditMode = false;
    this.selectedRxMedicineField.keyCode = "default"
  }

}


