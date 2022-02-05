namespace back.DialogueBD
{
    public static class DB_TypeRetour
    {
        public static backlogContext context;

        public static IQueryable Lister()
        {
            return context.TypeRetours.OrderBy(o => o.Nom).Select(o => new { o.Id, o.Nom });
        }

        public static int Ajouter(TypeRetour _type)
        {
            context.TypeRetours.Add(_type);
            context.SaveChanges();

            return context.TypeComptes.OrderByDescending(o => o.Id).First().Id;
        }

        public static void Modifier(TypeRetour _type)
        {
            context.TypeRetours.Update(_type);
            context.SaveChanges();
        }
    }
}
