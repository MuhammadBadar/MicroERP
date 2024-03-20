import { ItemsService } from 'src/app/views/items/items.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { KeyAccountingService } from './../key-accounting.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { SupplierVM } from '../Models/SupplierVM';
import { CatalogService } from '../../catalog/catalog.service';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { Observable, Subject } from 'rxjs';

@Component({
  selector: 'app-manage-supplier',
  templateUrl: './manage-supplier.component.html',
  styleUrls: ['./manage-supplier.component.css']
})
export class ManageSupplierComponent implements OnInit {
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  Supplier: SupplierVM[] | undefined;
  selectedSupplier: SupplierVM;
  Countries?: SettingsVM[]
  Cities?: SettingsVM[]
  Accounts?: SettingsVM[]
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['companyName', 'contactName', 'account', 'discRate', 'phone', 'country', 'city', 'address', 'isActive', 'isCustomer', 'actions'];
  dataSource: any;
  constructor(
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
  ) {
    this.selectedSupplier = new SupplierVM();
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedSupplier = new SupplierVM
    this.GetSupplier();
    this.GetSettings(EnumTypes.City);
    this.GetSettings(EnumTypes.Country);
    this.GetAccounts();
    this.selectedSupplier.isActive = true
  }
  GetAccounts() {
    var stng = new SettingsVM
    stng.levelId = EnumTypes.SubSidiary
    stng.isActive = true
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Accounts = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
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
        if (etype == EnumTypes.City)
          this.Cities = res;
        if (etype == EnumTypes.Country)
          this.Countries = res;
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  ReSetValues() {
    this.GetSettings(EnumTypes.Country);
    this.GetSettings(EnumTypes.City);
  }
  OnSelectCountry(val: SettingsVM) {
    var stng = new SettingsVM
    stng.parentId = val.id;
    stng.isActive = true
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Cities = res
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetSupplier() {
    this.accSvc.GetSupplier().subscribe({
      next: (res: SupplierVM[]) => {
        this.Supplier = res;
        this.dataSource = new MatTableDataSource(this.Supplier);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteSupplier(sup: SupplierVM) {
    this.accSvc.IsSupplierBeingUsed(sup.id).subscribe((res) => {
      if (res == false) {
        sup.isActive = false
        sup.isCustomer = false
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
            this.accSvc.UpdateSupplier(sup).subscribe({
              next: (data) => {
                Swal.fire(
                  'Deleted!',
                  'Supplier has been deleted.',
                  'success'
                )
                this.GetSupplier();
              }, error: (e) => {
                this.catSvc.ErrorMsgBar("Error Occurred", 5000)
                console.warn(e);
              }
            })
          } else {
            this.GetSupplier()
          }
        })
      }
      else this.catSvc.ErrorMsgBar("Can't Continue as this Supplier is being used", 5000)
    });
  }
  GetSupplierForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedSupplier = new SupplierVM;
    this.selectedSupplier.id = id
    console.warn(this.selectedSupplier);
    this.accSvc.SearchSupplier(this.selectedSupplier).subscribe({
      next: (res: SupplierVM[]) => {
        this.Supplier = res;
        if (res[0].countryId != 0 && res[0].countryId != undefined) {
          var stng = new SettingsVM
          stng.id = res[0].countryId
          this.OnSelectCountry(stng)
        }
        this.selectedSupplier = this.Supplier[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveSupplier() {
    this.proccessing = true
    if (this.selectedSupplier.accId == 0 || this.selectedSupplier.accId == undefined)
      this.userForm.form.controls["accId"].setErrors({ 'incorrect': true })
    if (!this.userForm.invalid) {
      this.accSvc.SaveSupplier(this.selectedSupplier).subscribe({
        next: (res) => {
          this.catSvc.SuccessMsgBar("Supplier Successfully Added!", 5000)
          this.Add = true;
          this.Edit = false;
          this.proccessing = false
          this.ngOnInit();
        }, error: (e) => {
          this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          console.warn(e);
          this.proccessing = false
        }
      })
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
  }
  UpdateSupplier() {
    this.proccessing = true
    if (this.selectedSupplier.accId == 0 || this.selectedSupplier.accId == undefined)
      this.userForm.form.controls["accId"].setErrors({ 'incorrect': true })
    if (!this.userForm.invalid) {
      this.accSvc.UpdateSupplier(this.selectedSupplier).subscribe({
        next: (res) => {
          this.catSvc.SuccessMsgBar("Supplier Successfully Updated!", 5000)
          this.Add = true;
          this.Edit = false;
          this.proccessing = false
          this.ngOnInit();
        }, error: (e) => {
          this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          console.warn(e);
          this.proccessing = false
        }
      })
    }
    else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
  }
  Refresh() {
    this.Add = true;
    this.Edit = false;
  }
  IsCustomerCheck(sup: SupplierVM) {
    if (sup.isCustomer == false) {
      this.accSvc.IsCustomerBeingUsed(sup.customerId).subscribe((res) => {
        if (res == false) {
          this.DealSupplierAsCustmoer(sup)
        }
        else {
          this.catSvc.ErrorMsgBar("Can't Continue as this Customer is being used", 5000)
          this.GetSupplier()
        }
      })
    }
    else
      this.DealSupplierAsCustmoer(sup)
  }
  DealSupplierAsCustmoer(sup: SupplierVM) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You want to Continue",
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes'
    }).then((result) => {
      if (result.value) {
        this.accSvc.DealSupplierAsACustomer(sup).subscribe({
          next: (value) => {
            Swal.fire(
              'Updated!',
              'Successfully Updated.',
              'success'
            )
            this.GetSupplier()
          }, error: (err) => {
            this.GetSupplier()
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          },
        })
      } else {
        this.GetSupplier()
      }
    })
  }

}



