import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccueilComponent } from './component/admin/accueil/accueil.component';
import { GestionCompteComponent } from './component/admin/gestion-compte/gestion-compte.component';
import { GestionProjetComponent } from './component/admin/gestion-projet/gestion-projet.component';
import { ConnexionComponent } from './component/connexion/connexion.component';
import { AccueilDevComponent } from './component/dev/accueil/accueil-dev.component';
import { ListingTacheComponent } from './component/dev/listing-tache/listing-tache.component';
import { ConnexionGuard } from './guard/connexion.guard';

const routes: Routes = [
  { path: "", component: ConnexionComponent },
  { path: "acceuilAdmin", canActivate: [ConnexionGuard], component: AccueilComponent },
  { path: "acceuilDev", canActivate: [ConnexionGuard], component: AccueilDevComponent },
  { path: "gestion-compte", canActivate: [ConnexionGuard], component: GestionCompteComponent },
  { path: "gestion-projet", canActivate: [ConnexionGuard], component: GestionProjetComponent },

  {path: "tache/:id", canActivate: [ConnexionGuard], component: ListingTacheComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
