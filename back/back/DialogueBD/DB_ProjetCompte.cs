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
    }
}
