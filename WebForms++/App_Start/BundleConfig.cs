using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WFPlusPlusBiggy
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            //I added this for the datatables css
            bundles.Add(new StyleBundle("~/Content/DataTablesCSS").Include(
                        "~/Content/DataTables/css/dataTables.bootstrap.min.css"));

            //I added this for datatables js
            bundles.Add(new ScriptBundle("~/bundles/DataTablesJs").Include(
                 "~/Scripts/DataTables/jquery.dataTables.min.js",
                 "~/Scripts/DataTables/dataTables.bootstrap.min.js"
                 ));

            //I added this script to handle datatables for the main grid
            bundles.Add(new ScriptBundle("~/bundles/MyBiggyDataTablesJs").Include(
                 "~/Scripts/MyBiggyDataTables.js"));

            //I added this script to get rid of the 404 error
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                 "~/Scripts/WebForms/WebForms.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}