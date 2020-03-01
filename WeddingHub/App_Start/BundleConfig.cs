using System.Web;
using System.Web.Optimization;

namespace WeddingHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/AdminStyles/js/bootstrap.min.js",
                      "~/AdminStyles/js/jquery.nice-select.min.js",
                      "~/AdminStyles/js/menumaker.min.js",
                      "~/AdminStyles/js/fastclick.js",
                      "~/AdminStyles/js/return-to-top.js",
                      "~/AdminStyles/js/custom-script.js",
                      "~/AdminStyles/js/bootstrap.min.js",
                      "~/AdninStyles/js/jquery.min.js"
                   
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/AdminStyles/css/bootstrap.min.css",
                  
                      "~/AdminStyles/fontawesome/css/fontawesome-all.css",
                      "~/AdminStyles/fontello/css/fontello.css",
                      "~/AdminStyles/css/owl.carousel.css",
                      "~/AdminStyles/css/owl.theme.default.css",
       
                      "~/AdminStyles/css/style.css"
           ));
        }
    }
}
