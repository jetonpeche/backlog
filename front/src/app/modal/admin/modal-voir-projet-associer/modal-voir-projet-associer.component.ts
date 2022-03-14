import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OutilService } from 'src/app/service/outil.service';
import { ProjetService } from 'src/app/service/projet.service';
import { Projet } from 'src/app/types/Projet';

@Component({
  selector: 'app-modal-voir-projet-associer',
  templateUrl: './modal-voir-projet-associer.component.html',
  styleUrls: ['./modal-voir-projet-associer.component.scss']
})
export class ModalVoirProjetAssocierComponent implements OnInit 
{
  listeProjetAssocier: Projet[] = [];
  listeProjetPasAssocier: Projet[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private projetServ: ProjetService,
    private outilServ: OutilService) { }

  ngOnInit(): void 
  {
    this.ListerProjetCompte();
    this.ListerProjetPasAssocier();
  }

  FormatNumTel(_numTel: string): string
  {
    let numReturn = "";
    const LISTE_MATCH = _numTel.match(/(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})/);
    
    for (let i = 1; i < LISTE_MATCH.length; i++) 
    {
      const element = LISTE_MATCH[i];

      if(i == 1)
        numReturn += element;
      else
        numReturn += `-${element}`;
    }

    return numReturn;
  }

  private ListerProjetCompte(): void
  {
    this.projetServ.Liste2(this.data.idCompte).subscribe({
      next: (liste: Projet[]) =>
      {
        this.listeProjetAssocier = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }

  private ListerProjetPasAssocier(): void
  {
    this.projetServ.ListerProjetPasAssocier(this.data.idCompte).subscribe({
      next: (liste: Projet[]) =>
      { 
        this.listeProjetPasAssocier = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }
}
