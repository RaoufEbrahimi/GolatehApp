using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace Ship
{
    public class BundleConfig
    {
 
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            BundleResolver.Current = new CustomBundleResolver();




            var AdminGolatehJS = new Bundle("~/bundles/js/AdminGolatehJS");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/js/bootstrap.bundle.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/js/jquery.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/simplebar/js/simplebar.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/metismenu/js/metisMenu.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/perfect-scrollbar/js/perfect-scrollbar.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/highcharts.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/highcharts-more.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/variable-pie.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/solid-gauge.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/highcharts-3d.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/cylinder.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/funnel3d.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/exporting.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/export-data.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/js/accessibility.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/js/index4.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/datatable/js/jquery.dataTables.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/datatable/js/dataTables.bootstrap5.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/js/app.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/notifications/js/lobibox.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/notifications/js/notifications.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/notifications/js/notification-custom-script.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/jquery-confirm.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/persian-date.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/persian-datepicker.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/select2/js/select2.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/apexcharts-bundle/js/apexcharts.min.js");
            AdminGolatehJS.Include("~/Content/theme/admin/Syn/vertical/assets/js/pace.min.js");
            AdminGolatehJS.Builder = nullBuilder;
            AdminGolatehJS.Transforms.Add(scriptTransformer);
            AdminGolatehJS.Orderer = nullOrderer;
            bundles.Add(AdminGolatehJS);



            var AdminGolatehCSS = new Bundle("~/bundles/css/AdminGolatehCSS");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/simplebar/css/simplebar.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/perfect-scrollbar/css/perfect-scrollbar.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/metismenu/css/metisMenu.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/highcharts/css/highcharts.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/pace.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/bootstrap.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/app.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/icons.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/dark-theme.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/semi-dark.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/css/header-colors.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/datatable/css/dataTables.bootstrap5.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/notifications/css/lobibox.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/select2/css/select2.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/select2/css/select2-bootstrap4.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/simplebar/css/simplebar.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/metismenu/css/metisMenu.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/perfect-scrollbar/css/perfect-scrollbar.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/jquery-confirm.min.css");
            AdminGolatehCSS.Include("~/Content/theme/admin/Syn/vertical/assets/plugins/persian-datepicker.css");
            AdminGolatehCSS.Builder = nullBuilder;
            AdminGolatehCSS.Transforms.Add(styleTransformer);
            AdminGolatehCSS.Orderer = nullOrderer;
            bundles.Add(AdminGolatehCSS);






















            var SiteGolatehJS = new Bundle("~/bundles/js/SiteGolatehJS");
            SiteGolatehJS.Include("~/Content/theme/site/js/vendor/modernizr-3.5.0.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/vendor/jquery-1.12.4.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/popper.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/bootstrap.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/slick.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/one-page-nav-min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/jquery.waterwheelCarousel.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/jquery.counterup.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/jquery.waypoints.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/wow.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/jquery.scrollUp.min.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/main.js");
            SiteGolatehJS.Include("~/Content/theme/site/js/toastr.min.js");
            SiteGolatehJS.Builder = nullBuilder;
            SiteGolatehJS.Transforms.Add(scriptTransformer);
            SiteGolatehJS.Orderer = nullOrderer;
            bundles.Add(SiteGolatehJS);



            var SiteGolatehCSS = new Bundle("~/bundles/css/SiteGolatehCSS");
            SiteGolatehCSS.Include("~/Content/theme/site/css/bootstrap.min.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/animate.min.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/fontawesome-all.min.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/flaticon.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/slick.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/default.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/style.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/responsive.css");
            SiteGolatehCSS.Include("~/Content/theme/site/css/toastr.min.css");

            SiteGolatehCSS.Builder = nullBuilder;
            SiteGolatehCSS.Transforms.Add(styleTransformer);
            SiteGolatehCSS.Orderer = nullOrderer;
            bundles.Add(SiteGolatehCSS);




            BundleTable.EnableOptimizations = true;
        }
    }
}
