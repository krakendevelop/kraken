using System;
using Newtonsoft.Json;

namespace BusinessLogic.Users.Friends
{
  public class Friendship : BaseEntity
  {
    public int InitiatorUserId { get; private set; }
    public int TargetUserId { get; private set; }
    public DateTime RequestTime { get; private set; }
    public DateTime? AcceptTime { get; private set; }

    [JsonIgnore] public bool IsAccepted => AcceptTime.HasValue;

    public Friendship(int initiatorUserId, int targetUserId)
    {
      InitiatorUserId = initiatorUserId;
      TargetUserId = targetUserId;
      RequestTime = DateTime.UtcNow;
    }

    public void Accept()
    {
      AcceptTime = DateTime.UtcNow;
    }
  }
}