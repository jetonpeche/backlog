import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Compte } from 'src/app/classes/Compte';
import { TypeCompte } from 'src/app/classes/TypeCompte';
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

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<ModalModifierCompteComponent>, private typeCompteServ: TypeCompteService, private outilService: OutilService) { }

  ngOnInit(): void 
  {
    this.ListerTypeRole();
  }

  Modifier(_form: NgForm): void
  {
    this.estModifier = true;
    const NOM_ROLE = this.listeTypeCompte.find(c => c.Id == _form.value.IdTypeCompte).Nom;
    _form.value.TypeCompte = NOM_ROLE;

    this.dialogRef.close(_form.value);
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
