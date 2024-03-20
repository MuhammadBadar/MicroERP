import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { StockTransferVM, STLineVM } from './../../Models/StockTransferVM';
import { KeyAccountingService } from './../../key-accounting.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import Swal from 'sweetalert2';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-stock-transfer-list',
  templateUrl: './stock-transfer-list.component.html',
  styleUrls: ['./stock-transfer-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class StockTransferListComponent implements OnInit {
  StockTransfer: StockTransferVM[] | undefined;;
  selectedRowIndex = -1;
  selectedRow: StockTransferVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayeStockTransferolumns: string[] = ['product', 'description', 'godown', 'productUnits', 'qty'];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'invNo', 'transferTo', 'transferFrom', 'isActive', 'Action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<STLineVM>> | undefined;
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public keySvc: KeyAccountingService,
    public catSvc: CatalogService,
    //  private _loc: Location,
  ) {
    this.selectedRow = new StockTransferVM;
  }
  ngOnInit(): void {
    this.GetStockTransfer();
  }
  highlight(row: StockTransferVM) {
    debugger
    this.selectedRow = new StockTransferVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetStockTransfer() {
    this.selectedRow = new StockTransferVM;
    this.keySvc.GetStockTransfer().subscribe({
      next: (res: StockTransferVM[]) => {
        this.StockTransfer = res;
        this.dataSource = new MatTableDataSource(this.StockTransfer);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditStockTransfer(StockTransfer: StockTransferVM) {
    this.route.navigate(['/keyAccounting/stockTransfer'], {
      queryParams: {
        id: StockTransfer.id
      }
    });
  }
  DeleteStockTransfer(id: number) {
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
        this.keySvc.DeleteStockTransfer(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetStockTransfer();
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
    this.selectedRow = new StockTransferVM
  }
}



