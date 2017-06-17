using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Common.Exceptions;
using log4net;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class PostController : BaseController
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public ActionResult Get(int postId)
    {
      var post = PostManager.Get(postId);
      var model = BuildModel(post);

      return Json(model, JsonRequestBehavior.AllowGet);
    }

    public ActionResult GetNext(int pageIndex, int pageSize)
    {
      var nextPosts = PostManager
        .GetAll(pageIndex * pageSize, pageSize)
        .Select(BuildModel)
        .ToList();
      Logger.Debug("Success");
      return Json(nextPosts, JsonRequestBehavior.AllowGet);
    }

    private static PostModel BuildModel(Post post)
    {
      var model = new PostModel(post);

      // todo vkoshman read from DB
      var ratings = new List<Rating>
        {
          new Rating(1, RatingKindId.Like, RatingTargetKindId.Post, post.Id),
          new Rating(1, RatingKindId.Like, RatingTargetKindId.Post, post.Id),
          new Rating(1, RatingKindId.Dislike, RatingTargetKindId.Post, post.Id),
        };

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