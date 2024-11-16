using Ship.Model.ViewModel.Site;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ship.Model.BLL.Admin;

namespace Ship.Areas.Admin.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private static readonly string[] _validExtensions = { ".jpg", ".jpeg", ".bmp", ".gif", ".png" }; //  etc

        [HttpPost]
        public JsonResult SubmitProfile(string FirstName, string LastName, string NationalCode, string Phone, string Password)
        {
            HttpPostedFileBase file = null;

            if (Request.Files["Image2"] != null)
            {
                file = Request.Files["Image2"];

                if (_validExtensions.Contains(System.IO.Path.GetExtension(file.FileName).ToLower()) == false)
                {
                    var t = new MessagePage() { Title = "خطا!", Body = "پسوند عکس مورد تایید نیست", Status = false };
                    return Json(t);
                }
            }

            int IdCustomer = int.Parse(User.Identity.Name);

            var Res = SiteSettingBLL.EditUser(FirstName, LastName, NationalCode, Phone, Password, IdCustomer, file);

            if (Res)
            {
                var t = new MessagePage() { Title = "موفق!", Body = "عملیات با موفقیت انجام شد", Status = true };
                return Json(t);
            }
            else
            {
                var t = new MessagePage() { Title = "خطا!", Body = "خطا", Status = false };
                return Json(t);
            }
        }
    }
}