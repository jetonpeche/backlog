import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Compte } from 'src/app/classes/Compte';
import { Variable } from 'src/app/classeStatic/Variable';
import { TypeRole } from 'src/app/enums/TypeRole';
import { ConnexionService } from 'src/app/service/connexion.service';
import { OutilService } from 'src/app/service/outil.service';

@Component({
  selector: 'app-connexion',
  templateUrl: './connexion.component.html',
  styleUrls: ['./connexion.component.scss']
})
export class ConnexionComponent 
{
  voirMdp: boolean = false;

  constructor(private connServ: ConnexionService, private outilServ: OutilService, private router: Router) { }

  SeConnecter(_form: NgForm): void
  {
    this.connServ.Connexion(_form.value).subscribe({
      next: (retour: any) =>
      {       
        if(retour != false)
        {
          Variable.EstConnecter = true;

          switch (retour.TypeCompte) 
          {
            case TypeRole.DEVELOPPEUR:
              this.router.navigate(["/acceuilAdmin"]);
              break;

            case TypeRole.CLIENT:
              this.router.navigate(["/acceuil"]);
              break;

            case TypeRole.DEVELOPPEUR:
              this.router.navigate(["/acceuilDev"]);
              break;
          }
        }
        else
        {
          this.outilServ.ToastErreur("Login ou mot de passe incorrect");
        }
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }

  VoirCacherMdp(): void
  {
    this.voirMdp = !this.voirMdp;
  }
}
