using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new Bundle("~/bundles/js").Include(
                       "~/Scripts/js-template.js"));

            bundles.Add(new Bundle("~/bundles/jqueryajax").Include(
                       "~/Scripts/jquery.unobtrusive*"));


            bundles.Add(new Bundle("~/bundles/sweetalert").Include(
                       "~/Scripts/sweetalert.min.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/styles-template.css",
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert.css",
                      "~/Content/signin.css"));
        }
    }
}
