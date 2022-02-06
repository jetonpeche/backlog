import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { TypeCompte } from 'src/app/classes/TypeCompte';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { TypeCompteService } from 'src/app/service/type-compte.service';

@Component({
  selector: 'app-modal-ajouter-compte',
  templateUrl: './modal-ajouter-compte.component.html',
  styleUrls: ['./modal-ajouter-compte.component.scss']
})
export class ModalAjouterCompteComponent implements OnInit 
{
  listeTypeCompte: TypeCompte[] = [];

  constructor(private typeCompteServ: TypeCompteService, private compteServ: CompteService, private outilService: OutilService, private diagRef: MatDialogRef<ModalAjouterCompteComponent>) { }

  ngOnInit(): void 
  {
    this.ListerTypeRole();
  }

  Ajouter(_form: NgForm): void
  {
    // creer un mdp
    _form.value.Mdp = `${_form.value.Nom}.${_form.value.Prenom}`;

    this.compteServ.Ajouter(_form.value).subscribe({
      next: (id: number) =>
      {
        if(id != 0)
        {
          _form.value.Id = id;
          this.outilService.ToastSucces("Le compte a été ajouté");

          // supprime le mdp du json
          delete _form.value.Mdp;

          const NOM_TYPE_COMPTE = this.listeTypeCompte.find(c => c.Id == +_form.value.IdTypeCompte).Nom;
          _form.value.TypeCompte = NOM_TYPE_COMPTE;

          this.diagRef.close(_form.value);
        }
        else
        {
          this.outilService.ToastErreur("Le compte n'a pas pu être ajouté");
        }
      },
      error: () =>
      {
        this.outilService.ToastErreurHttp();
      }
    })

  }

  private ListerTypeRole(): void
  {
    this.typeCompteServ.Lister().subscribe({
      next: (liste: TypeCompte[]) =>
      {
        this.listeTypeCompte = liste;
      },
      error: () =>
      {
        this.outilService.ToastErreurHttp();
      }
    })
  }
}
