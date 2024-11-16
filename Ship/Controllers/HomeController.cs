using Ship.Model.BLL.Admin;
using Ship.Model.Db;
using Ship.Model.ViewModel;

using System;
using System.Linq;
using System.Web.Mvc;

namespace Ship.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [Route("Agreement")]
        public ActionResult TermsAndConditions()
        {
            return View();
        }


        [Route("Activate")]
        public ActionResult Activate(string id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult joint(string email)
        {
            if (IsValidEmail(email) == false)
            {
                return Json("0");
            }
            return Json(SiteSettingBLL.joint(email));
        }

        [HttpPost]
        public JsonResult ContactAdd(string name, string email, string subject, string message)
        {
            if (IsValidEmail(email) == false)
            {
                return Json("0");
            }
            return Json(SiteSettingBLL.ContactAdd(name, email, subject, message));
        }


        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public JsonResult Register2(string firstname, string lastname, string nationalcode, string password, string mobile)
        {
            var row2 = SiteSettingBLL.Register(firstname, lastname, nationalcode, password, mobile);

            if (row2 == "1")
            {
                SiteSettingBLL.Login(nationalcode, password, 0);

                return Json(new { status = true, msg = "" });
            }
            if (row2 == "0")
            {
                return Json(new { status = false, msg = "خطا در ثبتنام" });
            }
            else
            {
                return Json(new { status = false, msg = row2 });
            }
        }






       

    }
}
