using Newtonsoft.Json;
using Ship.Model.BLL.Admin;
using Ship.Model.Db;
using Ship.Model.Extension;
using Ship.Model.ViewModel.Admin;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;


namespace Ship.Areas.Tankha.Controllers
{
    [Authorize(Roles = "TankhaGardan")]
    public class SafarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public PartialViewResult GetListSafar(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListSafar3(IdShip).Where(v => v.IsActive).ToList();

            return PartialView("~/Areas/tankha/Views/Safar/ListSafar.cshtml", dd);
        }

        [HttpPost]
        public JsonResult DeleteSafar(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.DeleteSafar(IdUser, Id));
        }

        [HttpPost]
        public JsonResult AddSaveSafar(int IdSoom, string FromDate, string ToDate, string Tozihat, string Title, string PriceFood, string TamiratGolate)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            PriceFood = PriceFood.ToEnNum();

            return Json(SiteSettingBLL.AddSaveSafar(IdUser, IdSoom, FromDate, ToDate, Tozihat, Title, int.Parse(PriceFood), decimal.Parse(TamiratGolate.Replace(".", "/"))));
        }
        [HttpPost]
        public JsonResult EditSaveSafarMaster(int IdSoom, long Id, string FromDate, string ToDate, string Tozihat, string Title, int PriceFood, string TamiratGolate)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.EditSaveSafarMaster(IdUser, IdSoom, Id, FromDate, ToDate, Tozihat, Title, PriceFood, decimal.Parse(TamiratGolate.Replace(".", "/"))));
        }
        public PartialViewResult _Add(int Id = 0)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var itm = SiteSettingBLL.GetInfohHeaderSafar(IdUser, Id);

            return PartialView("~/Areas/tankha/Views/Safar/_Add.cshtml", itm.Soom);
        }
        public PartialViewResult EditSafarMaster(int Id, int IdSoom)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListSafar(IdUser, IdSoom).First(v => v.Id == Id && v.IsActive);

            return PartialView("~/Areas/tankha/Views/Safar/_Edit.cshtml", dd);
        }

        [HttpPost]
        public JsonResult CloseSafar(int Id, bool Status)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var trss = SiteSettingBLL.CloseSafar(IdUser, Id, Status);


            return Json(new { status = trss.Length == 0, msg = trss });
        }
        [HttpPost]
        public JsonResult SafarIsOpen(int IdSoom)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.SafarIsOpen(IdUser, IdSoom));
        }

        public PartialViewResult GetDeteilSafar(int Id, int IdSoom)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListSafar2(IdSoom).First(v => v.Id == Id && v.IsActive);

            return PartialView("~/Areas/tankha/Views/Safar/_EditDeteilSafar.cshtml", dd);
        }


        [HttpPost]
        public JsonResult AddAfradToSafar(int IdSoom, int IdSafar, int IdAfrad, string IdSemat, string Rate, bool IsBimeh)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var res = SiteSettingBLL.AddAfradToSafar(IdUser, IdSoom, IdSafar, IdAfrad, int.Parse(IdSemat), decimal.Parse(Rate.Replace(".", "/")), IsBimeh);

            return Json(res);
        }
        [HttpPost]
        public PartialViewResult GetListAfradRelation(int IdSafar, int IdSoom)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListAfradRelation(IdSafar, IdSoom);

            return PartialView("~/Areas/tankha/Views/Safar/Partial/GetListAfrad.cshtml", dd);
        }

        [HttpPost]
        public JsonResult DeleteSafarAfradRelation(int IdSafar, int IdSoom, long Id)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.DeleteSafarAfradRelation(IdSafar, IdSoom, Id));
        }



        [HttpPost]
        public JsonResult HandMoneyToSafar(int IdSoom, int IdSafar, int IdAfrad, int? IdKala, string Weight, string Tozihat, string Price, string CreateDateMoney, int? IdUnit, long IdNote)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            long? _Weight = null;
            long? _Price = null;

            if (string.IsNullOrWhiteSpace(Price) == false)
            {
                _Price = long.Parse(Price.Replace(",", ""));
            }
            if (string.IsNullOrWhiteSpace(Weight) == false)
            {
                _Weight = long.Parse(Weight.Replace(",", ""));
            }

            return Json(SiteSettingBLL.HandMoneyToSafar(IdSoom, IdSafar, IdAfrad, IdKala, _Weight, Tozihat, _Price, CreateDateMoney, IdUnit, IdNote));
        }

        [HttpPost]
        public PartialViewResult GetListSafarToHandMoney(int IdSoom, int IdSafar)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListSafarToHandMoney(IdSafar, IdSoom);

            return PartialView("~/Areas/tankha/Views/Safar/Partial/GetListHandMoney.cshtml", dd);
        }


        [HttpPost]
        public JsonResult DeleteSafarToHandMoney(int IdSafar, int IdSoom, long Id)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.DeleteSafarToHandMoney(IdSafar, IdSoom, Id));
        }

        [HttpPost]
        public JsonResult AddFactorForosh(int IdSoom, int IdSafar, int IdKala, decimal Weight, string Tozihat, string Price, string CreateDate, int Idunit)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            Price = Price.ToEnNum();

            return Json(SiteSettingBLL.AddFactorForosh(IdUser, IdSoom, IdSafar, IdKala, Weight, Tozihat, long.Parse(Price), CreateDate, Idunit));
        }

        [HttpPost]
        public PartialViewResult GetListFactor(int IdSoom, int IdSafar)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListFactor2(IdSafar, IdSoom, 14, IdUser);

            return PartialView("~/Areas/tankha/Views/Safar/Partial/GetListFactorForosh.cshtml", dd);
        }

        [HttpPost]
        public JsonResult DeleteFactor(int IdSafar, int IdSoom, long Id, int IdType)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.DeleteFactor2(IdUser, IdSafar, IdSoom, Id, IdType));
        }




        [HttpPost]
        public JsonResult CheckerSoomOpen(int IdSoom)
        {
            return Json(SiteSettingBLL.CheckerSoomOpen(IdSoom));
        }








        [HttpPost]
        public PartialViewResult GetListFactorTamirat(int IdSoom, int IdSafar)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListFactor2(IdSafar, IdSoom, 16, IdUser);

            return PartialView("~/Areas/tankha/Views/Safar/Partial/GetListFactorTamirat.cshtml", dd);
        }

        [HttpPost]
        public JsonResult AddFactorForoshTamirat(int IdSoom, int IdSafar, string IdKala, decimal? Weight, string Tozihat, string Price, string CreateDate, decimal? Tedad)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            Price = Price.ToEnNum();

            return Json(SiteSettingBLL.AddFactorForoshTamirat2(IdUser, IdSoom, IdSafar, IdKala, Weight, Tozihat, long.Parse(Price), CreateDate, Tedad));
        }



        [HttpPost]
        public PartialViewResult GetListFactorMahtaiaj(int IdSoom, int IdSafar)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListFactor2(IdSafar, IdSoom, 18, IdUser);

            return PartialView("~/Areas/tankha/Views/Safar/Partial/GetListFactorMahtaiaj.cshtml", dd);
        }

        [HttpPost]
        public JsonResult AddFactorForoshMahtaiaj(int IdSoom, int IdSafar, int IdKala, decimal? Weight, string Tozihat, string Price, string CreateDate, decimal? Tedad, int Idunit)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            Price = Price.ToEnNum();

            return Json(SiteSettingBLL.AddFactorForoshMahtaiaj2(IdUser, IdSoom, IdSafar, IdKala, Weight, Tozihat, long.Parse(Price), CreateDate, Tedad, Idunit));
        }


        [HttpPost]
        public PartialViewResult GetListFactorKharid(int IdSoom, int IdSafar)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListFactor2(IdSafar, IdSoom, 13, IdUser);

            return PartialView("~/Areas/tankha/Views/Safar/Partial/GetListFactorKharid.cshtml", dd);
        }

        [HttpPost]
        public JsonResult AddFactorForoshKharid(int IdSoom, int IdSafar, int IdKala, decimal? Weight, string Tozihat, string Price, string CreateDate, decimal? Tedad, int Idunit)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            Price = Price.ToEnNum();

            return Json(SiteSettingBLL.AddFactorForoshKharid2(IdUser, IdSoom, IdSafar, IdKala, Weight, Tozihat, long.Parse(Price), CreateDate, Tedad, Idunit));
        }





        public ActionResult PrintSafar(string id = "")
        {
            return View();
        }


        public virtual ActionResult ViewerEvent()
        {
            return Stimulsoft.Report.Mvc.StiMvcViewer.ViewerEventResult(this.HttpContext);
        }
        public virtual ActionResult Interaction()
        {
            return Stimulsoft.Report.Mvc.StiMvcViewer.InteractionResult(this.HttpContext);
        }
        public virtual ActionResult PrintReport()
        {
            return Stimulsoft.Report.Mvc.StiMvcViewer.PrintReportResult(this.HttpContext);
        }
        public virtual ActionResult ExportReport()
        {
            return Stimulsoft.Report.Mvc.StiMvcViewer.ExportReportResult(this.HttpContext);
        }

        public ActionResult GetReport(string id = "")
        {
            using (var x = new GolatehEntities())
            {
                if (x.Safar.AsNoTracking().Any(v => v.Id.ToString() == id && v.IsActive == true))
                {
                    string fff = "dfsgvbfdb";
                    int.Parse(fff);
                }
            }



            StiReport report = new StiReport
            {
                ReportName = Guid.NewGuid().ToString()
            };

            report.Load(Server.MapPath(@"~/Content/Mrt/PrintSafar.mrt"));

            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            var tt = SiteSettingBLL.GetPrintSafar(int.Parse(id), IdUser);

            report.RegData("DataSource2", tt);


            var FactorForosh = SiteSettingBLL.GetListFactor2(int.Parse(id), IdUser, 14);
            report.RegData("ListFactorForosh", "ListFactorForosh", FactorForosh);


            var Afrad = SiteSettingBLL.GetListAfradRelation2(int.Parse(id), IdUser);
            report.RegData("Afrad", "Afrad", Afrad);


            var Sahamdar = SiteSettingBLL.GetListSahamdar2(int.Parse(id), IdUser);
            report.RegData("Sahamdar", "Sahamdar", Sahamdar);

            var HandMoney = SiteSettingBLL.GetListSafarToHandMoney2(int.Parse(id), IdUser);
            HandMoney = HandMoney.OrderBy(b => b.KalaName.Length).ToList();
            foreach (var item in HandMoney)
            {
                if (string.IsNullOrWhiteSpace(item.KalaName))
                {
                    item.Price = 0;
                }
            }
            report.RegData("HandMoney", "HandMoney", HandMoney);




            var Hazine = SiteSettingBLL.GetListFactor2(int.Parse(id));
            report.RegData("Hazine", "Hazine", Hazine);


            var Mosaede = SiteSettingBLL.GetListSafarToHandMoney3(int.Parse(id));
            report.RegData("Mosaede", "Mosaede", Mosaede);


            report.Compile();
            report.Render();

            return Stimulsoft.Report.Mvc.StiMvcViewer.GetReportSnapshotResult(this.HttpContext, report);
        }


        [HttpPost]
        public JsonResult CopySafar(int IdSafar, int IdSoom)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.CopySafar(IdUser, IdSafar, IdSoom));
        }


        [HttpPost]
        public PartialViewResult ToShowPage(int Id, string IdSafar, string IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            string FromDate = "";
            string ToDate = "";

            using (var x = new GolatehEntities())
            {
                var _Safar = x.Safar.AsNoTracking().First(b => b.Id.ToString() == IdSafar);

                FromDate = PersianCalander.ToShamsi(_Safar.FromDate);
                ToDate = PersianCalander.ToShamsi(_Safar.ToDate);
            }

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;

            string Page = "";
            if (Id == 1)
            {
                Page = "~/Areas/tankha/Views/Safar/Partial/ListAfrad.cshtml";
            }
            if (Id == 2)
            {
                Page = "~/Areas/tankha/Views/Safar/Partial/HandMoney.cshtml";
            }
            if (Id == 3)
            {
                Page = "~/Areas/tankha/Views/Safar/Partial/FactorKharid.cshtml";
            }
            if (Id == 4)
            {
                Page = "~/Areas/tankha/Views/Safar/Partial/FactorForosh.cshtml";
            }
            if (Id == 5)
            {
                Page = "~/Areas/tankha/Views/Safar/Partial/FactorTamirat.cshtml";
            }
            if (Id == 6)
            {
                Page = "~/Areas/tankha/Views/Safar/Partial/FactorMahtaiaj.cshtml";
            }
            if (Id == 7)
            {
                using (var x = new GolatehEntities())
                {
                    long IdSoom = x.Safar.AsNoTracking().First(b => b.Id.ToString() == IdSafar).IdSoom;
                    var dd = SiteSettingBLL.GetListSafar(IdUser, (int)IdSoom).First(v => v.Id.ToString() == IdSafar && v.IsActive);
                    return PartialView("~/Areas/tankha/Views/Safar/Partial/_EditMaster.cshtml", dd);
                }
            }
            return PartialView(Page, (Id == 1 ? IdShip : IdSafar));
        }


        [HttpPost]
        public JsonResult ChekerOpenSafar(int IdSafar, int IdSoom)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            string msg = "";

            using (var x = new GolatehEntities())
            {
                if (x.Safar.AsNoTracking().Any(v => v.Id == IdSafar && v.IsActive == true && v.IdSoom == IdSoom))
                {
                    msg = "برای پرینت سفر آن را ببندید";
                }
            }

            return Json(new { msg = msg, status = msg.Length == 0 });
        }

        [HttpPost]
        public JsonResult SavePriceMachele(string model)
        {
            var model22 = JsonConvert.DeserializeObject<List<PriceViewMOdel>>(model);

            using (var x = new GolatehEntities())
            {
                foreach (var item in model22)
                {
                    var itm = x.SafarToFactor.First(v => v.Id == item.Id);

                    itm.Tedad = item.Tedad;
                    itm.Price = item.Fi;
                    itm.TotalPrice = item.Tedad * item.Fi;

                    if (itm.IdTypeFactor == 14)
                    {
                        if (x.SafarToHandMoney.AsNoTracking().Any(b => b.IdSafar == itm.IdSafar && b.IdKala == itm.IdKala))
                        {
                            foreach (var cc in x.SafarToHandMoney.Where(b => b.IdSafar == itm.IdSafar && b.IdKala == itm.IdKala).ToList())
                            {
                                cc.Price = itm.Price;
                                cc.TotalPrice = (long)itm.Price * (long)cc.Weight;
                            }
                        }
                    }
                }

                x.SaveChanges();
            }
            return Json(1);
        }





        public StiReport GetReport2(string id = "")
        {
            using (var x = new GolatehEntities())
            {
                if (x.Safar.AsNoTracking().Any(v => v.Id.ToString() == id && v.IsActive == true))
                {
                    string fff = "dfsgvbfdb";
                    int.Parse(fff);
                }
            }



            StiReport report = new StiReport
            {
                ReportName = Guid.NewGuid().ToString()
            };

            report.Load(Server.MapPath(@"~/Content/Mrt/PrintSafar.mrt"));

            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            bool IsNotFrosh = false;

            var Hazine = SiteSettingBLL.GetListFactor2(int.Parse(id));
            if (Hazine.Any(v => v.IdTypeFactor == 14) == false)
            {
                IsNotFrosh = true;
            }




            var tt = SiteSettingBLL.GetPrintSafar(int.Parse(id), IdUser);
            if (IsNotFrosh)
            {
                tt.PriceHarGolate = "0";
            }

            report.RegData("DataSource2", tt);


            var FactorForosh = SiteSettingBLL.GetListFactor2(int.Parse(id), IdUser, 14);
            report.RegData("ListFactorForosh", "ListFactorForosh", FactorForosh);


            var Afrad = SiteSettingBLL.GetListAfradRelation2(int.Parse(id), IdUser);
            if (IsNotFrosh)
            {
                foreach (var x in Afrad)
                {
                    x.TotalPrice = 0;
                }
            }
            report.RegData("Afrad", "Afrad", Afrad);


            var Sahamdar = SiteSettingBLL.GetListSahamdar2(int.Parse(id), IdUser);
            if (IsNotFrosh)
            {
                foreach (var x in Sahamdar)
                {
                    x.TotalPrice = 0;
                }
            }
            report.RegData("Sahamdar", "Sahamdar", Sahamdar);

            var HandMoney = SiteSettingBLL.GetListSafarToHandMoney2(int.Parse(id), IdUser);
            HandMoney = HandMoney.OrderBy(b => b.KalaName.Length).ToList();
            foreach (var item in HandMoney)
            {
                if (string.IsNullOrWhiteSpace(item.KalaName))
                {
                    item.Price = 0;
                }
            }
            report.RegData("HandMoney", "HandMoney", HandMoney);

            report.RegData("Hazine", "Hazine", Hazine);


            var Mosaede = SiteSettingBLL.GetListSafarToHandMoney3(int.Parse(id));
            foreach (var item in Mosaede)
            {
                if (string.IsNullOrWhiteSpace(item.KalaName))
                {
                    item.Price = 0;
                }
            }

            report.RegData("Mosaede", "Mosaede", Mosaede);


            if (Mosaede.Any() == false)
            {
                var gHeader = report.GetComponentByName("HeaderMosaede");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 18) == false)
            {
                var gHeader = report.GetComponentByName("HeaderBand6");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 16) == false)
            {
                var gHeader = report.GetComponentByName("HeaderBand5");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 14) == false)
            {
                var gHeader = report.GetComponentByName("HeaderBand4");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 13) == false)
            {
                var gHeader = report.GetComponentByName("HeaderHazine");
                gHeader.Enabled = false;
            }

            if (Afrad.Any() == false)
            {
                var gHeader = report.GetComponentByName("HeaderAfrad");
                gHeader.Enabled = false;
            }
            if (HandMoney.Any() == false)
            {
                var gHeader = report.GetComponentByName("HeaderBand3");
                gHeader.Enabled = false;
            }






            if (Mosaede.Any() == false)
            {
                var gHeader = report.GetComponentByName("FooterBand6");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 18) == false)
            {
                var gHeader = report.GetComponentByName("FooterBand5");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 16) == false)
            {
                var gHeader = report.GetComponentByName("FooterBand4");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 14) == false)
            {
                var gHeader = report.GetComponentByName("FooterBand3");
                gHeader.Enabled = false;
            }
            if (Hazine.Any(v => v.IdTypeFactor == 13) == false)
            {
                var gHeader = report.GetComponentByName("FooterBand2");
                gHeader.Enabled = false;
            }
            if (Afrad.Any() == false)
            {
                var gHeader = report.GetComponentByName("FooterBand8");
                gHeader.Enabled = false;
            }
            if (HandMoney.Any() == false)
            {
                var gHeader = report.GetComponentByName("FooterBand9");
                gHeader.Enabled = false;
            }

            report.Compile();
            report.Render();

            return report;
        }


        public ActionResult PrintPdf(string id = "")
        {
            Stimulsoft.Report.Export.FontsInfoStore.LoadFontInfoToStore("BNazanin", Server.MapPath(@"~/Content/Mrt/BNazanin.ttf"));
            Stimulsoft.Report.Export.FontsInfoStore.LoadFontInfoToStore("BNaznnBd", Server.MapPath(@"~/Content/Mrt/BNaznnBd.ttf"));
            StiOptions.Export.Pdf.AllowFontsCache = true;

            var report = this.GetReport2(id);


            var settings = new Stimulsoft.Report.Export.StiPdfExportSettings() { ImageQuality = 1.0f, ImageResolution = 300, EmbeddedFonts = true, UseUnicode = true, StandardPdfFonts = true };
            var service = new Stimulsoft.Report.Export.StiPdfExportService();
            var stream = new MemoryStream();

            service.ExportTo(report, stream, settings);

            return File(stream.ToArray(), "application/octet-stream", DateTime.Now.Ticks + ".pdf");
        }




        public List<StiReport> MultiPrint(string id)
        {
            List<StiReport> lst = new List<StiReport>();
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            using (var x = new GolatehEntities())
            {
                var ff = x.Safar.AsNoTracking().First(v => v.Id.ToString() == id && v.IsActive == false);

                foreach (var item in ff.SafarToAfrad.Where(v => v.IdSemat != 10).Select(v => v.IdPernonnel).ToList())
                {
                    StiReport itm = new StiReport();
                    itm.Load(Server.MapPath(@"~/Content/Mrt/PrintJasho.mrt"));

                    var data = SiteSettingBLL.PrintGolate(long.Parse(id), item);

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
                    }


                    itm.RegData("DataSource1", data);

                    itm.Render();
                    lst.Add(itm);
                }
                return lst;
            }
        }


        public ActionResult PrintPdf2(string id = "")
        {
            Stimulsoft.Report.Export.FontsInfoStore.LoadFontInfoToStore("BNazanin", Server.MapPath(@"~/Content/Mrt/BNazanin.ttf"));
            Stimulsoft.Report.Export.FontsInfoStore.LoadFontInfoToStore("BNaznnBd", Server.MapPath(@"~/Content/Mrt/BNaznnBd.ttf"));
            StiOptions.Export.Pdf.AllowFontsCache = true;

            StiReport report = new StiReport()
            {
                NeedsCompiling = false,
                IsRendered = true,
                ReportUnit = StiReportUnitType.Centimeters
            };
            report.RenderedPages.Clear();
            Stimulsoft.Report.Units.StiUnit newUnit = Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(report.ReportUnit);


            foreach (var item in MultiPrint(id))
            {
                Stimulsoft.Report.Units.StiUnit oldUnit = Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(item.ReportUnit);
                bool needConvert = report.ReportUnit != item.ReportUnit;
                foreach (StiPage page in item.CompiledReport.RenderedPages)
                {
                    page.Report = report;
                    page.NewGuid();
                    if (needConvert) page.Convert(oldUnit, newUnit);
                    report.RenderedPages.Add(page);
                }
            }


            var settings = new Stimulsoft.Report.Export.StiPdfExportSettings() { ImageQuality = 1.0f, ImageResolution = 300, EmbeddedFonts = true, UseUnicode = false, StandardPdfFonts = true };
            var service = new Stimulsoft.Report.Export.StiPdfExportService();
            var stream = new MemoryStream();

            service.ExportTo(report, stream, settings);

            return File(stream.ToArray(), "application/octet-stream", DateTime.Now.Ticks + ".pdf");
        }
    }
}