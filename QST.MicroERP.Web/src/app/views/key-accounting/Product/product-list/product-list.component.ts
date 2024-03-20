import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';

import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import Swal from 'sweetalert2';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { ItemVM, ProductAttribVm } from '../../Models/ItemVM';
import { NgForm } from '@angular/forms';
import { KeyAccountingService } from '../../key-accounting.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ProductListComponent implements OnInit {
  Item: ItemVM[] | undefined;;
  selectedRowIndex = -1;
  selectedRow: ItemVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayedColumns: string[] = ['attrib', 'attribValues'];
  itemUOMColumn: string[] = ['item', 'uomType', 'uom', 'salePrice', 'purPrice']
  dataSource: any;
  filterVal: ItemVM = new ItemVM();
  columnsToDisplay = ['expand', 'name', 'itemType', 'saleRate', 'purRate', 'possibleSaleUnits', 'possiblePurUnits',
    'retailRate', 'isActive', 'Action'];
  itemVariantsColumns: string[] = ['item', 'possibleValues', 'saleExtraRate',
    'purchaseExtraRate', 'barCode', 'stockValue']
  @ViewChild('Form', { static: true }) filterForm!: NgForm;
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<ProductAttribVm>> | undefined;
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public keySvc: KeyAccountingService,
    public catSvc: CatalogService,
    //  private _loc: Location,
  ) {
    this.selectedRow = new ItemVM;
  }
  ngOnInit(): void {
    this.GetItem();
  }
  highlight(row: ItemVM) {
    debugger
    this.selectedRow = new ItemVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetItem() {
    this.selectedRow = new ItemVM;
    this.keySvc.GetItem().subscribe({
      next: (res: ItemVM[]) => {
        this.Item = res;
        console.warn(res)
        this.dataSource = new MatTableDataSource(this.Item);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditItem(Item: ItemVM) {
    this.route.navigate(['/product/item'], {
      queryParams: {
        id: Item.id
      }
    });
  }
  DeleteItem(id: number) {
    debugger
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
        this.keySvc.DeleteItem(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetItem();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
            console.warn(e);
          }
        })
      }
    })
  }
  Refresh() {
    // this.GetItem()
    this.selectedRowIndex = -1
    //  this.toggleRow(this.selectedRow)
    this.selectedRow = new ItemVM

  }
  SearchInProduct() {
    var text;
    var Product = this.filterVal
    console.warn(Product)
    console.warn(Product.retailRate)
    this.SearchProduct(Product)
  }
  ResetGrid() {
    this.filterForm.reset()
    this.GetItem()
  }
  SearchProduct(value: ItemVM) {
    this.keySvc.SearchItem(value).subscribe({
      next: (res: ItemVM[]) => {
        this.dataSource = new MatTableDataSource(res);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occured!", 5000)
        console.warn(e);
      }
    })
  }
}



