using System.Web;
using System.Web.Optimization;

namespace QST.ERP.WebApi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                            "~/ngClient/app/scripts/vendor/jquery.js",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Style/css").Include(
                      "~/ngClient/app/styles/bootstrap.css",
                      "~/ngClient/app/styles/style.css",
                      "~/ngClient/app/styles/font-awesome.css",
                      "~/ngClient/app/styles/autocomplete.css",
                      "~/ngClient/app/styles/ui-bootstrap-csp.css",
                      "~/ngClient/app/styles/simple-sidebar.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/QST")
                .Include("~/ngClient/app/scripts/vendor/angular.js",
                "~/ngClient/app/scripts/vendor/angular-route.js",
                "~/ngClient/app/scripts/vendor/angular-messages.min.js",
                "~/ngClient/app/scripts/vendor/angular-resource.js",
                "~/ngClient/app/scripts/vendor/angular-animate.js",
                "~/ngClient/app/scripts/vendor/angular-sanitize.js",
                "~/ngClient/app/scripts/vendor/ui-bootstrap-tpls-1.3.3.min.js",
                
                "~/ngClient/app/scripts/vendor/ngToast.js")
                
                .IncludeDirectory("~/ngClient/app/scripts/vendor", "*.js")
                .Include("~/ngClient/app/scripts/app.js")
                .IncludeDirectory("~/ngClient/app/scripts/directives", "*.js")
                .IncludeDirectory("~/ngClient/app/scripts/services", "*.js")
                .IncludeDirectory("~/ngClient/app/scripts/controllers", "*.js")
                
                );
           
        }
    }
}