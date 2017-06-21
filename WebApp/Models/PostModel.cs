using System.Collections.Generic;
using BusinessLogic.Posts;

namespace WebApp.Models
{
  public class PostModel : Post
  {
    public int LikeCount { get; private set; }
    public int DislikeCount { get; private set; }
    public int CommentCount { get; private set; }
    public List<CommentModel> CommentsModel { get; private set; }
    public PartialUserModel UserModel { get; private set; }

    public PostModel(Post post)
      : base(post.UserId, post.Text, post.ImageUrl)
    {
      SetId(post.Id);
    }

    public PostModel FillUpCommentsModel(List<CommentModel> model)
    {
      CommentsModel = model;
      return this;
    }

    public PostModel FillUpUserModel(PartialUserModel model)
    {
      UserModel = model;
      return this;
    }

    public PostModel FillUpRatings(int likes, int dislikes)
    {
      LikeCount = likes;
      DislikeCount = dislikes;
      return this;
    }
  }
}