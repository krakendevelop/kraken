using System;
using System.Collections.Generic;
using BusinessLogic.Comments;
using Data;

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
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Ratings] WHERE [TargetId]=@TargetId AND [TargetKindId]=@TargetKindId")
          .SetParam("@TargetId", postId)
          .SetParam("@TargetKindId", RatingTargetKindId.Post)
          .ExecuteReader(reader =>
          {
            List<Rating> ratings = null;
            while (reader.Read())
            {
              if (ratings == null)
                ratings = new List<Rating>();

              ratings.Add(Rating.Read(reader));
            }

            return ratings;
          });
      }
    }

    public List<Rating> ReadByCommentId(int commentId)
    {
      throw new NotImplementedException();
    }
  }
}