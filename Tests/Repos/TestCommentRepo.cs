using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Comments;
using log4net;

namespace Tests.Repos
{
  public class TestCommentRepo : ICommentRepo
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private int _lastCommentId;
    private readonly List<Comment> _comments;

    public TestCommentRepo()
    {
      _lastCommentId = 0;
      _comments = new List<Comment>();

      InitComments();
      Logger.DebugFormat("Initialized with {0} test comments", _comments.Count);
    }

    private void InitComments()
    {
      for (int i = 0; i < 5000; i++)
      {
        _lastCommentId++;
        var postId = i % 50;
        string content = $"This is a comment with Id {_lastCommentId} for post {postId}";
        var comment = new Comment(0, postId, content);
        _comments.Add(comment);
      }
    }

    public int Save(Comment comment)
    {
      comment.Id = ++_lastCommentId;
      _comments.Add(comment);
      return _lastCommentId;
    }

    public void Update(int id, Comment comment)
    {
      var commentToUpdate = _comments.Single(c => c.Id == id);
      commentToUpdate.Update(comment.Content);
    }

    public Comment Read(int id)
    {
      return _comments.SingleOrDefault(c => c.Id == id);
    }

    public List<Comment> ReadPostComments(int postId)
    {
      return _comments
        .Where(c => c.PostId == postId)
        .ToList();
    }
  }
}