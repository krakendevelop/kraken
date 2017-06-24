using System;
using BusinessLogic.Comments;
using Tests.Repos;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      var commentManager = new CommentManager(new CommentRepo(), new TestRatingRepo());
      var repo = new CommentRepo();
      int id = repo.Save(new Comment(3, 2, "This is Text ", "http://www.hello.com"));
      var comment = repo.Read(id);
      repo.Update(id, comment.Update("Text is new now", "image is the same?"));
      var comments = repo.ReadPostComments(2);
      Console.WriteLine("Finished");
      Console.ReadKey();
    }
  }
}
