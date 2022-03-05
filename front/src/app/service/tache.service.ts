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

  ModifierEtat(_info): Observable<any>
  {
    return this.http.put(`${environment.URL_API}/Tache/modifier`, _info);
  }
}
