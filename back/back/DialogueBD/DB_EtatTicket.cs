namespace back.DialogueBD
{
    public class DB_EtatTicket
    {
        public static backlogContext context;

        public static IQueryable Lister()
        {
            return context.EtatTickets.OrderBy(o => o.Nom).Select(o => new { o.Id, o.Nom });
        }

        public static int Ajouter(EtatTicket _type)
        {
            context.EtatTickets.Add(_type);
            context.SaveChanges();

            return context.EtatTickets.OrderByDescending(o => o.Id).First().Id;
        }

        public static void Modifier(EtatTicket _type)
        {
            context.EtatTickets.Update(_type);
            context.SaveChanges();
        }
    }
}
