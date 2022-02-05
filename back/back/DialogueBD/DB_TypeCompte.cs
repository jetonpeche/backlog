namespace back.DialogueBD
{
    public static class DB_TypeCompte
    {
        public static backlogContext context;

        public static IQueryable Lister()
        {
            return context.TypeComptes.OrderBy(o => o.Nom).Select(e => new { e.Id, e.Nom });
        }

        public static int Ajouter(TypeCompte _type)
        {
            context.TypeComptes.Add(_type);
            context.SaveChanges();

            return context.TypeComptes.OrderByDescending(o => o.Id).First().Id;
        }

        public static void Modifier(TypeCompte _type)
        {
            context.TypeComptes.Update(_type);
            context.SaveChanges();
        }
    }
}
