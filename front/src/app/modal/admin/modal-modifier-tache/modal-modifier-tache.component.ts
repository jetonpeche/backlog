import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SignalService } from 'src/app/service/signal.service';
import { Tache } from 'src/app/types/Tache';

@Component({
  selector: 'app-modal-modifier-tache',
  templateUrl: './modal-modifier-tache.component.html',
  styleUrls: ['./modal-modifier-tache.component.scss']
})
export class ModalModifierTacheComponent implements OnInit 
{
  tache: Tache;
  idProjet: number;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private signalrServ: SignalService, private dialogRef: MatDialogRef<ModalModifierTacheComponent>) { }

  ngOnInit(): void 
  {
    this.tache = this.data.tache;
    this.idProjet = this.data.idProjet;
  }

  ModifierTache(_form: NgForm): void
  {
    this.signalrServ.DemanderModifDescriptionTache(_form.value);
    this.dialogRef.close(_form.value.Description);
  }
}
