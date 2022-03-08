using Microsoft.AspNetCore.SignalR;

namespace back.signal
{
    public class HubSignal: Hub
    {
        public HubSignal(backlogContext _context)
        {
            DB_ProjetTache.context = _context;
        }

        public async Task ListerTache(int _idProjet)
        {
            var liste = DB_ProjetTache.ListerTache(_idProjet);

            RejoindreGrpProjet(_idProjet);

            await Clients.Caller.SendAsync("reponseListeTache", JsonConvert.SerializeObject(liste));
        }

        public async Task AjouterTache(Import_Tache _tache)
        {
            ProjetTache tache = new()
            {
                Description = Outil.ProtectionXSS(_tache.Description),
                IdProjet = _tache.IdProjet,
                IdStatusTache = 1
            };

            var tacheRetour = DB_ProjetTache.Ajouter(tache);
            string tacheRetourString = JsonConvert.SerializeObject(tacheRetour);

            // envoie a l'emetteur
            await Clients.Caller.SendAsync("reponseNouvelleTacheExpediteur", tacheRetourString);

            // envoie au groupe sauf l'emetteur
            await Clients.GroupExcept($"projet{_tache.IdProjet}", Context.ConnectionId).SendAsync("reponseNouvelleTache", tacheRetourString);
        }

        public async Task ModifierEtatTache(Import_TacheModif _tache)
        {
            ProjetTache tache = new()
            {
                Id = _tache.Id,
                IdStatusTache = _tache.IdStatusTache,
                IdProjet = _tache.IdProjet,
                Description = Outil.ProtectionXSS(_tache.Description)
            };

            dynamic info = DB_ProjetTache.Modifier(tache);
            string tacheRetourString = JsonConvert.SerializeObject(
                new 
                { 
                    IdStatusTache = _tache.IdStatusTache, 
                    Id = _tache.Id, 
                    CouleurFontStatusTache = info.CouleurFont, 
                    NomStatusTache = info.Nom 
                });

            await Clients.GroupExcept($"projet{_tache.IdProjet}", Context.ConnectionId).SendAsync("reponseDemanderModifTache", tacheRetourString);
        }

        public async Task ModifierDescriptionTache(Import_TacheModif _tache)
        {
            ProjetTache tache = new()
            {
                Id = _tache.Id,
                IdStatusTache = _tache.IdStatusTache,
                IdProjet = _tache.IdProjet,
                Description = Outil.ProtectionXSS(_tache.Description)
            };

            DB_ProjetTache.Modifier(tache);
            string retour = JsonConvert.SerializeObject(
                new
                {
                    Id = _tache.Id,
                    Description = _tache.Description
                });

            await Clients.GroupExcept($"projet{_tache.IdProjet}", Context.ConnectionId).SendAsync("reponseModifDescTache", retour);
        }

        public async Task SupprimerTache(ProjetTache _tache)
        {
            DB_ProjetTache.Supprimer(_tache.Id);
            string idTache = JsonConvert.SerializeObject(new { Id = _tache.Id });

            await Clients.Caller.SendAsync("reponseSupprimerTacheExpediteur", idTache);

            await Clients.GroupExcept($"projet{_tache.IdProjet}", Context.ConnectionId).SendAsync("reponseSupprimerTache", idTache);
        }

        public async void QuitterGrpProjet(int _idProjet)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"projet{_idProjet}");
        }

        private async void RejoindreGrpProjet(int _idProjet)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"projet{_idProjet}");
        }
    }
}
