import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { KeyAccountingService } from './../key-accounting.service';
import { Component, NgModule, OnInit, ViewChild } from '@angular/core';
import { ItemVM, ProductAttribVm, ProductwithVariantsVM } from '../Models/ItemVM';
import { CatalogService } from '../../catalog/catalog.service';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { AttributesVm } from '../Models/AttributesVm';
import { AttributeValuesVm } from '../Models/AttributesValuesVm';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { ConfigureProductComponent } from '../Product/configure-product/configure-product.component';
import { ManageAttributesComponent } from '../manage-attributes/manage-attributes.component';
import { ManageAttributesValuesComponent } from '../manage-attributes-values/manage-attributes-values.component';
import { ConfigureProductAttribComponent } from '../Product/configure-product-attrib/configure-product-attrib.component';
import { ItemVariantsVM } from '../Models/ItemVariants';
import { EnumTypes } from '../../catalog/Models/EnumTypes';
import { ItemsService } from '../../items/items.service';
import { ConfigureSaleRateByVariantComponent } from '../Product/configure-sale-rate-by-variant/configure-sale-rate-by-variant.component';
import { ConfigurePurchaseRateByVariantComponent } from '../Product/configure-purchase-rate-by-variant/configure-purchase-rate-by-variant.component';
import { ConfigureBarCodeComponent } from '../Product/configure-bar-code/configure-bar-code.component';
import { UOMConversionVm } from '../Models/UOMConversionVm';
import { UOMConversionVM } from '../../items/Models/UOMConversionVM';
import { SalesUOMComponent } from '../Product/sales-uom/sales-uom.component';
import { PurchaseUOMComponent } from '../Product/purchase-uom/purchase-uom.component';
import { ItemUOMVm } from '../Models/ItemUOMVm';
import { UOMTypes } from '../Models/Enum/UOMTypes';
import { ProductTaxesVM } from '../Models/ProductTaxesVM';
@Component({
  selector: 'app-manage-item',
  templateUrl: './manage-item.component.html',
  styleUrls: ['./manage-item.component.css'],
})
export class ManageItemComponent implements OnInit {
  products: ProductwithVariantsVM[] = []
  taxesSource: any
  taxesColumns: string[] = ['product', 'taxes'];
  taxes: SettingsVM[] = []
  itemId: number
  TabIndex: number = 0
  ispurStRateRequired: boolean = true
  isgstPurRateRequired: boolean = true
  isSaleStRateRequired: boolean = true
  isgstSaleRateRequired: boolean = true
  lineAddMode: boolean = true
  lineEditMode: boolean = false
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  Item: ItemVM[] | undefined;
  selectedItem: ItemVM;
  selectedProductAttrib: ProductAttribVm
  Countries?: SettingsVM[]
  Cities?: SettingsVM[]
  Attributes: SettingsVM[]
  AttribValues: SettingsVM[]
  ProductAttribs: ProductAttribVm[] = []
  itemVariants: ItemVariantsVM[] = []
  uomConversions: UOMConversionVm[]
  saleUnitList: ItemUOMVm[] = []
  purUnitList: ItemUOMVm[] = []
  itemTypes: SettingsVM[]
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  @ViewChild('proAttr', { static: true }) proAttribForm!: NgForm;
  product: ItemVM
  displayedColumns: string[] = ['name', 'saleRate', 'purRate', 'gstSaleRate', 'saleStRate', 'gstPurRate',
    'purStRate', 'retailRate', 'saleUnit', 'purUnit', 'extraRate', 'actions'];
  DisplayedColumns: string[] = ['attrib', 'attribValues', 'actions'];
  itemVariantsColumns: string[] = ['item', 'possibleValues', 'saleExtraRate',
    'purchaseExtraRate', 'barCode', 'stockValue', 'actions']
  // itemVariantsColumns: string[] = ['item', 'possibleValues', 'saleExtraRate',
  //   'purchaseExtraRate', 'barCode', 'stockValue', 'actions']
  iVarDataSource: any
  dataSource: any;
  DataSource: any;
  constructor(
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private location: Location,
  ) {
    this.selectedProductAttrib = new ProductAttribVm
    this.selectedItem = new ItemVM();
    this.product = new ItemVM
    this.selectedItem.isActive = true
  }
  ngOnInit(): void {
    this.ProductAttribs = []
    this.route.queryParams.subscribe(params => {
      this.itemId = params['id'];
    });
    if (this.itemId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetItemForEdit(this.itemId);
    }
    else {
      this.Add = true;
      this.Edit = false;
    }
    this.GetItem();
    this.GetItemTypes()
    this.GetAttributeValues()
    this.GetAttributes()
    this.UOMConversions()
    this.GetProduct();
    this.GetTaxes()
  }
  //#region API Search Methods
  UOMConversions() {
    var unit = new UOMConversionVm
    unit.isActive = true
    this.accSvc.SearchUOMConversion(unit).subscribe({
      next: (res: UOMConversionVm[]) => {
        this.uomConversions = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetItem() {
    this.accSvc.GetItem().subscribe({
      next: (res: ItemVM[]) => {
        this.Item = res;
        this.dataSource = new MatTableDataSource(this.Item);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetAttributes() {
    var attrib = new SettingsVM
    attrib.isActive = true
    attrib.enumTypeId = EnumTypes.Attributes
    this.catSvc.SearchSettings(attrib).subscribe({
      next: (res: SettingsVM[]) => {
        this.Attributes = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetItemTypes() {
    var type = new SettingsVM
    type.isActive = true
    type.enumTypeId = EnumTypes.ItemTypes
    this.catSvc.SearchSettings(type).subscribe({
      next: (res: SettingsVM[]) => {
        this.itemTypes = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetAttributeValues() {
    var attrib = new SettingsVM
    attrib.isActive = true
    attrib.enumTypeId = EnumTypes.AttributesValues
    if (this.selectedProductAttrib.attribId != 0)
      attrib.parentId = this.selectedProductAttrib.attribId
    this.catSvc.SearchSettings(attrib).subscribe({
      next: (res: SettingsVM[]) => {
        this.AttribValues = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetItemForEdit(id: number) {
    var item = new ItemVM;
    item.id = id
    this.accSvc.SearchItem(item).subscribe({
      next: (res: ItemVM[]) => {
        this.Item = res;
        this.selectedItem = this.Item[0]
        this.product = res[0]
        this.ProductAttribs = []
        this.selectedItem.productAttribs?.forEach(element => {
          this.ProductAttribs.push(element)
        });
        this.itemVariants = []
        this.selectedItem.variants?.forEach(x => {
          this.itemVariants.push(x)
        })
        this.selectedItem.itemUOMList.forEach(x => {
          if (x.uomTypeId == UOMTypes.SaleUOM)
            this.saleUnitList.push(x)
          else if (x.uomTypeId == UOMTypes.PurchaseUOM)
            this.purUnitList.push(x)
        })
        this.iVarDataSource = new MatTableDataSource(this.itemVariants)
        this.DataSource = new MatTableDataSource(this.ProductAttribs);
        this.selectedItem.variants = []
        //this.selectedItem.itemUOMList = []
        this.Edit = true;
        this.Add = false;
        //this.TextChangeEventforPur()
        //this.TextChangeEventforSale()
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SearchItem() {

  }
  //#endregion
  //#region Save & Update Methods
  SaveItem() {
    this.proccessing = true
    this.CheckValidation();
    if (!this.userForm.invalid) {
      this.SetItemUOMList();
      this.accSvc.SaveItem(this.selectedItem).subscribe({
        next: (res: ItemVM) => {
          this.catSvc.SuccessMsgBar("Product Successfully Added!", 5000)
          this.proccessing = false
          this.selectedItem = res
          this.product = res
          this.ProductAttribs = []
          this.selectedItem.productAttribs?.forEach(element => {
            this.ProductAttribs.push(element)
          });
          this.Edit = true;
          this.Add = false
          this.DataSource = new MatTableDataSource(this.ProductAttribs);
          this.refreshLine()
          this.GetProduct();
          this.GetTaxes()
          //this.Refresh();
        }, error: (e) => {
          this.catSvc.ErrorMsgBar("Error Occurred", 5000)
          console.warn(e);
          this.proccessing = false
        }
      })
    } else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
      this.TabIndex = 0
    }
  }
  Update() {
    var item = new ItemVM
    item = this.selectedItem
    this.UpdateItem(item)
  }
  UpdateItem(item) {
    debugger
    this.proccessing = true
    this.CheckValidation();
    if (!this.userForm.invalid) {
      // this.SetItemUOMList();
      this.accSvc.UpdateItem(item).subscribe({
        next: (res: ItemVM) => {
          this.catSvc.SuccessMsgBar("Product Successfully Updated!", 5000)
          this.proccessing = false
          this.product = res
          this.selectedItem = res
          this.ProductAttribs = []
          this.selectedItem.productAttribs?.forEach(element => {
            this.ProductAttribs.push(element)
          });
          this.DataSource = new MatTableDataSource(this.ProductAttribs);
          this.itemVariants = []
          this.selectedItem.variants?.forEach(x => {
            this.itemVariants.push(x)
          })
          this.iVarDataSource = new MatTableDataSource(this.itemVariants)
          this.selectedItem.variants = []
          //this.selectedItem.itemUOMList = []
          this.refreshLine()
          this.GetProduct();
          this.GetTaxes()
          // this.Refresh();
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
      this.TabIndex = 0
    }
  }
  //#endregion
  //#region Product Attributes
  async AddLine() {
    this.CheckValidation();
    if (!this.userForm.invalid) {
      if (this.selectedProductAttrib.attribId == 0 || this.selectedProductAttrib.attribId == undefined)
        this.proAttribForm.form.controls['attribId'].setErrors({ 'incorrect': true });
      if (!this.proAttribForm.invalid) {
        if (this.lineEditMode)
          this.selectedProductAttrib.dBoperation = 2
        else
          this.selectedProductAttrib.dBoperation = 1
        if (this.selectedProductAttrib.dBoperation == 1) {
          this.lineAddMode = true
        }
        if (this.selectedProductAttrib.id > 0)
          this.ProductAttribs = this.ProductAttribs.filter(x => x.id != this.selectedProductAttrib.id)
        this.ProductAttribs.push(this.selectedProductAttrib)
        this.selectedItem.productAttribs = []
        this.selectedItem.productAttribs?.push(this.selectedProductAttrib)
        if (this.selectedItem?.id > 0) {
          var item = new ItemVM
          item = this.selectedItem
          item.variants = []
          await this.UpdateItem(item);
        }
        else
          await this.SaveItem();
        this.Add = false
        this.Edit = true
      }
      else {
        this.catSvc.ErrorMsgBar("Please fill all required fields of line", 5000)
      }
    } else {
      this.catSvc.ErrorMsgBar("Please fill all required fields of Product", 5000)
      this.TabIndex = 0
    }
  }
  DeleteProductAttrib(id: number) {
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
        console.warn("0")
        this.accSvc.DeleteProductAttrib(id).subscribe({
          next: async (data) => {
            Swal.fire(
              'Deleted!',
              'Successfully deleted.',
              'success'
            )
            var item = new ItemVM;
            item.id = this.selectedItem.id
            this.accSvc.SearchItem(item).subscribe({
              next: (res: ItemVM[]) => {
                this.Item = res;
                this.product = res[0]
                this.selectedItem = this.Item[0]
                this.ProductAttribs = []
                this.selectedItem.productAttribs?.forEach(element => {
                  this.ProductAttribs.push(element)
                });
                this.itemVariants = []
                this.selectedItem.variants?.forEach(x => {
                  this.itemVariants.push(x)
                })
                this.iVarDataSource = new MatTableDataSource(this.itemVariants)
                this.DataSource = new MatTableDataSource(this.ProductAttribs);
                this.Edit = true;
                this.Add = false;
                //this.TextChangeEventforPur()
                //this.TextChangeEventforSale()
                this.generateVariants()
              }, error: (e) => {
                this.catSvc.ErrorMsgBar("Error Occurred", 5000)
                console.warn(e);
              }
            })
            //this.generateVariants()
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  //#endregion
  //#region Product Variants
  generateVariants() {
    // if (this.selectedItem.variants.length > 0)
    //   this.catSvc.ErrorMsgBar("Variants for this Product already defined", 6000)
    // else {
    debugger
    var variantList = []
    if (this.ProductAttribs.length > 0) {
      var arrays = this.split(this.ProductAttribs)
      var attribValIds = this.itemAttribIds(arrays)
      var combination = this.cartesian(attribValIds);
      combination.forEach(element => {
        var variant = new ItemVariantsVM()
        variant.attributeValuesIds = (element.sort((a, b) => +a - +b)).join(',')
        variant.dboperation = 1
        //var search = this.itemVariants.find(x => x.attributeValuesIds == variant.attributeValuesIds)
        //if (search == undefined)
        variantList.push(variant)
      });
      var item = new ItemVM
      item = this.selectedItem
      item.productAttribs = []
      item.variants = variantList
      this.UpdateItem(item)

    }
    else
      this.catSvc.ErrorMsgBar("Please add some attributes to generate Variants", 6000)
    //  }
  }
  itemAttribIds(arr) {
    var retArray = []
    arr.forEach((element) => {
      let ids = element.map(({ attribValId }) => attribValId)
      retArray.push(ids)
    });
    return retArray;
  }
  split(arr) {
    let groupingViaCommonProperty = Object.values(
      arr.reduce((acc, current) => {
        acc[current.attribId] = acc[current.attribId] ?? [];
        acc[current.attribId].push(current);
        return acc;
      }, {})
    );
    return groupingViaCommonProperty
  }
  cartesian(args) {
    var r = [],
      max = args.length - 1;
    function helper(arr, i) {
      for (var j = 0, l = args[i].length; j < l; j++) {
        var a = arr.slice(0); // clone arr
        a.push(args[i][j]);
        if (i == max)
          r.push(a);
        else
          helper(a, i + 1);
      }
    }
    helper([], 0);
    return r;
  }
  //#endregion
  //#region Dialogs
  ConfigureSaleRate(id: number) {
    let dialogRef = this.dialog.open(ConfigureSaleRateByVariantComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '600px'
      , data: { id: id }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetItemForEdit(this.selectedItem.id)
      });
  }
  ConfigurePurRate(id: number) {
    let dialogRef = this.dialog.open(ConfigurePurchaseRateByVariantComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '600px'
      , data: { id: id }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetItemForEdit(this.selectedItem.id)
      });
  }
  ConfigureBarCode(id: number) {
    let dialogRef = this.dialog.open(ConfigureBarCodeComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '600px'
      , data: { id: id }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetItemForEdit(this.selectedItem.id)
      });
  }
  AttribValDialog() {
    var dialogRef = this.dialog.open(ManageAttributesValuesComponent, {
      width: '1200px', height: '590px'
      , data: { isDialog: true, attribId: this.selectedProductAttrib.attribId }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetAttributeValues()
      });
  }
  AttributeDialog() {
    var dialogRef = this.dialog.open(ManageAttributesComponent, {
      width: '1200px', height: '590px'
      , data: { isDialog: true }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetAttributes()
      });
  }
  SalesUOMDialog() {
    var dialogRef = this.dialog.open(SalesUOMComponent, {
      width: '900px', height: '490px'
      , data: { item: this.selectedItem }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        if (res) {
          this.saleUnitList = res.itemUOMList
          this.selectedItem.saleUnitIds = res.saleUnits
          this.SetItemUOMList()
          if (this.Edit)
            this.UpdateItem(this.selectedItem)
          //this.Update()
        }
      });
  }
  PurchaseUOMDialog() {
    var dialogRef = this.dialog.open(PurchaseUOMComponent, {
      width: '900px', height: '490px'
      , data: { item: this.selectedItem }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        debugger
        if (res) {
          debugger
          this.purUnitList = res.itemUOMList
          this.selectedItem.purUnitIds = res.purchaseUnits
          this.SetItemUOMList()
          if (this.Edit)
            this.UpdateItem(this.selectedItem)
        }
      });
  }
  //#endregion
  //#region Reset Form
  Refresh() {
    this.Add = true;
    this.Edit = false;
    this.selectedItem = new ItemVM
    // this.GetItem();
    this.userForm.controls['purStRate'].enable();
    this.userForm.controls['gstPurRate'].enable();
    this.userForm.controls['saleStRate'].enable();
    this.userForm.controls['gstSaleRate'].enable();
  }
  RefreshIcon() {
    this.Refresh()
    this.refreshLine()
  }
  refreshLine() {
    this.selectedProductAttrib = new ProductAttribVm
    this.GetAttributeValues()
  }
  //#endregion
  //#region Others
  SetItemUOMList() {
    this.selectedItem.itemUOMList = []
    this.saleUnitList.forEach(element => {
      this.selectedItem.itemUOMList.push(element)
    });
    this.purUnitList.forEach(element => {
      this.selectedItem.itemUOMList.push(element)
    });
  }
  TextChangeEventforPur() {
    if (this.selectedItem.gstPurRate != 0 && this.selectedItem.gstPurRate != undefined) {
      this.userForm.controls['purStRate'].disable();
      this.ispurStRateRequired = false
      this.selectedItem.purStRate = undefined
    } else {
      this.userForm.controls['purStRate'].enable();
      this.ispurStRateRequired = true
    }
    if (this.selectedItem.purStRate != 0 && this.selectedItem.purStRate != undefined) {
      this.userForm.controls['gstPurRate'].disable();
      this.isgstPurRateRequired = false
      this.selectedItem.gstPurRate = undefined
    } else {
      this.userForm.controls['gstPurRate'].enable();
      this.isgstPurRateRequired = true
    }
  }
  TextChangeEventforSale() {
    if (this.selectedItem.gstSaleRate != 0 && this.selectedItem.gstSaleRate != undefined) {
      this.userForm.controls['saleStRate'].disable();
      this.isSaleStRateRequired = false
      this.selectedItem.saleStRate = undefined
    } else {
      this.userForm.controls['saleStRate'].enable();
      this.isSaleStRateRequired = true
    }
    if (this.selectedItem.saleStRate != 0 && this.selectedItem.saleStRate != undefined) {
      this.userForm.controls['gstSaleRate'].disable();
      this.isgstSaleRateRequired = false
      this.selectedItem.gstSaleRate = undefined
    } else {
      this.userForm.controls['gstSaleRate'].enable();
      this.isgstSaleRateRequired = true
    }
  }
  isDisable(val: SettingsVM): boolean {
    var find = this.ProductAttribs.find(x => x.attribValId == val.id)
    if (find == undefined)
      return false
    else
      return true
  }
  Back() {
    this.location.back();
  }
  CheckValidation() {
    if (this.selectedItem.typeId == 0 || this.selectedItem.typeId == undefined)
      this.userForm.controls['typeId'].setErrors({ 'incorrect': true })
  }
  //#endregion
  //#region Taxes
  GetTaxes() {
    var stng = new SettingsVM
    stng.isActive = true
    stng.enumTypeId = EnumTypes.Taxes
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.taxes = res
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
      }
    })
  }
  GetProduct() {
    var item = new ProductwithVariantsVM

    this.accSvc.SearchItemwithVariants(item).subscribe({
      next: (res: ProductwithVariantsVM[]) => {
        this.products = res
        console.warn(res)
        this.taxesSource = new MatTableDataSource(res)
      }, error: () => {

      }
    })
  }
  onBlur(proTax: ProductTaxesVM) {
    debugger
    if (proTax.amount) {
      proTax.isActive = true
      this.accSvc.SaveProductTaxes(proTax).subscribe({
        next: () => {
          this.catSvc.SuccessMsgBar("Product Tax Successfully Updated", 4000)
        }, error: () => {
          this.catSvc.ErrorMsgBar("Error Occurred", 4000);
        }
      })
    }
  }
  //#endregion
}


