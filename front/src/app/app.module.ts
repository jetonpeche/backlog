import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

//#region mat
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatListModule} from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import {MatTabsModule} from '@angular/material/tabs';
import {MatTableModule} from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatDialogModule} from '@angular/material/dialog';
import {MatSelectModule} from '@angular/material/select';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatBadgeModule} from '@angular/material/badge';
//#endregion

//#region services
import { OutilService } from './service/outil.service';
import { ConnexionService } from './service/connexion.service';
import { CompteService } from './service/compte.service';
import { TypeCompteService } from './service/type-compte.service';
//#endregion

//#region component
import { AccueilComponent } from './component/admin/accueil/accueil.component';
import { GestionCompteComponent } from './component/admin/gestion-compte/gestion-compte.component';
import { AppComponent } from './app.component';
import { ConnexionComponent } from './component/connexion/connexion.component';
import { ModalAjouterCompteComponent } from './modal/admin/modal-ajouter-compte/modal-ajouter-compte.component';
import { ModalModifierCompteComponent } from './modal/admin/modal-modifier-compte/modal-modifier-compte.component';
import { GestionProjetComponent } from './component/admin/gestion-projet/gestion-projet.component';
import { ModalAjouterProjetComponent } from './modal/admin/modal-ajouter-projet/modal-ajouter-projet.component';
import { AccueilDevComponent } from './component/dev/accueil/accueil-dev.component';
import { ListingTacheComponent } from './component/dev/listing-tache/listing-tache.component';
import { ModalAjouterTacheComponent } from './modal/dev/modal-ajouter-tache/modal-ajouter-tache.component';
import { ModalVoirTacheProjetComponent } from './modal/admin/modal-voir-tache-projet/modal-voir-tache-projet.component';
import { ModalConfirmationComponent } from './modal/modal-confirmation/modal-confirmation.component';
import { ModalModifierProjetComponent } from './modal/admin/modal-modifier-projet/modal-modifier-projet.component';
import { ModalModifierTacheComponent } from './modal/admin/modal-modifier-tache/modal-modifier-tache.component';
import { ModalVoirProjetAssocierComponent } from './modal/admin/modal-voir-projet-associer/modal-voir-projet-associer.component';
//#endregion

@NgModule({
  declarations: [
    AppComponent,
    ConnexionComponent,
    AccueilComponent,
    GestionCompteComponent,
    ModalAjouterCompteComponent,
    ModalModifierCompteComponent,
    GestionProjetComponent,
    ModalAjouterProjetComponent,
    AccueilDevComponent,
    ListingTacheComponent,
    ModalAjouterTacheComponent,
    ModalVoirTacheProjetComponent,
    ModalConfirmationComponent,
    ModalModifierProjetComponent,
    ModalModifierTacheComponent,
    ModalVoirProjetAssocierComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      progressBar: true,
      progressAnimation: 'increasing'
      //positionClass: 
    }),
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatMenuModule,
    MatSidenavModule,
    MatListModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    MatSelectModule,
    MatExpansionModule,
    MatBadgeModule
  ],
  entryComponents: [ModalAjouterCompteComponent, ModalModifierCompteComponent],
  providers: [OutilService, ConnexionService, CompteService, TypeCompteService],
  bootstrap: [AppComponent]
})
export class AppModule { }
