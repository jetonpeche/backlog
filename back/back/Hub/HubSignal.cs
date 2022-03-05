using Microsoft.AspNetCore.SignalR;

namespace back.signal
{
    public class HubSignal: Hub
    {
        public async Task ListerTache(int _idProjet)
        {
            DB_ProjetTache.context = GetContext();
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

            DB_ProjetTache.context = GetContext();
            var tacheRetour = DB_ProjetTache.Ajouter(tache);
            string tacheRetourString = JsonConvert.SerializeObject(tacheRetour);

            await Clients.Group($"projet{_tache.IdProjet}").SendAsync("reponseNouvelleTache", tacheRetourString);
        }

        public async Task ModifierEtatTache(Import_TacheStatusModif _tache)
        {
            ProjetTache tache = new()
            {
                Id = _tache.Id,
                IdStatusTache = _tache.IdStatusTache,
                IdProjet = _tache.IdProjet,
                Description = Outil.ProtectionXSS(_tache.Description)
            };

            DB_ProjetTache.context = GetContext();
            DB_ProjetTache.Modifier(tache);
            string tacheRetourString = JsonConvert.SerializeObject(new { IdStatusTache = _tache.IdStatusTache, Id = _tache.Id });

            await Clients.GroupExcept($"projet{_tache.IdProjet}", Context.ConnectionId).SendAsync("reponseDemanderModifTache", tacheRetourString);
        }

        public async void QuitterGrpProjet(int _idProjet)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"projet{_idProjet}");
        }

        private async void RejoindreGrpProjet(int _idProjet)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"projet{_idProjet}");
        }

        private backlogContext GetContext()
        {
            return new backlogContext();
        }
    }
}
