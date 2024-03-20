import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, ActivatedRoute, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CatalogService } from '../catalog/catalog.service';
import { Credentials } from 'src/app/credentials';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';

@Injectable()
export class AuthorizationCheck implements CanActivate {
   Info: any;
   constructor(private router: Router,
      private storeSvc: StorageService,
      private catSvc: CatalogService
   ) {

   }
   canActivate(
      routeSnapShot: ActivatedRouteSnapshot,
      state: RouterStateSnapshot,
   ):
      | Observable<boolean | UrlTree>
      | Promise<boolean | UrlTree>
      | boolean
      | UrlTree {
      debugger
      let routeId = routeSnapShot.data["RouteId"] as Array<string>;
      if (routeId != null) {
         var isReadOnly = null;
         for (let routeKeyCode of routeId) {
            console.log(routeKeyCode);
            if (routeKeyCode != '') {
               let permission = this.catSvc.getPermission(routeKeyCode);
               if (permission != null) //return false;
                  isReadOnly = permission;
            }
         }
         if (isReadOnly == null)
            this.router.navigate(['/unAuth/401']);
      }
      var token = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_TOKEN);
      if (token) {
         return true;
      }
      else {
         //alert("Please First Sign In to Get Access ")
         this.router.navigate(['/']);
         return true;
      }
   }
   // canActivate(): boolean {
   //    var token = localStorage.getItem('Token');
   //    if (token) {
   //       return true;
   //    }
   //    else {
   //       //alert("Please First Sign In to Get Access ")
   //       this.router.navigate(['/']);
   //       return true;
   //    }
   // }
}