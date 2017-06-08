using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using BusinessLogic.Users;

namespace BusinessLogic
{
  public static class Repositories
  {
    public static PostRepo Posts;
    public static CommentRepo Comments;
    public static RatingRepo Ratings;
    public static UserRepo Users;

    public static void Init()
    {
      Posts = new PostRepo();
      Comments = new CommentRepo();
      Ratings = new RatingRepo();
    }
  }
}