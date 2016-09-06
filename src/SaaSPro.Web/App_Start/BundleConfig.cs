using System.Web.Optimization;

namespace SaaSPro.Web
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
            // Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            // Scripts
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/plugins")
                .Include("~/Scripts/bootstrap.js",
                          "~/Scripts/holder.min.js",
                          "~/Scripts/respond.js",
                          "~/Scripts/jquery-{version}.js",
                         "~/Scripts/bootstrap-datepicker.js",
                         "~/Scripts/toastr.js",
                         "~/Scripts/saaspro.js",
                         "~/Scripts/jquery.signalR-{version}.js"));
        }
    }
}