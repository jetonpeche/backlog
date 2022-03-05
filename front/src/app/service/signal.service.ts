import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { Tache } from '../types/Tache';
import { OutilService } from './outil.service';

@Injectable({
  providedIn: 'root'
})
export class SignalService 
{
  hubConnexion: signalR.HubConnection;

  constructor(private outilServ: OutilService) { }

  StartConnexion()
  {
    this.hubConnexion = new signalR.HubConnectionBuilder()

    // toastr fait reference au program.cs sur app.maphub
    .withUrl(`${environment.URL_API}/toastr`, 
    {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    })
    .build();

    // lancer la connexion
    this.hubConnexion.start().then(
      () =>
      {
        console.log("la connexion a été faite");
      }
    )
  }

  DemanderQuitterGrpProjet(_idProjet: number): void
  {
    this.hubConnexion.invoke("QuitterGrpProjet", _idProjet);
  }

  async DemanderAjouterTache(_info): Promise<void>
  {
    await this.hubConnexion.invoke("AjouterTache", _info).catch(
      () =>
      {
        this.outilServ.ToastErreurHttp();
      });
  }

  async DemanderListeTache(_idProjet: number): Promise<void>
  {
    await this.hubConnexion.invoke("ListerTache", _idProjet);
  }

  async DemanderModifTache(_info): Promise<void>
  {
    await this.hubConnexion.invoke("ModifierEtatTache", _info);
  }
}
