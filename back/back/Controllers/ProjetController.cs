using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjetController : Controller
    {
        private backlogContext context;

        public ProjetController(backlogContext _context)
        {
            DB_Projet.context = context = _context;
        }

        [HttpGet("lister")]
        public string Lister()
        {
            try
            {
                var liste = DB_Projet.Lister();

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpGet("lister/{idCompteClient}")]
        public string Lister([FromRoute] int idCompteClient)
        {
            try
            {
                var liste = DB_Projet.Lister(idCompteClient);

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpGet("lister2/{idCompteDev}")]
        public string Lister2([FromRoute] int idCompteDev)
        {
            try
            {
                var liste = DB_Projet.ListerProjetDuDev(idCompteDev);

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
        /// <param name="_projet"></param>
        /// <response code="200">id du compte ajouter</response>
        /// <response code="400">0</response>
        [HttpPost("ajouter")]
        public string Ajouter([FromBody] Import_Projet _projet)
        {
            try
            {
                Projet projet = new()
                {
                    Nom = Outil.ProtectionXSS(_projet.Nom),
                    Description = Outil.ProtectionXSS(_projet.Description),
                    IdCompteClient = _projet.IdCompteClient
                };

                int id = DB_Projet.Ajouter(projet);

                // ajouter les gens au projet
                DB_ProjetCompte.context = context;
                DB_ProjetCompte.Ajouter(_projet.listeCompte, id);

                return JsonConvert.SerializeObject(id);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(0);
            }
        }
    }
}
