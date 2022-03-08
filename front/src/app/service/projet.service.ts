import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Projet } from '../types/Projet';

@Injectable({
  providedIn: 'root'
})
export class ProjetService 
{
  constructor(private http: HttpClient) { }

  Lister(): Observable<Projet[]>
  {
    return this.http.get<Projet[]>(`${environment.URL_API}/projet/lister`);
  }

  Liste2(_idDeveloppeur: number): Observable<Projet[]>
  {
    return this.http.get<Projet[]>(`${environment.URL_API}/projet/lister2/${_idDeveloppeur}`);
  }

  Ajouter(_info): Observable<number>
  {
    return this.http.post<number>(`${environment.URL_API}/projet/ajouter`, _info);
  }

  Supprimer(_idProjet: number): Observable<any>
  {
    return this.http.delete<any>(`${environment.URL_API}/projet/supprimer/${_idProjet}`);
  }
}
