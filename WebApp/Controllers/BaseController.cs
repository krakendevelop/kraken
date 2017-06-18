using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Tests.Repos;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    private static readonly IRatingRepo RatingRepo = new TestRatingRepo();

    protected static PostManager PostManager = new PostManager(new TestPostRepo(), RatingRepo);
    protected static CommentManager CommentManager = new CommentManager(new TestCommentRepo(), RatingRepo);

    protected BaseController()
    {
    }

    protected int CurrentUserId
    {
      get { return 0; }
    }
  }
}