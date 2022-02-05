namespace back.ModelsImport;

public class Import_Compte
{
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string Mdp { get; set; } = null!;
    public int IdEntreprise { get; set; }
    public int IdTypeCompte { get; set; }
}

