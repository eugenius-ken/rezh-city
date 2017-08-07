using System.Web;
using System.Web.Optimization;

namespace RezhCity.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/fileinput").Include(
                      "~/Content/bootstrap-fileinput/css/fileinput*"));
            //bundles.Add(new StyleBundle("~/Content/flexslider").Include(
            //          "~/Content/flexslider/flexslider.css",
            //          "~/Content/flexslider/flexslider.less"));
            #endregion

            #region js
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
            "~/Scripts/jquery.inputmask/inputmask.js",
            "~/Scripts/jquery.inputmask/jquery.inputmask.js"));
            //"~/Scripts/jquery.inputmask/inputmask.extensions.js",
            //"~/Scripts/jquery.inputmask/inputmask.date.extensions.js",
            //"~/Scripts/jquery.inputmask/inputmask.numeric.extensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
            "~/Scripts/fileinput*",
            "~/Scripts/locales/ru.js"));

            bundles.Add(new ScriptBundle("~/bundles/flexslider").Include(
            "~/Scripts/jquery.flexslider-min.js"));
            #endregion
        }
    }
}
