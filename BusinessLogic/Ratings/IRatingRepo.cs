using System.Collections.Generic;
using BusinessLogic.Data;

namespace BusinessLogic.Ratings
{
  public interface IRatingRepo : IRepo
  {
    int Save(Rating rating);
    void Delete(Rating rating);
    List<Rating> ReadByUserId(int userId);
    List<Rating> ReadByPostId(int postId);
    List<Rating> ReadByCommentId(int commentId);
  }
}