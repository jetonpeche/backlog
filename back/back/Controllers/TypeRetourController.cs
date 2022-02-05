using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeRetourController : Controller
    {
        public TypeRetourController(backlogContext _context)
        {
            DB_TypeRetour.context = _context;
        }


        /// <summary>
        ///  Liste les type de retour dispo indiqué sur les tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("lister")]
        public string Lister()
        {
            try
            {
                var liste = DB_TypeRetour.Lister();

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
        /// <param name="_type"></param>
        /// <returns>Renvoie l'id de l'ajout</returns>
        [HttpPost("Ajouter")]
        public string Ajouter([FromBody] Import_Type _type)
        {
            try
            {
                TypeRetour typeRetour = new()
                {
                    Nom = Outil.ProtectionXSS(_type.Nom)
                };

                var liste = DB_TypeRetour.Ajouter(typeRetour);

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
        /// <param name="_type"></param>
        /// <returns>Renvoie true si c'est OK</returns>
        [HttpPut("Modifier")]
        public string Modifier([FromBody] TypeRetour _type)
        {
            try
            {
                _type.Nom = Outil.ProtectionXSS(_type.Nom);

                var liste = DB_TypeRetour.Ajouter(_type);

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}
