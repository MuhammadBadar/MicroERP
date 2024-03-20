import { ChangeDetectorRef, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
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
import { ManageEnumLineWithParentComponent } from 'src/app/views/catalog/manage-enum-line-with-parent/manage-enum-line-with-parent.component';
import { ManageEnumLineComponent } from 'src/app/views/catalog/manage-enum-line/manage-enum-line.component';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent implements OnInit {
  isReadOnly: boolean = false
  age: Age
  patColumns: string[] = ['patientName', 'dateOfBirth', 'gender', 'contactNo', 'city', 'area', 'houseNo', 'address', 'remarks', 'isActive', 'actions'];
  currDate: Date = new Date
  AddMode: boolean = true
  EditMode: boolean = false
  proccessing: boolean = false;
  patDataSource: any
  hide = true;
  pat: PatientVM[] = []
  selectedPatient: PatientVM
  @ViewChild('PatientForm', { static: true }) PatientForm: NgForm;
  genders: SettingsVM[]
  cities: SettingsVM[]
  areas: SettingsVM[]
  dialogRef: any
  dialogData: any;
  Id: number = 0;
  FieldData = [];
  isDialog: boolean = false;
  group!: FormGroup;
  fields: SettingsTypeVM[] = [];
  formControls = {};
  countries: SettingsVM[]
  constructor(
    private fb: FormBuilder,
    private cdref: ChangeDetectorRef,
    private injector: Injector,
    private pmsSvc: PMSService,
    private dialog: MatDialog,
    private catSvc: CatalogService) {
    this.age = new Age
    this.selectedPatient = new PatientVM
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Patients)
    //this.GetPatient();
    this.GetSettings(EnumTypes.Country);
    this.GetSettings(EnumTypes.City);
    this.GetSettings(EnumTypes.Areas);
    this.GetGenders()
    this.age = new Age
    this.selectedPatient = new PatientVM
    this.selectedPatient.clientId = +localStorage.getItem("ClientId")
    this.selectedPatient.isActive = true;
    this.GetField();
    if (this.dialogData != null) {
      if (this.dialogData.patId != undefined) {
        this.EditPatient(this.dialogData.patId)
      }
    }
  }
  GetField() {
    var stng = new SettingsTypeVM
    stng.isActive = true
    stng.parentId = EnumTypes.PatientFields
    stng.clientId = +localStorage.getItem("ClientId")
    stng.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(stng).subscribe((res: SettingsTypeVM[]) => {
      debugger
      this.fields = res;
      if (this.fields.length > 0) {
        const formGroup = {};
        this.fields.forEach(field => {
          // this.group.addControl(formControl.name, new FormControl(''));
          formGroup[field.id] = new FormControl('');
        });
        this.group = new FormGroup(formGroup);
      } else this.fields = []
    });
  }
  GetGenders() {
    var stng = new SettingsVM
    stng.isActive = true;
    stng.enumTypeId = EnumTypes.Gender
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.genders = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  GetSettings(enumType: EnumTypes) {
    var stng = new SettingsVM
    stng.isActive = true;
    stng.enumTypeId = enumType
    stng.clientId = +localStorage.getItem("ClientId")
    stng.moduleId = Modules.PMS
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        if (enumType == EnumTypes.City)
          this.cities = res
        else if (enumType == EnumTypes.Country)
          this.countries = res
        else if (enumType == EnumTypes.Areas)
          this.areas = res
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  GetPatient() {
    var pat = new PatientVM
    pat.isActive = true
    this.pmsSvc.SearchPatient(pat).subscribe({
      next: (value: PatientVM[]) => {
        console.warn(value)
        this.pat = value
        this.patDataSource = new MatTableDataSource(this.pat)
      }, error: (err) => {
        alert('Error to retrieve Patient');
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  SavePatient() {
    debugger
    this.selectedPatient.clientId = +localStorage.getItem("ClientId")
    this.FieldData = []
    if (this.selectedPatient.dateOfBirth != null) {
      this.selectedPatient.dateOfBirth = moment(this.selectedPatient.dateOfBirth).toDate()
      this.selectedPatient.dateOfBirth = new Date(Date.UTC(this.selectedPatient.dateOfBirth.getFullYear(), this.selectedPatient.dateOfBirth.getMonth(), this.selectedPatient.dateOfBirth.getDate()))
    }
    if (this.PatientForm.invalid) {
      const _controls = this.PatientForm.controls;
      for (const name in _controls) {
        if (_controls[name].hasError('required')) {
          this.catSvc.ErrorMsgBar(` ${name} is required field`, 6000)
          break
        }
      }
    } else {
      if (this.group && this.group.invalid) {
        const controls = this.group.controls;
        for (const name in controls) {
          if (controls[name].hasError('required')) {
            this.group.controls[name].markAsTouched();
            const field = this.fields.find(x => x.id.toString() == name)
            this.catSvc.ErrorMsgBar(` ${field.name} is required field`, 6000)
            break
          }
        }
      } else {
        if (this.fields.length != 0)
          if (this.group)
            Object.keys(this.group.controls).forEach(async (key: any) => {
              const obstractControl = this.group.get(key);
              const newRow = {
                "patient": 0, "fieldValue": obstractControl.value.toString(), "fieldId": key,
                "isActive": true
              }
              this.FieldData.push(newRow)
              this.selectedPatient.ptFData = this.FieldData
            })
        if (this.EditMode)
          this.UpdatePatient()
        else {
          this.pmsSvc.SavePatient(this.selectedPatient).subscribe({
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
      }
    }
    // } else {
    //   this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
    //   this.proccessing = false
    // }
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
    this.cdref.markForCheck();
  }
  EditPatient(id) {
    this.selectedPatient = new PatientVM;
    this.selectedPatient.id = id
    this.selectedPatient.clientId = +localStorage.getItem("ClientId")
    this.pmsSvc.SearchPatient(this.selectedPatient).subscribe({
      next: (res: PatientVM[]) => {
        this.selectedPatient = res[0]
        this.getAge()
        this.EditMode = true;
        this.AddMode = false;
        if (this.group)
          for (let index = 0; index < this.selectedPatient.ptFData.length; index++) {
            var FieldId = this.selectedPatient.ptFData[index].fieldId;
            var FieldValue = this.selectedPatient.ptFData[index].fieldValue
            if (this.selectedPatient.ptFData[index].type == "Check Box") {
              var ret = this.convertStringToBool(FieldValue)
              console.warn(ret)
              this.group.get(FieldId.toString())?.setValue(this.convertStringToBool(FieldValue));
              this.group.controls[FieldId].setValue(this.convertStringToBool(FieldValue))
            } else {
              this.group.get(FieldId.toString())?.setValue(FieldValue);
              this.group.controls[FieldId].setValue(FieldValue)
            }
          }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  convertStringToBool(value: string): boolean {
    return value === "true";
  }
  UpdatePatient() {
    this.proccessing = true;
    this.pmsSvc.UpdatePatient(this.selectedPatient).subscribe({
      next: (value) => {
        this.catSvc.SuccessMsgBar("Successfully Updated", 5000);
        //this.Refresh()
      },
      error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000);
        console.warn(err);
        this.proccessing = false;
      }
    });
  }
  Refresh() {
    this.proccessing = false
    this.AddMode = true;
    this.EditMode = false;
    this.ngOnInit();
  }
  DeletePatient(id: number) {
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
  calculateDOB() {
    this.selectedPatient.dateOfBirth = this.catSvc.calculateDOB(this.age.years, this.age.months, this.age.days)
  }
  getAge() {
    this.age = this.catSvc.CalculateAge(this.selectedPatient.dateOfBirth)
  }
  validateNo(e): boolean {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
      return false
    return true
  }
  OpenCountryDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Country, isDialog: true,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId"),
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Country)
      }
    })
  }
  OpenCityDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineWithParentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.City,
        isDialog: true,
        parentType: EnumTypes.Country,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId"),
        parentId: this.selectedPatient.countryId
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.City)
      }
    })
  }
  OpenAreaDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineWithParentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Areas, isDialog: true,
        parentType: EnumTypes.City,
        parentId: this.selectedPatient.cityId,
        moduleId: Modules.PMS,
        clientId: +localStorage.getItem("ClientId"),
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Areas)
      }
    })
  }
  SearchCities() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.City
    stng.moduleId = Modules.PMS
    stng.clientId = +localStorage.getItem("ClientId")
    stng.parentId = this.selectedPatient.countryId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.cities = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SearchArea() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Areas
    stng.moduleId = Modules.PMS
    stng.clientId = +localStorage.getItem("ClientId")
    stng.parentId = this.selectedPatient.cityId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.areas = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
}
class Age {
  years: number
  months: number
  days: number
}

