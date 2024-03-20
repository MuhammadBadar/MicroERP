import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import Swal from 'sweetalert2';
import { KeyAccountingService } from '../../key-accounting.service';
import { UOMConversionVm } from '../../Models/UOMConversionVm';

@Component({
  selector: 'app-uomconversion',
  templateUrl: './uomconversion.component.html',
  styleUrls: ['./uomconversion.component.css']
})
export class UOMConversionComponent implements OnInit {
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  uom?: SettingsVM[];
  selectedUOMConversion: UOMConversionVm
  UOMConversions?: UOMConversionVm[];
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['UOM', 'QTY', 'ConversionRate', 'ConvertedUOM', 'displayUOM', 'isBaseUnit', 'isActive', 'actions'];
  dataSource: any;
  constructor(
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
  ) {
    this.selectedUOMConversion = new UOMConversionVm();
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedUOMConversion = new UOMConversionVm
    this.GetUOMConversion();
    this.GetUOM();
    this.selectedUOMConversion.isActive = true;
  }
  Check() {
    this.validFields = true;
  }
  GetUOMConversion() {
    this.accSvc.GetUOMConversion().subscribe({
      next: (res: UOMConversionVm[]) => {
        this.UOMConversions = res;
        this.dataSource = new MatTableDataSource(this.UOMConversions);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetUOM() {
    var setting = new SettingsVM()
    setting.isActive = true
    setting.enumTypeId = EnumTypes.UOM
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        this.uom = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteUOMConversion(id: number) {
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
        this.accSvc.DeleteUOMConversion(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'UOMConversion has been deleted.',
              'success'
            )
            this.GetUOMConversion();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetUOMConversionForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedUOMConversion = new UOMConversionVm;
    this.selectedUOMConversion.id = id
    this.accSvc.SearchUOMConversion(this.selectedUOMConversion).subscribe({
      next: (res: UOMConversionVm[]) => {
        this.selectedUOMConversion = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveUOMConversion() {
    if (this.selectedUOMConversion.uomId == 0 || this.selectedUOMConversion.uomId == undefined)
      this.userForm.controls['uomId'].setErrors({ incorrect: true })
    if (this.selectedUOMConversion.convertedUOMId == 0 || this.selectedUOMConversion.convertedUOMId == undefined)
      this.userForm.controls['convertedUOMId'].setErrors({ incorrect: true })
    if (!this.userForm.invalid) {
      if (this.selectedUOMConversion.convertedUOMId == this.selectedUOMConversion.uomId)
        if (this.selectedUOMConversion.qty == this.selectedUOMConversion.multiplier)
          this.selectedUOMConversion.isBaseUnit = true
      if (this.Edit)
        this.UpdateUOMConversion();
      else
        this.accSvc.SaveUOMConversion(this.selectedUOMConversion).subscribe({
          next: (res) => {
            this.catSvc.SuccessfullyAddMsg()
            this.Add = true;
            this.Edit = false;
            this.ngOnInit();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
    } else
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
  }
  UpdateUOMConversion() {
    this.accSvc.UpdateUOMConversion(this.selectedUOMConversion).subscribe({
      next: (res) => {
        this.catSvc.SuccessfullyUpdateMsg()
        this.Add = true;
        this.Edit = false;
        this.ngOnInit();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.selectedUOMConversion = new UOMConversionVm
    this.Add = true;
    this.Edit = false;
    this.selectedUOMConversion.isActive = true;
  }
}

