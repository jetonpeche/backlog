import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CanalSignalR } from 'src/app/enums/CanalSignalR';
import { ModalAjouterTacheComponent } from 'src/app/modal/dev/modal-ajouter-tache/modal-ajouter-tache.component';
import { OutilService } from 'src/app/service/outil.service';
import { SignalService } from 'src/app/service/signal.service';
import { StatusTacheService } from 'src/app/service/status-tache.service';
import { StatusTache } from 'src/app/types/StatusTache';
import { Tache } from 'src/app/types/Tache';

@Component({
  selector: 'app-listing-tache',
  templateUrl: './listing-tache.component.html',
  styleUrls: ['./listing-tache.component.scss']
})
export class ListingTacheComponent implements OnInit, OnDestroy 
{
  idProjet: number;
  listeTache: Tache[] = [];
  listeStatusTache: StatusTache[] = [];

  constructor(
    private route: ActivatedRoute, 
    private dialog: MatDialog,
    private statusTacheServ: StatusTacheService, 
    private outilServ: OutilService, 
    private signalrServ: SignalService) { }

  ngOnInit(): void 
  {
    this.idProjet = +this.route.snapshot.params["id"];

    this.signalrServ.DemanderListeTache(this.idProjet);

    this.ReponseServeurDemanderModifTache();
    this.ReponseServeurModifDescriptionTache();
    this.ReponseServeurListeTache();

    this.ReponseServeurNouvelleTache();
    this.ReponseServeurNouvelleTache_Epediteur();

    this.ReponseServeurTacheSupprimer();

    this.ListerStatusTache();
  }

  OuvrirModalAjouterTache(): void
  {
    this.dialog.open(ModalAjouterTacheComponent, { width: "50%", data: { idProjet: this.idProjet }});
  }

  ChangerEtat(_idStatut: number, _tache: Tache): void
  {
    const TACHE_MODIFIER = { Id: _tache.Id, Description: _tache.Description, IdProjet: this.idProjet, IdStatusTache: _idStatut };

    this.signalrServ.DemanderModifEtatTache(TACHE_MODIFIER);

    _tache.IdStatusTache = _idStatut;
  }

  SetCouleurCarre(_idStatut: number): string
  {
    if(!_idStatut || _idStatut == 0)
    return "";

    const COULEUR_HEX = this.listeStatusTache.find(t => t.Id == _idStatut).CouleurFont;

    return COULEUR_HEX;
  }

  SetNomStatus(_idStatut: number): string
  {
    if(!_idStatut || _idStatut == 0)
    return "";

    const NOM = this.listeStatusTache.find(t => t.Id == _idStatut).Nom;

    return NOM;
  }

  private ReponseServeurDemanderModifTache(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_MODIF_STATUT_TACHE, (retour: string) =>
    {
      const INFO_TACHE = JSON.parse(retour);

      let tache = this.listeTache.find(t => t.Id == INFO_TACHE.Id);
      tache.IdStatusTache = INFO_TACHE.IdStatusTache;
    });
  }

  private ReponseServeurModifDescriptionTache(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_MODIF_DESC_TACHE, (retour: string) =>
    {
      const TACHE_RETOUR = JSON.parse(retour);

      let tache = this.listeTache.find(t => t.Id == TACHE_RETOUR.Id);

      tache.Description = TACHE_RETOUR.Description;
    });
  }

  private ReponseServeurListeTache(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_LISTE_TACHE, (retour: string) =>
    {
      this.listeTache = JSON.parse(retour);    
    });
  }

  private ReponseServeurNouvelleTache(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_NOUVELLE_TACHE, (retour: string) =>
    { 
      this.listeTache.push(JSON.parse(retour));
      this.outilServ.ToastInfo("Une nouvelle tache a été ajoutée");
    });
  }

  private ReponseServeurNouvelleTache_Epediteur(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_NOUVELLE_TACHE_EXPEDITEUR, (retour: string) =>
    {
      this.listeTache.push(JSON.parse(retour));
    });
  }

  private ReponseServeurTacheSupprimer(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_SUPP_TACHE, (retour: string) =>
    {
      const ID_TACHE: number = JSON.parse(retour).Id;

      const INDEX = this.listeTache.findIndex(t => t.Id == ID_TACHE);
      this.listeTache.splice(INDEX, 1);

      this.outilServ.ToastInfo("Une tache a été supprimée");
    });
  }

  private ListerStatusTache(): void
  {
    this.statusTacheServ.Lister().subscribe({
      next: (liste) =>
      {
        this.listeStatusTache = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    });
  }

  ngOnDestroy(): void 
  {   
    this.signalrServ.DemanderQuitterGrpProjet(this.idProjet);

    // fermer les reponses serveur pour le client
    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_MODIF_STATUT_TACHE);
    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_LISTE_TACHE);

    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_MODIF_DESC_TACHE);

    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_NOUVELLE_TACHE_EXPEDITEUR);
    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_NOUVELLE_TACHE);

    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_SUPP_TACHE);
  }
}
