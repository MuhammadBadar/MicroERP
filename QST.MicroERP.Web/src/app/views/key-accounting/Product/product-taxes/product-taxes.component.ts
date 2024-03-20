import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { KeyAccountingService } from '../../key-accounting.service';
import { ItemVM, ProductwithVariantsVM } from '../../Models/ItemVM';
import { ProductTaxesVM } from '../../Models/ProductTaxesVM';

@Component({
  selector: 'app-product-taxes',
  templateUrl: './product-taxes.component.html',
  styleUrls: ['./product-taxes.component.css']
})
export class ProductTaxesComponent implements OnInit {
  products: ProductwithVariantsVM[] = []
  dataSource: any
  displayedColumns: string[] = ['product', 'taxes'];
  taxes: SettingsVM[] = []
  constructor(
    public accSvc: KeyAccountingService,
    public catSvc: CatalogService
  ) {

  }
  ngOnInit() {
    this.GetProduct();
    this.GetTaxes()
  }
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
        this.dataSource = new MatTableDataSource(res)
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
}
