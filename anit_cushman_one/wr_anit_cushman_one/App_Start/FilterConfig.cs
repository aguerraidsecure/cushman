using System.Web;
using System.Web.Mvc;

namespace wr_anit_cushman_one
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
