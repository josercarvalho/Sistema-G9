using System.Web;
using System.Web.Optimization;

namespace SistemaG9.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.mask.js",
                        "~//Scripts/MyJS/example.js",
                        "~/Scripts/MyJS/methods_pt.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerysteps").Include("~/Scripts/jquery.steps.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/twitter-bootstrap-hover-dropdown.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap-Flatly.css",
                        "~/Content/AdminLTE.css",
                        "~/Content/font-awesome.css",
                        "~/Content/ionicons.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/jquerysteps").Include("~/Content/jquery.steps.css"));
        }
    }
}
