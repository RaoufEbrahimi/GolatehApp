using Ship.Model.BLL.Admin;
using System.Web.Mvc;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class PersonnelManagementController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public PartialViewResult GetListPersonnelManagement()
        {
            var xc = SiteSettingBLL.GetListPersonnelManagement();
            return PartialView("~/Areas/Admin/Views/PersonnelManagement/_List.cshtml", xc);
        }


        [HttpPost]
        public JsonResult PersonnelManagementSubmit(string FirstName, string LastName, string UserName, string Password)
        {
            return Json(SiteSettingBLL.PersonnelManagementSubmit(FirstName, LastName, UserName, Password));
        }

        [HttpPost]
        public JsonResult PersonnelManagementDelete(int Id)
        {
            return Json(SiteSettingBLL.PersonnelManagementDelete(Id));
        }

        
        [HttpPost]
        public JsonResult ChangeStatusPersonnel(int Id, bool IsActive)
        {
            return Json(SiteSettingBLL.ChangeStatusPersonnel(Id, IsActive));
        }
        [HttpPost]
        public JsonResult ChangeStatusPersonnelArticle(int Id, bool IsActive)
        {
            return Json(SiteSettingBLL.ChangeStatusPersonnelArticle(Id, IsActive));
        }
    }
}