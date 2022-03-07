import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-confirmation',
  templateUrl: './modal-confirmation.component.html',
  styleUrls: ['./modal-confirmation.component.scss']
})
export class ModalConfirmationComponent implements OnInit 
{
  texte: string;
  titre: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void 
  {
    this.titre = this.data.titre;
    this.texte = this.data.texte;  
  }

}
