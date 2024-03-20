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
import { PatientVM } from '../../Models/PatientVM';
import { PMSService } from '../../pms.service';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';

@Component({
  selector: 'app-PatientField-fields',
  templateUrl: './patient-fields.component.html',
  styleUrls: ['./patient-fields.component.css']
})
export class PatientFieldsComponent implements OnInit {
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
  selectedPatientField: SettingsTypeVM
  selectedValues: SettingsVM
  selectedVal: SettingsVM
  valuesList: SettingsVM[] = []
  @ViewChild('PatientFieldForm', { static: true }) PatientFieldForm: NgForm;
  fieldTypes: SettingsVM[]
  inputTypes: SettingsVM[]
  dialogRef: any
  dialogData: any;
  constructor(
    private injector: Injector,
    private pmsSvc: PMSService,
    private catSvc: CatalogService) {
    this.selectedPatientField = new SettingsTypeVM
    this.selectedPatientField.keyCode = "default"
    this.selectedValues = new SettingsVM
    this.selectedVal = new SettingsVM
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    this.selectedPatientField = new SettingsTypeVM
    this.selectedValues = new SettingsVM
    this.valuesList = []
    this.GetPatientFields();
    this.GetSettings(EnumTypes.FieldTypes);
    this.GetSettings(EnumTypes.InputTypes);
    this.selectedPatientField.isActive = true;
    if (this.dialogData != null) {
      if (this.dialogData.fieldId != undefined) {
        this.EditPatientField(this.dialogData.fieldId)
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
  GetPatientFields() {
    // var type = new SettingsTypeVM
    // type.isActive = true
    // this.catSvc.SearchSettingsType(type).subscribe({
    //   next: (value: SettingsTypeVM[]) => {
    //     this.patFields = value
    //     this.dataSource = new MatTableDataSource(this.patFields)
    //   }, error: (err) => {
    //     alert('Error to retrieve PatientField');
    //     this.catSvc.ErrorMsgBar("Error Occurred", 5000)
    //   },
    // })
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
          type = this.selectedPatientField
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
              this.selectedPatientField = data
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
  SavePatientField() {
    if (this.selectedPatientField.keyCode == "default" || this.selectedPatientField.keyCode == null)
      this.PatientFieldForm.controls["keyCode"].setErrors({ "incorrect": true })
    if (!this.PatientFieldForm.invalid) {
      this.selectedPatientField.parentId = EnumTypes.PatientFields
      this.selectedPatientField.settingList = this.valuesList
      this.selectedPatientField.clientId = +localStorage.getItem("ClientId")
      this.selectedPatientField.moduleId = Modules.PMS
      if (this.EditMode)
        this.UpdatePatientField()
      else {
        this.catSvc.SaveSettingsType(this.selectedPatientField).subscribe({
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
  EditPatientField(id) {
    this.selectedPatientField = new SettingsTypeVM;
    this.selectedPatientField.id = id
    this.selectedPatientField.parentId = EnumTypes.PatientFields
    this.selectedPatientField.clientId = +localStorage.getItem("ClientId")
    this.selectedPatientField.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(this.selectedPatientField).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.selectedPatientField = res[0]
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
  UpdatePatientField() {
    this.proccessing = true;
    this.catSvc.UpdateSettingsType(this.selectedPatientField).subscribe({
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
    this.selectedPatientField.keyCode = "default"
  }
  DeletePatientField(id: number) {
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
        this.pmsSvc.DeletePatient(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Topic has been deleted.',
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
}

