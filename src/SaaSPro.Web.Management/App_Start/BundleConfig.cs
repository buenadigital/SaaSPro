using System.Web.Optimization;

namespace SaaSPro.Web.Management
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-table/bootstrap-table.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-{version}.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/themes/base/autocomplete.css",
                    "~/Content/bootstrap-responsive.css",
                    "~/Content/bootstrap-table.min.css",
                    "~/Content/morris.css",
                    "~/Content/styles.css"));
		}
	}
}