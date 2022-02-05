namespace back.Controllers
{
    public class EtatTicketController : Controller
    {
        public EtatTicketController(backlogContext _context)
        {
            DB_TypeCompte.context = _context;
        }

        [HttpGet("lister")]
        public string Lister()
        {
            try
            {
                var liste = DB_EtatTicket.Lister();

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
                EtatTicket etatTicket = new()
                {
                    Nom = Outil.ProtectionXSS(_type.Nom)
                };

                var liste = DB_EtatTicket.Ajouter(etatTicket);

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpPut("Modifier")]
        public string Modifier([FromBody] EtatTicket _type)
        {
            try
            {
                _type.Nom = Outil.ProtectionXSS(_type.Nom);

                var liste = DB_EtatTicket.Ajouter(_type);

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}
