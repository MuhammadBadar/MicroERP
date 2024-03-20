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
import { Observable, Subject } from 'rxjs';
import { ItemUOMVm } from '../../Models/ItemUOMVm';

@Component({
  selector: 'app-configure-product',
  templateUrl: './configure-product.component.html',
  styleUrls: ['./configure-product.component.css']
})
export class ConfigureProductComponent implements OnInit {
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
  displayedColumns: ['name', 'values']
  group: FormGroup;
  searchedVariant: ItemVariantsVM
  @ViewChild('userForm', { static: true }) UserForm!: NgForm;
  constructor(
    private cdref: ChangeDetectorRef,
    private formBuilder: FormBuilder,
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    public dialogRef: MatDialogRef<ConfigureProductComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.selectedProduct = new ItemVM
    this.selectedProductAttrib = new ProductAttribVm
    this.selectedLine = new SalesLineVM
    this.searchedVariant = new ItemVariantsVM()
  }
  async ngOnInit(): Promise<void> {
    if (this.data != null) {
      this.itemId = this.data.id;
      if (this.data.productAttribId) {
        this.productAttribIds = this.data.productAttribId
        this.ProductAttribIds = this.productAttribIds.split(',')
      }
      if (this.data.isEditMode) {
        this.addMode = false
        this.editMode = true
      }
      if (this.data.selectedLine) {
        this.totalSaleRate = this.data.selectedLine.saleRate
        this.selectedLine = this.data.selectedLine
      }
      await this.GetProductById(this.itemId)

      //this.GetProducts()
    }
  }
  searchItemUOM() {
    if (this.selectedLine.saleUnitId > 0) {
      var uom = new ItemUOMVm
      uom.id = this.selectedLine.saleUnitId
      this.accSvc.SearchItemUOM(uom).subscribe({
        next: (retVal: ItemUOMVm[]) => {
          this.selectedProduct.saleRate = retVal[0].salePrice
        }, error: () => {

        }
      })
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
    // if ((this.selectedProductAttrib.productId != 0 && this.selectedProductAttrib.productId != undefined) &&
    //   (this.selectedProductAttrib.attribValId != 0 && this.selectedProductAttrib.attribValId != undefined)) {
    //   var attrib = new ProductAttribVm
    //   attrib.productId = this.selectedProductAttrib.productId
    //   attrib.id = this.selectedProductAttrib.attribValId
    //   this.accSvc.SearchProductAttrib(attrib).subscribe({
    //     next: (value: ProductAttribVm[]) => {

    //       if (value[0].saleRate > 0)
    //         this.selectedProductAttrib.saleRate += value[0].saleRate
    //       else
    //         this.selectedProductAttrib.saleRate = 0
    //       this.selectedProductAttrib.attribute = value[0].attribute
    //       this.selectedProductAttrib.attributeValue = value[0].attributeValue
    //       this.selectedProductAttrib.product = value[0].product
    //     }, error: (err) => {
    //       console.warn(err)
    //       this.catSvc.ErrorMsgBar("Error Occurred", 6000)
    //     },
    //   })
    // }
  }
  GetProductById(id: number) {
    var pro = new ItemVM
    pro.id = id
    this.accSvc.SearchItem(pro).subscribe({
      next: (value: ItemVM[]) => {
        this.selectedProduct = value[0]

        this.searchItemUOM()

        const formGroup = {};
        this.group = new FormGroup(formGroup);
        this.selectedProduct.attributes.forEach(formControl => {
          formGroup[formControl.name] = new FormControl('');
        });


        var attributes = []
        this.selectedProduct.attributes.forEach(element => {
          attributes.push(element.name)
        });

        this.selectedProductVariants = []
        if (this.ProductAttribIds.length > 0) {
          this.ProductAttribIds.forEach(element => {
            if (element == "0" || element == undefined)
              this.selectedProductVariants.push(new ProductAttribVm)
            else {
              var val = this.selectedProduct.productAttribs.find(x => x.attribValId == +element)
              this.selectedProductVariants.push(val)
            }
          });
          this.selectedProductAttribs = this.selectedProductVariants
        }
        // this.selectedProduct.productAttribs.forEach(element => {
        //   if (this.ProductAttribIds.length > 0)
        //     this.ProductAttribIds.forEach(x => {
        //       if (element.id == +x)
        //         this.selectedProductVariants.push(element)
        //       else
        //         this.selectedProductVariants.push(new ProductAttribVm)
        //     });
        // });

        for (let index = 0; index < attributes.length; index++) {
          const obstractControl = this.group.get(attributes[index]);
          if (obstractControl != undefined)
            this.group.controls[attributes[index]].setValue(this.selectedProductVariants[index])
        }
        //this.totalSaleRate = this.selectedProduct.saleRate
        // attributes.forEach(x => {
        //   debugger
        //   const obstractControl = this.group.get(x);
        //   console.warn(obstractControl)
        //   if (obstractControl != undefined)
        //     this.group.controls[x].setValue(this.ProductAttribIds[x])
        // })

        // this.dataSource = this.selectedProduct.attributes
        // this.selectedProductAttrib = new ProductAttribVm
        // this.selectedProductAttrib.productId = this.selectedProduct.id
        // this.totalSaleRate = 0
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 6000)
      },
    })
  }
  Add() {
    const invalid = [];
    const controls = this.group.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    console.warn(invalid)
    // var des = ""

    // this.ProductAttribIds = []
    // Object.keys(this.group.controls).forEach(async (key: any) => {
    //   const obstractControl = this.group.get(key);
    //   var value = obstractControl.value
    //   //#region  Set ProductAttribIds
    //   if (value == undefined || value.id == undefined) {
    //     this.ProductAttribIds.push("0")
    //   }
    //   else {
    //     this.ProductAttribIds.push(value.attribValId.toString())
    //   }
    //#endregion
    //#region SetFinalDescription & SetTotalSaleRate
    // if (value != undefined) {
    //   if (value.id != 0 && value.id != undefined) {
    //     // des += value.attributeValue + ","
    //     // this.finalDes = `${value.product} (${des}`
    //     this.totalSaleRate = this.totalSaleRate += value.saleRate
    //   }
    // }
    //#endregion
    // })
    // this.finalDes = this.finalDes.substring(0, this.finalDes.length - 1);
    //this.finalDes += ")"

    // var search = this.ProductAttribIds.find(X => X == "0")
    this.ProductAttribIds = this.ProductAttribIds.sort((a, b) => +a - +b)
    this.productAttribIds = this.ProductAttribIds.toString()
    if (invalid.length == 0) {
      this.totalSaleRate = this.selectedProduct.saleRate
      this.SearchVariant().subscribe((res) => {
        if (res) {
          this.totalSaleRate = this.searchedVariant.saleExtraRate + this.selectedProduct.saleRate
          this.dialogRef.close({
            data: this.selectedProductAttrib,
            saleRate: this.totalSaleRate,
            //description: this.finalDes,
            description: `${this.searchedVariant.item} (${this.searchedVariant.possibleValues}) `,
            product: this.selectedProduct,
            productAttribIds: this.productAttribIds,
            itemVariantId: this.searchedVariant.id
          });
        } else { this.catSvc.ErrorMsgBar("This Variant not found Please Choose another", 5000) }
      })
    } else
      this.catSvc.ErrorMsgBar(`Please select " ${invalid[0]} " of the ${this.selectedProduct.name}`, 4000)
    // this.catSvc.ErrorMsgBar("Please select all Attributes to continue", 4000)
  }
  radioChange(data) {
    // this.totalSaleRate = 0
    // Object.keys(this.group.controls).forEach((key: any) => {
    //   debugger
    //   const obstractControl = this.group.get(key);
    //   var value = obstractControl.value
    //   //#region SetFinalDescription & SetTotalSaleRate
    //   if (value != undefined) {
    //     if (value.id != 0 && value.id != undefined) {
    //       this.totalSaleRate = this.totalSaleRate += value.saleRate
    //     }
    //   }
    //   //#endregion
    // })
    var find = this.selectedProductAttribs.find(x => x.attribId == data.attribId)
    if (find != undefined)
      this.selectedProductAttribs = this.selectedProductAttribs.filter(x => x.attribId != data.attribId)
    this.selectedProductAttribs.push(data)
    this.GetProductAttrib(data)
    this.totalSaleRate = 0
    // this.ProductAttribIds = []
    // var des = "";
    this.selectedProductAttribs.forEach(element => {
      this.totalSaleRate = this.totalSaleRate += element.saleRate
      // if (this.selectedProductAttribs[0] == element)
      //   des += element.attributeValue
      // else
      //   des += "," + element.attributeValue
      // this.finalDes = `${element.product} (${des} )`
      // this.ProductAttribIds.push(element.id.toString())
    });
    this.ProductAttribIds = []
    this.productAttribIds = ''
    this.selectedProductAttribs.forEach(x => {
      if (x != undefined) {
        if (x.attribValId != 0 && x.attribValId != undefined)
          this.ProductAttribIds.push(x.attribValId.toString())
      }
    })
    this.ProductAttribIds.sort((a, b) => +a - +b)
    this.productAttribIds = this.ProductAttribIds.toString()
    this.SearchVariant().subscribe((res) => {
      if (res == true) {
        this.selectedProductAttrib.saleRate = this.searchedVariant.saleExtraRate
        this.totalSaleRate = this.selectedProduct.saleRate + this.searchedVariant.saleExtraRate
      }
      else {
        this.totalSaleRate = this.selectedProduct.saleRate
        this.selectedProductAttrib.saleRate = 0
      }
    })
    // this.productAttribIds = this.ProductAttribIds.toString()
    // console.warn(this.productAttribIds)
  }
  isChecked(id): boolean {
    var find = this.ProductAttribIds.find(x => x == id)
    if (find == undefined)
      return false
    else
      return true
  }
  SearchVariant(): Observable<boolean> {
    this.searchedVariant = new ItemVariantsVM
    var variant = new ItemVariantsVM
    variant.itemId = this.selectedProduct.id
    variant.attributeValuesIds = this.productAttribIds
    var subject = new Subject<boolean>();
    this.accSvc.SearchItemVariants(variant).subscribe({
      next: (retVal: ItemVariantsVM[]) => {
        this.searchedVariant = retVal[0]
        console.warn(retVal)
        if (retVal.length == 1)
          subject.next(true);
        else {
          this.searchedVariant = new ItemVariantsVM
          // this.catSvc.ErrorMsgBar("This Variant not found Please Choose another", 5000)
          subject.next(false);
        }
      }, error: () => {
        this.catSvc.ErrorMsgBar(" Error Occurred", 9000)
      }
    })
    return subject.asObservable();
  }
  GetControlValues() {
    this.ProductAttribIds = []
    Object.keys(this.group.controls).forEach(async (key: any) => {
      debugger
      const obstractControl = this.group.get(key);
      var value = obstractControl.value
      if (value == undefined || value.id == undefined)
        this.ProductAttribIds.push("0")
      else
        this.ProductAttribIds.push(value.attribValId.toString())
    })
    this.productAttribIds = this.ProductAttribIds.toString()
  }
}

