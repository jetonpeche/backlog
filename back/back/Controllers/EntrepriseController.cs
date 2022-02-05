namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EntrepriseController : Controller
    {
        public EntrepriseController(backlogContext _context)
        {
            DB_Entreprise.context = _context;
        }

        [HttpGet("lister")]
        public string Lister()
        {
            try
            {
                var liste = DB_Entreprise.Lister();
                
                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpPost("ajouter")]
        public string Ajouter([FromBody] Import_Type _type)
        {
            try
            {
                Entreprise entreprise = new()
                {
                    Nom = Outil.ProtectionXSS(_type.Nom)
                };

                int id = DB_Entreprise.Ajouter(entreprise);

                return JsonConvert.SerializeObject(id);
            }
            catch (Exception e)
            {

                return JsonConvert.SerializeObject(e);
            }
        }
    }
}
