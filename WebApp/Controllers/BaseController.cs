using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using BusinessLogic.Users;
using BusinessLogic.Users.Auth;
using Common.Exceptions;
using Tests.Repos;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    private static readonly IRatingRepo RatingRepo = new TestRatingRepo();

    protected static PostManager PostManager = new PostManager(new PostRepo(), RatingRepo);
    protected static CommentManager CommentManager = new CommentManager(new TestCommentRepo(), RatingRepo);
    protected static AuthUserManager AuthManager = new AuthUserManager(new TestAuthUserRepo());
    protected static UserManager UserManager = new UserManager(new TestUserRepo());

    protected User CurrentUser { get; private set; }

    protected BaseController()
    {
    }

    protected void ProcessSuccesfulLogin(AuthUser authUser)
    {
      var user = UserManager.Get(authUser.Id);

      if (user == null)
        throw new KrakenException("User signed in sucessfully but couldn't load their profile Id: " + authUser.Id);

      CurrentUser = user;
    }

    protected void ProcessLogOut()
    {
      if (CurrentUser == null)
        throw new KrakenException("Loging out and user is null");

      CurrentUser = null;
    }
  }
}