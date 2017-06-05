using System.Web.Mvc;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    protected int CurrentUserId
    {
      get { return 0; }
    }
  }
}