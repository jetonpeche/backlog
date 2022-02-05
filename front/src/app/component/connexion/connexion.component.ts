import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ConnexionService } from 'src/app/service/connexion.service';

@Component({
  selector: 'app-connexion',
  templateUrl: './connexion.component.html',
  styleUrls: ['./connexion.component.scss']
})
export class ConnexionComponent implements OnInit {

  constructor(private connServ: ConnexionService) { }

  ngOnInit(): void {
  }

  SeConnecter(_form: NgForm): void
  {
    this.connServ.Connexion(_form.value).subscribe(
      (retour) =>
      {
        console.log(retour);
      }
    )
  }
}
