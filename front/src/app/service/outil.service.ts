import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class OutilService 
{
  constructor(private toast: ToastrService) { }

  ToastErreurHttp(): void
  {
    this.toast.error("Erreur réseaux", "Vous n'etes plus connecté à internet");
  }

  ToastSucces(_msg: string): void
  {
    this.toast.success(_msg);
  }

  ToastErreur(_msg: string): void
  {
    this.toast.error(_msg);
  }

  ToastWarning(_msg: string): void
  {
    this.toast.warning(_msg);
  }
}
