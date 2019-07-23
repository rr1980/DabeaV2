import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, CanLoad, Route, UrlSegment } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class AuthExternGuard implements CanActivate, CanLoad {


  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authService.currentUserValue;

    if (currentUser && currentUser.isExtern) {
      return true;
    }

    //this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    alert("Keine Berechtigung!");
    return false;
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean>|Promise<boolean>|boolean {
    const currentUser = this.authService.currentUserValue;

    if (currentUser && currentUser.isExtern) {
      return true;
    }

    alert("Keine Berechtigung!");
    return false;
  }

}
