using System.Web;

namespace Uniques.Library.Utilities
{
    public static class HttpContextIPHelper
    {
        public static string GetUserIP()
        {
            string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
