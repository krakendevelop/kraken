using System;
using System.Collections.Generic;

namespace BusinessLogic.Ratings
{
  public class RatingRepo : IRatingRepo
  {
    public int Save(Rating rating)
    {
      throw new NotImplementedException();
    }

    public void Delete(Rating rating)
    {
      //Delete here ignoring regardless of like or dislike
      throw new NotImplementedException();
    }

    public List<Rating> ReadByUserId(int userId)
    {
      throw new NotImplementedException();
    }

    public List<Rating> ReadByPostId(int postId)
    {
      throw new NotImplementedException();
    }

    public List<Rating> ReadByCommentId(int commentId)
    {
      throw new NotImplementedException();
    }
  }
}