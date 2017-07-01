using System.Collections.Generic;

namespace BusinessLogic.Comments
{
  public interface ICommentRepo : IRepo
  {
    int Save(Comment comment);
    int Update(int id, Comment comment);
    Comment Read(int id);
    List<Comment> ReadPostComments(int postId);
  }
}