using Ship.Model.BLL.Admin;
using Ship.Model.Db;
using Ship.Model.ViewModel.Admin;
using Stimulsoft.Report;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Ship.Areas.Tankha.Controllers
{
    [Authorize(Roles = "TankhaGardan")]
    public class SoomController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public PartialViewResult _Add(int Id = 0)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            var itm = new GetListSoomViewModel();
            if (Id > 0)
            {
                itm = SiteSettingBLL.GetListSoom(IdUser).First(v => v.Id == Id && v.IsActive);
            }
            else
            {
                itm.Id = 0;
            }

            return PartialView("~/Areas/Tankha/Views/Soom/_Add.cshtml", itm);
        }
        [HttpPost]
        public JsonResult AddSave(long Id, int IdShip, bool IsActive, string FromDate, string ToDate, string Tozihat, string SoomName)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.SoomAddSave(IdUser, Id, IdShip, IsActive, FromDate, ToDate, Tozihat, SoomName));
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.SoomDelete(IdUser, Id));
        }
        [HttpPost]
        public PartialViewResult GetListSoom(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var dd = SiteSettingBLL.GetListSoom22(IdShip);

            return PartialView("~/Areas/Tankha/Views/Soom/_List.cshtml", dd);
        }
        [HttpPost]
        public JsonResult CloseSoom(int Id, bool Status)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var ret = SiteSettingBLL.CloseSoom(IdUser, Id, Status);

            return Json(new { status = ret.Length == 0, msg = ret });
        }
        [HttpPost]
        public JsonResult SoomIsOpen(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            return Json(SiteSettingBLL.SoomIsOpen(IdUser, IdShip));
        }




        [HttpPost]
        public JsonResult ChekerOpenSoom(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));

            var tt = SiteSettingBLL.GetListSoom(IdUser).First(v => v.Id == Id);

            return Json(tt.IsActive);
        }

        [HttpPost]
        public JsonResult ChekerOpenSafarInSoom(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            string msg = "";

            using (var x = new GolatehEntities())
            {
                var ff = x.Soom.AsNoTracking().First(v => v.Id == Id && v.IdPersonnel == IdUser);

                if (ff == null || ff.Safar.Any(c => c.IsActive))
                {
                    msg = "سفر ها در این صوم بسته نشده است";
                }

                if (ff.IsActive)
                {
                    msg = "برای پرینت صوم را ببندید";
                }
            }
            return Json(new { msg = msg, status = msg.Length == 0 });
        }






        public ActionResult PrintSoom(string id = "")
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
                var ff = x.Soom.AsNoTracking().First(v => v.Id.ToString() == id && v.IsActive == false);

                if (ff == null || ff.Safar.Any(c => c.IsActive))
                {
                    string fff = "dfsgvbfdb";
                    int.Parse(fff);
                }
            }

            StiReport report = new StiReport
            {
                ReportName = Guid.NewGuid().ToString()
            };
            report.Load(Server.MapPath(@"~/Content/Mrt/PrintSoom.mrt"));


            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            var tt = SiteSettingBLL.GetPrintSoom(int.Parse(id), IdUser);
            report.RegData("DataSource2", "DataSource2", tt);


            var Sahamdar = SiteSettingBLL.GetListSahamdar3(int.Parse(id), IdUser);
            report.RegData("Sahamdar", "Sahamdar", Sahamdar);


            var Hazine = SiteSettingBLL.GetListFactor3(int.Parse(id));
            report.RegData("Hazine", "Hazine", Hazine);

            var KalaFromSoom = SiteSettingBLL.GetListKalaFromSoom(int.Parse(id));
            report.RegData("KalaFromSoom", "KalaFromSoom", KalaFromSoom);

            var PernonnelGolate = SiteSettingBLL.GetPrintPernonnelGolateSoom(int.Parse(id));
            report.RegData("PernonnelGolate", "PernonnelGolate", PernonnelGolate);

            report.Compile();
            report.Render();

            return Stimulsoft.Report.Mvc.StiMvcViewer.GetReportSnapshotResult(this.HttpContext, report);
        }






        public StiReport GetReport2(string id = "")
        {
            using (var x = new GolatehEntities())
            {
                var ff = x.Soom.AsNoTracking().First(v => v.Id.ToString() == id && v.IsActive == false);

                if (ff == null || ff.Safar.Any(c => c.IsActive))
                {
                    string fff = "dfsgvbfdb";
                    int.Parse(fff);
                }
            }

            StiReport report = new StiReport
            {
                ReportName = Guid.NewGuid().ToString()
            };
            report.Load(Server.MapPath(@"~/Content/Mrt/PrintSoom.mrt"));


            int IdUser = int.Parse(User.Identity.Name.Replace("TankhaGardan", ""));
            var tt = SiteSettingBLL.GetPrintSoom(int.Parse(id), IdUser);
            report.RegData("DataSource2", "DataSource2", tt);


            var Sahamdar = SiteSettingBLL.GetListSahamdar3(int.Parse(id), IdUser);
            report.RegData("Sahamdar", "Sahamdar", Sahamdar);


            var Hazine = SiteSettingBLL.GetListFactor3(int.Parse(id));
            report.RegData("Hazine", "Hazine", Hazine);

            var KalaFromSoom = SiteSettingBLL.GetListKalaFromSoom(int.Parse(id));
            report.RegData("KalaFromSoom", "KalaFromSoom", KalaFromSoom);

            var PernonnelGolate = SiteSettingBLL.GetPrintPernonnelGolateSoom(int.Parse(id));
            report.RegData("PernonnelGolate", "PernonnelGolate", PernonnelGolate);


            if (KalaFromSoom.Any(v => v.IdTypeFactor == 16) == false)
            {
                var gHeader = report.GetComponentByName("DataBand5");
                gHeader.Enabled = false;
            }

            if (KalaFromSoom.Any(v => v.IdTypeFactor == 13) == false)
            {
                var gHeader = report.GetComponentByName("DataBand3");
                gHeader.Enabled = false;
            }

            if (KalaFromSoom.Any(v => v.IdTypeFactor == 14) == false)
            {
                var gHeader = report.GetComponentByName("DataBand1");
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


            var settings = new Stimulsoft.Report.Export.StiPdfExportSettings() { ImageQuality = 1.0f, ImageResolution = 300, EmbeddedFonts = true, UseUnicode = false, StandardPdfFonts = true };
            var service = new Stimulsoft.Report.Export.StiPdfExportService();
            var stream = new MemoryStream();

            service.ExportTo(report, stream, settings);

            return File(stream.ToArray(), "application/octet-stream", DateTime.Now.Ticks + ".pdf");
        }




    }
}