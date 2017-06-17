using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Comments;

namespace Tests.Repos
{
  public class TestCommentRepo : ICommentRepo
  {
    private int _lastCommentId;
    private readonly List<Comment> _comments;

    public TestCommentRepo()
    {
      _lastCommentId = 0;
      _comments = new List<Comment>();
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