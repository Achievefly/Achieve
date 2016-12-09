using System.Web;
using System.Web.Optimization;

namespace AchieveManageWeb
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            //这里可以做js和css文件压缩，压缩前所有图片文件地址必须改成绝对路径，太麻烦，暂时放弃
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/Form/css").Include(
                "~/Content/NFineUI/css/framework-font.css",
                "~/Content/NFineUI/css/framework-theme.css",
                "~/Content/NFineUI/js/bootstrap/bootstrap.min.css",
                "~/Content/NFineUI/js/wdtree/tree.css",
                "~/Content/NFineUI/js/select2/select2.min.css",
                "~/Content/NFineUI/js/wizard/wizard.css",
                "~/Content/NFineUI/css/framework-ui.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/Form/js").Include(
                "~/Content/NFineUI/js/jquery/jquery-2.1.1.min.js",
                "~/Content/NFineUI/js/bootstrap/bootstrap.js",
                "~/Content/NFineUI/js/wdtree/tree.js",
                "~/Content/NFineUI/js/select2/select2.min.js",
                "~/Content/NFineUI/js/wizard/wizard.js",
                "~/Content/NFineUI/js/validate/jquery.validate.min.js",
                "~/Content/NFineUI/js/datepicker/WdatePicker.js",
                "~/Content/NFineUI/js/framework-ui.js",
                "~/Content/NFineUI/js/framework-form.js"
                ));
            bundles.Add(new StyleBundle("~/Content/Index/css").Include(
                "~/Content/NFineUI/css/framework-font.css",
                "~/Content/NFineUI/css/framework-theme.css",
                "~/Content/NFineUI/js/bootstrap/bootstrap.min.css",
                "~/Content/NFineUI/js/jqgrid/jqgrid.css",
                "~/Content/NFineUI/js/select2/select2.min.css",
                "~/Content/NFineUI/css/framework-ui.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/Index/js").Include(
                "~/Content/NFineUI/js/jquery/jquery-2.1.1.min.js",
                "~/Content/NFineUI/js/bootstrap/bootstrap.js",
                "~/Content/NFineUI/js/select2/select2.min.js",
                "~/Content/NFineUI/js/jqgrid/jqgrid.min.js",
                "~/Content/NFineUI/js/validate/jquery.validate.min.js",
                "~/Content/NFineUI/js/jqgrid/grid.locale-cn.js",
                "~/Content/NFineUI/js/framework-ui.js",
                "~/Content/NFineUI/js/framework-form.js"
                ));
            bundles.Add(new StyleBundle("~/Content/Layout/css").Include(
                "~/Content/NFineUI/css/framework-font.css",
                "~/Content/NFineUI/css/framework-theme.css",
                "~/Content/NFineUI/js/bootstrap/bootstrap.min.css",
                "~/Content/NFineUI/js/wdtree/tree.css",
                "~/Content/NFineUI/js/jqgrid/jqgrid.css",
                "~/Content/NFineUI/css/framework-ui.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/Layout/js").Include(
                "~/Content/NFineUI/js/jquery/jquery-2.1.1.min.js",
                "~/Content/NFineUI/js/jquery-ui/jquery-ui.min.js",
                "~/Content/NFineUI/js/bootstrap/bootstrap.js",
                "~/Content/NFineUI/js/layout/jquery.layout.js",
                "~/Content/NFineUI/js/wdtree/tree.js",
                "~/Content/NFineUI/js/jqgrid/jqgrid.min.js",
                "~/Content/NFineUI/js/jqgrid/grid.locale-cn.js",
                "~/Content/NFineUI/js/framework-ui.js",
                "~/Content/NFineUI/js/framework-form.js"
                ));
        }
    }
}