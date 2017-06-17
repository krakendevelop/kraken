using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Tests.Repos;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    protected static PostManager PostManager = new PostManager(new TestPostRepo(), new RatingRepo());
    protected static CommentManager CommentManager = new CommentManager(new CommentRepo(), new RatingRepo());

    protected BaseController()
    {
    }

    protected int CurrentUserId
    {
      get { return 0; }
    }
  }
}