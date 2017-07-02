namespace TestConsole.CommandProcessing
{
  public class PostByUserProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var userId = int.Parse(cmdParams[0]);
      var posts = PostManager.GetUserPosts(userId);
      foreach (var post in posts)
        PrintPostPreview(post);

      return posts.Count == 0 ? "User has not posted anything" : "";
    }
  }
}