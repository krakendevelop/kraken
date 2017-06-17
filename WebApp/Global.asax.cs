using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace WebApp
{
  public class MvcApplication : System.Web.HttpApplication
  {
    private static readonly ILog Logger = LogManager.GetLogger(typeof(MvcApplication));

    protected void Application_Start()
    {
      Logger.Debug("Starting WebApp...");

      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      Logger.Debug("WebApp started");
    }

    protected void Application_Error()
    {
      var ex = Server.GetLastError();
      Logger.Error(ex);
    }
  }
}