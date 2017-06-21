using BusinessLogic.Comments;

namespace WebApp.Models
{
  public class CommentModel : Comment
  {
    public int LikeCount { get; private set; }
    public int DislikeCount { get; private set; }
    public PartialUserModel UserModel { get; private set; }


    public CommentModel(Comment comment)
      : base(comment.UserId, comment.PostId, comment.Text, comment.ImageUrl)
    {
      SetId(comment.Id);
    }

    public CommentModel FillUpUserModel(PartialUserModel model)
    {
      UserModel = model;
      return this;
    }

    public CommentModel FillUpRatings(int likes, int dislikes)
    {
      LikeCount = likes;
      DislikeCount = dislikes;
      return this;
    }
  }
}