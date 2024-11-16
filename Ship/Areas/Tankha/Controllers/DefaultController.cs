using Ship.Model.Db;
using System.Linq;
using System.Web.Mvc;

namespace Ship.Areas.Tankha.Controllers
{
    [Authorize(Roles = "TankhaGardan")]
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {

            var db = new GolatehEntities();
            
            return View();
        }         
    }
}