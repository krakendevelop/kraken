using BusinessLogic.Posts;

namespace TestConsole.Data
{
  public class DataTests
  {
    public void Test()
    {
      var postRepo = new PostRepo();

      var post = new Post("Test Title", 1, "Test content");
      postRepo.Save(post);
    }
  }
}