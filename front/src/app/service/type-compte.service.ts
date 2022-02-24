import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TypeCompte } from '../types/TypeCompte';

@Injectable({
  providedIn: 'root'
})
export class TypeCompteService 
{
  constructor(private http: HttpClient) { }

  Lister(): Observable<TypeCompte[]>
  {
    return this.http.get<TypeCompte[]>(`${environment.URL_API}/typeCompte/lister`);
  }
}
