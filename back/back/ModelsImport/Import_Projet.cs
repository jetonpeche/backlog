namespace back.ModelsImport
{
    public class Import_Projet
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int[] listeCompte { get; set; } = null!;
    }
}
