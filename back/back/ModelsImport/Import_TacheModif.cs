namespace back.ModelsImport
{
    public class Import_TacheModif
    {
        public int Id { get; set; }
        public int IdProjet { get; set; }
        public string Description { get; set; } = null!;
        public int IdStatusTache { get; set; }
    }
}
