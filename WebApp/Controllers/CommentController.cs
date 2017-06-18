using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Comments;
using BusinessLogic.Ratings;
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

    public static CommentModel BuildModel(Comment comment)
    {
      var model = new CommentModel(comment);

      var ratings = CommentManager.GetRatings(comment.Id);
      foreach (var rating in ratings)
      {
        switch (rating.KindId)
        {
          case RatingKindId.Like:
            model.LikeCount++;
            break;
          case RatingKindId.Dislike:
            model.DislikeCount++;
            break;
          default:
            throw new KrakenException(KrakenExceptionCode.Rating_IsUnknown,
              "Rating is not supposed to be unknown at this point");
        }
      }

      return model;
    }
  }
}