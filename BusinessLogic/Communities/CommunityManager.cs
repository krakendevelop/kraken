using System.Collections.Generic;
using Common.Exceptions;

namespace BusinessLogic.Communities
{
  public class CommunityManager
  {
    private readonly ICommunityRepo _communityRepo;
    private readonly ICommunitySubscriptionRepo _communitySubscriptionRepo;

    public CommunityManager(ICommunityRepo communityRepo, ICommunitySubscriptionRepo communitySubscriptionRepo)
    {
      _communityRepo = communityRepo;
      _communitySubscriptionRepo = communitySubscriptionRepo;
    }

    public List<Community> GetUserCommunities(int userId)
    {
      var subscriptions = _communitySubscriptionRepo.ReadAllByUserId(userId);
      if (subscriptions == null)
        return null;

      List<Community> communities = null;
      foreach (var subscription in subscriptions)
      {
        var community = _communityRepo.Read(subscription.CommunityId);
        if (community == null)
          throw new KrakenException("Community with Id " + subscription.CommunityId + " was not found");

        if (communities == null)
          communities = new List<Community>();

        communities.Add(community);
      }

      return communities;
    }

    public Community Create(int userId, string name, string pictureUrl)
    {
      var community = new Community(userId, name, pictureUrl);
      var id = _communityRepo.Save(community);
      community.SetId(id);

      return community;
    }

    public CommunitySubscription Subscribe(int userId, int communityId)
    {
      var subscription = new CommunitySubscription(userId, communityId);
      var id = _communitySubscriptionRepo.Save(subscription);
      subscription.SetId(id);

      return subscription;
    }

    public void Unsubscribe(int userId, int communityId)
    {
      var subscription = new CommunitySubscription(userId, communityId);
      _communitySubscriptionRepo.Delete(subscription);
    }
  }
}