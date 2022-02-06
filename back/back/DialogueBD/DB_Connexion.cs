using BC = BCrypt.Net.BCrypt;

namespace back.DialogueBD;

public static class DB_Connexion
{
    public static backlogContext context;

    /// <summary>
    /// Verifie que les logs sont corrects
    /// </summary>
    /// <param name="_login"></param>
    /// <param name="_mdp"></param>
    /// <returns>0 si rien trouvé sinon ID du compte</returns>
    public static int Connexion(string _login, string _mdp)
    {
        var compte = context.Comptes.Where(c => c.Mail == _login).Select(c => new { c.Mdp, c.Mail, c.Id }).FirstOrDefault();

        if(compte == null || !BC.Verify(_mdp, compte.Mdp))
            return 0;

        return compte.Id;
    }
}

