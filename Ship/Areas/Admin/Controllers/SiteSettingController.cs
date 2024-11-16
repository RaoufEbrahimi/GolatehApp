using Newtonsoft.Json;
using Ship.Model.BLL.Admin;
using Ship.Model.ViewModel.Admin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class SiteSettingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult ReadSiteSetting()
        {
            var xc = SiteSettingBLL.GetListSiteSetting().First();
            return PartialView("~/Areas/Admin/Views/SiteSetting/_List.cshtml", xc);
        }


        [HttpPost]
        public JsonResult SiteSettingSubmit(string model)
        {
            var result = JsonConvert.DeserializeObject<SiteSettingSaveViewModel>(model);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                result.Logo2 = file;
            }

            return Json(SiteSettingBLL.SiteSettingSubmit(result));
        }

    }
}