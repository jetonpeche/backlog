import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OutilService } from 'src/app/service/outil.service';
import { TacheService } from 'src/app/service/tache.service';
import { Tache } from 'src/app/types/Tache';

@Component({
  selector: 'app-modal-voir-tache-projet',
  templateUrl: './modal-voir-tache-projet.component.html',
  styleUrls: ['./modal-voir-tache-projet.component.scss']
})
export class ModalVoirTacheProjetComponent implements OnInit 
{
  listeTache: Tache[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private tacheServ: TacheService, private outilServ: OutilService) { }

  ngOnInit(): void 
  {
    this.tacheServ.Lister(this.data.idProjet).subscribe({
      next: (liste) =>
      {
        this.listeTache = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }
}
