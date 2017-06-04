using System.Collections.Generic;
using System.IO;
using System.Web;
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
      var nextPosts = PostManager.GetAll(pageIndex * pageSize, pageSize);
      return Json(nextPosts, JsonRequestBehavior.AllowGet);
    }
  }
}