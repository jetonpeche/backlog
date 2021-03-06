import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { ModalAjouterCompteComponent } from 'src/app/modal/admin/modal-ajouter-compte/modal-ajouter-compte.component';
import { ModalModifierCompteComponent } from 'src/app/modal/admin/modal-modifier-compte/modal-modifier-compte.component';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';
import { CompteClient } from 'src/app/types/CompteClient';
import { TypeRole } from 'src/app/enums/TypeRole';
import { ModalVoirProjetAssocierComponent } from 'src/app/modal/admin/modal-voir-projet-associer/modal-voir-projet-associer.component';
import { Variable } from 'src/app/classeStatic/Variable';

@Component({
  selector: 'app-gestion-compte',
  templateUrl: './gestion-compte.component.html',
  styleUrls: ['./gestion-compte.component.scss']
})
export class GestionCompteComponent implements OnInit, AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = ['Nom', 'Prenom', 'Mail', 'NomEntreprise', 'TypeCompte', 'projet', 'modifier'];
  listeCompte: MatTableDataSource<CompteClient>;

  constructor(private compteServ: CompteService, private outilServ: OutilService, private dialog: MatDialog) { }

  ngOnInit(): void 
  {
    this.listeCompte = new MatTableDataSource();
    this.ListerCompte();
  }

  ngAfterViewInit(): void
  {
    this.paginator._intl.itemsPerPageLabel = "Ligne par page";
    this.listeCompte.paginator = this.paginator;
    this.listeCompte.sort = this.sort;
  }

  EstCompteDevOuClient(_idCompte): boolean
  {
    return this.outilServ.EstRoleDev(_idCompte) || this.outilServ.EstRoleClient(_idCompte);
  }

  applyFilter(event: Event): void
  {
    const filterValue = (event.target as HTMLInputElement).value;
    this.listeCompte.filter = filterValue.trim().toLowerCase();

    if (this.listeCompte.paginator)
      this.listeCompte.paginator.firstPage();
  }

  OuvrirModalAjouterCompte(): void
  {
    const DIALOG_REF = this.dialog.open(ModalAjouterCompteComponent);

    DIALOG_REF.afterClosed().subscribe({
      next: (retour) =>
      {
        if(retour)
        {       
          this.listeCompte.data.push(retour);
          this.listeCompte.data = this.listeCompte.data;
        }
      }
    });
  }

  OuvrirModalModifierCompte(_compte: CompteClient): void
  { 
    const DIALOG_REF = this.dialog.open(ModalModifierCompteComponent, { data: { compte: _compte }});

    DIALOG_REF.beforeClosed().subscribe({
      next: (retour: CompteClient) =>
      {       
        if(DIALOG_REF.componentInstance.estModifier)
        {        
          _compte.Nom = retour.Nom;
          _compte.NomEntreprise = retour?.NomEntreprise ?? "";
          _compte.Adresse = retour?.Adresse ?? "";
          _compte.Prenom = retour.Prenom;
          _compte.Mail = retour.Mail;
          _compte.Tel = retour.Tel;
          _compte.TypeCompte = retour.TypeCompte;
        }
      }
    });
  }

  OuvrirModalVoirProjetAssocier(_idCompte: number): void
  {
    this.dialog.open(ModalVoirProjetAssocierComponent, { data: { idCompte: _idCompte }});
  }

  private ListerCompte(): void
  {
    this.compteServ.Lister().subscribe({
      next: (liste: CompteClient[]) =>
      {
        this.listeCompte.data = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }
}
