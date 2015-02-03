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
               Include("~/Content/semanticUI/components/*.js",
               "~/Content/semanticUI/semantic.js"));


            BundleTable.Bundles.Add(new StyleBundle("~/Content/semantic").
               Include("~/Content/semanticUI/*.css"));
        }
    }
}