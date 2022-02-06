import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Variable } from 'src/app/classeStatic/Variable';
import { ConnexionService } from 'src/app/service/connexion.service';
import { OutilService } from 'src/app/service/outil.service';

@Component({
  selector: 'app-connexion',
  templateUrl: './connexion.component.html',
  styleUrls: ['./connexion.component.scss']
})
export class ConnexionComponent implements OnInit {

  voirMdp: boolean = false;

  constructor(private connServ: ConnexionService, private outilServ: OutilService, private router: Router) { }

  ngOnInit(): void {
  }

  SeConnecter(_form: NgForm): void
  {
    this.connServ.Connexion(_form.value).subscribe({
      next: (retour) =>
      {
        if(retour == true)
        {
          Variable.EstConnecter = true;
          this.router.navigate([""]);
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
