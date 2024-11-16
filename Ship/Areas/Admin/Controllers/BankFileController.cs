using Salt.Model.Extension;
using Ship.Model.BLL.Admin;
using Ship.Model.Extension;
using Ship.Model.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class BankFileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetListSafarByIdSoom(long IdSoom)
        {
            return Json(SiteSettingBLL.GetListSafarByIdSoom(IdSoom).Where(v => v.IsActive == false).ToList());
        }


        [HttpPost]
        public JsonResult GetListSoomByIdShip(int IdShip)
        {
            return Json(SiteSettingBLL.GetListSoomByIdShip(IdShip));
        }



        [HttpPost]
        public PartialViewResult GetListAftadToSafar(int IdSafar)
        {
            int IdUser = int.Parse(User.Identity.Name);

            List<FileBankListViewModel> mdl = new List<FileBankListViewModel>();
            using (var x = new Model.Db.GolatehEntities())
            {
                var ff = x.Safar.AsNoTracking().First(v => v.Id == IdSafar && v.IsActive == false);

                foreach (var item in ff.SafarToAfrad.Where(v => v.IdSemat != 10).Select(v => v.IdPernonnel).ToList())
                {
                    var data = SiteSettingBLL.PrintGolate(IdSafar, item);

                    foreach (var cc in data)
                    {
                        if (cc.IdSemat == 8)
                        {
                            cc.PriceFood = "0";
                        }

                        if (cc.PriceForosh == "0")
                        {
                            cc.SahmJasho = "0";
                            cc.GolateBase = "0";
                            cc.Kosorat = (cc.MablagBimeh.Replace(",", "").ToInt() + cc.PriceMsaede.Replace(",", "").ToInt()).AddCommaToInt();
                            cc.KhalesPardakhti = "0";
                        }

                        cc.KhalesPardakhti = cc.KhalesPardakhti.Replace(",", "").Replace(",", "");
                        var _pardakht = Convert.ToInt64(cc.KhalesPardakhti);

                        var _per = x.Personnel.AsNoTracking().First(b => b.Id == item);

                        mdl.Add(new FileBankListViewModel() { NationalCode = _per.NationalCode, Family = cc.LastName, Id = _per.Id, Name = cc.FirstName, Semat = cc.SematName, TotalPrice = _pardakht });
                    }
                }
            }
            return PartialView("~/Areas/Admin/Views/BankFile/ListAftad.cshtml", mdl);
        }


        public string Melat(List<FileBankList2ViewModel> model)
        {
            List<MelatViewModel> melat = new List<MelatViewModel>();
            long random = 123456789;
            string today = PersianCalander.ToShamsi(DateTime.Now).Replace("/", "") + "_" + DateTime.Now.Ticks;
            using (var x = new Model.Db.GolatehEntities())
            {
                foreach (var ii in model)
                {
                    var itm = x.Personnel.AsNoTracking().First(b => b.Id == ii.AttrId);
                    random = random + 1;

                    melat.Add(new MelatViewModel() { date = today, mablag = ii.TotalPrice.ToString(), name = itm.FirstName + " " + itm.LastName, serial = random.ToString(), sheba = itm.ShebaNumber, tedadhavale = "1" });
                }
            }
            
            string[] columns = { "sheba", "date", "serial", "tedadhavale", "mablag", "name" };
            byte[] filecontent = ExcelExportHelper.ExportExcel2(melat, columns);

            System.IO.File.WriteAllBytes(Server.MapPath(@"/Content/ExportExcel/" + today + ".xls"), filecontent);

            return @"/Content/ExportExcel/" + today + ".xls";
        }
        public string Keshavarzi(List<FileBankList2ViewModel> model)
        {
            List<KeshavarziViewModel> Keshavarzi = new List<KeshavarziViewModel>();
            long random = 123456789;
            string today = PersianCalander.ToShamsi(DateTime.Now).Replace("/", "") + "_" + DateTime.Now.Ticks;
            using (var x = new Model.Db.GolatehEntities())
            {
                foreach (var ii in model)
                {
                    var itm = x.Personnel.AsNoTracking().First(b => b.Id == ii.AttrId);
                    random = random + 1;

                    Keshavarzi.Add(new KeshavarziViewModel() { mablag = ii.TotalPrice.ToString(), name = itm.FirstName + " " + itm.LastName, serial = random.ToString(), sheba = itm.ShebaNumber });
                }
                Keshavarzi.Add(new KeshavarziViewModel() { mablag = "T" });
            }


            string[] columns = { "mablag", "sheba", "name", "serial" };
            byte[] filecontent = ExcelExportHelper.ExportExcel2(Keshavarzi, columns);

            System.IO.File.WriteAllBytes(Server.MapPath(@"/Content/ExportExcel/" + today + ".xls"), filecontent);

            return @"/Content/ExportExcel/" + today + ".xls";
        }
        [HttpPost]
        public JsonResult SaveFile(List<FileBankList2ViewModel> model, int IdBank)
        {
            if (IdBank == 4)
            {
                string ret = Melat(model);
                return Json(new { ok = 1, url = ret });
            }
            else if (IdBank == 2)
            {
                string ret = Keshavarzi(model);
                return Json(new { ok = 1, url = ret });
            }
            else
            {
                return Json(new { ok = 0, url = "" });
            }
        }


    }
}