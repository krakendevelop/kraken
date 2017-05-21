using BusinessLogic.Comments;
using BusinessLogic.Posts;

namespace BusinessLogic
{
  public static class Repositories
  {
    public static PostRepo PostRepo;
    public static CommentRepo CommentRepo;

    public static void Init()
    {
      PostRepo = new PostRepo();
      CommentRepo = new CommentRepo();
    }
  }
}