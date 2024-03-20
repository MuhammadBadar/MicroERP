import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { KeyAccountingService } from '../../key-accounting.service';
import { ItemVariantsVM } from '../../Models/ItemVariants';
import { CatalogService } from 'src/app/views/catalog/catalog.service';

@Component({
  selector: 'app-configure-sale-rate-by-variant',
  templateUrl: './configure-sale-rate-by-variant.component.html',
  styleUrls: ['./configure-sale-rate-by-variant.component.css']
})
export class ConfigureSaleRateByVariantComponent implements OnInit {
  proVariantsId: number
  Action: any;
  dataSource: any;
  action?: string;
  selecteditemVariants: ItemVariantsVM
  constructor(
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    public dialogRef: MatDialogRef<ConfigureSaleRateByVariantComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.selecteditemVariants = new ItemVariantsVM
  }
  ngOnInit(): void {
    if (this.data != null) {
      this.proVariantsId = this.data.id;
      this.GetProductVariant()
    }
  }
  GetProductVariant() {
    var Variants = new ItemVariantsVM
    Variants.id = this.proVariantsId
    this.accSvc.SearchItemVariants(Variants).subscribe({
      next: (value: ItemVariantsVM[]) => {
        this.selecteditemVariants = value[0]
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 6000)
      },
    })
  }
  UpdateitemVariants() {
    this.accSvc.UpdateItemVariants(this.selecteditemVariants).subscribe({
      next: (value) => {
        this.catSvc.SuccessfullyUpdateMsg()
        this.dialogRef.close()
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
}


