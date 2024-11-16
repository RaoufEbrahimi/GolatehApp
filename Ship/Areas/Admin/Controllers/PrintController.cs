using Ship.Model.BLL.Admin;
using Ship.Model.Db;
using Ship.Model.Extension;
using Ship.Model.ViewModel.Admin;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class PrintController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public StiReport GetReport2(string from, string to, int type, int ship)
        {
            int IdUser = int.Parse(User.Identity.Name);


            StiReport report = new StiReport
            {
                ReportName = Guid.NewGuid().ToString()
            };
            report.Load(Server.MapPath(@"~/Content/Mrt/" + type + ".mrt"));

            var data = SiteSettingBLL.GetPrint(from, to, type, ship, IdUser) as List<Print1ViewModel>;
            report.RegData("dt", "dt", data);

            report.Dictionary.Variables["fromdate"].Value = from;
            report.Dictionary.Variables["todate"].Value = to;
            report.Dictionary.Variables["Date_Today"].Value = PersianCalander.ToShamsiTime(DateTime.Now).Substring(0, 16);

            using (var x = new GolatehEntities())
            {
                var ggt = x.Ship.AsNoTracking().First(b => b.Id == ship);

                report.Dictionary.Variables["ship"].Value = ggt.Title + "-" + ggt.Number;
            }

            report.Compile();
            report.Render();

            return report;
        }






        public ActionResult Export(string from, string to, int type, int ship)
        {
            from = from.ToEnNum();
            to = to.ToEnNum();

            var report = this.GetReport2(from, to, type, ship);

            var settings = new Stimulsoft.Report.Export.StiPdfExportSettings() { ImageQuality = 1.0f, ImageResolution = 300, EmbeddedFonts = true, UseUnicode = false, StandardPdfFonts = true };
            var service = new Stimulsoft.Report.Export.StiPdfExportService();
            var stream = new MemoryStream();

            service.ExportTo(report, stream, settings);

            return File(stream.ToArray(), "application/octet-stream", DateTime.Now.Ticks + ".pdf");
        }
    }
}