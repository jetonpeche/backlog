import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CanalSignalR } from 'src/app/enums/CanalSignalR';
import { OutilService } from 'src/app/service/outil.service';
import { SignalService } from 'src/app/service/signal.service';
import { StatusTacheService } from 'src/app/service/status-tache.service';
import { StatusTache } from 'src/app/types/StatusTache';
import { Tache } from 'src/app/types/Tache';
import { ModalAjouterTacheComponent } from '../../dev/modal-ajouter-tache/modal-ajouter-tache.component';
import { ModalModifierTacheComponent } from '../modal-modifier-tache/modal-modifier-tache.component';

@Component({
  selector: 'app-modal-voir-tache-projet',
  templateUrl: './modal-voir-tache-projet.component.html',
  styleUrls: ['./modal-voir-tache-projet.component.scss']
})
export class ModalVoirTacheProjetComponent implements OnInit, OnDestroy
{
  listeTache: Tache[] = [];
  listeStatusTache: StatusTache[] = [];
  idProjet: number;

  private listeTacheClone: Tache[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any, 
    private dialog: MatDialog, 
    private signalrServ: SignalService,
    private outilServ: OutilService,
    private statusTache: StatusTacheService) { }

  ngOnInit(): void 
  {
    this.idProjet = this.data.idProjet;
    this.signalrServ.DemanderListeTache(this.idProjet);

    this.ListerStatusTache();

    this.ReponseServeurSupprimerExpediteur();
    this.ReponseServeurSupprimer();
    this.ReponseServeurListeTache();

    this.ReponseServeurNouvelleTache_Expediteur();
    this.ReponseServeurNouvelleTache();

    this.ReponseServeurModifEtat();
  }

  Filtrer(_idStatusTache: number): void
  {
    this.listeTache = this.listeTacheClone.filter(t => t.IdStatusTache == _idStatusTache);
  }

  Rechercher(_recherche: string): void
  {
    if(_recherche == "")
      this.listeTache = this.listeTacheClone;
    else
    {
      this.listeTache = this.listeTacheClone.filter(
        (t) => 
        {
          return t.Description.toLowerCase().includes(_recherche.toLowerCase()) || 
                 t.NomStatusTache.toLowerCase().includes(_recherche.toLowerCase());
        });
    }
  }

  ConfirmerSupprimerTache(_tache: Tache): void
  {
    const TITRE = "Confirmation suppression tache";
    const TEXTE = "Veuillez confirmer pour supprimer la tache choisie";

    this.outilServ.ModalConfirmation(TITRE, TEXTE);

    this.outilServ.sujet.subscribe({
      next: (retour: boolean) =>
      {
        if(retour == true)
        {
          const TACHE = 
          { 
            Id: _tache.Id, 
            Description: _tache.Description, 
            IdProjet: this.idProjet, 
            IdStatusTache: _tache.IdStatusTache 
          };

          this.signalrServ.DemandeSuppTache(TACHE);
        }
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }

  OuvrirModalAjouterTache(): void
  {
    this.dialog.open(ModalAjouterTacheComponent, { width: "50%", data: { idProjet: this.idProjet }});
  }

  OuvrirModalModifierTache(_tache: Tache): void
  {
    const DIALOG_REF = this.dialog.open(ModalModifierTacheComponent, { width: "70%", data: { tache: _tache, idProjet: this.idProjet }});

    DIALOG_REF.afterClosed().subscribe({
      next: (descriptionModif: string) =>
      {
        if(descriptionModif)
          _tache.Description = descriptionModif;
      }
    });
  }

  private ListerStatusTache(): void
  {
    this.statusTache.Lister().subscribe({
      next: (liste) =>
      {
        this.listeStatusTache = liste;
      },
      error: () =>
      {
        this.outilServ.ToastErreurHttp();
      }
    })
  }

  private ReponseServeurNouvelleTache_Expediteur(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_NOUVELLE_TACHE_EXPEDITEUR, (retour: string) =>
    {
      this.listeTache.push(JSON.parse(retour));
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

  private ReponseServeurListeTache(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_LISTE_TACHE, (retour: string) => 
    {
      this.listeTache = this.listeTacheClone = JSON.parse(retour);
    });
  }

  private ReponseServeurSupprimerExpediteur(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_SUPP_TACHE_EXPEDITEUR, (retour: string) =>
    {
      const ID_TACHE: number = JSON.parse(retour).Id;

      const INDEX = this.listeTache.findIndex(t => t.Id == ID_TACHE);
      this.listeTache.splice(INDEX, 1);

      this.outilServ.ToastSucces("La tache a été supprimée");
    });
  }

  private ReponseServeurSupprimer(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_SUPP_TACHE, (retour: string) =>
    {
      const ID_TACHE: number = JSON.parse(retour).Id;

      const INDEX = this.listeTache.findIndex(t => t.Id == ID_TACHE);
      this.listeTache.splice(INDEX, 1);

      this.outilServ.ToastInfo("Une tache a été supprimée");
    });
  }

  private ReponseServeurModifEtat(): void
  {
    this.signalrServ.hubConnexion.on(CanalSignalR.REPONSE_MODIF_STATUT_TACHE, (retour: string) =>
    {
      const INFO_TACHE = JSON.parse(retour);

      let tache = this.listeTache.find(t => t.Id == INFO_TACHE.Id);
      tache.IdStatusTache = INFO_TACHE.IdStatusTache;
      tache.CouleurFontStatusTache = INFO_TACHE.CouleurFontStatusTache;
      tache.NomStatusTache = INFO_TACHE.NomStatusTache;
    });
  }

  ngOnDestroy(): void 
  {
    this.signalrServ.DemanderQuitterGrpProjet(this.idProjet);
    
    // se desabonner des reponses serveur
    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_SUPP_TACHE_EXPEDITEUR);
    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_SUPP_TACHE);

    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_NOUVELLE_TACHE_EXPEDITEUR);
    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_NOUVELLE_TACHE);

    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_LISTE_TACHE);

    this.signalrServ.hubConnexion.off(CanalSignalR.REPONSE_MODIF_STATUT_TACHE);
  }
}
