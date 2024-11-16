using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Ship.Model.BLL.Admin;
using Ship.Model.ViewModel.Admin;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft.Json;
using Ship.Model.Extension;

namespace Ship.Areas.Admin.Controllers
{
    public class BimehController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DeleteBimeh(string Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var ttt = SiteSettingBLL.DeleteBimeh(IdUser, Id);

            return Json(ttt);
        }

        [HttpPost]
        public JsonResult SaveBimeh(string[] IdIdPersonnel, string Y, string M, int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var ttt = SiteSettingBLL.SaveBimeh(IdUser, IdIdPersonnel, Y, M, IdShip);

            return Json(ttt);
        }


        [HttpPost]
        public PartialViewResult _Add(string Id, int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var _dd = new BimeViewModel();

            if (Id == "0")
            {
                _dd.Sal = "";
                _dd.Mah = "";
                _dd.IdShip = IdShip;
            }
            else
            {
                using (var x = new Model.Db.GolatehEntities())
                {
                    string[] Splt = Id.Split('|');

                    int IdShip2 = int.Parse(Splt[2]);
                    string Year = Splt[0];
                    string Month = Splt[1];

                    x.Ship.AsNoTracking().First(b => b.Id == IdShip2 && b.IdPersonnel == IdUser);

                    _dd.Sal = Year;
                    _dd.Mah = Month;
                    _dd.IdShip = IdShip2;
                    _dd.List = x.BimehAfrad.AsNoTracking().Where(b => b.IdShip == IdShip2 && b.Year == _dd.Sal && b.Month == _dd.Mah).Select(b => b.IdPersonnel).ToList();
                }
            }

            return PartialView("~/Areas/Admin/Views/Bimeh/_Add.cshtml", _dd);
        }


        [HttpPost]
        public PartialViewResult GetListBimeh(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dd = SiteSettingBLL.GetListShip(IdUser).Where(b => b.Id == IdShip).First();


            var lst = SiteSettingBLL.GetListBimehAfrad(IdShip);
            return PartialView("~/Areas/Admin/Views/Bimeh/_List.cshtml", lst);
        }




        public string GetConnection(string path)
        {
            return $"Provider=vfpoledb;Data Source={path};Collating Sequence=machine;Trusted_Connection=True;";
        }


   

    }
}