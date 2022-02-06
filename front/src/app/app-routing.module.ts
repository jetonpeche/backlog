import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccueilComponent } from './component/admin/accueil/accueil.component';
import { GestionCompteComponent } from './component/admin/gestion-compte/gestion-compte.component';
import { ConnexionComponent } from './component/connexion/connexion.component';

const routes: Routes = [
  { path: "", component: ConnexionComponent },
  { path: "acceuilAdmin", component: AccueilComponent },
  { path: "gestion-compte", component: GestionCompteComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
