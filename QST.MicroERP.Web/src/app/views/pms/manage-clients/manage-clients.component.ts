import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { ClientsVM} from 'src/app/views/pms/Models/ClientsVM'
import { CatalogService } from '../../catalog/catalog.service';
import { PMSService } from '../pms.service';

@Component({
  selector: 'app-manage-clients',
  templateUrl: './manage-clients.component.html',
  styleUrls: ['./manage-clients.component.css']
})
export class ManageClientsComponent implements OnInit {
 
  hide = true;
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  Clients: ClientsVM[] | undefined;
  selectedClients : ClientsVM
  @ViewChild('clientsForm', { static: true }) clientsForm!: NgForm;
  displayedColumns: string[] = ['clientName','category', 'address','city','contact','owner','releventPerson', 'isActive','actions'];
  dataSource: any;
  constructor(
    public pmsSvc: PMSService,
    private injector: Injector,
    private catSvc: CatalogService,
  ) {
    this.selectedClients = new ClientsVM();
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedClients = new ClientsVM
    this.GetClients()
    this.selectedClients.isActive = true;
    
  }
  GetClients() {
    this.pmsSvc.GetClients().subscribe({
      next: (res: ClientsVM[]) => {
        this.Clients = res;
        this.dataSource = new MatTableDataSource(this.Clients);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteClients(id: number) {
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
        this.pmsSvc.DeleteClients(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'Clients has been deleted.',
              'success'
            )
            this.ngOnInit();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetClientsForEdit(id: number) {
    this.selectedClients = new ClientsVM;
    this.selectedClients.id = id
    console.warn(this.selectedClients);
    this.pmsSvc.SearchClients(this.selectedClients).subscribe({
      next: (res: ClientsVM[]) => {
        this.Clients = res;
        console.warn(this.DeleteClients);
        this.selectedClients = this.Clients[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveClients() {
    this.pmsSvc.GetClients().subscribe({
      next: (res: ClientsVM[]) => {
        console.warn(res)
        var list = res
        if (this.Edit)
          list = list.filter(x => x.id != this.selectedClients.id)
          console.warn(list)
        var find = list.find(x => x.clientName == this.selectedClients.clientName  )
        if (find == undefined) {
          console.warn(this.clientsForm)
          if (!this.clientsForm.invalid) {
            if (this.Edit)
              this.UpdateClients()
            else {
              this.pmsSvc.SaveClients(this.selectedClients).subscribe({
                next: (res) => {
                  this.catSvc.SuccessMsgBar("Clients Successfully Added!", 5000)
                  this.Add = true;
                  this.Edit = false;
                  this.proccessing = false
                  this.ngOnInit();
                }, error: (e) => {
                  this.catSvc.ErrorMsgBar("Error Occurred", 5000)
                  console.warn(e);
                  this.proccessing = false
                }
              })
            }
          } else {
            this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
            this.proccessing = false
          }
        } else
          this.catSvc.ErrorMsgBar("This Name Already Taken ", 5000)
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  UpdateClients() {
    debugger
    this.pmsSvc.UpdateClients(this.selectedClients).subscribe({
      next: (res) => {
        
        this.catSvc.SuccessMsgBar("Name Successfully Updated!", 5000)
        this.Add = true;
        this.Edit = false;
        this.proccessing = false
        this.ngOnInit();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.proccessing = false
      }
    })
    this.proccessing = false
  }
  Refresh() {
    this.Add = true;
    this.Edit = false;
  }
}

