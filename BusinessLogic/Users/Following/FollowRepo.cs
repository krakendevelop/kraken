using System.Collections.Generic;
using BusinessLogic.Comments;
using Data;

namespace BusinessLogic.Users.Following
{
  public class FollowRepo : IFollowRepo
  {
    public int Save(Follow follow)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("INSERT INTO [Follows]" +
          "([InitiatorUserId], [TargetUserId], [CreateTime]) " +
          "VALUES(@InitiatorUserId, @TargetUserId, @CreateTime)")
          .SetParam("@InitiatorUserId", follow.InitiatorUserId)
          .SetParam("@TargetUserId", follow.TargetUserId)
          .SetParam("@CreateTime", follow.CreateTime)
          .Execute();
      }
    }

    public List<Follow> ReadFollowers(int userId)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Follows] WHERE [TargetUserId]=@TargetUserId")
          .SetParam("@TargetUserId", userId)
          .ExecuteReader(reader =>
          {
            List<Follow> follows = null;
            while (reader.Read())
            {
              if (follows == null)
                follows = new List<Follow>();

              follows.Add(Follow.Read(reader));
            }

            return follows;
          });
      }
    }

    public List<Follow> ReadFollows(int userId)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Follows] WHERE [InitiatorUserId]=@InitiatorUserId")
          .SetParam("@InitiatorUserId", userId)
          .ExecuteReader(reader =>
          {
            List<Follow> follows = null;
            while (reader.Read())
            {
              if (follows == null)
                follows = new List<Follow>();

              follows.Add(Follow.Read(reader));
            }

            return follows;
          });
      }
    }

    public int Delete(int initiatorUserId, int targetUserId)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("DELETE FROM [Follows] WHERE [InitiatorUserId]=@InitiatorUserId AND [TargetUserId]=@TargetUserId")
          .SetParam("@InitiatorUserId", initiatorUserId)
          .SetParam("@TargetUserId", targetUserId)
          .Execute();
      }
    }
  }
}