﻿using System.Web;
using System.Web.Optimization;

namespace Online_Shop
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/core").Include(
                      "~/Assets/client/css/bootstrap.css",
                      "~/Assets/client/css/font-awesome.min.css",
                      "~/Assets/client/css/bootstrap-theme.css",
                      "~/Assets/client/css/bootstrap-social.css",
                      "~/Assets/client/css/jquery-ui.css",
                      "~/Assets/client/css/style.css",
                      "~/Assets/client/css/slider.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jscore").Include(
                      "~/Assets/client/js/jquery-1.11.3.min.js",
                      "~/Assets/client/js/jquery-ui.js",
                      "~/Assets/client/js/bootstrap.min.js",
                      "~/Assets/client/js/move-top.js",
                      "~/Assets/client/js/easing.js",
                      "~/Assets/client/js/startstop-slider.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/controller").Include(
                      "~/Assets/client/js/controllers/baseController.js"
                      ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
