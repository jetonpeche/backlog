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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_projet"></param>
        /// <response code="200">true</response>
        /// <response code="400">false</response>
        /// <returns></returns>
        [HttpPut("modifier")]
        public string Modifier([FromBody] Import_ProjetModif _projet)
        {
            try
            {
                Compte compte = new()
                {
                    Id = _projet.IdClient,
                    Nom = _projet.NomClient,
                    Prenom = _projet.PrenomClient,
                    NomEntreprise = _projet.NomEntreprise,
                    Mail = _projet.Mail,
                    Tel = _projet.Tel,
                    Adresse = _projet.Adresse
                };

                Projet projet = new()
                {
                    Id = _projet.Id,
                    Nom = _projet.Nom,
                    Description = _projet.Description,
                    IdCompteClient = _projet.IdClient
                };

                DB_ProjetCompte.context = context;
                DB_ProjetCompte.Modifier(compte, projet);
                DB_ProjetCompte.Ajouter(_projet.listeIdCompteAjout, _projet.Id);
                DB_ProjetCompte.Supprimer(_projet.listeIdCompteSupp, _projet.Id);

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        /// <summary>
        /// Supprime tout se qui est en rapport avec le projet
        /// </summary>
        /// <param name="idProjet"></param>
        /// <response code="200">true</response>
        /// <response code="400">false</response>
        [HttpDelete("supprimer/{idProjet}")]
        public string Supprimer([FromRoute] int idProjet)
        {
            DB_Projet.Supprimer(idProjet);

            return JsonConvert.SerializeObject(true);
        }
    }
}
