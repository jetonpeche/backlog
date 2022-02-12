import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TypeCompte } from 'src/app/classes/TypeCompte';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { TypeCompteService } from 'src/app/service/type-compte.service';

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
    private typeCompteServ: TypeCompteService, 
    private outilService: OutilService,
    private compteService: CompteService) { }

  ngOnInit(): void 
  {
    this.ListerTypeRole();
  }

  Modifier(_form: NgForm): void
  {
    this.compteService.Modifier(_form.value).subscribe({
      next: (retour) =>
      {
        if(retour == true)
        {
          this.estModifier = true;
          const NOM_ROLE = this.listeTypeCompte.find(c => c.Id == _form.value.IdTypeCompte).Nom;
          _form.value.TypeCompte = NOM_ROLE;
      
          this.dialogRef.close(_form.value);
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
