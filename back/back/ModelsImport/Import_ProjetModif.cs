namespace back.ModelsImport
{
    public class Import_ProjetModif
    {
        public int Id { get; set; }
        public int IdStatutProjet { get; set; }
        public string Nom { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int IdClient { get; set; }
        public string NomClient { get; set; } = null!;
        public string PrenomClient { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string Adresse { get; set; } = null!;
        public string NomEntreprise { get; set; } = null!;

        public int[] listeIdCompteAjout { get; set; } = null!;
        public int[] listeIdCompteSupp { get; set; } = null!;
    }
}
