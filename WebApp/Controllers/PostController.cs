using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Common.Exceptions;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class PostController : BaseController
  {
    public ActionResult LoadNextPosts(int pageIndex, int pageSize)
    {
      var nextPosts = new List<PostModel>();
      foreach (var post in PostManager.GetAll(pageIndex * pageSize, pageSize))
      {
        var postModel = new PostModel(post);
        //var ratings = PostManager.GetRatings(post.Id); todo vkoshman
        var ratings = new List<Rating>
        {
          new Rating(1, RatingKindId.Like, RatingTargetKindId.Post, post.Id),
          new Rating(1, RatingKindId.Like, RatingTargetKindId.Post, post.Id),
          new Rating(1, RatingKindId.Dislike, RatingTargetKindId.Post, post.Id),
        };

        // todo vkoshman move this to business logic
        ratings.ForEach(r =>
        {
          switch (r.KindId)
          {
            case RatingKindId.Like:
              postModel.LikeCount++;
              break;
            case RatingKindId.Dislike:
              postModel.DislikeCount++;
              break;
            default:
              throw new KrakenException(KrakenExceptionCode.Rating_IsUnknown,
                "Rating is not supposed to be unknown at this point");
          }
        });

        nextPosts.Add(postModel);
      }

      return Json(nextPosts, JsonRequestBehavior.AllowGet);
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