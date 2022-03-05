namespace back.DialogueBD
{
    public static class DB_ProjetTache
    {
        public static backlogContext context;

        public static IQueryable ListerTache(int _idProjet)
        {
            var liste = from t in context.ProjetTaches
                        where t.IdProjet == _idProjet
                        select new
                        {
                            t.Id,
                            t.Description,

                            t.IdStatusTache,
                            NomStausTache = t.IdStatusTacheNavigation.Nom
                        };

            return liste;
        }

        public static dynamic Ajouter(ProjetTache _tache)
        {
            context.ProjetTaches.Add(_tache);
            context.SaveChanges();

            var tache = context.ProjetTaches
                .OrderByDescending(t => t.Id)
                .Select(t => 
                new 
                {
                    t.Id,
                    t.Description, 
                    t.IdStatusTache,
                    NomStausTache = t.IdStatusTacheNavigation.Nom
                }).First();

            return tache;
        }

        public static void Modifier(ProjetTache _tache)
        {
            context.ProjetTaches.Update(_tache);
            context.SaveChanges();
        }
    }
}
