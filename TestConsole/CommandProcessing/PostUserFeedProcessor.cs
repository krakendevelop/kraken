using System;

namespace TestConsole.CommandProcessing
{
  public class PostUserFeedProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var userId = int.Parse(cmdParams[0]);
      var posts = PostManager.GetNextFeedPosts(userId, DateTime.UtcNow, 10);

      foreach (var post in posts)
        PrintPostPreview(post);

      return posts.Count == 0 ? "User has not posted anything" : "";
    }
  }
}