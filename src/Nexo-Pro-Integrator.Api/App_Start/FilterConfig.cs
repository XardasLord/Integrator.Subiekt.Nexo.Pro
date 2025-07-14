using System.Web;
using System.Web.Mvc;

namespace Nexo_Pro_Integrator.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
