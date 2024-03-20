import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { KeyAccountingService } from './../key-accounting.service';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { CustomerVM } from '../Models/CustomerVM';
import { CatalogService } from '../../catalog/catalog.service';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { ItemsService } from '../../items/items.service';

@Component({
  selector: 'app-manage-customer',
  templateUrl: './manage-customer.component.html',
  styleUrls: ['./manage-customer.component.css']
})
export class ManageCustomerComponent implements OnInit {
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  Customer: CustomerVM[] | undefined;
  selectedCustomer: CustomerVM;
  Countries?: SettingsVM[]
  Cities?: SettingsVM[]
  Accounts?: SettingsVM[]
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  displayedColumns: string[] = ['name', 'account', 'email', 'phone', 'country', 'city', 'address', 'region', 'isActive', 'isSupplier', 'actions'];
  dataSource: any;
  constructor(
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
  ) {
    this.selectedCustomer = new CustomerVM();
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedCustomer = new CustomerVM
    this.GetCustomer();
    this.GetSettings(EnumTypes.City);
    this.GetSettings(EnumTypes.Country);
    this.GetAccounts()
    this.selectedCustomer.isActive = true
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
  ReSetValues() {
    this.GetSettings(EnumTypes.Country);
    this.GetSettings(EnumTypes.City);
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
  GetCustomer() {
    this.accSvc.GetCustomer().subscribe({
      next: (res: CustomerVM[]) => {
        this.Customer = res;
        this.dataSource = new MatTableDataSource(this.Customer);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteCustomer(cust: CustomerVM) {
    this.accSvc.IsCustomerBeingUsed(cust.id).subscribe((res) => {
      if (res == false) {
        cust.isActive = false;
        cust.isSupplier = false;
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
            this.accSvc.UpdateCustomer(cust).subscribe({
              next: (data) => {
                Swal.fire(
                  'Deleted!',
                  'Customer has been deleted.',
                  'success'
                )
                this.GetCustomer();
              }, error: (e) => {
                this.catSvc.ErrorMsgBar("Error Occurred", 5000)
                console.warn(e);
              }
            })
          } else {
            this.GetCustomer()
          }
        })
      }
      else this.catSvc.ErrorMsgBar("Can't Continue as this Customer is being used", 5000)
    })
  }
  GetCustomerForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedCustomer = new CustomerVM;
    this.selectedCustomer.id = id
    this.accSvc.SearchCustomer(this.selectedCustomer).subscribe({
      next: (res: CustomerVM[]) => {
        this.Customer = res;
        if (res[0].countryId != 0 && res[0].countryId != undefined) {
          var stng = new SettingsVM
          stng.id = res[0].countryId
          this.OnSelectCountry(stng)
        }
        this.selectedCustomer = this.Customer[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveCustomer() {
    if (this.selectedCustomer.accId == 0 || this.selectedCustomer.accId == undefined)
      this.userForm.form.controls['accId'].setErrors({ 'incorrect': true });
    this.proccessing = true
    if (!this.userForm.invalid) {
      this.accSvc.SaveCustomer(this.selectedCustomer).subscribe({
        next: (res) => {
          this.catSvc.SuccessMsgBar("Customer Successfully Added!", 5000)
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
  UpdateCustomer() {
    if (this.selectedCustomer.accId == 0 || this.selectedCustomer.accId == undefined)
      this.userForm.form.controls['accId'].setErrors({ 'incorrect': true });
    this.proccessing = true
    if (!this.userForm.invalid) {
      this.accSvc.UpdateCustomer(this.selectedCustomer).subscribe({
        next: (res) => {
          this.catSvc.SuccessMsgBar("Customer Successfully Updated!", 5000)
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
  IsSupplierCheck(cust: CustomerVM) {
    if (cust.isSupplier == false) {
      this.accSvc.IsSupplierBeingUsed(cust.supplierId).subscribe((res) => {
        if (res == false) {
          this.DealCustomerAsSupplier(cust)
        }
        else {
          this.catSvc.ErrorMsgBar("Can't Continue as this Supplier is being used", 5000)
          this.GetCustomer()
        }
      });
    } else
      this.DealCustomerAsSupplier(cust)
  }
  DealCustomerAsSupplier(cust: CustomerVM) {
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
        this.accSvc.DealCustomerAsASupplier(cust).subscribe({
          next: (value) => {
            Swal.fire(
              'Updated!',
              'Successfully Updated.',
              'success'
            )
            this.GetCustomer()
          }, error: (err) => {
            this.GetCustomer()
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          },
        })
      } else {
        this.GetCustomer()
      }
    })
  }
}


