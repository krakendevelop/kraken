using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Common;
using Common.Exceptions;
using Common.Serialization;
using log4net;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class PostController : BaseController
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public ActionResult Get(int postId)
    {
      Logger.DebugFormat("User {0} requested post with Id {1}", "Anonymous", postId);

      var post = PostManager.Get(postId);
      var model = BuildModel(post);

      Logger.DebugFormat("Responding with PostModel: {0}", model.ToJson());
      return Json(model, JsonRequestBehavior.AllowGet);
    }

    public ActionResult GetNext(int pageIndex, int pageSize)
    {
      Logger.DebugFormat("User {0} requested posts for pageIndex: {1} and size: {2}",
        "Anonymous", pageIndex, pageSize);

      var postModels = PostManager
        .GetAll(pageIndex * pageSize, pageSize)
        .Select(BuildModel)
        .ToList();

      Logger.DebugFormat("Responding with {0} PostModels", postModels.Count);
      return Json(postModels, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public ActionResult Post(string text, string imageUrl, int? communityId)
    {
      Logger.DebugFormat("User {0} requsted to post", CurrentUser.Id);
      PostManager.Create(CurrentUser.Id, text, imageUrl);
      return Json(1, JsonRequestBehavior.DenyGet);
    }

    [HttpPost]
    public ActionResult Like(int postId)
    {
      Logger.DebugFormat("User {0} requsted to like Post {1}", CurrentUser.Id, postId);
      PostManager.Like(CurrentUser.Id, postId);
      return Json(1, JsonRequestBehavior.DenyGet);
    }

    [HttpPost]
    public ActionResult Dislike(int postId)
    {
      Logger.DebugFormat("User {0} requsted to dislike Post {1}", CurrentUser.Id, postId);
      PostManager.Dislike(CurrentUser.Id, postId);
      return Json(1, JsonRequestBehavior.DenyGet);
    }

    private static PostModel BuildModel(Post post)
    {
      var model = new PostModel(post);

      var likes = 0;
      var dislikes = 0;

      // todo vkoshman move switch to extension method?
      var ratings = PostManager.GetRatings(post.Id);
      foreach (var rating in ratings)
      {
        switch (rating.KindId)
        {
          case RatingKindId.Like:
            likes++;
            break;
          case RatingKindId.Dislike:
            dislikes++;
            break;
          default:
            throw new KrakenException(KrakenExceptionCode.Rating_IsUnknown,
              "Rating is not supposed to be unknown at this point");
        }
      }

      var user = UserManager
        .Get(post.UserId)
        .AssertNotNull();

      var commentsModel = CommentManager
        .GetAll(post.Id)
        .Select(CommentController.BuildModel)
        .ToList();

      Logger.DebugFormat("Post {0}(UserId:{1}; {2}l/{3}d) contains {4} comments",
        post.Id, post.UserId, likes, dislikes, commentsModel.Count);

      return model
        .FillUpCommentsModel(commentsModel)
        .FillUpUserModel(new PartialUserModel(user))
        .FillUpRatings(likes, dislikes);
    }
  }
}