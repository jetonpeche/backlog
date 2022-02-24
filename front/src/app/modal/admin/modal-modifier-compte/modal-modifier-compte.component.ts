import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TypeCompte } from 'src/app/types/TypeCompte';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { Variable } from 'src/app/classeStatic/Variable';

@Component({
  selector: 'app-modal-modifier-compte',
  templateUrl: './modal-modifier-compte.component.html',
  styleUrls: ['./modal-modifier-compte.component.scss']
})
export class ModalModifierCompteComponent implements OnInit
{
  listeTypeCompte: TypeCompte[] = [];
  estModifier: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any, 
    private dialogRef: MatDialogRef<ModalModifierCompteComponent>,
    private outilService: OutilService,
    private compteService: CompteService) { }

  ngOnInit(): void 
  {
    this.listeTypeCompte = Variable.listeTypeCompte;
  }

  EstRoleClient(): boolean
  {
    return this.outilService.EstRoleClient(+this.data.compte.IdTypeCompte);
  }

  Modifier(_form: NgForm): void
  {
    if(_form.invalid)
    {
      this.outilService.ToastErreur("Veuillez remplir tous les champs");
      return;
    }

    this.compteService.Modifier(_form.value).subscribe({
      next: (retour) =>
      {
        if(retour == true)
        {
          this.estModifier = true;
          const NOM_ROLE = this.listeTypeCompte.find(c => c.Id == _form.value.IdTypeCompte).Nom;
          _form.value.TypeCompte = NOM_ROLE;
      
          this.outilService.ToastSucces("Le compte a été modifié");
          this.dialogRef.close(_form.value);
        }
      },
      error: () =>
      {
        this.outilService.ToastErreurHttp();
      }
    })

  }
}
