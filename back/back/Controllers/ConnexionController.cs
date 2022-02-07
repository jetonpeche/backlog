namespace back.Controllers;

[Route("[controller]")]
[ApiController]
public class ConnexionController : ControllerBase
{
    private readonly backlogContext context;

    public ConnexionController(backlogContext _context)
    {
        context = _context;
        DB_Connexion.context = _context;
    }

    [HttpPost("connexion")]
    public string Connexion([FromBody] Import_Connexion _logs)
    {
        try
        {
            int reponse = DB_Connexion.Connexion(Outil.ProtectionXSS(_logs.Login), Outil.ProtectionXSS(_logs.Mdp));

            if (reponse != 0)
            {
                DB_Compte.context = context;

                return JsonConvert.SerializeObject(DB_Compte.Compte(reponse));
            }
            else
            {
                return JsonConvert.SerializeObject(false);
            }
        }
        catch (Exception e)
        {
            return JsonConvert.SerializeObject(e);
        }

    }

}

