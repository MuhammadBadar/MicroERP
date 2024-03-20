import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { NotificationTemplateVM } from '../Models/NotificationTemplateVM';
import { CatalogService } from '../catalog.service';
import { NotificationVM } from '../Models/NotificationVM';
@Component({
  selector: 'app-notification-template',
  templateUrl: './notification-template.component.html',
  styleUrls: ['./notification-template.component.css']
})
export class NotificationTemplateComponent implements OnInit {

  proccessing: boolean = false;
  value
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  NotificationTemplates: NotificationTemplateVM[] | undefined;
  selectedTemplate: NotificationTemplateVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  @ViewChild('saleInvoice') saleInvoice: ElementRef;
  displayedColumns: string[] = ['name', 'keyCode', 'subject', 'isActive', 'actions'];
  dataSource: any;
  constructor(
    //public resSvc: RestaurantService,
    private catSvc: CatalogService,
  ) {
    this.selectedTemplate = new NotificationTemplateVM();

  }

  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedTemplate = new NotificationTemplateVM
    this.GetNotificationTemplate();
    this.SerachNotificationTemplate();
    this.selectedTemplate.isActive = true;
  }
  Check() {
    this.validFields = true;
  }
  GetNotificationTemplate() {
    this.catSvc.GetNotificationTemplate().subscribe({
      next: (res: NotificationTemplateVM[]) => {
        this.NotificationTemplates = res;
        this.dataSource = new MatTableDataSource(this.NotificationTemplates);
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SerachNotificationTemplate() {
    var NotificationTemplate = new NotificationTemplateVM
    NotificationTemplate.isActive = true
    this.catSvc.SearchNotificationTemplate(NotificationTemplate).subscribe({
      next: (res: NotificationTemplateVM[]) => {
        this.NotificationTemplates = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  DeleteNotificationTemplate(id: number) {
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
        this.catSvc.DeleteNotificationTemplate(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'NotificationTemplate has been deleted.',
              'success'
            )
            this.GetNotificationTemplate();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }
  GetNotificationTemplateForEdit(id: number) {
    window.scrollTo(0, 0)
    this.selectedTemplate = new NotificationTemplateVM;
    this.selectedTemplate.id = id
    console.warn(this.selectedTemplate);
    this.catSvc.SearchNotificationTemplate(this.selectedTemplate).subscribe({
      next: (res: NotificationTemplateVM[]) => {
        this.selectedTemplate = res[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SaveNotificationTemplate() {
    if (!this.userForm.invalid) {
      if (this.selectedTemplate.body == "" || this.selectedTemplate.body == null)
        this.catSvc.ErrorMsgBar("Please define some content for notification body", 5000)
      else {
        this.proccessing = true
        if (this.Edit)
          this.UpdateNotificationTemplate();
        else
          this.catSvc.SaveNotificationTemplate(this.selectedTemplate).subscribe({
            next: (res) => {
              this.catSvc.SuccessMsgBar("NotificationTemplate Successfully Added!", 5000)
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
  }
  UpdateNotificationTemplate() {
    this.proccessing = true
    if (!this.userForm.invalid) {
      this.catSvc.UpdateNotificationTemplate(this.selectedTemplate).subscribe({
        next: (res) => {
          this.catSvc.SuccessMsgBar("NotificationTemplate Successfully Updated!", 5000)
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
    else {
      this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
      this.proccessing = false
    }
  }
  Refresh() {
    this.Add = true;
    this.Edit = false;
    this.selectedTemplate = new NotificationTemplateVM
    this.selectedTemplate.isActive = true
  }
}


