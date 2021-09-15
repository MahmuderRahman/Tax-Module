using System;
using System.Web.Optimization;

namespace Test_Project
{
    public class BundleConfig
    {


        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*-vsdoc.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.min.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenEnabled);
        }

        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleTable.EnableOptimizations = false;
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.3.1.js",
                "~/Scripts/jquery-ui-1.12.10.js",
                "~/Scripts/lib/raphael-min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                //"~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/lib/Template/vendor/bootstrap/js/bootstrap.js",
                "~/Scripts/lib/Template/vendor/metisMenu/metisMenu.js",
                "~/Scripts/lib/Template/dist/js/sb-admin-2.js",
                "~/Scripts/bootstrap-notify.js",
                "~/Scripts/bootstrap-select.js",
                "~/Scripts/lib/AjaxManager.js",
                "~/Scripts/lib/JsHelper.js",
                "~/Scripts/site.js",
                "~/Scripts/lib/Notification/Messages.js",
                "~/Scripts/lib/Notification/notifit.js",
                "~/Scripts/lib/json2.js",
                "~/Scripts/lib/momentJs/moment.js",
                "~/Scripts/lib/momentJs/moment-timezone.js",
                "~/Scripts/lib/momentJs/moment-timezone-with-data.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Scripts/lib/Template/vendor/bootstrap/css/bootstrap.css",
                "~/Scripts/lib/Template/vendor/metisMenu/metisMenu.css",
                "~/Scripts/lib/Template/dist/css/sb-admin-2.css",
                "~/Scripts/lib/fontawesome-free-5.0.13/fontawesome-free-5.0.13/web-fonts-with-css/css/fontawesome-all.css",
                //"~/Scripts/lib/Template/vendor/font-awesome/css/font-awesome.css",
                "~/Content/lib/Notification/ValidationMsg.css",
                "~/Content/lib/Notification/notifIt.css",
                "~/Content/Site.css"
                ));




            #region DataTable

            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                "~/Scripts/lib/DataTables_1_0_13/media/js/jquery.dataTables.min.js",
                "~/Scripts/lib/DataTables_1_0_13/media/js/dataTables.bootstrap.min.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/FixedColumns/js/dataTables.fixedColumns.min.js",
                "~/Scripts/lib/DataTables_1_0_13/media/js/dataTables.rowsGroup.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Buttons/js/dataTables.buttons.min.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Buttons/js/buttons.bootstrap.min.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Export/jszip.min.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Export/pdfmake.min.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Export/vfs_fonts.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Buttons/js/buttons.html5.min.js",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Buttons/js/buttons.print.min.js"

                ));

            bundles.Add(new StyleBundle("~/CustomCSS/DataTable").Include(
                "~/Scripts/lib/DataTables_1_0_13/media/css/dataTables.bootstrap.min.css",
                "~/Scripts/lib/DataTables_1_0_13/extensions/FixedColumns/css/fixedColumns.bootstrap.min.css",
                "~/Scripts/lib/DataTables_1_0_13/extensions/Buttons/css/buttons.dataTables.min.css"
                ));

            #endregion


            #region Select Chosen

            bundles.Add(new ScriptBundle("~/bundles/SelectChosen").Include(
                "~/Scripts/lib/SelectChosen/js/chosen.jquery.min.js"
                ));

            bundles.Add(new StyleBundle("~/CustomCSS/SelectChosen").Include(
                "~/Scripts/lib/SelectChosen/css/chosen.css"
                ));

            #endregion


            #region DateTime Picker

            bundles.Add(new ScriptBundle("~/bundles/DateTimePicker").Include(
                "~/Scripts/lib/jquery.datetimepicker.js",
                "~/Scripts/lib/dateFormatter.js",
                "~/Scripts/lib/dateTimePickerLoader.js"
                ));

            bundles.Add(new StyleBundle("~/CustomCSS/DateTimePicker").Include(
                "~/Content/lib/jquery.datetimepicker.css"
                ));

            #endregion

        }
    }
}