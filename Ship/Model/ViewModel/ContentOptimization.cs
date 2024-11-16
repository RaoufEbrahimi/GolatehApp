using System.Linq;
using Ship.Model.BLL.Admin;

namespace Ship.Model.ViewModel
{
    public class ContentOptimization
    {
        public ContentOptimization(bool Main = true)
        {
            if (Main)
            {
                var SiteSet = SiteSettingBLL.GetListSiteSetting().First();

                SiteName = "SiteName";
                Locale = "fa_IR";
                Type = "website";

                Url = SiteSettings.CDN;
                Title = SiteSet.Title;
                Keywords = SiteSet.Keywords;
                Descption = SiteSet.Description;

                Author = SiteSet.Author;
                Abstract = SiteSet.Description;
                Copyright = System.DateTime.Now.Year.ToString();
                RevisitAfter = "3 days";
                Generator = "ASP.NET MVC";
                ImageIcon = SiteSet.Logo;
                IsUser = false;
            }
            else
            {
                SiteName = "SiteName";
                Locale = "fa_IR";
                Type = "website";

                Url = SiteSettings.CDN;
                Title = this.Title;
                Keywords = this.Keywords;
                Descption = this.Descption;

                Author = this.Author;
                Abstract = this.Abstract;
                Copyright = System.DateTime.Now.Year.ToString();
                RevisitAfter = "3 days";
                Generator = "ASP.NET MVC";
                ImageIcon = this.ImageIcon;
                IsUser = false;
            }
        }





        public bool IsUser { get; set; }
        public string SiteName { get; set; }
        public string Url { get; set; }
        public string Locale { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Descption { get; set; }
        public string Author { get; set; }
        public string Abstract { get; set; }
        public string Copyright { get; set; }


        public string ImageIcon { get; set; }


        public string RevisitAfter { get; set; }
        public string Generator { get; set; }
    }
}