using BusinessLogic.Comments;

namespace WebApp.Models
{
  public class CommentModel : Comment
  {
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }

    public CommentModel(Comment comment)
      : base(comment.UserId, comment.PostId, comment.Content)
    {
      Id = comment.Id;
    }
  }
}