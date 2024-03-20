import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { SettingsVM } from '../../Models/SettingsVM';
import { EnumTypes } from '../../Models/EnumTypes';
import { ManageEnumLineComponent } from '../../manage-enum-line/manage-enum-line.component';
import { CatalogService } from '../../catalog.service';
import { ClientsVM } from '../../Models/ClientsVM';
import { ManageEnumLineWithParentComponent } from '../../manage-enum-line-with-parent/manage-enum-line-with-parent.component';

@Component({
  selector: 'app-manage-clients',
  templateUrl: './manage-clients.component.html',
  styleUrls: ['./manage-clients.component.css']
})
export class ManageClientsComponent implements OnInit {
  searchedType?: EnumTypes[];
  hide = true;
  lecs?: SettingsVM[]
  CatSvc: CatalogService
  proccessing: boolean = false;
  Edit: boolean = false;
  Add: boolean = true;
  validFields: boolean = false;
  public date = new Date();
  cities: SettingsVM[]
  countries: SettingsVM[]
  categories: SettingsVM[]
  Clients: ClientsVM[] | undefined;
  selectedClients: ClientsVM
  dataSource: any;
  // selectedEnumType = new EnumTypes()
  @ViewChild('clientsForm', { static: true }) clientsForm!: NgForm;
  dialogRef: any
  dialogData;
  constructor(
    private dialog: MatDialog,
    private injector: Injector,
    private catSvc: CatalogService,

  ) {
    this.dialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedClients = new ClientsVM();
    // this.selectedEnumType = new EnumTypes();
  }
  ngOnInit(): void {
    this.Add = true;
    this.Edit = false;
    this.selectedClients = new ClientsVM
    // this.selectedEnumType = new EnumTypes
    this.GetSettings(EnumTypes.City)
    this.GetSettings(EnumTypes.Country)
    this.GetSettings(EnumTypes.ClientCategories)
    this.selectedClients.isActive = true;
    if (this.dialogData != null) {
      if (this.dialogData.cltId != undefined) {
        this.GetClientsForEdit(this.dialogData.cltId)
      }
    }
  }
  OpenCountryDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: { enumTypeId: EnumTypes.Country, isDialog: true, clientId: 0 }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetCountry()
      }
    })
  }
  OpenCityDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineWithParentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
      , data: { clientId: 0, enumTypeId: EnumTypes.City, isDialog: true, parentType: EnumTypes.Country, parentId: this.selectedClients.countryId }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.SearchCities()
      }
    })
  }
  GetCountry() {
    var stng = new SettingsVM
    stng.moduleId = 0
    stng.clientId = 0
    stng.enumTypeId = EnumTypes.Country
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.countries = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetCity() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.City
    stng.moduleId = 0
    stng.clientId = 0
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.cities = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  SearchCities() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.City
    stng.moduleId = 0
    stng.clientId = 0
    stng.parentId = this.selectedClients.countryId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.cities = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype == EnumTypes.City)
          this.cities = res;
        else if (etype == EnumTypes.ClientCategories)
          this.categories = res;
        else if (etype == EnumTypes.Country)
          this.countries = res;
      }, error: (e: any) => {
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
        this.catSvc.DeleteClients(id).subscribe({
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
    window.scrollTo(0, 0)
    this.selectedClients = new ClientsVM;
    this.selectedClients.id = id
    this.catSvc.SearchClients(this.selectedClients).subscribe({
      next: (res: ClientsVM[]) => {
        this.Clients = res;
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
    // this.catSvc.GetClients().subscribe({
    //   next: (res: ClientsVM[]) => {
    //     var list = res
    //     if (this.Edit)
    //       list = list.filter(x => x.id != this.selectedClients.id)
    //     var find = list.find(x => x.clientName == this.selectedClients.clientName  )
    //     if (find == undefined) 
    //     {
    if (this.selectedClients.categoryId == 0 || this.selectedClients.categoryId == undefined)
      this.clientsForm.controls["categoryId"].setErrors({ "incorrect": true })
    if (!this.clientsForm.invalid) {
      if (this.Edit)
        this.UpdateClients()
      else {
        this.catSvc.SaveClients(this.selectedClients).subscribe({
          next: (res) => {
            this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
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
    //     } else
    //       this.catSvc.ErrorMsgBar("This Name Already Taken ", 5000)
    //   }, error: (e) => {
    //     this.catSvc.ErrorMsgBar("Error Occurred", 5000)
    //     console.warn(e);
    //   }
    // })
  }
  OpenCatDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: { enumTypeId: EnumTypes.ClientCategories, isDialog: true, clientId: 0 }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.ClientCategories)
      }
    })
  }
  UpdateClients() {
    this.catSvc.UpdateClients(this.selectedClients).subscribe({
      next: (res) => {
        this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
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
    this.ngOnInit()
  }
  // Search(){
  //   var  topic = new EnumTypes();
  //   topic.courseId = this.selectedTask.courseId;
  //    topic.isActive = true;
  //   this.lmsSvc.SearchTopic(topic).subscribe({
  //    next: (value: TopicVM[]) => {
  //      this.topics = value
  //    }, error: (err) => {
  //      this.pmsSvc.ErrorMsgBar("Error Occurred", 5000)
  //    },
  //  })
  // Search(enu: EnumTypes) {
  //   var country = new EnumTypes();
  //   country.isActive = true;
  //   country.cityId = enu.cityId; // Yahan par selectedClients.cityId ke bajaye enu.cityId ka use kiya gaya hai.

  //   // Ab aap countryId ke against cities fetch kar sakte hain
  //   this.catSvc.GetCitiesByCountryId(country.countryId).subscribe({
  //     next: (cities: SettingsVM[]) => {
  //       // cities variable me aapke selected country ke cities honge, inhe aap UI me display kar sakte hain.
  //       // Example: this.cities = cities;
  //     },
  //     error: (err) => {
  //       this.catSvc.ErrorMsgBar("Error Occurred", 5000);
  //     },
  //   });
  // }

  // GetCitiesByCountryId(){

  // }
  //  Search(enu : EnumTypes) {
  //    var type = new enu EnumTypes();
  //    type.cityId = this.selectedEnumType.cityId;
  //    this.catSvc.SearchSettingsType(type).subscribe((res: EnumTypes[]) => {
  //      this.searchedType = res;
  //      this.dataSource = new MatTableDataSource(this.searchedType);
  //    });
  //  }
  // Search() {
  //   const type = EnumTypes; // Just use the enum directly

  //   // Access the enum values as needed
  //   const cityId = this.selectedClients.cityId;
  //   const countryId = this.selectedClients.countryId;

  //   // You can now use 'type', 'cityId', and 'countryId' in your code as needed
  //   // ...
  // }
}















