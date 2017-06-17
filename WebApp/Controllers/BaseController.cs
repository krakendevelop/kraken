using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Tests.Repos;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    protected PostManager PostManager;
    protected CommentManager CommentManager;

    protected BaseController()
    {
      PostManager = new PostManager(new TestPostRepo(), new RatingRepo());
      CommentManager = new CommentManager(new CommentRepo(), new RatingRepo());
    }

    protected int CurrentUserId
    {
      get { return 0; }
    }
  }
}