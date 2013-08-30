using System.Web.Optimization;

namespace ABuilder
{
    
	public class BundleConfigs
	{
		public static void RegisterBundles()
		{
			
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/knockout").Include("~/Scripts/knockout-*").Include("~/Scripts/knockout.mapping-latest.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
		}
	}
}
