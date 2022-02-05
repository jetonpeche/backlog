namespace back.Controllers;

[Route("[controller]")]
[ApiController]
public class ConnexionController : ControllerBase
{
    public ConnexionController(backlogContext _context)
    {
        DB_Connexion.context = _context;
    }

    [HttpPost("connexion")]
    public string Connexion([FromBody] Import_Connexion _logs)
    {
  bool reponse = DB_Connexion.Connexion(Outil.ProtectionXSS(_logs.Login), Outil.ProtectionXSS(_logs.Mdp));

            return JsonConvert.SerializeObject(reponse);

    }

}

