import { AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Projet } from 'src/app/types/Projet';
import { ModalAjouterProjetComponent } from 'src/app/modal/admin/modal-ajouter-projet/modal-ajouter-projet.component';
import { OutilService } from 'src/app/service/outil.service';
import { ProjetService } from 'src/app/service/projet.service';
import { ModalVoirTacheProjetComponent } from 'src/app/modal/admin/modal-voir-tache-projet/modal-voir-tache-projet.component';
import { ModalModifierProjetComponent } from 'src/app/modal/admin/modal-modifier-projet/modal-modifier-projet.component';

@Component({
  selector: 'app-gestion-projet',
  templateUrl: './gestion-projet.component.html',
  styleUrls: ['./gestion-projet.component.scss']
})
export class GestionProjetComponent implements OnInit, AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = ['Nom', 'Description', 'projet', 'modifier', 'supprimer'];
  listeProjet: MatTableDataSource<Projet>;

  constructor(
    private projetServ: ProjetService, 
    private outilServ: OutilService,
    private dialog: MatDialog) { }

  ngOnInit(): void 
  {
    this.listeProjet = new MatTableDataSource();
    this.ListerProjet();
  }

  ngAfterViewInit(): void
  {
    this.paginator._intl.itemsPerPageLabel = "Ligne par page";
    this.listeProjet.paginator = this.paginator;
    this.listeProjet.sort = this.sort;
  }

  applyFilter(event: Event): void
  {
    const filterValue = (event.target as HTMLInputElement).value;
    this.listeProjet.filter = filterValue.trim().toLowerCase();

    if (this.listeProjet.paginator)
      this.listeProjet.paginator.firstPage();
  }

  AfficherDescriptionCouper(_texte: string): string
  {
    return _texte.split(" ", 15).join(" ");
  }

  OuvrirModalCreerProjet(): void
  {
    const MODAL = this.dialog.open(ModalAjouterProjetComponent, { width: "50%", minHeight: "80%" });

    MODAL.beforeClosed().subscribe({
      next: (retour: Projet) =>
      {
        if(MODAL.componentInstance.estAjouter)
        {
          this.listeProjet.data.push(retour);
          this.listeProjet.data = this.listeProjet.data;
        }
      }
    });
  }

  OuvrirModalTacheProjet(_idProjet: number): void
  {
    this.dialog.open(ModalVoirTacheProjetComponent, { data: { idProjet: _idProjet }});
  }

  OuvrirModalModifierProjet(_projet: Projet): void
  {
    const DIALOG_REF = this.dialog.open(ModalModifierProjetComponent, { width: "80%", data: { projet: _projet }});

    DIALOG_REF.afterClosed().subscribe({
      next: (projetModif) =>
      {
        if(projetModif)
        { 
          _projet.Description = projetModif.Description;
          _projet.Nom = projetModif.Nom;
  
          _projet.Client.Adresse = projetModif.Adresse;
          _projet.Client.Mail = projetModif.Mail;
          _projet.Client.Tel = projetModif.Tel;
          _projet.Client.NomEntreprise = projetModif.NomEntreprise;
          _projet.Client.Nom = projetModif.NomClient;
          _projet.Client.Prenom = projetModif.PrenomClient;
        }
      }
    });
  }

  ConfirmerSupprimerProjet(_idProjet: number, _nomProjet: string): void
  {
    const TITRE = `Confirmation suppression du projet: ${_nomProjet}`;
    const TEXTE = `Attention vous etes sur le point de supprimer le projet: ${_nomProjet}, \n veuillez confirmer`;

    this.outilServ.ModalConfirmation(TITRE, TEXTE);
   
    this.outilServ.sujet.subscribe({
      next: (retour: boolean) =>
      {
        if(retour == true)
        {
          this.Supprimer(_idProjet, _nomProjet);
        }
      }
    });
  }

  private ListerProjet(): void
  {
    this.projetServ.Lister().subscribe({
      next: (liste: Projet[]) =>
      {
        this.listeProjet.data = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }

  private Supprimer(_idProjet: number, _nomProjet: string): void
  {
    this.projetServ.Supprimer(_idProjet).subscribe({
      next: (confirmationRetour: boolean) =>
      {
        if(confirmationRetour == true)
        {
          const INDEX = this.listeProjet.data.findIndex(p => p.Id == _idProjet);
          this.listeProjet.data.splice(INDEX, 1);

          this.listeProjet.data = this.listeProjet.data;

          this.outilServ.ToastSucces(`Le projet: ${_nomProjet} et toutes ses d??pendances ont ??t?? supprim??s`);
        }
        else
          this.outilServ.ToastErreur(`Echec de la suppression du projet: ${_nomProjet}`);
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }
}
