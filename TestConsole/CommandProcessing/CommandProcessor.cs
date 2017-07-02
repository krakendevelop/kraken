using System;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using BusinessLogic.Users.Following;

namespace TestConsole.CommandProcessing
{
  public abstract class CommandProcessor
  {
    protected static readonly PostManager PostManager = new PostManager(new PostRepo(), new RatingRepo(), new FollowRepo());
    protected static readonly CommentManager CommentManager = new CommentManager(new CommentRepo(), new RatingRepo());
    protected static readonly FollowManager FollowManager = new FollowManager(new FollowRepo());

    protected virtual void PrintPostPreview(Post post)
    {
      Console.WriteLine($"Id: {post.Id} " +
                        $"UserId: {post.UserId} " +
                        $"CommunityId: {post.CommunityId} " +
                        $"Text: {post.Text} " +
                        $"Image: {post.ImageUrl} " +
                        $"AcceptTime: {post.CreateTime} " +
                        $"UpdateTime {post.UpdateTime} " +
                        $"IsDeleted {post.IsDeleted}");
    }

    public abstract string Process(string[] cmdParams);
  }
}