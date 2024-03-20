import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { CatalogService } from '../../catalog/catalog.service';
@Component({
  selector: 'app-page401',
  templateUrl: './page401.component.html',
  styleUrls: ['./page401.component.css']
})
export class Page401Component {
  constructor(private loc: Location,
    private catSvc: CatalogService) {

  }
  goBack() {
    this.catSvc.CheckandSet()
    //this.loc.back();
  }
}
