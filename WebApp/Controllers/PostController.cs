using System;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Common;
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
      Logger.DebugFormat("User {0} requested post with Id {1}",
        CurrentUser?.Id.ToString() ?? "Anomymous", postId);

      var post = PostManager
        .Get(postId)
        .AssertNotNull();

      var model = BuildModel(post);

      Logger.DebugFormat("Responding with PostModel: {0}", model.ToJson());
      return Json(model, JsonRequestBehavior.AllowGet);
    }

    public ActionResult GetNext(int pageIndex, int pageSize)
    {
      Logger.DebugFormat("User {0} requested posts for pageIndex: {1} and size: {2}",
        CurrentUser?.Id.ToString() ?? "Anomymous", pageIndex, pageSize);

      var idFrom = Math.Max(1, pageIndex * pageSize);
      var postModels = PostManager
        .GetNextHot(idFrom, pageSize)
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

      int likes;
      var dislikes = 0;
      
      PostManager
        .GetRatings(post.Id)
        .CalcRatings(out likes, out dislikes);

      /*var user = UserManager
        .Get(post.UserId)
        .AssertNotNull();*/

      var commentsModel = CommentManager
        .GetAllByPostId(post.Id)
        .Select(CommentController.BuildModel)
        .ToList();

      Logger.DebugFormat("Post {0}(UserId:{1}; {2}l/{3}d) contains {4} comments",
        post.Id, post.UserId, likes, dislikes, commentsModel.Count);

      return model
        .FillUpCommentsModel(commentsModel)
        //.FillUpUserModel(new PartialUserModel(user))
        .FillUpRatings(likes, dislikes);
    }
  }
}