using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using BusinessLogic.Users.Following;

namespace TestConsole.CommandProcessing
{
  public abstract class CommandProcessor
  {
    protected static readonly PostManager PostManager = new PostManager(new PostRepo(), new RatingRepo());
    protected static readonly CommentManager CommentManager = new CommentManager(new CommentRepo(), new RatingRepo());
    protected static readonly FollowManager FollowManager = new FollowManager(new FollowRepo());

    public abstract string Process(string[] cmdParams);
  }
}