using System.Web.Mvc;
using BusinessLogic.Posts;

namespace WebApp.Controllers
{
  public class PostController : BaseController
  {
    public ActionResult LoadNextPosts(int pageIndex, int pageSize)
    {
      var nextPosts = PostManager.GetAll(pageIndex * pageSize, pageSize);
      return Json(nextPosts, JsonRequestBehavior.AllowGet);
    }

    public ActionResult Like(int postId)
    {
      //PostManager.Like(CurrentUserId, postId);
      return Json(1, JsonRequestBehavior.AllowGet);
    }

    public ActionResult Dislike(int postId)
    {
      //PostManager.Dislike(CurrentUserId, postId);
      return Json(1, JsonRequestBehavior.AllowGet);
    }
  }
}