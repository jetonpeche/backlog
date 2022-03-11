import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { ProjetService } from 'src/app/service/projet.service';
import { Compte } from 'src/app/types/Compte';
import { Projet } from 'src/app/types/Projet';

@Component({
  selector: 'app-modal-modifier-projet',
  templateUrl: './modal-modifier-projet.component.html',
  styleUrls: ['./modal-modifier-projet.component.scss']
})
export class ModalModifierProjetComponent implements OnInit 
{
  listeCompteDev: Compte[] = []; 
  listeIdCompteProjet: any[] = [];
  projet: Projet;

  private listeIdCompteSupp: number[] = [];
  private listeIdCompteAjout: number[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any, 
    private dialogRef: MatDialogRef<ModalModifierProjetComponent>, 
    private projetServ: ProjetService,
    private compteServ: CompteService,
    private outilServ: OutilService) { }

  ngOnInit(): void 
  {
    this.projet = this.data.projet;

    this.ListerIdCompteProjet(this.projet.Id);
  }

  CompteEstDansProjet(_idCompte: number): boolean
  {
    const INDEX = this.listeIdCompteProjet.findIndex(p => p.IdCompte == _idCompte);

    return INDEX != -1;
  }

  CheckIdCompte(_idCompte: number, _etat: boolean): void
  {
    const INDEX_ORIGINE = this.listeIdCompteProjet.findIndex(c => c.IdCompte == _idCompte);
    const INDEX_SUPP = this.listeIdCompteSupp.findIndex(c => c == _idCompte);
    const INDEX_AJOUT = this.listeIdCompteAjout.findIndex(c => c == _idCompte);
    
    // est selectionné
    if(_etat == true)
    {
      // nouveau compte
      if(INDEX_ORIGINE == -1)
      {
        // supp l'id de la liste des compte a supp
        if(INDEX_SUPP != -1)
          this.listeIdCompteSupp.splice(INDEX_SUPP, 1);
      
        this.listeIdCompteAjout.push(_idCompte);
      }
      else
      {
        this.listeIdCompteSupp.splice(INDEX_SUPP, 1);
      }
    }
    else
    {
      // nouveau tag
      if(INDEX_ORIGINE == -1)
      {
        // supp l'id de la liste des tag a ajouter
        if(INDEX_AJOUT != -1)
          this.listeIdCompteAjout.splice(INDEX_AJOUT, 1);

        this.listeIdCompteSupp.push(_idCompte);
      }
      else
      {
        this.listeIdCompteSupp.push(_idCompte);
      }
    }
  }

  Modifier(_form: NgForm): void
  {
    if(_form.invalid)
    {
      this.outilServ.ToastFormIncomplet();
      return;
    }

    // ajout des listes dans le JSON
    _form.value.listeIdCompteSupp = this.listeIdCompteSupp;
    _form.value.listeIdCompteAjout = this.listeIdCompteAjout;

    this.projetServ.Modifier(_form.value).subscribe({
      next: (retour: boolean) =>
      { 
        if(retour == true)
        {
          delete _form.value.listeIdCompteSupp;
          delete _form.value.listeIdCompteAjout;

          this.outilServ.ToastSucces(`Le projet: ${this.projet.Nom} à été modifié`);

          this.dialogRef.close(_form.value);
        }
        else
          this.outilServ.ToastErreur("Modification impossible");
      }, 
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }

  private ListerCompteDev(): void
  {
    this.compteServ.ListerDev().subscribe({
      next: (liste) =>
      {
        this.listeCompteDev = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }

  private ListerIdCompteProjet(_idProjet: number): void
  {
    this.compteServ.ListerIdCompteProjet(_idProjet).subscribe({
      next: (liste: any[]) =>
      {
        console.log(liste);
        
        this.listeIdCompteProjet = liste;

        this.ListerCompteDev();
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }
}
