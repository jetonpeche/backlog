import { Component, OnInit } from '@angular/core';
import { Variable } from 'src/app/classeStatic/Variable';
import { OutilService } from 'src/app/service/outil.service';
import { ProjetService } from 'src/app/service/projet.service';
import { Projet } from 'src/app/types/Projet';

@Component({
  selector: 'app-accueil-dev',
  templateUrl: './accueil-dev.component.html',
  styleUrls: ['./accueil-dev.component.scss']
})
export class AccueilDevComponent implements OnInit 
{
  listeProjet: Projet[] = [];

  constructor(private projetServ: ProjetService, private outilServ: OutilService) { }

  ngOnInit(): void 
  {
    this.ListerProjet();
  }

  ModalOuvrirProjet()
  {
    
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

  private ListerProjet(): void
  {
    //console.log(Variable.compteConnecter.Id);
    
    this.projetServ.Liste2(1).subscribe({
      next: (liste) =>
      {
        console.log(liste);
        
        this.listeProjet = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }
}
