import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { StatusTache } from '../types/StatusTache';

@Injectable({
  providedIn: 'root'
})
export class StatusTacheService {

  constructor(private http: HttpClient) { }

  Lister(): Observable<StatusTache[]>
  {
    return this.http.get<StatusTache[]>(`${environment.URL_API}/StatusTache/lister`);
  }
}
