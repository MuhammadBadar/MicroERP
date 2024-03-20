import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from '../../catalog.service';
import { PMSService } from 'src/app/views/pms/pms.service';
import { MatDialog } from '@angular/material/dialog';
import { NgForm } from '@angular/forms';
import { CreateUserDialogComponent } from 'src/app/views/security/manage-user/manage-user-dialog/create-user-dialog/create-user-dialog.component';
import { ClientsVM } from '../../Models/ClientsVM';
import { StorageData } from '../../Models/StorageData';
import { ManageClientsComponent } from '../manage-clients/manage-clients.component';
import { ModulesComponent } from '../../modules/modules.component';
import { ManageSMTPCredsComponent } from '../../SMTPCredentials/manage-smtpcreds/manage-smtpcreds.component';
import { RouteIds } from '../../Models/Enums/RouteIds';
import { Roles } from '../../Models/Enums/Roles';
@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {
  isReadOnly: boolean = false
  dialogRef: any
  Clients: ClientsVM[]
  selectedClients: ClientsVM
  @ViewChild('clientsForm', { static: true }) clientsForm!: NgForm;
  displayedColumns: string[] = ['clientName', 'userEmail', 'modules', 'category', 'contact', 'owner', 'releventPerson', 'address', 'city', 'isActive', 'actions'];
  dataSource: any;
  constructor(
    private dialog: MatDialog,
    private catSvc: CatalogService,
  ) {
    this.selectedClients = new ClientsVM();
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.Clients)
    this.GetClients()
  }
  GetClients() {
    this.catSvc.GetClients().subscribe({
      next: (res: ClientsVM[]) => {
        this.Clients = res;
        this.dataSource = new MatTableDataSource(this.Clients);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  OpenUserDialog(row: ClientsVM) {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '450px'
      , data: { clt: row, id: row.userId, isDialog: true, roleId: Roles.ClientAdmin, dialogTitle: row.clientName }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  EditClient(row) {
    this.dialogRef = this.dialog.open(ManageClientsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '550px'
      , data: { cltId: row.id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(ManageClientsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '550px'
      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  OpenModuleDialog(row) {
    this.dialogRef = this.dialog.open(ModulesComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '600px', minHeight: '350px', maxHeight: "550px"
      , data: { client: row }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
        //this.catSvc.triggerRefresh()
        // if (res.client)
        //this.catSvc.openCltData(res.client)
      });
  }
  OpenSMPTCredsDialog(row) {
    this.dialogRef = this.dialog.open(ManageSMTPCredsComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '900px', minHeight: '330px', maxHeight: "550px"
      , data: { cltId: row.id }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
}

