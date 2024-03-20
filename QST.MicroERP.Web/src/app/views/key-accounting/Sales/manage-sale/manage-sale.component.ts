import { Component, OnInit, ViewChild } from '@angular/core';
import { SalesLineVM, SalesVM } from '../../Models/SalesVM';
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
import { throws } from 'assert';
import { ConfigureProductByVariantComponent } from '../../Product/configure-product-by-variant/configure-product-by-variant.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfigureProductComponent } from '../../Product/configure-product/configure-product.component';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { Statses } from '../../Models/Enum/Status';
import { UOMTypes } from '../../Models/Enum/UOMTypes';
import { ProductTaxes } from '../../Models/Enum/ProductTaxes';
import { SalesAccounts } from '../../Models/Enum/SalesAccounts';
import { ItemTypes } from '../../Models/Enum/ItemTypes';
import { SaleGrossAccounts } from '../../Models/Enum/SaleGrossAccounts';
import { TransactionTypes } from '../../Models/Enum/TransactionTypes';
import { UOMConversionVm } from '../../Models/UOMConversionVm';
import { UOMConversionVM } from 'src/app/views/items/Models/UOMConversionVM';
import { ItemUOMVm } from '../../Models/ItemUOMVm';
import { ItemVariantsVM } from '../../Models/ItemVariants';
import { CustomerVM } from '../../Models/CustomerVM';
import { ProductTaxesVM } from '../../Models/ProductTaxesVM';
import { delay, Observable, of, Subject } from 'rxjs';
import { VoucherDetailsVM, VoucherVM } from '../../Models/VoucherVM';
import { VchTypes } from '../../Models/Enum/VchTypes';

@Component({
  selector: 'app-manage-sale',
  templateUrl: './manage-sale.component.html',
  styleUrls: ['./manage-sale.component.css']
})
export class ManageSaleComponent implements OnInit {
  html
  isGstRateRequired: boolean = true
  isGstRetRateRequired: boolean = true
  addButton = true
  supplierDiscRate!: number
  isLoading: boolean = false
  proccessing: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = false
  Edit: boolean = false;
  Add: boolean = true;
  Products?: ItemVM[]
  Suppliers?: SupplierVM[]
  Accounts?: SettingsVM[]
  SalesId!: number
  getVchById!: SalesVM[];
  selectedSales = new SalesVM
  selectedSalesLine = new SalesLineVM
  selectedDetail = new SalesLineVM
  saleLines: SalesLineVM[] = []
  customers: CustomerVM[] = []
  Salesmen: SettingsVM[]
  selectedProduct: ItemVM
  itemUOM: ItemUOMVm[] = []
  selectedCust: CustomerVM
  voucher: VoucherVM
  @ViewChild('SalesForm', { static: true }) SalesForm!: NgForm;
  @ViewChild('SalesLineForm', { static: true }) SalesLineForm!: NgForm;
  displayeSalesColumns: string[] = ['product', 'description', 'saleQty', 'saleRate', 'discRate',
    'gstRate', 'fTaxRate', 'whtRate', 'gstRetailRate', 'amount', 'disc', 'bulkDisc', 'qtyDisc', 'gst', 'gstRet',
    'fTax', 'wht', 'chrgsAdd', 'chrgsLess', 'retailRate', 'actions'];
  dataSource: any
  outputArray = [];
  constructor(
    public acntSvc: KeyAccountingService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private _location: Location,
    public catSvc: CatalogService,) {
    this.selectedSales = new SalesVM();
    this.selectedSalesLine = new SalesLineVM()
    this.selectedProduct = new ItemVM
    this.selectedCust = new CustomerVM
    this.voucher = new VoucherVM
  }
  ngOnInit(): void {
    this.saleLines = []
    this.route.queryParams.subscribe(params => {
      this.SalesId = params['id'];
    });
    if (this.SalesId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetSalesById();
    }
    else {
      this.Add = true;
      this.Edit = false;
      this.dataSource = new MatTableDataSource(this.saleLines);
    }
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.GetProducts()
    this.GetAccounts();
    this.GetSuppliers();
    this.GetSalesmen()
    this.GetCustomers()
    this.selectedSales = new SalesVM();
    this.selectedSalesLine = new SalesLineVM()
    this.selectedSales.isActive = true
  }
  GetSalesmen() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Saleman
    stng.isActive = true
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Salesmen = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
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
  GetCustomers() {
    var cust = new CustomerVM
    cust.isActive = true
    this.acntSvc.SearchCustomer(cust).subscribe({
      next: (res: CustomerVM[]) => {
        this.customers = res;
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
  GetSalesById() {
    var pur = new SalesVM
    pur.id = this.SalesId
    this.acntSvc.SearchSale(pur).subscribe({
      next: (res: SalesVM[]) => {
        this.getVchById = res;
        this.selectedSales = this.getVchById[0]
        this.saleLines = []
        this.selectedSales.saleLines?.forEach(element => {
          this.saleLines.push(element)
          var cust = new CustomerVM
          cust.id = this.selectedSales.custId
          this.acntSvc.SearchCustomer(cust).subscribe({
            next: (value: CustomerVM[]) => {
              // this.supplierDiscRate = value[0].discRate
              // this.selectedSalesLine.discRate = this.supplierDiscRate
              this.selectedCust = value[0]
            }, error: (err) => {

            },
          })
        });
        this.dataSource = new MatTableDataSource(this.saleLines);
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
    this.selectedSalesLine = new SalesLineVM
    this.selectedSalesLine.discRate = this.supplierDiscRate
    if (this.SalesId > 0)
      this.GetSalesById()
    this.SalesLineForm.controls['gstRetailRate'].enable();
    this.SalesLineForm.controls['gstRate'].enable();
  }
  Back() {
    this._location.back();
  }
  async Submit() {
    this.SendMail();
    this.SetDates()
    this.selectedSales.saleLines = this.saleLines
    this.proccessing = true
    this.SalesValidation()

    if (!this.SalesForm.invalid) {
      if (this.selectedSales.saleLines.length == 0)
        this.catSvc.ErrorMsgBar("Please Add some Sales Detail!", 5000)
      else {
        this.selectedSales.statusId = Statses.UnPosted
        await this.UpdateSales();
      }
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
    this.proccessing = false
  }
  SaveSales() {
    this.acntSvc.SaveSale(this.selectedSales).subscribe({
      next: (res: SalesVM) => {
        this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        this.selectedSales = res
        this.saleLines = []
        this.selectedSales.saleLines?.forEach(element => {
          this.saleLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.saleLines);
        console.warn(this.saleLines)
        this.RefreshDetail()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.saleLines = []
        this.proccessing = false
      }
    })
  }
  UpdateSales() {
    this.acntSvc.UpdateSale(this.selectedSales).subscribe({
      next: (res: SalesVM) => {
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
        this.selectedSales = res
        this.saleLines = []
        this.selectedSales.saleLines?.forEach(element => {
          this.saleLines.push(element)
        });
        this.dataSource = new MatTableDataSource(this.saleLines);
        this.RefreshDetail()
        this.proccessing = false
        if (!this.Edit && res.statusId == Statses.UnPosted)
          this.ngOnInit()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.saleLines = []
        this.proccessing = false
      }
    })
  }
  edit(SalesDet: SalesLineVM) {
    this.lineEditMode = true
    this.lineAddMode = false
    this.addButton = false
    this.selectedSalesLine = SalesDet
    this.selectedDetail = SalesDet
    this.selectedSalesLine.editMode = true
    //this.TextChangeEvent()
    this.GetItemUOM(this.selectedSalesLine.productId)
  }
  delete(SalesDet: SalesLineVM) {
    if (this.saleLines.length == 1) {
      this.catSvc.ErrorMsgBar("You Can't delete it,as Sales has only one line ,and the Sales Detail Can't be Empty", 9000)
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

          if (SalesDet.id == undefined || SalesDet.id == 0) {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
          } else {
            var Sales = new SalesVM
            Sales = this.selectedSales
            Sales.saleLines = []
            Sales.saleLines.push(SalesDet)
            SalesDet.dBoperation = 3
            this.acntSvc.UpdateSale(Sales).subscribe({
              next: (data: SalesVM) => {
                Swal.fire(
                  'Deleted!',
                  'Successfully Deleted.',
                  'success'
                )
                this.saleLines = []
                data.saleLines?.forEach(element => {
                  this.saleLines.push(element)
                });
                this.dataSource = new MatTableDataSource(this.saleLines);
                //this.calOfBottomFields();
                this.NetPayable()
                this.UpdateSales();
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
  async AddSalesLinetoList() {
    this.SalesValidation()
    if (!this.SalesForm.invalid) {
      if (this.selectedSalesLine.productId == 0 && this.selectedSalesLine.productId == undefined)
        this.SalesLineForm.form.controls['productId'].setErrors({ 'incorrect': true });
      if (!this.SalesLineForm.invalid) {
        if (this.lineEditMode)
          this.selectedSalesLine.dBoperation = 2
        else
          this.selectedSalesLine.dBoperation = 1
        if (this.selectedSalesLine.dBoperation == 1) {
          this.selectedSales.statusId = Statses.Draft
          this.lineAddMode = true
        }
        debugger
        if (this.selectedSalesLine.id > 0)
          this.saleLines = this.saleLines.filter(x => x.id != this.selectedSalesLine.id)
        this.saleLines.push(this.selectedSalesLine)
        this.selectedSales.saleLines = []
        this.selectedSales.saleLines.push(this.selectedSalesLine)
        this.NetPayable()
        //this.calOfBottomFields();
        if (this.selectedSales?.id > 0)
          await this.UpdateSales();
        else
          await this.SaveSales();
      }

      else {
        this.catSvc.ErrorMsgBar("Please fill all required fields of Sales Line", 5000)
      }
    } else
      this.catSvc.ErrorMsgBar("Please fill all required fields of Sales", 5000)
  }
  SalesValidation() {
    if (this.selectedSalesLine.productId == 0 || this.selectedSalesLine.productId == undefined)
      this.SalesLineForm.form.controls['productId'].setErrors({ 'incorrect': true });
    // if (this.itemUOM.length > 0)
    //   if (this.selectedSalesLine.saleUnitId == 0 || this.selectedSalesLine.saleUnitId == undefined)
    //     this.SalesLineForm.form.controls['saleUnitId'].setErrors({ 'incorrect': true });
    if (this.selectedSales.acId == 0 || this.selectedSales.acId == undefined)
      this.SalesForm.form.controls['acId'].setErrors({ 'incorrect': true });
    if (this.selectedSales.custId == 0 || this.selectedSales.custId == undefined)
      this.SalesForm.form.controls['custId'].setErrors({ 'incorrect': true });
    if (this.selectedSales.salesmanId == 0 || this.selectedSales.salesmanId == undefined)
      this.SalesForm.form.controls['salesmanId'].setErrors({ 'incorrect': true });
  }
  onBlur() {
    console.warn(this.selectedSales.id)
    if (this.selectedSales.id > 0) {
      this.NetPayable();
      this.SalesValidation();
      if (!this.SalesForm.invalid) {
        this.SetDates()
        this.acntSvc.UpdateSale(this.selectedSales).subscribe({
          next: (res: SalesVM) => {
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
    this.selectedSales.date = moment(this.selectedSales.date).toDate()
    this.selectedSales.date = new Date(Date.UTC(this.selectedSales.date.getFullYear(), this.selectedSales.date.getMonth(), this.selectedSales.date.getDate()))
  }
  onSelectAccount() {
    this.selectedSales.supplierId = this.Suppliers?.find(x => x.accId == this.selectedSales.acId)?.id
  }
  onSelectSupplier(val: SupplierVM) {
    this.selectedSales.acId = this.Accounts?.find(x => x.id == val.accId)?.id
    this.supplierDiscRate = val.discRate
    this.selectedSalesLine.discRate = this.supplierDiscRate
  }
  onSelectCustomer(val: CustomerVM) {
    this.selectedCust = val
    this.selectedSales.acId = this.Accounts?.find(x => x.id == val.accId)?.id
    //this.supplierDiscRate = val.discRate
    //this.selectedSalesLine.discRate = this.supplierDiscRate
  }
  onSelectProduct(val: ItemVM) {
    this.GetItemUOM(val.id)
    if (val.id != this.selectedProduct.id) {
      this.selectedSalesLine.productAttribIds = null
      this.selectedSalesLine.description = null
      this.selectedSalesLine.saleRate = 0
    }
    this.selectedProduct = val
    this.selectedSalesLine.itemType = val.typeId
    this.selectedSalesLine.saleRate = this.selectedProduct.saleRate

    // if (!this.lineEditMode || val.productAttribs.length! > 0) {
    //   this.SetValuesAccordingProduct(val)
    //   this.TextChangeEvent();
    // }
    this.SetValues()
    this.OpenConfigureProductByVariantDialog(val)
  }
  ApplyFarmulas() {

    if (this.selectedSalesLine.fTaxRate == undefined || this.selectedSalesLine.fTaxRate == null)
      this.selectedSalesLine.fTaxRate = 0
    if (this.selectedSalesLine.gstRetailRate == undefined)
      this.selectedSalesLine.gstRetailRate = 0
    if (this.selectedSalesLine.gstRate == undefined)
      this.selectedSalesLine.gstRate = 0
    if (this.selectedSalesLine.whtRate == undefined)
      this.selectedSalesLine.whtRate = 0
    if (this.selectedSalesLine.saleRate == undefined)
      this.selectedSalesLine.saleRate = 0
    if (this.selectedSalesLine.discRate == undefined)
      this.selectedSalesLine.discRate = 0
    if (this.selectedSalesLine.amount == undefined)
      this.selectedSalesLine.amount = 0
    if (this.selectedSalesLine.disc == undefined)
      this.selectedSalesLine.disc = 0
    if (this.selectedSalesLine.bulkDisc == undefined)
      this.selectedSalesLine.bulkDisc = 0
    if (this.selectedSalesLine.qtyDisc == undefined)
      this.selectedSalesLine.qtyDisc = 0
    if (this.selectedSalesLine.gst == undefined)
      this.selectedSalesLine.gst = 0
    if (this.selectedSalesLine.gstRet == undefined)
      this.selectedSalesLine.gstRet = 0
    if (this.selectedSalesLine.fTax == undefined)
      this.selectedSalesLine.fTax = 0
    if (this.selectedSalesLine.wht == undefined)
      this.selectedSalesLine.wht = 0
    if (this.selectedSalesLine.chrgsAdd == undefined)
      this.selectedSalesLine.chrgsAdd = 0
    if (this.selectedSalesLine.chrgsLess == undefined)
      this.selectedSalesLine.chrgsLess = 0
    if (this.selectedSalesLine.retailRate == undefined)
      this.selectedSalesLine.retailRate = 0

    // Gross Amount
    this.selectedSalesLine.amount = this.selectedSalesLine.saleQty * this.selectedSalesLine.saleRate
    // Discount
    this.selectedSalesLine.disc = (this.selectedSalesLine.amount * this.selectedSalesLine.discRate) / 100
    // GST
    this.selectedSalesLine.gst = ((this.selectedSalesLine.amount - this.selectedSalesLine.disc
      - this.selectedSalesLine.bulkDisc - this.selectedSalesLine.qtyDisc) *
      (this.selectedSalesLine.gstRate / 100))
    // GST Retail
    this.selectedSalesLine.gstRet = this.selectedSalesLine.saleQty *
      this.selectedSalesLine.retailRate * this.selectedSalesLine.gstRetailRate / 100
    // F.Tax
    this.selectedSalesLine.fTax = (this.selectedSalesLine.amount - this.selectedSalesLine.disc -
      this.selectedSalesLine.bulkDisc - this.selectedSalesLine.qtyDisc) * this.selectedSalesLine.fTaxRate / 100
    // Wht Tax
    this.selectedSalesLine.wht = (this.selectedSalesLine.amount - this.selectedSalesLine.disc -
      this.selectedSalesLine.bulkDisc - this.selectedSalesLine.qtyDisc + this.selectedSalesLine.gst +
      this.selectedSalesLine.gstRet + this.selectedSalesLine.fTax) * this.selectedSalesLine.whtRate / 100
    this.NetPayable()
  }
  NetPayable() {
    debugger
    if (this.selectedSales.packChrgs == undefined)
      this.selectedSales.packChrgs = 0
    if (this.selectedSales.freightChrgs == undefined)
      this.selectedSales.freightChrgs = 0
    if (this.selectedSales.discount == undefined)
      this.selectedSales.discount = 0
    // NetPaybale
    this.selectedSales.netPayable = this.GetAmount() - this.GetDiscount() - this.GetBulkDisc() -
      this.GetQtyDisc() + this.GetGST() + this.GetGSTRet() + this.GetWht() + this.GetFTax() + this.GetChrgsAdd()
      - this.GetChrgsLess() - this.selectedSales.freightChrgs - this.selectedSales.discount + this.selectedSales.packChrgs
  }
  SetValues() {
    this.getProductValues(ProductTaxes.GSTSaleRate, this.selectedProduct.id, false)
    this.getProductValues(ProductTaxes.WhtRate, this.selectedProduct.id, false)
    this.getProductValues(ProductTaxes.GstRetailRate, this.selectedProduct.id, false)
    this.getProductValues(ProductTaxes.FTaxRate, this.selectedProduct.id, false)
  }
  SetValuesByVariant() {
    this.getProductValues(ProductTaxes.GSTSaleRate, this.selectedSalesLine.itemVariantId, true)
    this.getProductValues(ProductTaxes.WhtRate, this.selectedSalesLine.itemVariantId, true)
    this.getProductValues(ProductTaxes.GstRetailRate, this.selectedSalesLine.itemVariantId, true)
    this.getProductValues(ProductTaxes.FTaxRate, this.selectedSalesLine.itemVariantId, true)
  }
  getProductValues(taxId, id: number, isVariant: boolean) {
    var proTax = new ProductTaxesVM
    proTax.productId = id
    proTax.taxId = taxId
    proTax.isVariant = isVariant
    proTax.isActive = true
    this.acntSvc.SearchProductTaxes(proTax).subscribe({
      next: (res: ProductTaxesVM[]) => {
        if (res.length > 0) {
          if (taxId == ProductTaxes.FTaxRate)
            this.selectedSalesLine.fTaxRate = res[0].amount
          else if (taxId == ProductTaxes.GSTSaleRate)
            this.selectedSalesLine.gstRate = res[0].amount
          else if (taxId == ProductTaxes.WhtRate)
            this.selectedSalesLine.whtRate = res[0].amount
          else if (taxId == ProductTaxes.GstRetailRate)
            this.selectedSalesLine.gstRetailRate = res[0].amount
        } else {
          if (taxId == ProductTaxes.FTaxRate)
            this.selectedSalesLine.fTaxRate = 0
          else if (taxId == ProductTaxes.GSTSaleRate)
            this.selectedSalesLine.gstRate = 0
          else if (taxId == ProductTaxes.WhtRate)
            this.selectedSalesLine.whtRate = 0
          else if (taxId == ProductTaxes.GstRetailRate)
            this.selectedSalesLine.gstRetailRate = 0
        }
        this.selectedSalesLine.retailRate = this.selectedProduct.retailRate
        this.ApplyFarmulas()
      }, error: () => {

      }
    })
  }
  GetItemUOM(itmId) {
    var itemUOM = new ItemUOMVm
    itemUOM.itemId = itmId
    itemUOM.uomTypeId = UOMTypes.SaleUOM
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
    item.id = this.selectedSalesLine.productId
    this.acntSvc.SearchItem(item).subscribe({
      next: (items: ItemVM[]) => {
        this.selectedProduct = items[0]
        this.OpenConfigureProductByVariantDialog(this.selectedProduct)
      }
    })
  }
  SetValuesAccordingProduct(val: ItemVM) {
    this.selectedSalesLine.saleRate = val.saleRate
    this.selectedSalesLine.gstRate = val.gstSaleRate
    this.selectedSalesLine.gstRetailRate = val.saleStRate
    this.selectedSalesLine.retailRate = val.retailRate
    if (this.selectedSalesLine.saleQty != undefined && this.selectedSalesLine.saleRate != undefined)
      this.selectedSalesLine.amount = this.selectedSalesLine.saleQty * this.selectedSalesLine.saleRate
  }
  OpenConfigureProductByVariantDialog(val: ItemVM) {
    if (val.productAttribs.length > 0 || this.selectedSalesLine.productAttribIds) {
      let dialogRef = this.dialog.open(ConfigureProductComponent, {
        disableClose: true, panelClass: 'calendar-form-dialog', width: '750px'
        , data: { id: val.id, productAttribId: this.selectedSalesLine.productAttribIds, selectedLine: this.selectedSalesLine, isEditMode: this.lineEditMode }
      });
      dialogRef.afterClosed().subscribe({
        next: (res) => {
          if (res) {
            // var productAttrib = new ProductAttribVm
            // productAttrib = res.data
            if (res.product) {
              if (res.product.id != this.selectedSalesLine.productId) {
                this.selectedSalesLine.productId = res.product.id
                this.SetValuesAccordingProduct(res.product)
                this.TextChangeEvent();
              }
            }
            if (res.saleRate > 0) {
              this.selectedSalesLine.saleRate = res.saleRate
              this.selectedSalesLine.amount = this.selectedSalesLine.saleQty * this.selectedSalesLine.saleRate
            }
            if (res.productAttribIds)
              this.selectedSalesLine.productAttribIds = res.productAttribIds
            //this.selectedSalesLine.description = `${productAttrib.product} (${productAttrib.attributeValue})`
            if (res.description)
              this.selectedSalesLine.description = `${res.description} `
            if (res.itemVariantId)
              this.selectedSalesLine.itemVariantId = res.itemVariantId
            this.SetValuesByVariant()
          } else {
            // if (this.selectedSalesLine.productAttribIds == null || this.selectedSalesLine.productAttribIds == "")
            //   this.RefreshDetail()
          }
        }
      })
    }
    //else
    //this.selectedSalesLine.description = null
  }
  calculateAmount() {
    if (this.selectedSalesLine.saleQty != undefined && this.selectedSalesLine.saleRate != undefined)
      this.selectedSalesLine.amount = this.selectedSalesLine.saleQty * this.selectedSalesLine.saleRate
  }
  calOfBottomFields() {
    let totalGross = 0
    let totalDiscount = 0
    let totalGstRate = 0
    let totalGstRetRate = 0
    let totalGst = 0
    this.saleLines.forEach(line => {
      var gross = line.saleQty * line.saleRate
      totalGross += gross
      var discount = gross * line.discRate / 100
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
    this.selectedSales.gross = totalGross
    this.selectedSales.discount = totalDiscount
    this.selectedSales.gst = totalGst
  }
  TextChangeEvent() {
    if (this.selectedSalesLine.gstRate != 0 && this.selectedSalesLine.gstRate != undefined) {
      this.SalesLineForm.controls['gstRetailRate'].disable();
      this.isGstRetRateRequired = false
      this.selectedSalesLine.gstRetailRate = undefined
    } else {
      this.SalesLineForm.controls['gstRetailRate'].enable();
      this.isGstRetRateRequired = true
    }
    if (this.selectedSalesLine.gstRetailRate != 0 && this.selectedSalesLine.gstRetailRate != undefined) {
      this.SalesLineForm.controls['gstRate'].disable();
      this.isGstRateRequired = false
      this.selectedSalesLine.gstRate = undefined
    } else {
      this.SalesLineForm.controls['gstRate'].enable();
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
    }).then(async (result) => {
      if (result.value) {
        this.isLoading = true
        this.selectedSales.isPosted = true
        this.selectedSales.statusId = Statses.Posted
        //this.SendMail()
        await this.acntSvc.PostSalesToGL(this.selectedSales);
        this.isLoading = false
        //this.onBlur()
      } else {
        this.selectedSales.isPosted = false
      }
    })
  }
  PostSalesToGL() {
    debugger
    this.GetItemType().subscribe(() => {
      this.GetAccountDetail(SaleGrossAccounts.smallPkg).subscribe({
        next: (res) => {
          if (res)
            this.GetAccountDetail(SaleGrossAccounts.largePkg).subscribe({
              next: (res) => {
                if (res)
                  this.GetAccountDetail(SalesAccounts.SaleDiscount).subscribe({
                    next: (res) => {
                      if (res)
                        this.GetAccountDetail(SalesAccounts.BulkDiscount).subscribe({
                          next: (res) => {
                            if (res)
                              this.GetAccountDetail(SalesAccounts.QtyDiscount).subscribe({
                                next: (res) => {
                                  if (res)
                                    this.GetAccountDetail(SalesAccounts.SaleGST).subscribe({
                                      next: (res) => {
                                        if (res)
                                          this.GetAccountDetail(SalesAccounts.SaleGSTRet).subscribe({
                                            next: (res) => {
                                              if (res)
                                                this.GetAccountDetail(SalesAccounts.FurtherTAX).subscribe({
                                                  next: (res) => {
                                                    if (res) this.GetAccountDetail(SalesAccounts.Withholding).subscribe({
                                                      next: (res) => {
                                                        if (res)
                                                          this.GetAccountDetail(SalesAccounts.ChargesAdd).subscribe({
                                                            next: (res) => {
                                                              if (res) this.GetAccountDetail(SalesAccounts.ChargesLess).subscribe({
                                                                next: (res) => {
                                                                  if (res)
                                                                    this.GetAccountDetail(SalesAccounts.PackingCharges).subscribe({
                                                                      next: (res) => {
                                                                        if (res)
                                                                          this.GetAccountDetail(SalesAccounts.FreightCharges).subscribe({
                                                                            next: (res) => {
                                                                              if (res)
                                                                                this.GetAccountDetail(SalesAccounts.InvoiceDiscount).subscribe({
                                                                                  next: (res) => {
                                                                                    if (res) {
                                                                                      var totalCredit = this.GetCreditTotal()
                                                                                      var totalDebit = this.GetDebitTotal()
                                                                                      var custDebit = Math.abs(totalCredit - totalDebit)
                                                                                      var line = new VoucherDetailsVM
                                                                                      line.debit = custDebit
                                                                                      line.acId = this.selectedSales.acId
                                                                                      this.voucher.voucherDetails.push(line);
                                                                                      this.voucher.statusId = Statses.Draft
                                                                                      this.voucher.vchTypeKeyCode = "SALE"
                                                                                      this.voucher.vchTypeId = VchTypes.Sale
                                                                                      this.voucher.vchDate = new Date
                                                                                      this.acntSvc.SaveVoucher(this.voucher).subscribe({
                                                                                        next: () => {
                                                                                          this.catSvc.SuccessMsgBar("Suucessfully Posted in to GL", 6000)
                                                                                        },
                                                                                        error: () => {
                                                                                          this.catSvc.ErrorMsgBar("Error Occurred", 2000)
                                                                                        }
                                                                                      })
                                                                                    }
                                                                                  }
                                                                                })
                                                                            }
                                                                          })
                                                                      }
                                                                    })
                                                                }
                                                              })
                                                            }
                                                          })
                                                      }
                                                    })
                                                  }
                                                })
                                            }
                                          })
                                      }
                                    })
                                }
                              })
                          }
                        })
                    }
                  })
              }
            })
        }
      })
    })

  }
  GetDebitTotal() {
    this.voucher.voucherDetails.forEach(element => {
      if (element.debit == undefined)
        element.debit = 0
    });
    return this.voucher.voucherDetails?.map(t => t.debit).reduce((acc, value) => acc + value, 0);
  }
  GetCreditTotal() {
    this.voucher.voucherDetails.forEach(element => {
      if (element.credit == undefined)
        element.credit = 0
    });
    return this.voucher.voucherDetails?.map(t => t.credit).reduce((acc, value) => acc + value, 0);
  }
  GetItemType(): Observable<void> {
    var subject = new Subject<boolean>();
    this.selectedSales.saleLines.forEach(element => {
      var product = new ItemVM
      product.id = element.productId
      this.acntSvc.SearchItem(product).subscribe({
        next: (res: ItemVM[]) => {
          element.itemType = res[0].typeId
        }, error: () => {
          ;
        }
      })
    });
    return of(null).pipe(delay(2000));
  }
  GetAccountDetail(id: number): Observable<boolean> {
    debugger
    var subject = new Subject<boolean>();
    var acc = new SettingsVM
    acc.id = id
    acc.isActive = true
    this.catSvc.SearchSettings(acc).subscribe({
      next: (res: SettingsVM[]) => {
        if (id == SaleGrossAccounts.smallPkg) {
          var smallPkgGrossSale = this.selectedSales.saleLines
            .filter(line => line.itemType === ItemTypes.smallPkg)
            .reduce((sum, line) => sum + line.amount, 0);

          if (smallPkgGrossSale > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = smallPkgGrossSale
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SaleGrossAccounts.largePkg) {
          var largePkgGrossSale = this.selectedSales.saleLines
            .filter(line => line.itemType === ItemTypes.largePkg)
            .reduce((sum, line) => sum + line.amount, 0);
          if (largePkgGrossSale > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = largePkgGrossSale
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.InvoiceDiscount) {
          if (this.selectedSales.discount > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = this.selectedSales.discount
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.SaleDiscount) {
          //SaleDiscount
          var disc = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.disc, 0);
          if (disc > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = disc
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.BulkDiscount) {
          //BulkDiscount
          var bulkDisc = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.bulkDisc, 0);
          if (bulkDisc > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = bulkDisc
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.QtyDiscount) {
          //QtyDiscount
          var qtyDisc = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.qtyDisc, 0);
          if (qtyDisc > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = qtyDisc
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.SaleGST) {
          //SaleGst
          var gst = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.gst, 0);
          if (gst > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = gst
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.SaleGSTRet) {
          //GstRet
          var gstRet = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.gstRet, 0);
          if (gstRet > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = gstRet
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.Withholding) {
          //wth
          var wth = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.wht, 0);
          if (wth > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = wth
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.FurtherTAX) {
          //fTax
          var fTax = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.fTax, 0);
          if (fTax > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = fTax
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.ChargesAdd) {
          //ChargAdd
          var chrgAdd = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.chrgsAdd, 0);
          if (chrgAdd > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = chrgAdd
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.ChargesLess) {
          //chrgLess
          var chrgLess = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.chrgsLess, 0);
          if (chrgLess > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = chrgLess
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.PackingCharges) {
          //pkgChrgs
          if (this.selectedSales.packChrgs > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = this.selectedSales.packChrgs
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.FreightCharges) {
          //freightChrgs
          if (this.selectedSales.freightChrgs > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = this.selectedSales.freightChrgs
            this.voucher.voucherDetails.push(line)
          }
        }
        subject.next(true);
      }, error: () => {

      }
    })
    return subject.asObservable();
  }
  onselectItemUOM(uom: ItemUOMVm) {
    if (this.selectedSalesLine.itemVariantId > 0) {
      var itmVar = new ItemVariantsVM
      itmVar.id = this.selectedSalesLine.itemVariantId
      this.acntSvc.SearchItemVariants(itmVar).subscribe({
        next: (retVal: ItemVariantsVM[]) => {
          this.selectedSalesLine.saleRate = retVal[0].saleExtraRate + uom.salePrice
          this.ApplyFarmulas()
        }, error: () => {

        }
      })
    } else {
      this.selectedSalesLine.saleRate = uom.salePrice
      this.ApplyFarmulas()
    }
  }
  OnDeselectUOM() {
    if (this.selectedSalesLine.itemVariantId > 0) {
      var itmVar = new ItemVariantsVM
      itmVar.id = this.selectedSalesLine.itemVariantId
      this.acntSvc.SearchItemVariants(itmVar).subscribe({
        next: (retVal: ItemVariantsVM[]) => {
          this.selectedSalesLine.saleRate = retVal[0].saleExtraRate + this.selectedProduct.saleRate
          this.ApplyFarmulas()
        }, error: () => {

        }
      })
    } else {
      this.selectedSalesLine.saleRate = this.selectedProduct.saleRate
      this.ApplyFarmulas()
    }
  }
  SendMail() {
    if (this.selectedCust.sendEmail)
      this.acntSvc.SendMailtoCustomer(this.selectedSales, this.selectedCust)
    this.html = this.catSvc.html
  }
  GetQtyTotal() {
    return this.saleLines?.map(t => t.saleQty).reduce((acc, value) => acc + value, 0);
  }
  GetDiscount() {
    return this.saleLines?.map(t => t.disc).reduce((acc, value) => acc + value, 0);
  }
  GetBulkDisc() {
    return this.saleLines?.map(t => t.bulkDisc).reduce((acc, value) => acc + value, 0);
  }
  GetQtyDisc() {
    return this.saleLines?.map(t => t.qtyDisc).reduce((acc, value) => acc + value, 0);
  }
  GetGST() {
    return this.saleLines?.map(t => t.gst).reduce((acc, value) => acc + value, 0);
  }
  GetGSTRet() {
    return this.saleLines?.map(t => t.gstRet).reduce((acc, value) => acc + value, 0);
  }
  GetFTax() {
    return this.saleLines?.map(t => t.fTax).reduce((acc, value) => acc + value, 0);
  }
  GetWht() {
    return this.saleLines?.map(t => t.wht).reduce((acc, value) => acc + value, 0);
  }
  GetChrgsAdd() {
    return this.saleLines?.map(t => t.chrgsAdd).reduce((acc, value) => acc + value, 0);
  }
  GetChrgsLess() {
    return this.saleLines?.map(t => t.chrgsLess).reduce((acc, value) => acc + value, 0);
  }
  GetAmount() {
    return this.saleLines?.map(t => t.amount).reduce((acc, value) => acc + value, 0);
  }
}


