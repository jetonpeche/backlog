namespace back.DialogueBD
{
    public static class DB_ProjetCompte
    {
        public static backlogContext context;

        public static void Ajouter(int[] _listeIdCompte, int _idProjet)
        {
            if(_listeIdCompte.Length == 0)
                return;

            foreach (int element in _listeIdCompte)
            {
                context.ProjetComptes.Add(new() { IdCompte = element, IdProjet = _idProjet });
            }

            context.SaveChanges();
        }

        public static void Modifier(Compte _compte, Projet _projet)
        {
            dynamic compte = context.Comptes.Where(c => c.Id == _compte.Id).Select(c => new { c.Mdp, c.IdTypeCompte }).First();

            _compte.Mdp = compte.Mdp;
            _compte.IdTypeCompte = compte.IdTypeCompte;

            _projet.IdStatus = context.Projets.Where(p => p.Id == _projet.Id).Select(p => p.IdStatus).First();

            context.Comptes.Update(_compte);
            context.Projets.Update(_projet);

            context.SaveChanges();
        }

        public static void Supprimer(int[] _listeIdCompte, int _idProjet)
        {
            List<ProjetCompte> liste = new List<ProjetCompte>();

            foreach (int id in _listeIdCompte)
            {
                liste.Add(new() { IdCompte = id, IdProjet = _idProjet });
            }

            context.ProjetComptes.RemoveRange(liste);

            context.SaveChanges();
        }
    }
}
