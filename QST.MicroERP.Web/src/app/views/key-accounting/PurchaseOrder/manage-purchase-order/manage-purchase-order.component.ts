import { Component, OnInit, ViewChild } from '@angular/core';
import { PurchaseLineVM, PurchaseVM } from '../../Models/PurchaseOrderVM';
import * as moment from 'moment';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { ItemVM, ProductAttribVm } from '../../Models/ItemVM';
import { KeyAccountingService } from '../../key-accounting.service';
import { ActivatedRoute } from '@angular/router';
import { ItemsService } from 'src/app/views/items/items.service';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { NgForm } from '@angular/forms';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { Location } from '@angular/common';
import { SupplierVM } from '../../Models/SupplierVM';
import { MatDialog } from '@angular/material/dialog';
import { ConfigureProductComponent } from '../../Product/configure-product/configure-product.component';
import { AttributeValuesVm } from '../../Models/AttributesValuesVm';
import { ConfigurePurchaseRateComponent } from '../../Product/configure-purchase-rate/configure-purchase-rate.component';
import { Statses } from '../../Models/Enum/Status';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ItemUOMVm } from '../../Models/ItemUOMVm';
import { UOMTypes } from '../../Models/Enum/UOMTypes';
import { ItemVariantsVM } from '../../Models/ItemVariants';
@Component({
  selector: 'app-manage-purchase-order',
  templateUrl: './manage-purchase-order.component.html',
  styleUrls: ['./manage-purchase-order.component.css']
})
export class ManagePurchaseOrderComponent implements OnInit {
  isGstRateRequired: boolean = true
  isGstRetRateRequired: boolean = true
  addButton = true
  supplierDiscRate!: number
  proccessing: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = false
  Edit: boolean = false;
  Add: boolean = true;
  Products?: ItemVM[]
  Suppliers?: SupplierVM[]
  Accounts?: SettingsVM[]
  PurchaseId!: number
  getVchById!: PurchaseVM[];
  selectedPurchase = new PurchaseVM
  selectedPurchaseLine = new PurchaseLineVM
  selectedDetail = new PurchaseLineVM
  PurchaseLines: PurchaseLineVM[] = []
  selectedProduct: ItemVM
  itemUOM: ItemUOMVm[] = []
  @ViewChild('PurchaseForm', { static: true }) PurchaseForm!: NgForm;
  @ViewChild('PurchaseLineForm', { static: true }) PurchaseLineForm!: NgForm;
  displayePurchaseColumns: string[] = ['product', 'purUnit', 'description', 'qty', 'purchaseRate', 'discPer',
    'gstRate', 'gstRetailRate', 'retailRate', 'amount', 'actions'];
  dataSource: any
  outputArray = [];
  constructor(
    public acntSvc: KeyAccountingService,
    private route: ActivatedRoute,
    private _location: Location,
    public dialog: MatDialog,
    public catSvc: CatalogService,) {
    this.selectedPurchase = new PurchaseVM();
    this.selectedPurchaseLine = new PurchaseLineVM()
    this.selectedProduct = new ItemVM
  }
  ngOnInit(): void {
    this.PurchaseLines = []
    this.route.queryParams.subscribe(params => {
      this.PurchaseId = params['id'];
    });
    if (this.PurchaseId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetPurchaseById();
    }
    else {
      this.Add = true;
      this.Edit = false;
      this.dataSource = new MatTableDataSource(this.PurchaseLines);
    }
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.GetProducts()
    this.GetAccounts();
    this.GetSuppliers();
    this.selectedPurchase = new PurchaseVM();
    this.selectedPurchaseLine = new PurchaseLineVM()
    this.selectedPurchase.isActive = true
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
  GetSuppliers() {
    var sup = new SupplierVM
    sup.isActive = true
    this.acntSvc.SearchSupplier(sup).subscribe({
      next: (res: SupplierVM[]) => {
        this.Suppliers = res;
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
  GetPurchaseById() {
    var pur = new PurchaseVM
    pur.id = this.PurchaseId
    this.acntSvc.SearchPurchase(pur).subscribe({
      next: (res: PurchaseVM[]) => {
        this.getVchById = res;
        this.selectedPurchase = this.getVchById[0]
        this.PurchaseLines = []
        this.selectedPurchase.purchaseLines?.forEach(element => {
          this.PurchaseLines.push(element)
          var sup = new SupplierVM
          sup.id = this.selectedPurchase.supplierId
          this.acntSvc.SearchSupplier(sup).subscribe({
            next: (value: SupplierVM[]) => {
              this.supplierDiscRate = value[0].discRate
              this.selectedPurchaseLine.discPer = this.supplierDiscRate
            }, error: (err) => {

            },
          })
        });
        this.dataSource = new MatTableDataSource(this.PurchaseLines);
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
    this.selectedPurchaseLine = new PurchaseLineVM
    this.selectedPurchaseLine.discPer = this.supplierDiscRate
    if (this.PurchaseId > 0)
      this.GetPurchaseById()
    this.PurchaseLineForm.controls['gstRetailRate'].enable();
    this.PurchaseLineForm.controls['gstRate'].enable();
  }
  Back() {
    this._location.back();
  }
  async Submit() {
    this.SetDates()
    this.selectedPurchase.purchaseLines = this.PurchaseLines
    this.proccessing = true
    this.PurchaseValidation()

    if (!this.PurchaseForm.invalid) {
      if (this.selectedPurchase.purchaseLines.length == 0)
        this.catSvc.ErrorMsgBar("Please Add some Purchase Detail!", 5000)
      else {
        this.selectedPurchase.statusId = Statses.UnPosted
        await this.UpdatePurchase();
      }
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
    this.proccessing = false
  }
  SavePurchase() {
    this.acntSvc.SavePurchase(this.selectedPurchase).subscribe({
      next: (res: PurchaseVM) => {
        this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        this.selectedPurchase = res
        this.PurchaseLines = []
        this.selectedPurchase.purchaseLines?.forEach(element => {
          this.PurchaseLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.PurchaseLines);
        console.warn(this.PurchaseLines)
        this.RefreshDetail()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.PurchaseLines = []
        this.proccessing = false
      }
    })
  }
  UpdatePurchase() {
    this.acntSvc.UpdatePurchase(this.selectedPurchase).subscribe({
      next: (res: PurchaseVM) => {
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
        this.selectedPurchase = res
        this.PurchaseLines = []
        this.selectedPurchase.purchaseLines?.forEach(element => {
          this.PurchaseLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.PurchaseLines);
        this.RefreshDetail()
        this.proccessing = false
        if (!this.Edit && res.statusId == Statses.UnPosted)
          this.ngOnInit()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.PurchaseLines = []
        this.proccessing = false
      }
    })
  }
  edit(PurchaseDet: PurchaseLineVM) {
    this.lineEditMode = true
    this.lineAddMode = false
    this.addButton = false
    this.selectedPurchaseLine = PurchaseDet
    this.selectedDetail = PurchaseDet
    this.selectedPurchaseLine.editMode = true
    this.TextChangeEvent()
    this.GetItemUOM(this.selectedPurchaseLine.productId)
  }
  delete(PurchaseDet: PurchaseLineVM) {
    if (this.PurchaseLines.length == 1) {
      this.catSvc.ErrorMsgBar("You Can't delete it,as Purchase has only one line ,and the Purchase Detail Can't be Empty", 9000)
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

          if (PurchaseDet.id == undefined || PurchaseDet.id == 0) {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
          } else {
            var Purchase = new PurchaseVM
            Purchase = this.selectedPurchase
            Purchase.purchaseLines = []
            Purchase.purchaseLines.push(PurchaseDet)
            PurchaseDet.dBoperation = 3
            this.acntSvc.UpdatePurchase(Purchase).subscribe({
              next: (data: PurchaseVM) => {
                Swal.fire(
                  'Deleted!',
                  'Successfully Deleted.',
                  'success'
                )
                this.PurchaseLines = []
                data.purchaseLines?.forEach(element => {
                  this.PurchaseLines.push(element)
                });
                this.dataSource = new MatTableDataSource(this.PurchaseLines);
                this.calOfBottomFields()
                this.UpdatePurchase();
              }, error: (e: any) => {
                this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
                console.warn(e);
              }
            })
          }
        }
      })
    }
  }
  async AddPurchaseLinetoList() {
    this.PurchaseValidation()
    //  if (this.selectedPurchaseLine.gstRate > 0 && this.selectedPurchaseLine.gstRetailRate > 0) {
    if (!this.PurchaseForm.invalid) {
      if (this.selectedPurchaseLine.productId == 0 && this.selectedPurchaseLine.productId == undefined)
        this.PurchaseLineForm.form.controls['productId'].setErrors({ 'incorrect': true });
      if (!this.PurchaseLineForm.invalid) {
        if (this.lineEditMode)
          this.selectedPurchaseLine.dBoperation = 2
        else
          this.selectedPurchaseLine.dBoperation = 1
        if (this.selectedPurchaseLine.dBoperation == 1) {
          this.selectedPurchase.statusId = Statses.Draft
          this.lineAddMode = true
        }
        debugger
        if (this.selectedPurchaseLine.id > 0)
          this.PurchaseLines = this.PurchaseLines.filter(x => x.id != this.selectedPurchaseLine.id)
        this.PurchaseLines.push(this.selectedPurchaseLine)
        this.selectedPurchase.purchaseLines = []
        this.selectedPurchase.purchaseLines?.push(this.selectedPurchaseLine)
        this.calOfBottomFields();
        if (this.selectedPurchase?.id > 0)
          await this.UpdatePurchase();
        else
          await this.SavePurchase();
      }

      else {
        this.catSvc.ErrorMsgBar("Please fill all required fields of Purchase Line", 5000)
      }
    } else
      this.catSvc.ErrorMsgBar("Please fill all required fields of Purchase", 5000)
    //}
    // else
    // this.catSvc.ErrorMsgBar("Please add some GstRate or GstRetailRate", 5000)
  }
  PurchaseValidation() {
    // if (this.selectedPurchaseLine.gstRate! > 0 && this.selectedPurchaseLine.gstRetailRate! > 0) {
    //   this.PurchaseLineForm.form.controls['gstRate'].setErrors({ 'incorrect': true });
    //   this.PurchaseLineForm.form.controls['gstRetailRate'].setErrors({ 'incorrect': true });
    // }
    if (this.selectedPurchaseLine.productId == 0 || this.selectedPurchaseLine.productId == undefined)
      this.PurchaseLineForm.form.controls['productId'].setErrors({ 'incorrect': true });
    // if (this.itemUOM.length > 0)
    //   if (this.selectedPurchaseLine.purUnitId == 0 || this.selectedPurchaseLine.purUnitId == undefined)
    //     this.PurchaseLineForm.form.controls['purUnitId'].setErrors({ 'incorrect': true });
    if (this.selectedPurchase.acId == 0 || this.selectedPurchase.acId == undefined)
      this.PurchaseForm.form.controls['acId'].setErrors({ 'incorrect': true });
    if (this.selectedPurchase.supplierId == 0 || this.selectedPurchase.supplierId == undefined)
      this.PurchaseForm.form.controls['supplierId'].setErrors({ 'incorrect': true });
  }
  onBlur() {
    console.warn(this.selectedPurchase.id)
    if (this.selectedPurchase.id > 0) {
      this.PurchaseValidation();
      if (!this.PurchaseForm.invalid) {
        this.SetDates()
        this.acntSvc.UpdatePurchase(this.selectedPurchase).subscribe({
          next: (res: PurchaseVM) => {
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
    this.selectedPurchase.date = moment(this.selectedPurchase.date).toDate()
    this.selectedPurchase.date = new Date(Date.UTC(this.selectedPurchase.date.getFullYear(), this.selectedPurchase.date.getMonth(), this.selectedPurchase.date.getDate()))
  }
  onSelectAccount() {
    this.selectedPurchase.supplierId = this.Suppliers?.find(x => x.accId == this.selectedPurchase.acId)?.id
  }
  onSelectSupplier(val: SupplierVM) {
    this.selectedPurchase.acId = this.Accounts?.find(x => x.id == val.accId)?.id
    this.supplierDiscRate = val.discRate
    this.selectedPurchaseLine.discPer = this.supplierDiscRate
  }
  // onSelectProduct(val: ItemVM) {
  //   this.selectedPurchaseLine.purchaseRate = val.purRate
  //   this.selectedPurchaseLine.gstRate = val.gstPurRate
  //   this.selectedPurchaseLine.gstRetailRate = val.purStRate
  //   this.selectedPurchaseLine.retailRate = val.retailRate
  //   if (this.selectedPurchaseLine.qty != undefined && this.selectedPurchaseLine.purchaseRate != undefined)
  //     this.selectedPurchaseLine.amount = this.selectedPurchaseLine.qty * this.selectedPurchaseLine.purchaseRate
  //   this.TextChangeEvent();
  // }
  onSelectProduct(val: ItemVM) {
    this.GetItemUOM(val.id)
    if (val.id != this.selectedProduct.id) {
      this.selectedPurchaseLine.productAttribIds = null
      this.selectedPurchaseLine.description = null
      this.selectedPurchaseLine.purchaseRate = 0
    }
    this.selectedProduct = val
    if (!this.lineEditMode || val.productAttribs.length! > 0) {
      this.SetValuesAccordingProduct(val)
      this.TextChangeEvent();
    }
    this.OpenConfigureProductByVariantDialog(val)
  }
  GetItemUOM(itmId) {
    var itemUOM = new ItemUOMVm
    itemUOM.itemId = itmId
    itemUOM.uomTypeId = UOMTypes.PurchaseUOM
    itemUOM.isActive = true
    this.acntSvc.SearchItemUOM(itemUOM).subscribe({
      next: (res: ItemUOMVm[]) => {
        this.itemUOM = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetProductById() {
    var item = new ItemVM
    item.id = this.selectedPurchaseLine.productId
    this.acntSvc.SearchItem(item).subscribe({
      next: (items: ItemVM[]) => {
        this.selectedProduct = items[0]
        this.OpenConfigureProductByVariantDialog(this.selectedProduct)
      }
    })
  }
  SetValuesAccordingProduct(val: ItemVM) {
    this.selectedPurchaseLine.purchaseRate = val.purRate
    this.selectedPurchaseLine.gstRate = val.gstPurRate
    this.selectedPurchaseLine.gstRetailRate = val.purStRate
    this.selectedPurchaseLine.retailRate = val.retailRate
    if (this.selectedPurchaseLine.qty != undefined && this.selectedPurchaseLine.purchaseRate != undefined)
      this.selectedPurchaseLine.amount = this.selectedPurchaseLine.qty * this.selectedPurchaseLine.purchaseRate
  }
  OpenConfigureProductByVariantDialog(val: ItemVM) {
    if (val.productAttribs.length > 0 || this.selectedPurchaseLine.productAttribIds) {
      let dialogRef = this.dialog.open(ConfigurePurchaseRateComponent, {
        disableClose: true, panelClass: 'calendar-form-dialog', width: '750px'
        , data: { id: val.id, productAttribId: this.selectedPurchaseLine.productAttribIds, selectedLine: this.selectedPurchaseLine, isEditMode: this.lineEditMode }
      });
      dialogRef.afterClosed().subscribe({
        next: (res) => {
          if (res) {
            // var productAttrib = new ProductAttribVm
            // productAttrib = res.data
            if (res.product) {
              if (res.product.id != this.selectedPurchaseLine.productId) {
                this.selectedPurchaseLine.productId = res.product.id
                this.SetValuesAccordingProduct(res.product)
                this.TextChangeEvent();
              }
            }
            if (res.purchaseRate > 0) {
              this.selectedPurchaseLine.purchaseRate = res.purchaseRate
              this.selectedPurchaseLine.amount = this.selectedPurchaseLine.qty * this.selectedPurchaseLine.purchaseRate
            }
            if (res.productAttribIds)
              this.selectedPurchaseLine.productAttribIds = res.productAttribIds
            //this.selectedPurchaseLine.description = `${productAttrib.product} (${productAttrib.attributeValue})`
            if (res.description)
              this.selectedPurchaseLine.description = `${res.description} `
            if (res.itemVariantId)
              this.selectedPurchaseLine.itemVariantId = res.itemVariantId
          } else {
            if (this.selectedPurchaseLine.productAttribIds == null || this.selectedPurchaseLine.productAttribIds == "")
              this.RefreshDetail()
            // this.selectedPurchaseLine.description = null
          }
        }
      })
    }
    //else
    //this.selectedPurchaseLine.description = null
  }
  calculateAmount() {
    if (this.selectedPurchaseLine.qty != undefined && this.selectedPurchaseLine.purchaseRate != undefined)
      this.selectedPurchaseLine.amount = this.selectedPurchaseLine.qty * this.selectedPurchaseLine.purchaseRate
  }
  calOfBottomFields() {
    debugger
    let totalGross = 0
    let totalDiscount = 0
    let totalGstRate = 0
    let totalGstRetRate = 0
    let totalGst = 0
    this.PurchaseLines.forEach(line => {
      var gross = line.qty * line.purchaseRate
      totalGross += gross
      var discount = gross * line.discPer / 100
      totalDiscount += discount
      if (line.gstRate > 0) {
        var gst = (gross - discount) * line.gstRate
        totalGstRate += gst
      }
      else if (line.gstRetailRate > 0) {
        var gstRet = line.retailRate * line.gstRetailRate / 100
        totalGstRetRate += gstRet
      }
      totalGst = totalGstRate + totalGstRetRate
    });
    this.selectedPurchase.gross = totalGross
    this.selectedPurchase.discount = totalDiscount
    this.selectedPurchase.gst = totalGst

  }
  TextChangeEvent() {
    // if (this.selectedPurchaseLine.gstRate! > 0 && this.selectedPurchaseLine.gstRetailRate! > 0) {
    //   this.PurchaseLineForm.form.controls['gstRate'].setErrors({ 'incorrect': true });
    //   this.PurchaseLineForm.form.controls['gstRetailRate'].setErrors({ 'incorrect': true });
    // }
    if (this.selectedPurchaseLine.gstRate != 0 && this.selectedPurchaseLine.gstRate != undefined) {
      this.PurchaseLineForm.controls['gstRetailRate'].disable();
      this.isGstRetRateRequired = false
      this.selectedPurchaseLine.gstRetailRate = undefined
    } else {
      this.PurchaseLineForm.controls['gstRetailRate'].enable();
      this.isGstRetRateRequired = true
    }
    if (this.selectedPurchaseLine.gstRetailRate != 0 && this.selectedPurchaseLine.gstRetailRate != undefined) {
      this.PurchaseLineForm.controls['gstRate'].disable();
      this.isGstRateRequired = false
      this.selectedPurchaseLine.gstRate = undefined
    } else {
      this.PurchaseLineForm.controls['gstRate'].enable();
      this.isGstRateRequired = true
    }
  }
  IsPostedCheck(event: MatCheckboxChange): void {
    debugger
    Swal.fire({
      title: 'Are you sure?',
      text: "You want to post  this voucher",
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, post it!'
    }).then((result) => {
      if (result.value) {
        this.selectedPurchase.isPosted = true
        this.selectedPurchase.statusId = Statses.Posted
        Swal.fire(
          'Successfully Posted.',
          'success'
        )
        this.onBlur()
      } else {
        this.selectedPurchase.isPosted = false
      }
    })
  }
  onselectItemUOM(uom: ItemUOMVm) {
    if (this.selectedPurchaseLine.itemVariantId > 0) {
      var itmVar = new ItemVariantsVM
      itmVar.id = this.selectedPurchaseLine.itemVariantId
      this.acntSvc.SearchItemVariants(itmVar).subscribe({
        next: (retVal: ItemVariantsVM[]) => {
          this.selectedPurchaseLine.purchaseRate = retVal[0].purchaseExtraRate + uom.purPrice
        }, error: () => {

        }
      })
    }
  }
  resetPurPrice() {
    if (this.selectedPurchaseLine.itemVariantId > 0) {
      var itmVar = new ItemVariantsVM
      itmVar.id = this.selectedPurchaseLine.itemVariantId
      this.acntSvc.SearchItemVariants(itmVar).subscribe({
        next: (retVal: ItemVariantsVM[]) => {
          this.selectedPurchaseLine.purchaseRate = retVal[0].purchaseExtraRate + this.selectedProduct.purRate
        }, error: () => {

        }
      })
    }
  }
}


