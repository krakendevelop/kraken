using System.Collections.Generic;

namespace BusinessLogic.Comments
{
  public static class CommentManager
  {
    public static Comment Create(int userId, int postId, string content)
    {
      var comment = new Comment(postId, userId, content);
      Repositories.CommentRepo.Save(comment);
      return comment;
    }

    public static void Update(Comment comment)
    {
      Repositories.CommentRepo.Update(comment.Id, comment);
    }

    public static List<Comment> GetAll(int postId)
    {
      return Repositories.CommentRepo.ReadPostComments(postId);
    }
  }
}