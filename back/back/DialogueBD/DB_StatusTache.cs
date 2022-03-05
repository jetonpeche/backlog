namespace back.DialogueBD
{
    public static class DB_StatusTache
    {
        public static backlogContext context;

        public static IQueryable Lister()
        {
            return context.StatusTaches.Select(t => new { t.Id, t.Nom, t.CouleurFont });
        }

        public static int Ajouter()
        {
            return 1;
        }
    }
}
