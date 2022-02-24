import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Variable } from '../classeStatic/Variable';
import { TypeRole } from '../enums/TypeRole';
import { TypeCompte } from '../types/TypeCompte';

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

  EstRoleClient(_idRole: number): boolean
  {
    if(!_idRole)
      return false;

    const RETOUR = this.ChercherNomRoleById(_idRole);

    return RETOUR == TypeRole.CLIENT;
  }

  EstRoleAdmin(_idRole: number): boolean
  {
    if(!_idRole)
      return false;

    const RETOUR = this.ChercherNomRoleById(_idRole);

    return RETOUR == TypeRole.ADMIN;
  }

  EstRoleDev(_idRole: number): boolean
  { 
    if(!_idRole)
      return false;

    const RETOUR = this.ChercherNomRoleById(_idRole);

    return RETOUR == TypeRole.DEVELOPPEUR;
  }

  private ChercherNomRoleById(_id: number): string
  { 
    return Variable.listeTypeCompte.find(p => p.Id == _id).Nom;
  }
}
