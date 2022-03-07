using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TacheProjetController : Controller
    {
        public TacheProjetController(backlogContext _context)
        {
            DB_ProjetTache.context = _context;
        }

        [HttpGet("lister/{idProjet}")]
        public string Lister([FromRoute] int idProjet)
        {
            try
            {
                var liste = DB_ProjetTache.ListerTache(idProjet);

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        [HttpGet("DerniereTacheAjouter/{idProjet}")]
        public string RecupererDerniereTacheAjouter([FromRoute] int idProjet)
        {
            try
            {
                var tache = DB_ProjetTache.DerniereTacheAjouter(idProjet);

                return JsonConvert.SerializeObject(tache);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }
    }
}
