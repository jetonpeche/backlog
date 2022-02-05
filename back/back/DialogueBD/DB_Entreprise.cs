namespace back.DialogueBD;

public static class DB_Entreprise
{
    public static backlogContext context;

    public static IQueryable Lister()
    {
        return context.Entreprises.OrderBy(e => e.Nom).Select(e => new { e.Id, e.Nom });
    }

    public static int Ajouter(Entreprise _entreprise)
    {
        context.Entreprises.Add(_entreprise);
        context.SaveChanges();

        return context.Entreprises.OrderByDescending(e => e.Id).First().Id;
    }
}

