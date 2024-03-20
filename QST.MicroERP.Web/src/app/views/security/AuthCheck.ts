import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, ActivatedRoute } from '@angular/router';
import { CatalogService } from '../catalog/catalog.service';
@Injectable()
export class AuthCheck implements CanActivate {
    Info: any;
    constructor(private router: Router,
        private catSvc: CatalogService
    ) {

    }

    canActivate(): boolean {
        var token = localStorage.getItem('Token');
        if (token) {
            this.catSvc.CheckandSet()
            return true;
        }
        else {
            //alert("Please First Sign In to Get Access ")
           // this.router.navigate(['/secLogin']);
            return true;
        }
    }
}