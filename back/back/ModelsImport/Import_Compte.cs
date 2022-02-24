namespace back.ModelsImport;

public class Import_Compte
{
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string? Mdp { get; set; }
    public string Tel { get; set; } = null!;
    public string? Adresse { get; set; }
    public string? NomEntreprise { get; set; }
    public int IdTypeCompte { get; set; }
}

