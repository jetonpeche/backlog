namespace back.DialogueBD
{
    public static class DB_Projet
    {
        public static backlogContext context;

        public static IQueryable Lister()
        {
            return context.Projets.Select(p => new { p.Id, p.Nom, p.Description });
        }

        public static int Ajouter(Projet _projet)
        {
            context.Projets.Add(_projet);
            context.SaveChanges();

            return context.Projets.OrderByDescending(p => p.Id).First().Id;
        }
    }
}
