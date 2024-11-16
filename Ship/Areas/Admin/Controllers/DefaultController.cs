using Ship.Model.BLL.Admin;
using Ship.Model.ViewModel.Admin;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public JsonResult C1()
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.Dashboard1(IdUser);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C2()
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.Dashboard1(IdUser);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C3()
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.Dashboard1(IdUser);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C4(int Type)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard2(IdUser, Type);

            return Json(dash1);
        }


        [HttpPost]
        public JsonResult C5()
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 20, 0) as List<PrintDashboard4ViewModel>;



            List<long> tmp = new List<long>();

            tmp.AddRange(dash1.Select(b => b.A).ToArray());
            tmp.AddRange(dash1.Select(b => b.B).ToArray());
            tmp.AddRange(dash1.Select(b => b.C).ToArray());

            var ret = new
            {
                A = dash1.Select(b => b.A).ToArray(),
                B = dash1.Select(b => b.B).ToArray(),
                C = dash1.Select(b => b.C).ToArray(),
                D = dash1.Select(b => b.D).ToArray(),
                MinPrice = (tmp == null || tmp.Count == 0) ? 0 : tmp.Min()
            };
            return Json(ret);
        }


        [HttpPost]
        public JsonResult ReloadCC5(int index)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, index, 0) as List<PrintDashboard4ViewModel>;

            List<long> tmp = new List<long>();

            tmp.AddRange(dash1.Select(b => b.A).ToArray());
            tmp.AddRange(dash1.Select(b => b.B).ToArray());
            tmp.AddRange(dash1.Select(b => b.C).ToArray());

            var ret = new
            {
                A = dash1.Select(b => b.A).ToArray(),
                B = dash1.Select(b => b.B).ToArray(),
                C = dash1.Select(b => b.C).ToArray(),
                D = dash1.Select(b => b.D).ToArray(),
                MinPrice = (tmp == null || tmp.Count == 0) ? 0 : tmp.Min()
            };
            return Json(ret);
        }



        [HttpPost]
        public JsonResult ReloadCC56(int index, int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, index, IdShip) as List<PrintDashboard4ViewModel>;

            List<long> tmp = new List<long>();

            tmp.AddRange(dash1.Select(b => b.A).ToArray());
            tmp.AddRange(dash1.Select(b => b.B).ToArray());
            tmp.AddRange(dash1.Select(b => b.C).ToArray());

            var ret = new
            {
                A = dash1.Select(b => b.A).ToArray(),
                B = dash1.Select(b => b.B).ToArray(),
                C = dash1.Select(b => b.C).ToArray(),
                D = dash1.Select(b => b.D).ToArray(),
                MinPrice = (tmp == null || tmp.Count == 0) ? 0 : tmp.Min()
            };
            return Json(ret);
        }
    }
}