using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    public class TacheProjetController : Controller
    {
        public TacheProjetController(backlogContext _context)
        {
            DB_ProjetTache.context = _context;
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
