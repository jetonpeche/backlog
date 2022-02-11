import { Component, OnDestroy, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Variable } from './classeStatic/Variable';
import { SignalService } from './service/signal.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy
{
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
  .pipe(
    map(result => result.matches),
    shareReplay()
  );
  constructor(private breakpointObserver: BreakpointObserver, private signalServ: SignalService){ }

  ngOnInit() 
  {
    this.signalServ.StartConnexion();   
    
    setTimeout(() => {
      this.signalServ.DemanderAuServeur();
      this.signalServ.ReponseServeur();
    }, 2000);
  }

  ngOnDestroy(): void {
      this.signalServ.hubConnexion.off("askServerReponse");
  }

  EstConnecter(): boolean
  {
    return Variable.EstConnecter;
  }
}
