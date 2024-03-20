import { Globals } from './../../globals';
import { SecurityService } from './../../views/security/security.service';
import { ItemsService } from './../../views/items/items.service';
import { Component, OnInit, ChangeDetectorRef, IterableDiffers } from '@angular/core';
import { INavData } from '@coreui/angular';

import { navItems } from './_nav';
import { SettingsTypeVM } from 'src/app/views/catalog/Models/SettingsTypeVM';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { BehaviorSubject } from 'rxjs';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { Roles } from 'src/app/views/catalog/Models/Enums/Roles';
import { UserVM } from 'src/app/views/security/models/user-vm';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
})
export class DefaultLayoutComponent implements OnInit {
  navItems: INavData[] = [];
  url!: string
  items: INavData[] = [];
  values: INavData[] = [];
  subValues: INavData[] = [];
  subVals: INavData[] = [];
  subVal: INavData[] = [];
  menuList = []
  //public navItems = navItems;
  SettingsType?: SettingsTypeVM[] = undefined;
  Type?: SettingsTypeVM = undefined;
  refresh = new BehaviorSubject<boolean>(true);
  Settings: SettingsVM[] = [];
  settings: SettingsVM[] = [];
  subSettings: SettingsVM[] = [];
  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  constructor(
    public differ: IterableDiffers,
    public secSvc: SecurityService,
    private storeSvc: StorageService,
    public catSvc: CatalogService,
  ) {
  }
  ngOnInit(): void {
    this.catSvc.refresh$.subscribe(() => {
      this.GetSettings();
    })
    this.GetSettings();
  }
  GetSettingsType() {
    this.navItems = []
    navItems.splice(0, navItems.length);
    var type = new SettingsTypeVM();
    type.isActive = true
    type.id = EnumTypes.BackOffice
    this.catSvc.SearchSettingsType(type).subscribe({
      next: (res: SettingsTypeVM[]) => {
        this.SettingsType = res;
        this.Type = this.SettingsType[0]
        if (this.Type != undefined)
          this.settings = this.settings?.filter(x => x.enumTypeId == this.Type?.id);
        //  if (this.settings.length != 0) {
        this.settings.forEach(element => {
          var SubSettings = this.subSettings?.filter(x => x.parentId == element.id)
          this.subValues = []

          // if (SubSettings.length != 0) {
          SubSettings.forEach(abb => {
            var subSubSetting = this.Settings?.filter(x => x.parentId == abb.id)
            this.subVals = []

            // if (subSubSetting.length != 0) {
            subSubSetting.forEach(ab => {
              var ad = { name: ab.name, url: Globals.webUrl + ab.keyCode }
              this.subVals.push(ad)
            })
            //  }
            var url = ""
            if (this.subVals.length > 0)
              url = ""
            else
              url = Globals.webUrl
            var ac = { name: abb.name, children: this.subVals, url: url + abb.keyCode }
            this.subValues.push(ac)
          })
          //  }
          var url = ""
          if (this.subValues.length > 0)
            url = ""
          else
            url = Globals.webUrl
          var a = { name: element.name, children: this.subValues, url: url + element.keyCode }
          navItems.push(a);
          this.navItems = navItems;
        });
        // }
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Failed to load Side Menu", 8000)
      }
    })
  }
  GetSettings() {

    var menu = {}
    this.menuList = []
    this.settings = []
    this.subSettings = []
    this.subSettings = []

    this.navItems = []
    navItems.splice(0, navItems.length);
    var stng = new SettingsVM();
    stng.isActive = true
    var user = new UserVM
    user.id = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    user.roleId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID)
    // if (!this.catSvc.isClientAdmin)
    user.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLT_ID)
    this.catSvc.SearchActiveMenu(user).subscribe({
      next: (res: SettingsVM[]) => {
        this.Settings = res
        this.subSettings = res
        this.settings = res
        var moduleIds: number[]
        this.menuList = []
        var mIds = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_MODULE_IDS)
        if (mIds != null) {
          var Ids = mIds.split(",")
          moduleIds = Ids.map(str => parseInt(str));
        } else
          moduleIds = []
        this.settings = this.settings?.filter(x =>
          (x.enumTypeId == EnumTypes.BackOffice && moduleIds.includes(x.id)) && x.clientId == 0
          ||
          (x.enumTypeId == EnumTypes.BackOffice && x.isSystemDefined == true
            && +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID) == Roles.SuperAdmin && x.clientId == 0
          )
        );
        this.settings.forEach(element => {
          var SubSettings = this.Settings?.filter(x => x.parentId == element.id)
          this.subValues = []
          SubSettings.forEach(abb => {
            var subSubSetting = this.Settings?.filter(x => x.parentId == abb.id)
            this.subVals = []
            subSubSetting.forEach(ab => {
              var ad = { name: ab.name, url: Globals.webUrl + ab.keyCode }
              this.subVals.push(ad)
            })
            var url = ""
            if (this.subVals.length > 0)
              url = ""
            else
              url = Globals.webUrl
            var ac = { name: abb.name, children: this.subVals, url: url + abb.keyCode }
            this.subValues.push(ac)
          })
          var url = ""
          if (this.subValues.length > 0)
            url = ""
          else
            url = Globals.webUrl
          menu = { name: element.name, children: this.subValues, url: url + element.keyCode }
          this.menuList.push(menu)
          //this.catSvc.triggerRefresh()
        });
        navItems.splice(0, navItems.length);
        navItems.push(...this.menuList)
        this.navItems = navItems;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Failed to load Side Menu", 8000)
      }
    })
  }
}