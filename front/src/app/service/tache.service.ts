import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tache } from '../types/Tache';

@Injectable({
  providedIn: 'root'
})
export class TacheService {

  constructor(private http: HttpClient) { }

  Lister(_idProjet: number): Observable<Tache[]>
  {
    return this.http.get<Tache[]>(`${environment.URL_API}/TacheProjet/lister/${_idProjet}`);
  }
}
