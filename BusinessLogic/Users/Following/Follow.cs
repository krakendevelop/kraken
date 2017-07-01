using System;
using System.Data.SqlClient;

namespace BusinessLogic.Users.Following
{
  public class Follow : BaseEntity
  {
    public int InitiatorUserId { get; private set; }
    public int TargetUserId { get; private set; }
    public DateTime CreateTime { get; private set; }

    public Follow(int id, int initiatorUserId, int targetUserId, DateTime createTime)
      : this(initiatorUserId, targetUserId)
    {
      SetId(id);
      CreateTime = createTime;
    }

    public Follow(int initiatorUserId, int targetUserId)
    {
      InitiatorUserId = initiatorUserId;
      TargetUserId = targetUserId;
      CreateTime = DateTime.UtcNow;
    }

    public static Follow Read(SqlDataReader reader)
    {
      return new Follow(
        reader.GetInt32(0),
        reader.GetInt32(1),
        reader.GetInt32(2),
        reader.GetDateTime(3)
      );
    }
  }
}