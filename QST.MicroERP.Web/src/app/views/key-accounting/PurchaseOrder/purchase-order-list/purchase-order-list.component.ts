import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { PurchaseVM, PurchaseLineVM } from './../../Models/PurchaseOrderVM';
import { KeyAccountingService } from './../../key-accounting.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import Swal from 'sweetalert2';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Statses } from '../../Models/Enum/Status';

@Component({
  selector: 'app-purchase-order-list',
  templateUrl: './purchase-order-list.component.html',
  styleUrls: ['./purchase-order-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class PurchaseOrderListComponent implements OnInit {
  Purchase: PurchaseVM[] | undefined;
  UnPosted: number;
  selectedRowIndex = -1;
  selectedRow: PurchaseVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayedColumns: string[] = ['product', 'purUnit', 'qty', 'purchaseRate', 'discPer',
    'gstRate', 'gstRetailRate', 'amount', 'description'];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'invNo', 'supplier', 'accName', 'gross', 'discount', 'gst', 'credit', 'debit', 'status', 'isPosted', 'description', 'isActive', 'post', 'Action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<PurchaseLineVM>> | undefined;
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public keySvc: KeyAccountingService,
    public catSvc: CatalogService,
    //  private _loc: Location,
  ) {
    this.UnPosted = Statses.UnPosted
    this.selectedRow = new PurchaseVM;
  }
  ngOnInit(): void {
    this.GetPurchase();
  }
  highlight(row: PurchaseVM) {
    debugger
    this.selectedRow = new PurchaseVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetPurchase() {
    this.selectedRow = new PurchaseVM;
    this.keySvc.GetPurchase().subscribe({
      next: (res: PurchaseVM[]) => {
        this.Purchase = res;
        this.dataSource = new MatTableDataSource(this.Purchase);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditPurchase(Purchase: PurchaseVM) {
    this.route.navigate(['/keyAccounting/mngPurchase'], {
      queryParams: {
        id: Purchase.id
      }
    });
  }
  DeletePurchase(id: number) {
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
        this.keySvc.DeletePurchase(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetPurchase();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
            console.warn(e);
          }
        })
      }
    })
  }
  Refresh() {
    this.selectedRowIndex = -1
    this.selectedRow = new PurchaseVM
  }
  IsPostedCheck(event: MatCheckboxChange, PurchaseOrder: PurchaseVM): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You want to post  this PurchaseOrder",
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, post it!'
    }).then((result) => {
      if (result.value) {
        PurchaseOrder.isPosted = true
        PurchaseOrder.statusId = Statses.Posted
        this.UpdatePurchaseOrder(PurchaseOrder)
      } else {
        this.GetPurchase()
      }
    })
  }
  UpdatePurchaseOrder(PurchaseOrder: PurchaseVM) {
    this.keySvc.UpdatePurchase(PurchaseOrder).subscribe({
      next: (data: any) => {
        Swal.fire(
          'Successfully Posted.',
          'success'
        )
        this.GetPurchase();
        this.Refresh();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
}



