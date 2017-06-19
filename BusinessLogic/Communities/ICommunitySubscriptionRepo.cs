using System.Collections.Generic;

namespace BusinessLogic.Communities
{
  public interface ICommunitySubscriptionRepo
  {
    int Save(CommunitySubscription subscription);
    int Delete(CommunitySubscription subscription);
    List<CommunitySubscription> ReadAllByUserId(int userId);
  }
}