import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Compte } from 'src/app/classes/Compte';
import { ModalAjouterCompteComponent } from 'src/app/modal/admin/modal-ajouter-compte/modal-ajouter-compte.component';
import { CompteService } from 'src/app/service/compte.service';
import { OutilService } from 'src/app/service/outil.service';

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
  listeCompte: MatTableDataSource<Compte>;

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

  private ListerCompte(): void
  {
    this.compteServ.Lister().subscribe({
      next: (liste: Compte[]) =>
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
