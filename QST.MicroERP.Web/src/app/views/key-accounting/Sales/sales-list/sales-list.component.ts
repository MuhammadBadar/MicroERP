import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SalesVM, SalesLineVM } from './../../Models/SalesVM';
import { KeyAccountingService } from './../../key-accounting.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { Statses } from '../../Models/Enum/Status';

@Component({
  selector: 'app-sales-list',
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class SalesListComponent implements OnInit {
  Sale: SalesVM[] | undefined;;
  selectedRowIndex = -1;
  UnPosted: number
  isLoading: boolean = false
  selectedRow: SalesVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayedColumns: string[] = ['product', 'saleUnit', 'description', 'saleQty', 'saleRate', 'discRate',
    'gstRate', 'fTaxRate', 'whtRate', 'gstRetailRate', 'amount', 'disc', 'bulkDisc', 'qtyDisc', 'gst', 'gstRet',
    'fTax', 'wht', 'chrgsAdd', 'chrgsLess', 'retailRate',];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'invNo', 'cust', 'salesman', 'accName', 'packChrgs', 'freightChrgs', 'discount', 'netPayable', 'credit', 'debit', 'status', 'isPosted', 'description', 'isActive', 'post', 'Action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<SalesLineVM>> | undefined;
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public keySvc: KeyAccountingService,
    public catSvc: CatalogService,
    //  private _loc: Location,
  ) {
    this.UnPosted = Statses.UnPosted
    this.selectedRow = new SalesVM;
  }
  ngOnInit(): void {
    this.GetSale();
  }
  highlight(row: SalesVM) {
    debugger
    this.selectedRow = new SalesVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetSale() {
    this.selectedRow = new SalesVM;
    this.keySvc.GetSale().subscribe({
      next: (res: SalesVM[]) => {
        this.Sale = res;
        this.dataSource = new MatTableDataSource(this.Sale);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditSale(Sale: SalesVM) {
    this.route.navigate(['/keyAccounting/mngSale'], {
      queryParams: {
        id: Sale.id
      }
    });
  }
  DeleteSale(id: number) {
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
        this.keySvc.DeleteSale(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetSale();
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
    this.selectedRow = new SalesVM
  }
  async IsPostedCheck(event: MatCheckboxChange, Sale: SalesVM) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You want to post  this Sale",
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, post it!'
    }).then(async (result) => {
      if (result.value) {
        this.isLoading = true
        Sale.isPosted = true
        Sale.statusId = Statses.Posted
        //this.UpdateSale(Sale)
        await this.keySvc.PostSalesToGL(Sale)
        console.warn(9999)
        this.GetSale();
        this.Refresh();
        this.isLoading = false
      } else {
        this.GetSale()
      }
    })
  }
  UpdateSale(Sale: SalesVM) {
    this.keySvc.UpdateSale(Sale).subscribe({
      next: (data: any) => {
        // Swal.fire(
        //   'Successfully Posted.',
        //   'success'
        // )
        this.GetSale();
        this.Refresh();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetQtyTotal() {
    return this.selectedRow?.saleLines.map(t => t.saleQty).reduce((acc, value) => acc + value, 0);
  }
  GetDiscount() {
    return this.selectedRow?.saleLines.map(t => t.disc).reduce((acc, value) => acc + value, 0);
  }
  GetBulkDisc() {
    return this.selectedRow?.saleLines.map(t => t.bulkDisc).reduce((acc, value) => acc + value, 0);
  }
  GetQtyDisc() {
    return this.selectedRow?.saleLines.map(t => t.qtyDisc).reduce((acc, value) => acc + value, 0);
  }
  GetGST() {
    return this.selectedRow?.saleLines.map(t => t.gst).reduce((acc, value) => acc + value, 0);
  }
  GetGSTRet() {
    return this.selectedRow?.saleLines.map(t => t.gstRet).reduce((acc, value) => acc + value, 0);
  }
  GetFTax() {
    return this.selectedRow?.saleLines.map(t => t.fTax).reduce((acc, value) => acc + value, 0);
  }
  GetWht() {
    return this.selectedRow?.saleLines.map(t => t.wht).reduce((acc, value) => acc + value, 0);
  }
  GetChrgsAdd() {
    return this.selectedRow?.saleLines.map(t => t.chrgsAdd).reduce((acc, value) => acc + value, 0);
  }
  GetChrgsLess() {
    return this.selectedRow?.saleLines.map(t => t.chrgsLess).reduce((acc, value) => acc + value, 0);
  }
  GetAmount() {
    return this.selectedRow?.saleLines.map(t => t.amount).reduce((acc, value) => acc + value, 0);
  }
}




