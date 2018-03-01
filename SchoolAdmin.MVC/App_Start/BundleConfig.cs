using System.Web;
using System.Web.Optimization;

namespace SchoolAdmin.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //#############################################################################################
            //#                                    START MATERIAL DESIGN
            //#############################################################################################

            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/material-css").Include(
                "~/Content/material-fonts.css",
                "~/Content/material-dashboard.css"));

           bundles.Add(new ScriptBundle("~/bundles/material-js").Include(
               "~/Scripts/material.min.js",
               "~/Scripts/chartlist.min.js",
               "~/Scripts/arrive.min.js",
               "~/Scripts/perfect-scrollbar.jquery.min.js",
                "~/Scripts/bootstrap.notify.js",
                "~/Scripts/material-dashboard.js",
                "~/Scripts/demo.js"));

            //#############################################################################################
            //#                                    END MATERIAL DESIGN
            //#############################################################################################

            //Flash messages
            bundles.Add(new ScriptBundle("~/bundles/flash-messages").Include(
                "~/Scripts/jQuery.flashMessage.js",
                "~/Scripts/jquery.cookie.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
