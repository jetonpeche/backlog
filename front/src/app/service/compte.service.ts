import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Compte } from '../classes/Compte';

@Injectable({
  providedIn: 'root'
})
export class CompteService 
{

  constructor(private http: HttpClient) { }

  ListerCompte(): Observable<Compte[]>
  {
    return this.http.get<Compte[]>(`${environment.URL_API}/compte/lister`);
  }
}
