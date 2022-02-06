using BC = BCrypt.Net.BCrypt;

namespace back.Controllers;

[Route("[controller]")]
[ApiController]
public class CompteController : Controller
{
    public CompteController(backlogContext _context)
    {
        DB_Compte.context = _context;
    }

    [HttpGet("lister")]
    public string Lister()
    {
        try
        {
            var liste = DB_Compte.ListerCompte();

            return JsonConvert.SerializeObject(liste);
        }
        catch (Exception e)
        {
            return JsonConvert.SerializeObject(e);
        }
    }

    [HttpPost("Ajouter")]
    public string Ajouter([FromBody] Import_Compte _compte)
    {
        try
        {
            Compte compte = new()
            {
                Nom = Outil.ProtectionXSS(_compte.Nom),
                Prenom = Outil.ProtectionXSS(_compte.Prenom),
                Mail = Outil.ProtectionXSS(_compte.Mail),
                Mdp = Outil.ProtectionXSS(BC.HashPassword(_compte.Mdp)),
                NomEntreprise = Outil.ProtectionXSS(_compte.NomEntreprise),
                Tel = Outil.ProtectionXSS(_compte.Tel),
                IdTypeCompte = _compte.IdTypeCompte
            };

            int id = DB_Compte.CreerCompte(compte);

            return JsonConvert.SerializeObject(id);
        }
        catch (Exception)
        {
            return JsonConvert.SerializeObject(0);
        }
    }

    [HttpPut("Modifier/{id}")]
    public string Modifier([FromBody] Import_Compte _compte, int id)
    {
        try
        {
            Compte compte = new()
            {
                Id = id,
                Nom = Outil.ProtectionXSS(_compte.Nom),
                Prenom = Outil.ProtectionXSS(_compte.Prenom),
                Mail = Outil.ProtectionXSS(_compte.Mail),
                NomEntreprise = Outil.ProtectionXSS(_compte.NomEntreprise),
                Tel= Outil.ProtectionXSS(_compte.Tel),
                IdTypeCompte = _compte.IdTypeCompte
            };

            DB_Compte.Modifier(compte);

            return JsonConvert.SerializeObject(true);
        }
        catch (Exception)
        {
            return JsonConvert.SerializeObject(false);
        }
    }
}

