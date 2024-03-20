import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { DCVM, DCDetailsVM } from './../../Models/DeliveryChallanVM';
import { KeyAccountingService } from './../../key-accounting.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import Swal from 'sweetalert2';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-delivery-challan-list',
  templateUrl: './delivery-challan-list.component.html',
  styleUrls: ['./delivery-challan-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DeliveryChallanListComponent implements OnInit {
  DC: DCVM[] | undefined;;
  selectedRowIndex = -1;
  selectedRow: DCVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayedColumns: string[] = ['product', 'description', 'qty'];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'acName', 'customer', 'invNo', 'isActive', 'Action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<DCDetailsVM>> | undefined;
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public keySvc: KeyAccountingService,
    public catSvc: CatalogService,
    //  private _loc: Location,
  ) {
    this.selectedRow = new DCVM;
  }
  ngOnInit(): void {
    this.GetDC();
  }
  highlight(row: DCVM) {
    debugger
    this.selectedRow = new DCVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  GetDC() {
    this.selectedRow = new DCVM;
    this.keySvc.GetDC().subscribe({
      next: (res: DCVM[]) => {
        this.DC = res;
        this.dataSource = new MatTableDataSource(this.DC);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditDC(dc: DCVM) {
    this.route.navigate(['/keyAccounting/dc'], {
      queryParams: {
        id: dc.id
      }
    });
  }
  DeleteDC(id: number) {
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
        this.keySvc.DeleteDC(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetDC();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
            console.warn(e);
          }
        })
      }
    })
  }
  Refresh() {
    // this.GetDC()
    this.selectedRowIndex = -1
    //  this.toggleRow(this.selectedRow)
    this.selectedRow = new DCVM

  }
}



