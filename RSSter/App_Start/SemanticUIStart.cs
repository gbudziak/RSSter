using System.Web;
using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(
      typeof(RSSter.SemanticUiStart), "PostStart")]


namespace RSSter
{
    public static class SemanticUiStart
    {
        public static void PostStart()
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/semantic").
               Include("~/Scripts/dimmer.min.js",
                       "~/Scripts/semantic.js",
                       "~/Scripts/semantic.site.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/semantic").
               Include("~/Content/checkbox.min.css",
                       "~/Content/dimmer.min.css",
                       "~/Content/icon.min.css",
                       "~/Content/card.min.css",
                       "~/Content/grid.min.css",
                       "~/Content/semantic.css",
                       "~/Content/semantic.site.css"));
        }
    }
}