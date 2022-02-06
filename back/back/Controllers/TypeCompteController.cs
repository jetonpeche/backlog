namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeCompteController : Controller
    {
        public TypeCompteController(backlogContext _context)
        {
            DB_TypeCompte.context = _context;
        }

        [HttpGet("lister")]
        public string Lister()
        {
            try
            {
                var liste = DB_TypeCompte.Lister();

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpPost("Ajouter")]
        public string Ajouter([FromBody] Import_Type _type)
        {
            try
            {
                TypeCompte typeCompte = new()
                {
                    Nom = Outil.ProtectionXSS(_type.Nom)
                };

                var liste = DB_TypeCompte.Ajouter(typeCompte);

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpPut("Modifier")]
        public string Modifier([FromBody] TypeCompte _type)
        {
            try
            {
                _type.Nom = Outil.ProtectionXSS(_type.Nom);

                DB_TypeCompte.Modifier(_type);

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }
    }
}
