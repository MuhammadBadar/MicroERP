import { ItemVM, ProductAttribVm } from './../../Models/ItemVM';
import { CustomerVM } from './../../Models/CustomerVM';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { ItemsService } from 'src/app/views/items/items.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import Swal from 'sweetalert2';
import { KeyAccountingService } from '../../key-accounting.service';
import { DCDetailsVM, DCVM } from '../../Models/DeliveryChallanVM';
import { Location } from '@angular/common';
import * as moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { ConfigureProductComponent } from '../../Product/configure-product/configure-product.component';
@Component({
  selector: 'app-manage-delivery-challan',
  templateUrl: './manage-delivery-challan.component.html',
  styleUrls: ['./manage-delivery-challan.component.css']
})
export class ManageDeliveryChallanComponent implements OnInit {
  addButton = true
  proccessing: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = true
  Edit: boolean = false;
  Add: boolean = true;
  Customer?: CustomerVM[]
  Products?: ItemVM[]
  Accounts?: SettingsVM[]
  dcId!: number
  getVchById!: DCVM[];
  selectedDC = new DCVM
  selectedDCDetail = new DCDetailsVM
  selectedDetail = new DCDetailsVM
  DCDetails: DCDetailsVM[] = []
  @ViewChild('DCForm', { static: true }) DCForm!: NgForm;
  @ViewChild('DCDetailForm', { static: true }) DCDetailForm!: NgForm;
  displayedColumns: string[] = ['product', 'description', 'qty', 'actions'];
  dataSource: any
  outputArray = [];
  constructor(
    public acntSvc: KeyAccountingService,
    private route: ActivatedRoute,
    private _location: Location,
    public dialog: MatDialog,
    public catSvc: CatalogService,) {
    this.selectedDC = new DCVM();
    this.selectedDCDetail = new DCDetailsVM()

  }
  ngOnInit(): void {
    this.DCDetails = []
    this.route.queryParams.subscribe(params => {
      this.dcId = params['id'];
    });
    if (this.dcId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetDCById();
    }
    else {
      this.Add = true;
      this.Edit = false;
      this.dataSource = new MatTableDataSource(this.DCDetails);
    }
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.GetCutomer()
    this.GetProducts()
    this.GetAccounts();
    this.selectedDC = new DCVM();
    this.selectedDCDetail = new DCDetailsVM()

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
  GetCutomer() {
    var cust = new CustomerVM
    cust.isActive = true
    this.acntSvc.SearchCustomer(cust).subscribe({
      next: (res: CustomerVM[]) => {
        this.Customer = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetProducts() {
    var item = new ItemVM
    item.isActive = true
    this.acntSvc.SearchItem(item).subscribe({
      next: (res: ItemVM[]) => {
        this.Products = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetDCById() {
    this.acntSvc.GetDCById(this.dcId).subscribe({
      next: (res: DCVM[]) => {
        this.getVchById = res;
        this.selectedDC = this.getVchById[0]
        this.DCDetails = []
        this.selectedDC.dcDetails?.forEach(element => {
          this.DCDetails.push(element)
        });
        this.dataSource = new MatTableDataSource(this.DCDetails);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred !", 6000)
        console.warn(e);
      }
    })
  }
  RefreshDetail() {
    this.lineAddMode = false;
    this.addButton = true
    this.lineEditMode = false;
    this.selectedDCDetail = new DCDetailsVM
    if (this.dcId > 0)
      this.GetDCById()
  }
  Back() {
    this._location.back();
  }
  async Submit() {
    this.SetDates()
    this.selectedDC.dcDetails = this.DCDetails
    this.proccessing = true
    this.DCValidation()

    if (!this.DCForm.invalid) {
      if (this.selectedDC.dcDetails.length == 0)
        this.catSvc.ErrorMsgBar("Please Add some DC Detail!", 5000)
      else
        await this.UpdateDC();
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
    this.proccessing = false
  }
  SaveDC() {
    this.acntSvc.SaveDC(this.selectedDC).subscribe({
      next: (res: DCVM) => {
        this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        this.selectedDC = res
        this.DCDetails = []
        this.selectedDC.dcDetails?.forEach(element => {
          this.DCDetails.push(element)
        });
        this.dataSource = new MatTableDataSource(res.dcDetails);
        this.RefreshDetail()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.DCDetails = []
        this.proccessing = false
      }
    })
  }
  UpdateDC() {
    this.acntSvc.UpdateDC(this.selectedDC).subscribe({
      next: (res: DCVM) => {
        if (this.Edit) {
          if (this.lineEditMode)
            this.catSvc.SuccessfullyUpdateMsg();
          else if (this.lineAddMode)
            this.catSvc.SuccessfullyAddMsg();
          else
            this.catSvc.SuccessfullyUpdateMsg();
        } else {
          if (this.lineEditMode)
            this.catSvc.SuccessfullyUpdateMsg();
          else
            this.catSvc.SuccessfullyAddMsg();
        }
        this.selectedDC = res
        this.DCDetails = []
        this.selectedDC.dcDetails?.forEach(element => {
          this.DCDetails.push(element)
        });
        this.dataSource = new MatTableDataSource(res.dcDetails);
        this.RefreshDetail()
        this.proccessing = false
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.DCDetails = []
        this.proccessing = false
      }
    })
  }
  edit(dcDet: DCDetailsVM) {
    this.lineEditMode = true
    this.lineAddMode = false
    this.addButton = false
    this.selectedDCDetail = dcDet
    this.selectedDetail = dcDet
    this.selectedDCDetail.editMode = true
  }
  delete(dcDet: DCDetailsVM) {
    if (this.DCDetails.length == 1) {
      this.catSvc.ErrorMsgBar("You Can't delete it,as Dc has only one line ,and the Dc Detail Can't be Empty", 9000)
    } else {
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

          if (dcDet.id == undefined || dcDet.id == 0) {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
          } else {
            var DC = new DCVM
            DC = this.selectedDC
            DC.dcDetails = []
            DC.dcDetails.push(dcDet)
            dcDet.dBoperation = 3
            this.acntSvc.UpdateDC(DC).subscribe({
              next: (data: DCVM) => {
                Swal.fire(
                  'Deleted!',
                  'Successfully Deleted.',
                  'success'
                )
                this.DCDetails = []
                data.dcDetails?.forEach(element => {
                  this.DCDetails.push(element)
                });
                this.dataSource = new MatTableDataSource(data.dcDetails);
              }, error: (e) => {
                this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
                console.warn(e);
              }
            })
          }
        }
      })
    }
  }
  async AddDCDetailtoList() {
    debugger
    this.DCValidation()
    if (!this.DCForm.invalid) {
      if (this.selectedDCDetail.productId == 0)
        this.DCDetailForm.form.controls['productId'].setErrors({ 'incorrect': true });
      if (!this.DCDetailForm.invalid) {

        if (this.lineEditMode)
          this.selectedDCDetail.dBoperation = 2
        else
          this.selectedDCDetail.dBoperation = 1
        if (this.selectedDCDetail.dBoperation == 1) {
          this.lineAddMode = true
        }
        this.DCDetails.push(this.selectedDCDetail)
        this.selectedDC.dcDetails = []
        this.selectedDC.dcDetails?.push(this.selectedDCDetail)

        if (this.selectedDC?.id > 0)
          await this.UpdateDC();
        else
          await this.SaveDC();
      }

      else {
        this.catSvc.ErrorMsgBar("Please fill all required fields of DC Line", 5000)
      }
    } else {
      this.catSvc.ErrorMsgBar("Please fill all required fields of DC", 5000)
    }
  }
  DCValidation() {
    if (this.selectedDC.acId == 0)
      this.DCForm.form.controls['acId'].setErrors({ 'incorrect': true });
    if (this.selectedDC.custId == 0)
      this.DCForm.form.controls['custId'].setErrors({ 'incorrect': true });
  }
  onBlur() {
    console.warn(this.selectedDC.id)
    if (this.selectedDC.id > 0) {
      if (!this.DCForm.invalid) {
        this.SetDates()
        this.acntSvc.UpdateDC(this.selectedDC).subscribe({
          next: (res: DCVM) => {
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
          }, error: (e: any) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
            this.proccessing = false
          }
        })
      } else {
        this.catSvc.ErrorMsgBar("Please fill all required fields", 5000)
      }
    }
  }
  SetDates() {
    this.selectedDC.date = moment(this.selectedDC.date).toDate()
    this.selectedDC.date = new Date(Date.UTC(this.selectedDC.date.getFullYear(), this.selectedDC.date.getMonth(), this.selectedDC.date.getDate()))
  }
  onSelectAccount() {
    this.selectedDC.custId = this.Customer?.find(x => x.accId == this.selectedDC.acId)?.id
  }
  onSelectCustomer(val: CustomerVM) {
    this.selectedDC.acId = this.Accounts?.find(x => x.id == val.accId)?.id
  }
  onSelectProduct(val: ItemVM) {
    if (val.productAttribs.length > 0) {
      let dialogRef = this.dialog.open(ConfigureProductComponent, {
        disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
        , data: { id: val.id }
      });
      dialogRef.afterClosed().subscribe((res) => {
        if (res) {
          {
            var productAttrib = new ProductAttribVm
            productAttrib = res.data
            if (res.product) {
              if (res.product.id != this.selectedDCDetail.productId) {
                this.selectedDCDetail.productId = res.product.id
              }
            }
            this.selectedDCDetail.description = `${productAttrib.product} (${productAttrib.attributeValue})`
          }
        }
      })
    } else
      this.selectedDCDetail.description = null
  }
}

