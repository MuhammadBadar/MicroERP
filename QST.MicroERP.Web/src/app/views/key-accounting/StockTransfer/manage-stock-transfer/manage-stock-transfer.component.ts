import { ItemVM, ProductAttribVm } from './../../Models/ItemVM';
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
import { STLineVM, StockTransferVM } from '../../Models/StockTransferVM';
import { Location } from '@angular/common';
import * as moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { ConfigureProductComponent } from '../../Product/configure-product/configure-product.component';

@Component({
  selector: 'app-manage-stock-transfer',
  templateUrl: './manage-stock-transfer.component.html',
  styleUrls: ['./manage-stock-transfer.component.css']
})
export class ManageStockTransferComponent implements OnInit {
  addButton = true
  proccessing: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = true
  Edit: boolean = false;
  Add: boolean = true;
  Products?: ItemVM[]
  Godowns?: SettingsVM[]
  StockTransferId!: number
  getVchById!: StockTransferVM[];
  selectedStockTransfer = new StockTransferVM
  selectedSTLine = new STLineVM
  selectedDetail = new STLineVM
  STLines: STLineVM[] = []
  @ViewChild('StockTransferForm', { static: true }) StockTransferForm!: NgForm;
  @ViewChild('STLineForm', { static: true }) STLineForm!: NgForm;
  displayeStockTransferColumns: string[] = ['product', 'description', 'productUnits', 'qty', 'godown', 'actions'];
  dataSource: any
  outputArray = [];
  constructor(
    public dialog: MatDialog,
    public acntSvc: KeyAccountingService,
    private route: ActivatedRoute,
    private _location: Location,
    public catSvc: CatalogService,) {
    this.selectedStockTransfer = new StockTransferVM();
    this.selectedSTLine = new STLineVM()
    this.selectedStockTransfer.isActive = true
  }
  ngOnInit(): void {
    this.STLines = []
    this.route.queryParams.subscribe(params => {
      this.StockTransferId = params['id'];
    });
    if (this.StockTransferId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetStockTransferById();
    }
    else {
      this.Add = true;
      this.Edit = false;
      this.dataSource = new MatTableDataSource(this.STLines);
    }
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.GetProducts()
    this.GetGodowns();
    this.selectedStockTransfer = new StockTransferVM();
    this.selectedSTLine = new STLineVM()

  }
  GetGodowns() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Godown
    stng.isActive = true
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Godowns = res;
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
  GetStockTransferById() {
    this.acntSvc.GetStockTransferById(this.StockTransferId).subscribe({
      next: (res: StockTransferVM[]) => {
        this.getVchById = res;
        this.selectedStockTransfer = this.getVchById[0]
        this.STLines = []
        this.selectedStockTransfer.stockTransferLines?.forEach(element => {
          this.STLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.STLines);
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
    this.selectedSTLine = new STLineVM
    if (this.StockTransferId > 0)
      this.GetStockTransferById()
  }
  Back() {
    this._location.back();
  }
  async Submit() {
    this.SetDates()
    this.selectedStockTransfer.stockTransferLines = this.STLines
    this.proccessing = true
    this.StockTransferValidation()

    if (!this.StockTransferForm.invalid) {
      if (this.selectedStockTransfer.stockTransferLines.length == 0)
        this.catSvc.ErrorMsgBar("Please Add some StockTransfer Detail!", 5000)
      else
        await this.UpdateStockTransfer();
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
    this.proccessing = false
  }
  SaveStockTransfer() {
    this.acntSvc.SaveStockTransfer(this.selectedStockTransfer).subscribe({
      next: (res: StockTransferVM) => {
        this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        this.selectedStockTransfer = res
        this.STLines = []
        this.selectedStockTransfer.stockTransferLines?.forEach(element => {
          this.STLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.STLines);
        console.warn(this.STLines)
        this.RefreshDetail()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.STLines = []
        this.proccessing = false
      }
    })
  }
  UpdateStockTransfer() {
    this.acntSvc.UpdateStockTransfer(this.selectedStockTransfer).subscribe({
      next: (res: StockTransferVM) => {
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
        this.selectedStockTransfer = res
        this.STLines = []
        this.selectedStockTransfer.stockTransferLines?.forEach(element => {
          this.STLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.STLines);
        this.RefreshDetail()
        this.proccessing = false
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.STLines = []
        this.proccessing = false
      }
    })
  }
  edit(StockTransferDet: STLineVM) {
    this.lineEditMode = true
    this.lineAddMode = false
    this.addButton = false
    this.selectedSTLine = StockTransferDet
    this.selectedDetail = StockTransferDet
    this.selectedSTLine.editMode = true
  }
  delete(StockTransferDet: STLineVM) {
    if (this.STLines.length == 1) {
      this.catSvc.ErrorMsgBar("You Can't delete it,as StockTransfer has only one line ,and the StockTransfer Detail Can't be Empty", 9000)
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

          if (StockTransferDet.id == undefined || StockTransferDet.id == 0) {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
          } else {
            var StockTransfer = new StockTransferVM
            StockTransfer = this.selectedStockTransfer
            StockTransfer.stockTransferLines = []
            StockTransfer.stockTransferLines.push(StockTransferDet)
            StockTransferDet.dBoperation = 3
            this.acntSvc.UpdateStockTransfer(StockTransfer).subscribe({
              next: (data: StockTransferVM) => {
                Swal.fire(
                  'Deleted!',
                  'Successfully Deleted.',
                  'success'
                )
                this.STLines = []
                data.stockTransferLines?.forEach(element => {
                  this.STLines.push(element)
                });
                this.dataSource = new MatTableDataSource(this.STLines);
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
  async AddSTLinetoList() {
    debugger
    this.StockTransferValidation()
    if (!this.StockTransferForm.invalid) {
      if (this.selectedSTLine.productId == 0 && this.selectedSTLine.productId == undefined)
        this.STLineForm.form.controls['productId'].setErrors({ 'incorrect': true });
      if (!this.STLineForm.invalid) {
        if (this.lineEditMode)
          this.selectedSTLine.dBoperation = 2
        else
          this.selectedSTLine.dBoperation = 1
        if (this.selectedSTLine.dBoperation == 1) {
          this.lineAddMode = true
        }
        this.STLines.push(this.selectedSTLine)
        this.selectedStockTransfer.stockTransferLines = []
        this.selectedStockTransfer.stockTransferLines?.push(this.selectedSTLine)

        if (this.selectedStockTransfer?.id > 0)
          await this.UpdateStockTransfer();
        else
          await this.SaveStockTransfer();
      }

      else {
        this.catSvc.ErrorMsgBar("Please fill all required fields of StockTransfer Line", 5000)
      }
    } else {
      this.catSvc.ErrorMsgBar("Please fill all required fields of StockTransfer", 5000)
    }
  }
  StockTransferValidation() {
    // if (this.selectedStockTransfer.acId == 0)
    //   this.StockTransferForm.form.controls['acId'].setErrors({ 'incorrect': true });
    // if (this.selectedStockTransfer.custId == 0)
    //   this.StockTransferForm.form.controls['custId'].setErrors({ 'incorrect': true });
  }
  onBlur() {
    console.warn(this.selectedStockTransfer.id)
    if (this.selectedStockTransfer.id > 0) {
      if (!this.StockTransferForm.invalid) {
        this.SetDates()
        this.acntSvc.UpdateStockTransfer(this.selectedStockTransfer).subscribe({
          next: (res: StockTransferVM) => {
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
    this.selectedStockTransfer.date = moment(this.selectedStockTransfer.date).toDate()
    this.selectedStockTransfer.date = new Date(Date.UTC(this.selectedStockTransfer.date.getFullYear(), this.selectedStockTransfer.date.getMonth(), this.selectedStockTransfer.date.getDate()))
  }
  onSelectProduct(val: ItemVM) {
    this.selectedDetail.product = val.name
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
              if (res.product.id != this.selectedSTLine.productId) {
                this.selectedSTLine.productId = res.product.id
              }
            }
            this.selectedSTLine.description = `${productAttrib.product} (${productAttrib.attributeValue})`
          }
        }
      })
    } else
      this.selectedSTLine.description = null
  }
}


