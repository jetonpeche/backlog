import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalService 
{
  hubConnexion: signalR.HubConnection;

  constructor() { }

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

  async DemanderAuServeur()
  {
    await this.hubConnexion.invoke("AskServer", "a");
  }

  ReponseServeur()
  {
    this.hubConnexion.on("askServerReponse", (retour) =>
    {
      console.log(retour);
      
    })
  }
}
