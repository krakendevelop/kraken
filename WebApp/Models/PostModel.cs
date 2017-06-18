using System.Collections.Generic;
using BusinessLogic.Posts;

namespace WebApp.Models
{
  public class PostModel : Post
  {
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public int CommentCount { get; set; }
    public List<CommentModel> Comments { get; set; }

    public PostModel(Post post)
      : base(post.UserId, post.Title, post.Content)
    {
      Id = post.Id;
    }
  }
}