import { Component, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OutilService } from 'src/app/service/outil.service';
import { SignalService } from 'src/app/service/signal.service';

@Component({
  selector: 'app-modal-ajouter-tache',
  templateUrl: './modal-ajouter-tache.component.html',
  styleUrls: ['./modal-ajouter-tache.component.scss']
})
export class ModalAjouterTacheComponent
{
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private signalrServ: SignalService, private outilServ: OutilService) { }

  AjouterTache(_form: NgForm): void
  {
    this.signalrServ.DemanderAjouterTache(_form.value);
    this.outilServ.ToastSucces("Votre tache a été ajoutée");

    _form.reset();
  }
}
