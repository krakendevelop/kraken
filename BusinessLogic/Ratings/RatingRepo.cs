using System;
using System.Collections.Generic;
using Data;

namespace BusinessLogic.Ratings
{
  public class RatingRepo : IRatingRepo
  {
    public int Save(Rating rating)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("INSERT INTO [Ratings]" +
                        "([UserId], [KindId], [TargetKindId], [TargetId], [CreateTime]) " +
                        "OUTPUT INSERTED.Id VALUES(@UserId, @KindId, @TargetKindId, @TargetId, @CreateTime)")
          .SetParam("@UserId", rating.UserId)
          .SetParam("@KindId", rating.KindId)
          .SetParam("@TargetKindId", rating.TargetKindId)
          .SetParam("@TargetId", rating.TargetId)
          .SetParam("@CreateTime", rating.CreateTime)
          .ExecuteReader(r =>
          {
            r.Read();
            return r.GetInt32(0);
          });
      }
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