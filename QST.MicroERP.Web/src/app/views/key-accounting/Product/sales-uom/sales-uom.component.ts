import { SelectionModel } from '@angular/cdk/collections';
import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { KeyAccountingService } from '../../key-accounting.service';
import { UOMTypes } from '../../Models/Enum/UOMTypes';
import { ItemUOMVm } from '../../Models/ItemUOMVm';
import { ItemVM } from '../../Models/ItemVM';
import { UOMConversionVm } from '../../Models/UOMConversionVm';

@Component({
  selector: 'app-sales-uom',
  templateUrl: './sales-uom.component.html',
  styleUrls: ['./sales-uom.component.css']
})
export class SalesUOMComponent implements OnInit {
  DisplayedColumns: string[] = ['select', 'displayUOM', 'price'];
  DataSource: any
  selection = new SelectionModel<ItemUOMVm>(true, []);
  uomConversions: UOMConversionVm[] = []
  addMode: boolean = true
  editMode: boolean = false
  itemUOMList: ItemUOMVm[] = new Array()
  selectedItem: ItemVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  constructor(
    private cdref: ChangeDetectorRef,
    public accSvc: KeyAccountingService,
    private catSvc: CatalogService,
    public dialogRef: MatDialogRef<SalesUOMComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.DataSource = new MatTableDataSource(this.uomConversions)
  }
  ngOnInit(): void {
    this.UOMConversions()
  }
  UOMConversions() {
    var unit = new UOMConversionVm
    unit.isActive = true
    this.accSvc.SearchUOMConversion(unit).subscribe({
      next: (res: UOMConversionVm[]) => {
        this.uomConversions = res;
        this.SetListValues()
        // this.DataSource = new MatTableDataSource(this.uomConversions)
        console.warn(this.uomConversions)

      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  // SetListValues() {
  //   if (this.data != null) {
  //     if (this.data.item) {
  //       this.selectedItem = this.data.item
  //       var item = new ItemVM
  //       item.id = this.selectedItem.id
  //       this.accSvc.SearchItem(item).subscribe({
  //         next: (res: ItemVM[]) => {
  //           this.selectedItem = res[0]
  //           if (this.selectedItem.itemUOMList.length > 0) {
  //             this.editMode = true
  //             this.addMode = false
  //             var list = this.selectedItem.itemUOMList.filter(x => x.uomTypeId == UOMTypes.SaleUOM)
  //             list.forEach(element => {
  //               this.uomConversions = this.uomConversions.map(x => {
  //                 if (x.id == element.uomId) {
  //                   x.salePrice = element.salePrice
  //                   x.isChecked = true
  //                   return x;
  //                 }
  //                 else return x;
  //               });
  //             });
  //           }
  //         }, error: (e) => {
  //           this.catSvc.ErrorMsgBar("Error Occurred", 5000)
  //           console.warn(e);
  //         }
  //       })
  //     }
  //   }
  //   this.DataSource = new MatTableDataSource(this.uomConversions)
  // }
  SetListValues() {
    if (this.data != null) {
      if (this.data.item) {
        this.selectedItem = this.data.item
        if (this.selectedItem.itemUOMList.length > 0) {
          if (this.selectedItem.id > 0) {
            this.editMode = true
            this.addMode = false
          }
          var list = this.selectedItem.itemUOMList.filter(x => x.uomTypeId == UOMTypes.SaleUOM)
          list.forEach(element => {
            this.uomConversions = this.uomConversions.map(x => {
              if (x.id == element.uomId) {
                x.salePrice = element.salePrice
                x.isChecked = true
                return x;
              }
              else return x;
            });
          });
        }
      }
    }
    this.DataSource = new MatTableDataSource(this.uomConversions)
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
    this.cdref.markForCheck();
  }
  Add() {
    debugger
    this.itemUOMList = []
    var list = this.uomConversions.filter(x => x.isChecked == true)
    if (list.length > 0) {
      var find = list.find(x => x.salePrice == 0 || x.salePrice == undefined)
      if (find == undefined) {
        list.forEach(element => {
          var uom = new ItemUOMVm
          uom.uomId = element.id
          uom.salePrice = element.salePrice
          uom.uomTypeId = UOMTypes.SaleUOM
          this.itemUOMList.push(uom)
        });
        this.dialogRef.close({
          itemUOMList: this.itemUOMList,
          saleUnits: this.getIdsofsaleUnits(this.itemUOMList)
        });
      }
      else
        this.catSvc.ErrorMsgBar(`Please define price for  "${find.displayUOM}" as its checked `, 4000)
    } else
      this.catSvc.ErrorMsgBar("Please select some UOM to add ", 4000)
  }
  getIdsofsaleUnits(arr) {
    var list = arr.map(function (a) { return a.uomId; });
    return list;
  }

}
