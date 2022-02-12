import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Compte } from 'src/app/classes/Compte';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { ProjetService } from 'src/app/service/projet.service';

@Component({
  selector: 'app-modal-ajouter-projet',
  templateUrl: './modal-ajouter-projet.component.html',
  styleUrls: ['./modal-ajouter-projet.component.scss']
})
export class ModalAjouterProjetComponent implements OnInit 
{
  estAjouter: boolean = false;
  listeCompte: Compte[] = [];

  constructor(
    private projetServ: ProjetService, 
    private outilServ: OutilService, 
    private compteServ: CompteService, 
    private dialogRef: MatDialogRef<ModalAjouterProjetComponent>) { }

  ngOnInit(): void
  {
    this.ListerPersonne();
  }

  Ajouter(_form: NgForm): void
  { 
    if(_form.value.listeCompte == "")
      _form.value.listeCompte = [];

    this.projetServ.Ajouter(_form.value).subscribe({
      next: (retour: number) =>
      {
        this.estAjouter = true;
        _form.value.Id = retour;

        delete _form.value.listeCompte;

        this.dialogRef.close(_form.value);
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }

  private ListerPersonne(): void
  {
    this.compteServ.ListerDev().subscribe({
      next: (liste) =>
      {
        this.listeCompte = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }
}
