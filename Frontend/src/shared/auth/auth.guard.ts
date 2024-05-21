

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AppSessionService } from '../session/app-session.service';

@Injectable({
  providedIn: 'root'
})
export class authGuard implements CanActivate {
  constructor(
    private _router: Router,
    private _sessionService: AppSessionService,
) { 
    console.log("in guard auth")
}
  canActivate(
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      console.log('_sessionService ', this._sessionService.user);
        
      if (!this._sessionService.user) {
          this._router.navigate(['/login']);
          return false;
      }
      return true;
  }
  
}
