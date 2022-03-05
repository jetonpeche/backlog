namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusTacheController : Controller
    {
        public StatusTacheController(backlogContext _context)
        {
            DB_StatusTache.context = _context;
        }

        [HttpGet("lister")]
        public string Lister()
        {
            try
            {
                var liste = DB_StatusTache.Lister();

                return JsonConvert.SerializeObject(liste);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }
    }
}
