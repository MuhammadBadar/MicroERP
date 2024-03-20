import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { KeyAccountingService } from '../../key-accounting.service';
import { CreateUserRoleComponent } from 'src/app/views/security/assign-role-to-user/manage -user-role-dialog/create-user-role/create-user-role.component';
import { ItemVM, ProductAttribVm } from '../../Models/ItemVM';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { AttributeValuesVm } from '../../Models/AttributesValuesVm';
import { MatRadioChange } from '@angular/material/radio';
import { FormBuilder, FormControl, FormGroup, NgForm } from '@angular/forms';
import { SalesLineVM } from '../../Models/SalesVM';
import { ItemVariantsVM } from '../../Models/ItemVariants';


@Component({
  selector: 'app-configure-product-by-variant',
  templateUrl: './configure-product-by-variant.component.html',
  styleUrls: ['./configure-product-by-variant.component.scss']
})
export class ConfigureProductByVariantComponent implements OnInit {
  editMode: boolean = false
  addMode: boolean = true
  itemId: number
  ProductAttribIds: string[] = []
  productAttribIds: string
  Action: any;
  dataSource: any;
  action?: string;
  selectedProduct: ItemVM
  Products: ItemVM[]
  selectedProductAttrib: ProductAttribVm
  selectedProductAttribs: ProductAttribVm[] = []
  totalSaleRate: number = 0
  finalDes: string
  selectedLine: SalesLineVM
  selectedProductVariants: ProductAttribVm[] = []
  displayedColumns: ['name', 'values'];
  selectedVariant: ItemVariantsVM
  group: FormGroup;
  @ViewChild('userForm', { static: true }) UserForm!: NgForm;
  constructor(
    private cdref: ChangeDetectorRef,
    private formBuilder: FormBuilder,
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    public dialogRef: MatDialogRef<ConfigureProductByVariantComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.selectedProduct = new ItemVM
    this.selectedProductAttrib = new ProductAttribVm
    this.selectedLine = new SalesLineVM
    this.selectedVariant = new ItemVariantsVM
  }
  ngOnInit(): void {
    if (this.data != null) {
      this.itemId = this.data.id;
      this.selectedLine = this.data.selectedLine
      // if (this.data.productAttribId) {
      //   this.productAttribIds = this.data.productAttribId
      //   this.ProductAttribIds = this.productAttribIds.split(',')
      // }
      if (this.data.isEditMode) {
        this.addMode = false
        this.editMode = true
      }
      this.GetProductById(this.itemId)
    }
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
    this.cdref.markForCheck();
  }
  GetProducts() {
    var item = new ItemVM
    item.isActive = true
    this.accSvc.SearchItem(item).subscribe({
      next: (res: ItemVM[]) => {
        res = res.filter(x => x.productAttribs.length > 0)
        this.Products = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  onSelectProduct(item: ItemVM) {
    this.GetProductById(item.id)
  }
  GetProductAttrib(val: ProductAttribVm) {
    this.selectedProductAttrib = val
  }
  GetProductById(id: number) {
    var pro = new ItemVM
    pro.id = id
    this.accSvc.SearchItem(pro).subscribe({
      next: (value: ItemVM[]) => {
        this.selectedProduct = value[0]


        // const formGroup = {};
        // this.selectedProduct.attributes.forEach(formControl => {
        //   formGroup[formControl.name] = new FormControl('');
        // });
        // this.group = new FormGroup(formGroup);


        // debugger
        // var attributes = []
        // this.selectedProduct.attributes.forEach(element => {
        //   attributes.push(element.name)
        // });

        // this.selectedProductVariants = []
        // if (this.ProductAttribIds.length > 0) {
        //   this.ProductAttribIds.forEach(element => {
        //     if (element == "0" || element == undefined)
        //       this.selectedProductVariants.push(new ProductAttribVm)
        //     else {
        //       var val = this.selectedProduct.productAttribs.find(x => x.id == +element)
        //       this.selectedProductVariants.push(val)
        //     }
        //   });
        //   this.selectedProductAttribs = this.selectedProductVariants
        // }
        // console.warn(this.selectedProductAttribs)

        // for (let index = 0; index < attributes.length; index++) {
        //   const obstractControl = this.group.get(attributes[index]);
        //   if (obstractControl != undefined)
        //     this.group.controls[attributes[index]].setValue(this.selectedProductVariants[index])
        // }
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 6000)
      },
    })
  }
  Add() {
    // var des = ""
    // this.totalSaleRate = 0
    // this.ProductAttribIds = []
    // Object.keys(this.group.controls).forEach(async (key: any) => {
    //   debugger
    //   const obstractControl = this.group.get(key);
    //   var value = obstractControl.value
    //   console.warn(value)
    //   //#region  Set ProductAttribIds
    //   if (value == undefined || value.id == undefined)
    //     this.ProductAttribIds.push("0")
    //   else
    //     this.ProductAttribIds.push(value.id.toString())

    //   //#endregion
    //   //#region SetFinalDescription & SetTotalSaleRate
    //   if (value != undefined) {
    //     if (value.id != 0 && value.id != undefined) {
    //       des += value.attributeValue + ","
    //       this.finalDes = `${value.product} (${des}`
    //       this.totalSaleRate = this.totalSaleRate += value.saleRate
    //     }
    //   }
    //   //#endregion
    // })
    // this.finalDes = this.finalDes.substring(0, this.finalDes.length - 1);
    // this.finalDes += ")"
    // this.productAttribIds = this.ProductAttribIds.toString()
    // this.totalSaleRate = this.totalSaleRate += this.selectedProduct.saleRate
    this.dialogRef.close({
      data: this.selectedProductAttrib,
      saleRate: this.selectedVariant.saleExtraRate + this.selectedLine.saleRate,
      description: `${this.selectedProduct.name}( ${this.selectedVariant.possibleValues} )`,
      product: this.selectedProduct,
      productAttribIds: this.selectedVariant.attributeValuesIds
    });
  }
  radioChange(data) {
    // var find = this.selectedProductAttribs.find(x => x.attribId == data.attribId)
    // if (find != undefined)
    //   this.selectedProductAttribs = this.selectedProductAttribs.filter(x => x.attribId != data.attribId)
    // this.selectedProductAttribs.push(data)
    // this.GetProductAttrib(data)
    // this.totalSaleRate = 0
    // this.selectedProductAttribs.forEach(element => {
    //   this.totalSaleRate = this.totalSaleRate += element.saleRate
    // });
    this.selectedVariant = data
  }
  isChecked(id): boolean {
    var find = this.ProductAttribIds.find(x => x == id)
    if (find == undefined)
      return false
    else
      return true
  }

}


