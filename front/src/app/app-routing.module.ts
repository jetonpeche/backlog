import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccueilComponent } from './component/admin/accueil/accueil.component';
import { GestionCompteComponent } from './component/admin/gestion-compte/gestion-compte.component';
import { GestionProjetComponent } from './component/admin/gestion-projet/gestion-projet.component';
import { ConnexionComponent } from './component/connexion/connexion.component';
import { AccueilDevComponent } from './component/dev/accueil/accueil-dev.component';
import { ListingTacheComponent } from './component/dev/listing-tache/listing-tache.component';

const routes: Routes = [
  { path: "", component: ConnexionComponent },
  { path: "acceuilAdmin", component: AccueilComponent },
  { path: "acceuilDev", component: AccueilDevComponent },
  { path: "gestion-compte", component: GestionCompteComponent },
  { path: "gestion-projet", component: GestionProjetComponent },

  {path: "tache/:id", component: ListingTacheComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
