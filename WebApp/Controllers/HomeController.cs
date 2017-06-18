using System.Web.Mvc;

namespace WebApp.Controllers
{
  public class HomeController : BaseController
  {
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }

    [Authorize(Roles="Admin")]
    public ActionResult Admin()
    {
      return View();
    }
  }
}