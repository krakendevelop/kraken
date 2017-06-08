using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Posts;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    protected PostManager PostManager;
    protected CommentManager CommentManager;

    protected BaseController()
    {
      PostManager = new PostManager();
      CommentManager = new CommentManager();
    }

    protected int CurrentUserId
    {
      get { return 0; }
    }
  }
}