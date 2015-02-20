using System.Web.Http;
using System.Web.Routing;

namespace TriviaMobileService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}