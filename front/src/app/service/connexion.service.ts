import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConnexionService 
{
  constructor(private http: HttpClient) { }

  Connexion(infos): Observable<boolean>
  {
    return this.http.post<boolean>(`${environment.URL_API}/connexion/connexion`, infos);
  }
}
