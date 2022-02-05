using BC = BCrypt.Net.BCrypt;

namespace back.DialogueBD;

public static class DB_Connexion
{
    public static backlogContext context;

    public static bool Connexion(string _login, string _mdp)
    {
        var compte = context.Comptes.Where(c => c.Mail == _login).Select(c => new { c.Mdp, c.Mail }).FirstOrDefault();

        if(compte == null || !BC.Verify(_mdp, compte.Mdp))
            return false;

        return true;
    }
}

