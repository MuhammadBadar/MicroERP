import { VoucherDetailsVM } from './../../Models/VoucherVM';
import { Statses } from './../../Models/Enum/Status';
import { KeyAccountingService } from './../../key-accounting.service';
import { ChangeDetectorRef, Component, NgZone, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { VoucherVM } from '../../Models/VoucherVM';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ThisReceiver } from '@angular/compiler';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';

@Component({
  selector: 'app-voucher-list',
  templateUrl: './voucher-list.component.html',
  styleUrls: ['./voucher-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class VoucherListComponent implements OnInit {
  isReadOnly: boolean = false
  Voucher: VoucherVM[] | undefined;;
  selectedRowIndex = -1;
  selectedRow: VoucherVM
  UnPosted: number
  Edit: boolean = true;
  isPosted: boolean = false
  // displayedColumns: string[] = ['expand', 'date', 'vchNo', 'status', 'vendor', 'salesman', 'invNo', 'godown', 'docNo', 'docDate', 'isPosted', 'Action'];
  innerDisplayedColumns: string[] = ['acName', 'acId', 'debit', 'credit', 'desc'];
  dataSource: any;
  columnsToDisplay = ['expand', 'date', 'vchNo', 'vchType', 'status', 'isPosted', 'active', 'description', 'post', 'Action'];
  style = "background-image: linear-gradient(to bottom, rgb(2, 33, 58), rgb(7, 95, 122));color:white;font-weight:normal "
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  isActive: boolean
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<VoucherDetailsVM>> | undefined;
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public keySvc: KeyAccountingService,
    public catSvc: CatalogService,
    private zone: NgZone,
    private cdRef: ChangeDetectorRef,
    //  private _loc: Location,
  ) {
    this.selectedRow = new VoucherVM;
    this.UnPosted = Statses.UnPosted
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Vouchers)
    this.GetVoucher();
    // this.selectedRow.statusId = Statses.UnPosted
  }
  highlight(row: VoucherVM) {
    // window.scrollTo(0, 0)
    this.selectedRow = new VoucherVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row

  }
  Activate(row: VoucherVM) {
    this.keySvc.ActivateVoucher(row).subscribe({
      next: (value) => {
        this.catSvc.SuccessMsgBar("successfully Updated!", 5000)
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
      },
    })
  }
  Check(row: VoucherVM) {
    if (row.isActive == true)
      this.Activate(row)
    else this.DeActivate(row)
  }
  DeActivate(row: VoucherVM) {
    this.keySvc.DeActivateVoucher(row).subscribe({
      next: (value) => {
        this.catSvc.SuccessMsgBar("successfully Updated!", 5000)
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error OccurRed!", 5000)
      },
    })
  }
  GetVoucher() {
    this.selectedRow = new VoucherVM;
    var vch = new VoucherVM
    vch.clientId = +localStorage.getItem("ClientId")
    this.keySvc.SearchVoucher(vch).subscribe({
      next: (res: VoucherVM[]) => {
        this.Voucher = res;
        this.dataSource = new MatTableDataSource(this.Voucher);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditVoucher(vch: VoucherVM) {
    this.route.navigate(['/account/accounts/voucher'], {
      queryParams: {
        id: vch.id
      }
    });
  }
  openPdf(vch: VoucherVM) {
    this.keySvc.GetPdf(vch).subscribe({
      next: (response) => {
        console.warn(response)
        const blob = new Blob([response.body], { type: 'application/pdf' });
        const url = URL.createObjectURL(blob);
        window.open(url);
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
        console.warn(err)
      }
    }
    )
  }
  DeleteVoucher(id: number) {
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
        this.keySvc.DeleteVoucher(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetVoucher();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
            console.warn(e);
          }
        })
      }
    })
  }
  IsPostedCheck(event: MatCheckboxChange, voucher: VoucherVM): void {
    debugger
    Swal.fire({
      title: 'Are you sure?',
      text: "You want to post  this voucher",
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, post it!'
    }).then((result) => {
      if (result.value) {
        voucher.isPosted = true
        voucher.statusId = Statses.Posted
        this.UpdateVoucher(voucher)
      } else {
        this.GetVoucher()
      }
    })
  }
  UpdateVoucher(voucher: VoucherVM) {
    this.keySvc.UpdateVoucher(voucher).subscribe({
      next: (data: any) => {
        Swal.fire(
          'Successfully Posted.',
          'success'
        )
        this.GetVoucher();
        this.Refresh();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  Refresh() {
    this.selectedRowIndex = -1
    this.selectedRow = new VoucherVM
    this.selectedRow.isPosted = false
    this.selectedRow.statusId = Statses.Draft
  }
  Back() {
    // this._loc.back();
  }
  GetDebitTotal() {
    return this.selectedRow.voucherDetails?.map(t => t.debit).reduce((acc, value) => acc + value, 0);
  }
  GetCreditTotal() {
    return this.selectedRow.voucherDetails?.map(t => t.credit).reduce((acc, value) => acc + value, 0);
  }
  GetQtyTotal() {
    return this.selectedRow.voucherDetails?.map(t => t.qty).reduce((acc, value) => acc + value, 0);
  }
}



