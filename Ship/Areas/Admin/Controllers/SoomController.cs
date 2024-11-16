using Ship.Model.BLL.Admin;
using Ship.Model.Db;
using Ship.Model.ViewModel.Admin;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ship.Model.Extension;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class SoomController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public PartialViewResult _Add(int Id = 0)
        {
            int IdUser = int.Parse(User.Identity.Name);
            var itm = new GetListSoomViewModel();
            if (Id > 0)
            {
                itm = SiteSettingBLL.GetListSoom(IdUser).First(v => v.Id == Id && v.IsActive);
            }
            else
            {
                itm.Id = 0;
            }

            return PartialView("~/Areas/Admin/Views/Soom/_Add.cshtml", itm);
        }
        [HttpPost]
        public JsonResult AddSave(long Id, int IdShip, bool IsActive, string FromDate, string ToDate, string Tozihat, string SoomName)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.SoomAddSave(IdUser, Id, IdShip, IsActive, FromDate, ToDate, Tozihat, SoomName));
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.SoomDelete(IdUser, Id));
        }
        [HttpPost]
        public PartialViewResult GetListSoom(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var dd = SiteSettingBLL.GetListSoom(IdUser).Where(b => b.IdShip == IdShip).ToList();

            return PartialView("~/Areas/Admin/Views/Soom/_List.cshtml", dd);
        }
        [HttpPost]
        public JsonResult CloseSoom(int Id, bool Status)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var ret = SiteSettingBLL.CloseSoom(IdUser, Id, Status);

            return Json(new { status = ret.Length == 0, msg = ret });
        }
        [HttpPost]
        public JsonResult SoomIsOpen(int IdShip)
        {
            int IdUser = int.Parse(User.Identity.Name);

            return Json(SiteSettingBLL.SoomIsOpen(IdUser, IdShip));
        }




        [HttpPost]
        public JsonResult ChekerOpenSoom(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);

            var tt = SiteSettingBLL.GetListSoom(IdUser).First(v => v.Id == Id);

            return Json(tt.IsActive);
        }

        [HttpPost]
        public JsonResult ChekerOpenSafarInSoom(int Id)
        {
            int IdUser = int.Parse(User.Identity.Name);
            string msg = "";

            using (var x = new GolatehEntities())
            {
                var ff = x.Soom.AsNoTracking().First(v => v.Id == Id && v.IdPersonnel == IdUser);

                if (ff == null || ff.Safar.Any(c => c.IsActive))
                {
                    msg = "سفر ها در این صوم بسته نشده اند";
                }

                if (ff.IsActive)
                {
                    msg = "برای مشاهده محاسبات باید صوم را ببندید";
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


            int IdUser = int.Parse(User.Identity.Name);
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


            int IdUser = int.Parse(User.Identity.Name);
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





        public class SoomGolateMaster
        {
            public string FullName { get; set; }
            public string Semat { get; set; }
            public string IdPernonnel { get; set; }
            public string NameSoom { get; set; }
            public string NameLeng { get; set; }
            public string DateStart { get; set; }
            public string DateEnd { get; set; }
            public string DarsadLeng { get; set; }
            public string TedadGolate { get; set; }
            public string Title { get; set; }
        }

        public class SoomGolateDeteil
        {

            public string NameSafar { get; set; }
            public string IdSafar { get; set; }
            public string TedadSayad { get; set; }
            public string TedadGolate { get; set; }
            public string MablagForush { get; set; }
            public string MablagharGolate { get; set; }
            public string SahmSayad { get; set; }
            public string Sahmtabakhi { get; set; }
            public string SahAzLang { get; set; }
            public string Kasriha { get; set; }
            public string NakhalesDaryafti { get; set; }
            public string khalesDaryafti { get; set; }
            
        }



        public List<StiReport> MultiPrint(string id)
        {
            List<StiReport> lst = new List<StiReport>();

            using (var x = new GolatehEntities())
            {
                var aaa = x.Safar.AsNoTracking().First(v => v.IdSoom.ToString() == id && v.IsActive == false);
                var bbbb = x.Soom.AsNoTracking().First(v => v.Id.ToString() == id && v.IsActive == false);


                var Master = x.Database.SqlQuery<SoomGolateMaster>("EXEC dbo.PrintSoomGolateMaster @IdSoom = " + id).ToList();



                foreach (var item in Master)
                {
                    StiReport itm = new StiReport();
                    itm.Load(Server.MapPath(@"~/Content/Mrt/SoomGolate.mrt"));

                    var DeteilData = x.Database.SqlQuery<SoomGolateDeteil>("EXEC dbo.PrintSoomGolateDeteil @IdSoom = " + id + ",@IdPernonnel = " + item.IdPernonnel).ToList();

                    itm.RegData("MasterData", item);
                    itm.RegData("DeteilData", DeteilData);

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