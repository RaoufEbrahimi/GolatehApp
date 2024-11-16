using Ship.Model.BLL.Admin;
using Ship.Model.Extension;
using Ship.Model.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class ShipController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public PartialViewResult _Add(int Id = 0)
        {
            int IdUser = int.Parse(User.Identity.Name);
            var itm = new GetListShipViewModel();
            if (Id > 0)
            {
                itm = SiteSettingBLL.GetListShip(IdUser).First(v => v.Id == Id);
            }
            else
            {
                itm.Id = 0;
            }

            return PartialView("~/Areas/Admin/Views/Ship/_Add.cshtml", itm);
        }
        [HttpPost]
        public JsonResult AddSave(int Id, string Title, string Number, string YearProduction, string Tul, string Arz, string NameMotor, string HajMotor, string ShomareBime)
        {
            int IdUser = int.Parse(User.Identity.Name);

            string Image = "";

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                Image = @"~/Content/File/Ship/" + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath(Image));

                Image = Image.Replace("~/", "/");
            }


            int? _Tul = null;
            int? _Arz = null;
            int? _HajMotor = null;

            if (string.IsNullOrWhiteSpace(Tul) == false)
            {
                _Tul = int.Parse(Tul.ToEnNum());
            }
            if (string.IsNullOrWhiteSpace(Arz) == false)
            {
                _Arz = int.Parse(Arz.ToEnNum());
            }
            if (string.IsNullOrWhiteSpace(HajMotor) == false)
            {
                _HajMotor = int.Parse(HajMotor.ToEnNum());
            }

            var tt = SiteSettingBLL.ShipAddSave(IdUser, Id, Title, Number, YearProduction, _Tul, _Arz, NameMotor, _HajMotor, ShomareBime, Image);

            return Json(new { status = tt.Status, msg = tt.Msg });
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.ShipDelete(IdUser, Id));
        }
        [HttpPost]
        public PartialViewResult GetListShip()
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dd = SiteSettingBLL.GetListShip(IdUser);

            return PartialView("~/Areas/Admin/Views/Ship/_List.cshtml", dd);
        }



        [HttpPost]
        public PartialViewResult GetListShip2(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dd = SiteSettingBLL.GetListShip(IdUser).Where(v => v.Id == Id).ToList();

            return PartialView("~/Areas/Admin/Views/Ship/_List2.cshtml", dd);
        }


        [HttpPost]
        public PartialViewResult OpenSahamDar(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var itm = SiteSettingBLL.GetListShip(IdUser).First(v => v.Id == IdShip);

            var lst = new ShipViewModel() { Title = itm.Title, Id = itm.Id };

            return PartialView("~/Areas/Admin/Views/Ship/_SahamDar.cshtml", lst);
        }

        [HttpPost]
        public PartialViewResult GetListSahamDar(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dd = SiteSettingBLL.GetListSahamdar(IdShip, IdUser);

            return PartialView("~/Areas/Admin/Views/Ship/_ListSahamDar.cshtml", dd);
        }

        [HttpPost]
        public JsonResult DeleteSahamDar(int IdShip, int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.DeleteSahamDar(IdUser, IdShip, Id));
        }

        [HttpPost]
        public JsonResult AddSaveSahamDar(int Id, int IdUserSelected, string Rate, int IdDong)
        {
            int IdUser = int.Parse(User.Identity.Name);

            Rate = Rate.ToEnNum();

            return Json(SiteSettingBLL.AddSaveSahamDar(IdUser, Id, IdUserSelected, int.Parse(Rate), IdDong));
        }

        [HttpPost]
        public PartialViewResult OpenAfrad(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);
            var itm = SiteSettingBLL.GetListShip(IdUser).First(v => v.Id == IdShip);

            return PartialView("~/Areas/Admin/Views/Ship/_Afrad.cshtml", itm);
        }

        [HttpPost]
        public PartialViewResult GetListAfrad(int IdShip, bool Active)
        {
            int IdUser = int.Parse(User.Identity.Name);
            var itm = SiteSettingBLL.GetListAfrad(IdShip, IdUser).Where(v => v.IsActive == Active).ToList();

            return PartialView("~/Areas/Admin/Views/Ship/_ListAfrad.cshtml", itm);
        }

        [HttpPost]
        public JsonResult AddAfrad(int IdShip, int Id, string FirstName, string LastName, string NationalCode, string Phone, string ShebaNumber, string ShomareCart14, string ShomareHesab, int IdBank)
        {
            int IdUser = int.Parse(User.Identity.Name);
            HttpPostedFileBase file = null;

            if (Request.Files.Count > 0)
            {
                file = Request.Files[0];
            }

            var itm = SiteSettingBLL.AddAfrad(IdShip, IdUser, file, Id, FirstName, LastName, NationalCode, Phone, ShebaNumber, ShomareCart14, ShomareHesab, IdBank);

            return Json(new { status = itm.Length == 0, msg = itm });
        }

        [HttpPost]
        public JsonResult DeleteAfrad(int IdShip, int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.DeleteAfrad(IdUser, IdShip, Id));
        }

        [HttpPost]
        public JsonResult ToActiveAfrad(int IdShip, int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.ToActiveAfrad(IdUser, IdShip, Id));
        }



        [HttpPost]
        public PartialViewResult AfradOpenEdit(int Id, int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);
            var itm = SiteSettingBLL.GetListAfrad(IdShip, IdUser).First(v => v.Id == Id);

            return PartialView("~/Areas/Admin/Views/Ship/_AfradOpenEdit.cshtml", itm);
        }


        [HttpPost]
        public PartialViewResult OpenListHandMoneyAfrad(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);
            var itm = SiteSettingBLL.GetListHandMoney(IdShip);

            return PartialView("~/Areas/Admin/Views/Ship/_ListHandMoneyAfrad.cshtml", itm);
        }
        [HttpPost]
        public JsonResult DeleteAfradhand(long IdPersonnel, int IdShip)
        {
            return Json(SiteSettingBLL.DeleteAfradhand(IdPersonnel, IdShip));
        }





        [HttpPost]
        public JsonResult DeleteYadasht(int IdShip, string Id)
        {
            return Json(SiteSettingBLL.DeleteYadasht(IdShip, Id));
        }

        [HttpPost]
        public JsonResult AfradYadashtSave(int IdShip, string Date, string Desc, long IdPersonnel, long Price)
        {
            return Json(SiteSettingBLL.AfradYadashtSave(IdShip, Date, Desc, IdPersonnel, Price));
        }
        [HttpPost]
        public PartialViewResult GetListAfradYadasht(int IdShip)
        {
            return PartialView("~/Areas/Admin/Views/Ship/_ListAfradYadasht.cshtml", IdShip);
        }
        [HttpPost]
        public PartialViewResult GetListAfradYadasht2(int IdShip, long IdPersonnel)
        {
            var rttt = SiteSettingBLL.GetListYadasht(IdShip).Where(v => v.IdPersonnel == IdPersonnel).OrderBy(v => v.Id).ToList();
            return PartialView("~/Areas/Admin/Views/Ship/_ListAfradYadashtGridView.cshtml", rttt);
        }
        [HttpPost]
        public PartialViewResult KartezBedehi(int IdShip, long IdPersonnel, long Id)
        {
            string dd = IdShip.ToString() + ";" + IdPersonnel.ToString() + ";" + Id.ToString();
            return PartialView("~/Areas/Admin/Views/Ship/_ListAfradYadashtKartezBedehi.cshtml", dd);
        }
        [HttpPost]
        public PartialViewResult GetListAfradYadashtKartezBedehiGride(int IdShip, long IdPersonnel, long Id)
        {
            var rttt = SiteSettingBLL.GetListYadashtkartex(IdShip).Where(v => v.IdPersonnel == IdPersonnel && v.IdNote == Id).OrderBy(v => v.Id).ToList();
            return PartialView("~/Areas/Admin/Views/Ship/_ListAfradYadashtGridViewKartex.cshtml", rttt);
        }


        [HttpPost]
        public JsonResult AfradYadashtKartexSave(int IdShip, string Date, string Desc, long IdPersonnel, long Price, int? IdNot)
        {
            return Json(SiteSettingBLL.AfradYadashtKartexSave(IdShip, Date, Desc, IdPersonnel, Price, IdNot));
        }

        [HttpPost]
        public JsonResult DeleteYadashtKartex(int IdShip, string Id)
        {
            return Json(SiteSettingBLL.DeleteYadashtKartex(IdShip, Id));
        }


        public ActionResult MyShip()
        {
            return View();
        }



        [HttpPost]
        public JsonResult C1(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 1, IdShip);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C2(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 2, IdShip);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C3(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 3, IdShip);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C4(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 4, IdShip);

            return Json(dash1);
        }

        [HttpPost]
        public JsonResult C5(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 5, IdShip);

            return Json(dash1);
        }


        [HttpPost]
        public JsonResult C6(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 6, IdShip);

            return Json(dash1);
        }
        [HttpPost]
        public JsonResult C7(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 7, IdShip);

            return Json(dash1);
        }



        [HttpPost]
        public JsonResult C50(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dash1 = SiteSettingBLL.PrintDashboard3(IdUser, 9, IdShip) as List<PrintDashboard4ViewModel>;



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
                MinPrice = (tmp.Count == 0) ? 0 : tmp.Min()
            };

            return Json(ret);
        }

    }
}