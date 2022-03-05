import { Component, OnDestroy, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Variable } from './classeStatic/Variable';
import { SignalService } from './service/signal.service';
import { TypeCompte } from './types/TypeCompte';
import { OutilService } from './service/outil.service';
import { TypeCompteService } from './service/type-compte.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit
{
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
  .pipe(
    map(result => result.matches),
    shareReplay()
  );
  constructor(
    private breakpointObserver: BreakpointObserver, 
    private signalServ: SignalService,
    private outilServ: OutilService,
    private typeCompteServ: TypeCompteService){ }

  ngOnInit() 
  {
    this.ListerTypeCompte();
    this.signalServ.StartConnexion();
  }

  EstConnecter(): boolean
  {
    return Variable.EstConnecter;
  }

  private ListerTypeCompte(): void
  {
    this.typeCompteServ.Lister().subscribe({
      next: (liste: TypeCompte[]) =>
      {
        Variable.listeTypeCompte = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }
}
