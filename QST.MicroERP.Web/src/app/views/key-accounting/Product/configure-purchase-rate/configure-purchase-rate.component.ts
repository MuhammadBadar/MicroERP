import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { KeyAccountingService } from '../../key-accounting.service';
import { CreateUserRoleComponent } from 'src/app/views/security/assign-role-to-user/manage -user-role-dialog/create-user-role/create-user-role.component';
import { ItemVM, ProductAttribVm } from '../../Models/ItemVM';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { AttributeValuesVm } from '../../Models/AttributesValuesVm';
import { MatRadioChange } from '@angular/material/radio';
import { FormBuilder, FormControl, FormGroup, NgForm } from '@angular/forms';
import { ItemVariantsVM } from '../../Models/ItemVariants';
import { Observable, Subject } from 'rxjs';
import { PurchaseLineVM } from '../../Models/PurchaseOrderVM';
import { ItemUOMVm } from '../../Models/ItemUOMVm';

@Component({
  selector: 'app-configure-purchase-rate',
  templateUrl: './configure-purchase-rate.component.html',
  styleUrls: ['./configure-purchase-rate.component.css']
})
export class ConfigurePurchaseRateComponent implements OnInit {
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
  totalpurchaseRate: number = 0
  finalDes: string
  selectedLine: PurchaseLineVM
  selectedProductVariants: ProductAttribVm[] = []
  displayedColumns: ['name', 'values']
  group: FormGroup;
  searchedVariant: ItemVariantsVM
  actualPrice: number = 0
  @ViewChild('userForm', { static: true }) UserForm!: NgForm;
  constructor(
    private cdref: ChangeDetectorRef,
    private formBuilder: FormBuilder,
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    public dialogRef: MatDialogRef<ConfigurePurchaseRateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.selectedProduct = new ItemVM
    this.selectedProductAttrib = new ProductAttribVm
    this.selectedLine = new PurchaseLineVM
    this.searchedVariant = new ItemVariantsVM()
  }
  async ngOnInit(): Promise<void> {
    debugger
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
        this.totalpurchaseRate = this.data.selectedLine.purchaseRate
        this.selectedLine = this.data.selectedLine
      }
      await this.GetProductById(this.itemId)
    }
  }
  searchItemUOM() {
    if (this.selectedLine.purUnitId > 0) {
      var uom = new ItemUOMVm
      uom.id = this.selectedLine.purUnitId
      this.accSvc.SearchItemUOM(uom).subscribe({
        next: (retVal: ItemUOMVm[]) => {
          console.warn("2")
          this.selectedProduct.purRate = retVal[0].purPrice
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

        for (let index = 0; index < attributes.length; index++) {
          const obstractControl = this.group.get(attributes[index]);
          if (obstractControl != undefined)
            this.group.controls[attributes[index]].setValue(this.selectedProductVariants[index])
        }
        console.warn("1")
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 6000)
      },
    })
  }
  Add() {
    console.warn(this.group)
    const invalid = [];
    const controls = this.group.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    if (invalid.length == 0) {
      this.SearchVariant().subscribe((res) => {
        if (res) {
          this.totalpurchaseRate = this.searchedVariant.purchaseExtraRate + this.selectedProduct.purRate
          this.dialogRef.close({
            data: this.selectedProductAttrib,
            purchaseRate: this.totalpurchaseRate,
            description: `${this.searchedVariant.item} (${this.searchedVariant.possibleValues}) `,
            product: this.selectedProduct,
            productAttribIds: this.productAttribIds,
            itemVariantId: this.searchedVariant.id
          });
        } else { this.catSvc.ErrorMsgBar("This Variant not found Please Choose another", 5000) }
      })
    } else
      this.catSvc.ErrorMsgBar(`Please select " ${invalid[0]} " of the ${this.selectedProduct.name}`, 4000)

  }
  radioChange(data) {
    var find = this.selectedProductAttribs.find(x => x.attribId == data.attribId)
    if (find != undefined)
      this.selectedProductAttribs = this.selectedProductAttribs.filter(x => x.attribId != data.attribId)
    this.selectedProductAttribs.push(data)
    this.GetProductAttrib(data)
    this.totalpurchaseRate = 0
    this.selectedProductAttribs.forEach(element => {
      this.totalpurchaseRate = this.totalpurchaseRate += element.purRate
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
        this.selectedProductAttrib.purRate = this.searchedVariant.purchaseExtraRate
        this.totalpurchaseRate = this.selectedProduct.purRate + this.searchedVariant.purchaseExtraRate
      }
      else {
        this.selectedProductAttrib.purRate = 0
        this.totalpurchaseRate = this.selectedProduct.purRate
      }
    })
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
