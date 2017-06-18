﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
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

    private static PostModel BuildModel(Post post)
    {
      var model = new PostModel(post);

      var commentModels = CommentManager
        .GetAll(post.Id)
        .Select(CommentController.BuildModel)
        .ToList();

      Logger.DebugFormat("Post {0} contains {1} comments", post.Id, commentModels.Count);

      model.Comments = commentModels; // todo vkoshman not supposed to be here
      model.CommentCount = commentModels.Count; // todo v.koshman better way of counting comments

      var ratings = PostManager.GetRatings(post.Id);
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
      Logger.DebugFormat("User {0} requsted to like Post {1}", CurrentUser.Id, postId);
      PostManager.Like(CurrentUser.Id, postId);
      return Json(1, JsonRequestBehavior.AllowGet);
    }

    public ActionResult Dislike(int postId)
    {
      Logger.DebugFormat("User {0} requsted to dislike Post {1}", CurrentUser.Id, postId);
      PostManager.Dislike(CurrentUser.Id, postId);
      return Json(1, JsonRequestBehavior.AllowGet);
    }
  }
}