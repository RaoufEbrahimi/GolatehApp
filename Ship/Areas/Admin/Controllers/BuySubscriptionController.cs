using Ship.Model.Db;
using Ship.Model.ViewModel;

using System.Linq;
using System.Web.Mvc;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class BuySubscriptionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuyToPay(int id)
        {
            return View(id);
        }





        [HttpPost]
        public JsonResult PayOnline(long OrderID)
        {
            var Q = new GolatehEntities();

            var order = Q.Order.First(v => v.IdShomareOrder == OrderID);

  

            string[] _Host = Request.Url.Host.Split(new char[] { '.' });

      

            System.Net.ServicePointManager.Expect100Continue = false;
          
      

            return Json("");
        }


    }
}