using System.Collections.Generic;

namespace BusinessLogic.Comments
{
  public interface ICommentRepo
  {
    int Save(Comment comment);
    void Update(int id, Comment comment);
    Comment Read(int id);
    List<Comment> ReadPostComments(int postId);
  }
}