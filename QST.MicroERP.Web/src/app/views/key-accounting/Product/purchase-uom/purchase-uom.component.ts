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
  selector: 'app-purchase-uom',
  templateUrl: './purchase-uom.component.html',
  styleUrls: ['./purchase-uom.component.css']
})
export class PurchaseUOMComponent implements OnInit {
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
    public dialogRef: MatDialogRef<PurchaseUOMComponent>,
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
  SetListValues() {
    debugger
    if (this.data != null) {
      if (this.data.item) {
        this.selectedItem = this.data.item
        if (this.selectedItem.itemUOMList.length > 0) {
          if (this.selectedItem.id > 0) {
            this.editMode = true
            this.addMode = false
          }
          var list = this.selectedItem.itemUOMList.filter(x => x.uomTypeId == UOMTypes.PurchaseUOM)
          list.forEach(element => {
            this.uomConversions = this.uomConversions.map(x => {
              debugger
              if (x.id == element.uomId) {
                x.purPrice = element.purPrice
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
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.DataSource.data);
  }
  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: ItemUOMVm): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }
  Add() {
    debugger
    this.itemUOMList = []
    var list = this.uomConversions.filter(x => x.isChecked == true)
    if (list.length > 0) {
      var find = list.find(x => x.purPrice == 0 || x.purPrice == undefined)
      if (find == undefined) {
        list.forEach(element => {
          var uom = new ItemUOMVm
          uom.uomId = element.id
          uom.purPrice = element.purPrice
          uom.uomTypeId = UOMTypes.PurchaseUOM
          this.itemUOMList.push(uom)
        });
        debugger
        this.dialogRef.close({
          itemUOMList: this.itemUOMList,
          purchaseUnits: this.getIdsofPurUnits(this.itemUOMList)
        });
      }
      else
        this.catSvc.ErrorMsgBar(`Please define price for  "${find.displayUOM}" as its checked `, 4000)
    } else
      this.catSvc.ErrorMsgBar("Please select some UOM to add ", 4000)
  }
  getIdsofPurUnits(arr) {
    var list = arr.map(function (a) { return a.uomId; });
    return list;
  }
  isCheck(row) {
    if (row.isChecked == false) {
      row.purPrice = 0
      row.displayUOM = ""
    }
  }
}
