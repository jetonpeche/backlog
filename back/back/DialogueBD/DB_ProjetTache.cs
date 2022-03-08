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
                            NomStatusTache = t.IdStatusTacheNavigation.Nom,
                            CouleurFontStatusTache = t.IdStatusTacheNavigation.CouleurFont
                        };

            return liste;
        }

        public static dynamic DerniereTacheAjouter(int _idProjet)
        {
            var tache = (from t in context.ProjetTaches
                        where t.IdProjet == _idProjet
                        orderby t.Id descending
                        select new
                        {
                            t.Id,
                            t.Description,
                            t.IdStatusTache,
                            NomStatusTache = t.IdStatusTacheNavigation.Nom
                        }).First();

            return tache;
        }

        public static dynamic Ajouter(ProjetTache _tache)
        {
            context.ProjetTaches.Add(_tache);
            context.SaveChanges();

            var tache = (from t in context.ProjetTaches
                         where t.IdProjet == _tache.IdProjet
                         orderby t.Id descending
                         select new
                         {
                             t.Id,
                             t.Description,
                             t.IdStatusTache,
                             NomStausTache = t.IdStatusTacheNavigation.Nom
                         }).First();

            return tache;
        }

        /// <summary>
        /// Modifier une tache
        /// </summary>
        /// <param name="_tache"></param>
        /// <returns>CouleurFont, Nom (status)</returns>
        public static dynamic Modifier(ProjetTache _tache)
        {
            context.ProjetTaches.Update(_tache);
            context.SaveChanges();

            var info = (from p in context.ProjetTaches
                        where p.Id == _tache.Id
                        select new
                        {
                            p.IdStatusTacheNavigation.CouleurFont,
                            p.IdStatusTacheNavigation.Nom
                        }).First();

            return info;
        }

        public static void Supprimer(int _idTache)
        {
            ProjetTache tache = context.ProjetTaches.Where(p => p.Id == _idTache).First();

            context.ProjetTaches.Remove(tache);
            context.SaveChanges();
        }
    }
}
