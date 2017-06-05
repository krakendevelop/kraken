using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;

namespace BusinessLogic
{
  public static class Repositories
  {
    public static PostRepo Posts;
    public static CommentRepo Comments;
    public static RatingRepo Ratings;

    public static void Init()
    {
      Posts = new PostRepo();
      Comments = new CommentRepo();
      Ratings = new RatingRepo();
    }
  }
}