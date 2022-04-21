import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Variable } from '../classeStatic/Variable';

@Injectable({
  providedIn: 'root'
})
export class ConnexionGuard implements CanActivate 
{
  constructor(private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
      if(Variable.EstConnecter)
        return true;
      else
      {
        this.router.navigate([""]);
        return false;
      }
  }
}
