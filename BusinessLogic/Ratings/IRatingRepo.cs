using System.Collections.Generic;

namespace BusinessLogic.Ratings
{
  public interface IRatingRepo
  {
    int Save(Rating rating);
    void Delete(Rating rating);
    List<Rating> ReadByUserId(int userId);
    List<Rating> ReadByPostId(int postId);
    List<Rating> ReadByCommentId(int commentId);
  }
}