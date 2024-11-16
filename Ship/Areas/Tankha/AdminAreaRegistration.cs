using System.Web.Mvc;

namespace Ship.Areas.Tankha
{
    public class TankhaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tankha";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tankha_default",
                "Tankha/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "Ship.Areas.Tankha.Controllers" }
            );
        }
    }
}