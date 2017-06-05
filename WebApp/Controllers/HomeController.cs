using System.Web.Mvc;
using BusinessLogic.Posts;

namespace WebApp.Controllers
{
  public class HomeController : BaseController
  {
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }
  }
}