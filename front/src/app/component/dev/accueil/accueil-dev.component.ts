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

  private ListerProjet(): void
  {
    this.projetServ.Liste2(Variable.compteConnecter.Id).subscribe({
      next: (liste) =>
      {
        this.listeProjet = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }
}