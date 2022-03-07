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
import { ModalConfirmationComponent } from 'src/app/modal/modal-confirmation/modal-confirmation.component';

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

  }

  SupprimerProjet(_idProjet: number, _nomProjet: string): void
  {
    const TITRE = `Confirmation suppression du projet: ${_nomProjet}`;
    const TEXTE = `Attention vous etes sur le point de supprimer le projet: ${_nomProjet}, \n veuillez confirmer`;

    this.outilServ.ModalConfirmation(TITRE, TEXTE);
   
    this.outilServ.sujet.subscribe({
      next: (retour: boolean) =>
      {
        if(retour)
        {
          
        }
      }
    })
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
}
