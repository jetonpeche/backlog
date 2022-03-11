import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Compte } from '../types/Compte';
import { CompteClient } from '../types/CompteClient';

@Injectable({
  providedIn: 'root'
})
export class CompteService 
{
  constructor(private http: HttpClient) { }

  Lister(): Observable<CompteClient[]>
  {
    return this.http.get<CompteClient[]>(`${environment.URL_API}/compte/lister`);
  }

  ListerDev(): Observable<Compte[]>
  {
    return this.http.get<Compte[]>(`${environment.URL_API}/compte/listerDev`);
  }

  ListerClient(): Observable<CompteClient[]>
  {
    return this.http.get<CompteClient[]>(`${environment.URL_API}/compte/listerClient`);
  }

  ListerIdCompteProjet(_idProjet: number): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.URL_API}/compte/listerIdCompteProjet/${_idProjet}`);
  }

  Ajouter(_info: Compte): Observable<number>
  {
    return this.http.post<number>(`${environment.URL_API}/compte/ajouter`, _info);
  }

  Modifier(_info: Compte): Observable<boolean>
  {
    return this.http.put<boolean>(`${environment.URL_API}/compte/modifier/${_info.Id}`, _info);
  }
}
