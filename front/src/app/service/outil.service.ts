import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { Variable } from '../classeStatic/Variable';
import { TypeRole } from '../enums/TypeRole';
import { ModalConfirmationComponent } from '../modal/modal-confirmation/modal-confirmation.component';

@Injectable({
  providedIn: 'root'
})
export class OutilService 
{
  sujet: Subject<boolean>;

  constructor(private toast: ToastrService, private dialog: MatDialog) { }

  ModalConfirmation(_titre, _texte): void
  {
    this.sujet = new Subject<boolean>();

    const DIALOG_REF = this.dialog.open(ModalConfirmationComponent, 
      { 
        width: "20%",
        data: 
        { 
          titre: _titre, 
          texte: _texte
        }
      });

    DIALOG_REF.afterClosed().subscribe({
      next: (retour: boolean) =>
      {
        this.sujet.next(retour);

        // "supprimer" le sujet plus rien ne passe
        this.sujet.complete();
      }
    })
  }

  ToastErreurHttp(): void
  {
    this.toast.error("Erreur réseaux", "Vous n'etes plus connecté à internet");
  }

  ToastInfo(_msg: string): void
  {
    this.toast.info(_msg);
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
