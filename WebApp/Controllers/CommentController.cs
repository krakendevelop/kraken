using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Ratings;
using Common;
using Common.Exceptions;
using Common.Serialization;
using log4net;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class CommentController : BaseController
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public ActionResult GetByPostId(int postId)
    {
      Logger.DebugFormat("User {0} requested comments for post with Id {1}", "Anonymous", postId);

      var commentModels = CommentManager
        .GetAll(postId)
        .Select(BuildModel)
        .ToList();

      Logger.DebugFormat("Responding with CommentModels: {0}", commentModels.ToJson());
      return Json(commentModels, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public ActionResult Reply(int postId, string text, string imageUrl, int? commentId = null)
    {
      Logger.DebugFormat("User {0} requested to reply to post: {1}(comment: {2})", CurrentUser.Id, postId, commentId);
      CommentManager.Create(CurrentUser.Id, postId, text, imageUrl, commentId);
      return Json(1, JsonRequestBehavior.DenyGet);
    }

    [HttpPost]
    public ActionResult Like(int commentId)
    {
      Logger.DebugFormat("User {0} requsted to like Comment {1}", CurrentUser.Id, commentId);
      CommentManager.Like(CurrentUser.Id, commentId);
      return Json(1, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public ActionResult Dislike(int commentId)
    {
      Logger.DebugFormat("User {0} requsted to dislike Comment {1}", CurrentUser.Id, commentId);
      CommentManager.Dislike(CurrentUser.Id, commentId);
      return Json(1, JsonRequestBehavior.AllowGet);
    }

    public static CommentModel BuildModel(Comment comment)
    {
      var model = new CommentModel(comment);

      var likes = 0;
      var dislikes = 0;
      var ratings = CommentManager.GetRatings(comment.Id);
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
        .Get(comment.UserId)
        .AssertNotNull();

      return model
        .FillUpUserModel(new PartialUserModel(user))
        .FillUpRatings(likes, dislikes);
    }
  }
}