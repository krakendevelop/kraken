using System;

namespace BusinessLogic.Communities
{
  public class CommunitySubscription : BaseEntity
  {
    public int UserId { get; private set; }
    public int CommunityId { get; private set; }
    public DateTime CreateTime { get; private set; }

    public CommunitySubscription(int userId, int communityId)
    {
      UserId = userId;
      CommunityId = communityId;

      CreateTime = DateTime.UtcNow;
    }
  }
}