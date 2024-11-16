using System.Web.Configuration;

namespace Ship.Model.ViewModel
{

    public class PayRequestZarinPal
    {
        public string IMEI { get; set; }
        public string Authority { get; set; }
        public string Adress { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
    }
    public class VerifyZarinPalViewModel
    {
        public string Method { get; set; }
        public string IMEI { get; set; }
        public string Status { get; set; }
        public string Msg { get; set; }
    }


    public static class SiteSettings
    {
        //public static string Logo
        //{
        //    get
        //    {
        //        int Idlang = 0;

        //        HttpCookie cookie = HttpContext.Current.Request.Cookies["language"];
        //        if (cookie != null && cookie.Value != null)
        //            Idlang = SiteSettingBLL.GetIdLangFromName(cookie.Value);
        //        else
        //            Idlang = 1;

        //        return SiteSettingBLL.GetListSiteSetting(Idlang).First().Logo;
        //    }
        //}

        //[Display(Name = "StepTitle2", ResourceType = typeof(Resources.StaticTitle))]
        //public static string SiteName
        //{
        //    get
        //    {
        //        int Idlang = 0;

        //        HttpCookie cookie = HttpContext.Current.Request.Cookies["language"];
        //        if (cookie != null && cookie.Value != null)
        //            Idlang = SiteSettingBLL.GetIdLangFromName(cookie.Value);
        //        else
        //            Idlang = 1;

        //        return SiteSettingBLL.GetListSiteSetting(Idlang).First().Title;
        //    }
        //}

        public static string CDN
        {
            get
            {
                return WebConfigurationManager.AppSettings["CdnUrl"].ToString();
            }
        }
    }



    public class SocialShereViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Adress { get; set; }
    }
}

