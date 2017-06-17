using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Tests.Repos;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    protected static PostManager PostManager = new PostManager(new TestPostRepo(), new TestRatingRepo());
    protected static CommentManager CommentManager = new CommentManager(new TestCommentRepo(), new TestRatingRepo());

    protected BaseController()
    {
    }

    protected int CurrentUserId
    {
      get { return 0; }
    }
  }
}