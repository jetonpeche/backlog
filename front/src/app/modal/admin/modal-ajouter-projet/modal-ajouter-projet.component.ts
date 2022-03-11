import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Compte } from 'src/app/types/Compte';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { ProjetService } from 'src/app/service/projet.service';
import { CompteClient } from 'src/app/types/CompteClient';

@Component({
  selector: 'app-modal-ajouter-projet',
  templateUrl: './modal-ajouter-projet.component.html',
  styleUrls: ['./modal-ajouter-projet.component.scss']
})
export class ModalAjouterProjetComponent implements OnInit 
{
  estAjouter: boolean = false;
  listeCompteDev: Compte[] = [];
  listeCompteClient: CompteClient[] = [];

  constructor(
    private projetServ: ProjetService, 
    private outilServ: OutilService, 
    private compteServ: CompteService, 
    private dialogRef: MatDialogRef<ModalAjouterProjetComponent>) { }

  ngOnInit(): void
  {
    this.ListerCompteDev();
    this.ListerCompteClient();
  }

  Ajouter(_form: NgForm): void
  { 
    if(_form.invalid)
    {
      this.outilServ.ToastFormIncomplet();
      return;
    }

    if(_form.value.listeCompte == "")
      _form.value.listeCompte = [];

    _form.value.idCompteClient = _form.value.idCompteClient[0];

    this.projetServ.Ajouter(_form.value).subscribe({
      next: (retour: number) =>
      {
        if(retour != 0)
        {
          this.estAjouter = true;
          _form.value.Id = retour;
  
          delete _form.value.listeCompte;
  
          this.dialogRef.close(_form.value);
        }
        else
        {
          this.outilServ.ToastErreur("Le projet n'a pas pu être créé");
        }
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

  private ListerCompteClient(): void
  {
    this.compteServ.ListerClient().subscribe({
      next: (liste) =>
      {
        this.listeCompteClient = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }
}
