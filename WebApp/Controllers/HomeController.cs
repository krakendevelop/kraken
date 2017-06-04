using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic.Posts;

namespace WebApp.Controllers
{
  public class HomeController : Controller
  {
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult LoadNextPosts(int pageIndex, int pageSize)
    {
      var nextPosts = new List<Post>()
      {
        new Post(1, "Test title", "Test content")
      };

      return Json(nextPosts, JsonRequestBehavior.AllowGet);
    }
  }
}