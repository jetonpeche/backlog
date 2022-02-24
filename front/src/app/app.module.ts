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
import { ModalSupprimerProjetComponent } from './modal/admin/modal-supprimer-projet/modal-supprimer-projet.component';
import { AccueilDevComponent } from './component/dev/accueil/accueil-dev.component';
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
    ModalSupprimerProjetComponent,
    AccueilDevComponent
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
    MatExpansionModule
  ],
  entryComponents: [ModalAjouterCompteComponent, ModalModifierCompteComponent],
  providers: [OutilService, ConnexionService, CompteService, TypeCompteService],
  bootstrap: [AppComponent]
})
export class AppModule { }
