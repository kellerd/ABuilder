using System.Web.Optimization;
namespace ABuilder
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
			
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap*").Include("~/Scripts/app/App*"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap-responsive.css", "~/Content/bootstrap-docs.css"));
		}
	}
}
