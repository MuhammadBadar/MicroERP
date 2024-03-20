import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import Swal from 'sweetalert2';
import { RxMedExtraFieldsComponent } from '../rx-med-extra-fields/rx-med-extra-fields.component';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { RouteIds } from 'src/app/views/catalog/Models/Enums/RouteIds';


@Component({
  selector: 'app-rx-med-extra-fields-list',
  templateUrl: './rx-med-extra-fields-list.component.html',
  styleUrls: ['./rx-med-extra-fields-list.component.css'],

  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class RxMedExtraFieldsListComponent implements OnInit {
  isReadOnly: boolean = false
  SettingsType: SettingsTypeVM[] | undefined;;
  selectedRowIndex = -1;
  selectedRow: SettingsTypeVM
  Edit: boolean = true;
  isPosted: boolean = false
  innerDisplayedColumns: string[] = ['name'];
  dataSource: any;
  columnsToDisplay = ['expand', 'name', 'code', 'isRequired', 'isActive', 'Action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay];
  expandedElement: any;
  @ViewChild('outerSort', { static: true }) sort: MatSort | undefined;
  @ViewChildren('innerSort') innerSort: QueryList<MatSort> | undefined;
  @ViewChildren('innerTables') innerTables: QueryList<MatTable<SettingsVM>> | undefined;
  dialogRef: any
  constructor(
    private route: Router,
    public dialog: MatDialog,
    public catSvc: CatalogService,
    //  private _loc: Location,
  ) {
    this.selectedRow = new SettingsTypeVM;
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.RxParameters)
    this.GetSettingsType();
  }
  highlight(row: SettingsTypeVM) {
    debugger
    this.selectedRow = new SettingsTypeVM
    this.selectedRowIndex = row.id;
    this.selectedRow = row
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(RxMedExtraFieldsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '900px', maxHeight: '550px'

      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  GetSettingsType() {
    this.selectedRow = new SettingsTypeVM;
    var type = new SettingsTypeVM
    type.parentId = EnumTypes.RxExtraFields
    type.clientId = +localStorage.getItem("ClientId")
    type.moduleId = Modules.PMS
    this.catSvc.SearchSettingsType(type).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.SettingsType = res;
        this.dataSource = new MatTableDataSource(this.SettingsType);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  EditSettingsType(val: SettingsTypeVM) {
    this.dialogRef = this.dialog.open(RxMedExtraFieldsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '900px', maxHeight: '550px'
      , data: { fieldId: val.id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  DeleteSettingsType(id: number) {
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
        this.catSvc.DeleteSettingsType(id).subscribe({
          next: (data: any) => {
            Swal.fire(
              'Deleted!',
              'Successfully Deleted.',
              'success'
            )
            this.GetSettingsType();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
            console.warn(e);
          }
        })
      }
    })
  }
  Refresh() {
    // this.GetSettingsType()
    this.selectedRowIndex = -1
    //  this.toggleRow(this.selectedRow)
    this.selectedRow = new SettingsTypeVM

  }
}



