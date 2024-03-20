import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Inject, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CatalogService } from '../catalog.service';
import { SettingsVM } from '../Models/SettingsVM';
import { EnumTypes } from '../Models/EnumTypes';
import { MatTableDataSource } from '@angular/material/table';
import { ClientsVM } from '../Models/ClientsVM';

@Component({
  selector: 'app-modules',
  templateUrl: './modules.component.html',
  styleUrls: ['./modules.component.css']
})
export class ModulesComponent implements OnInit {
  modules: SettingsVM[]
  DisplayedColumns: string[] = ['select', 'displayUOM'];
  DataSource: any
  addMode: boolean = true
  editMode: boolean = false
  client: ClientsVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  constructor(
    private cdref: ChangeDetectorRef,
    private catSvc: CatalogService,
    public dialogRef: MatDialogRef<ModulesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.client = new ClientsVM
    //this.DataSource = new MatTableDataSource(this.uomConversions)
  }
  ngOnInit(): void {
    if (this.data != null)
      if (this.data.client != null) {
        this.client = this.data.client
        if (this.client.moduleIdList)
          if (this.client.moduleIdList.length > 0) {
            this.editMode = true
            this.addMode = false
          } else {
            this.editMode = false
            this.addMode = true
          }
      }
    this.getModules()
  }
  getModules() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.BackOffice
    stng.isActive = true
    stng.clientId = 0
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        res = res.filter(x => x.isSystemDefined == false)
        this.modules = res
        this.DataSource = new MatTableDataSource(this.modules)
        this.isChecked()
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      }
    })
  }
  isChecked() {
    this.modules = this.modules.map(x => {
      debugger
      if (this.client.moduleIdList.includes(x.id)) {
        x.isChecked = true
        return x;
      } else return x;
    })
    // var retVal = this.client.moduleIdList.includes(row.id)
    // row.isChecked = retVal
    // return retVal
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
    this.cdref.markForCheck();
  }
  Submit() {
    debugger
    var list = this.modules.filter(x => x.isChecked == true)
    this.client.moduleIdList = []
    list.forEach(element => {
      this.client.moduleIdList.push(element.id)
    });
    this.catSvc.UpdateClients(this.client).subscribe({
      next: (res: ClientsVM) => {
        console.warn(res)
        this.catSvc.SuccessMsgBar("Successfully Updated", 6000)
        // this.catSvc.openCltData(res)
        // this.catSvc.triggerRefresh()
        this.dialogRef.close({
          client: res
        });
      }, error: () => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      }
    })
  }
}
