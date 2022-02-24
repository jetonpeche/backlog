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

    [HttpGet("listerDev")]
    public string ListerDev()
    {
        try
        {
            var liste = DB_Compte.ListerCompteDev();

            return JsonConvert.SerializeObject(liste);
        }
        catch (Exception e)
        {
            return JsonConvert.SerializeObject(e);
        }
    }

    [HttpGet("listerClient")]
    public string ListerClient()
    {
        try
        {
            var liste = DB_Compte.ListerCompteClient();

            return JsonConvert.SerializeObject(liste);
        }
        catch (Exception e)
        {

            return JsonConvert.SerializeObject(e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_compte"></param>
    /// <response code="200">id du compte ajouter</response>
    /// <response code="400">0</response>
    [HttpPost("ajouter")]
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
                NomEntreprise = Outil.ProtectionXSS(_compte?.NomEntreprise),
                Adresse = Outil.ProtectionXSS(_compte?.Adresse),
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

    /// <summary>
    /// Modifier le compte
    /// </summary>
    /// <param name="_compte"></param>
    /// <param name="id"></param>
    /// <returns>Renvoie true ou false</returns>
    /// <response code="200">false</response>
    /// <response code="400">false</response>
    [HttpPut("modifier/{id}")]
    public string Modifier([FromBody] Import_Compte _compte, [FromRoute] int id)
    {
        try
        {
            Compte compte = new()
            {
                Id = id,
                Nom = Outil.ProtectionXSS(_compte.Nom),
                Prenom = Outil.ProtectionXSS(_compte.Prenom),
                Mail = Outil.ProtectionXSS(_compte.Mail),
                NomEntreprise = Outil.ProtectionXSS(_compte?.NomEntreprise),
                Adresse = Outil.ProtectionXSS(_compte?.Adresse),
                Tel = Outil.ProtectionXSS(_compte.Tel),
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

