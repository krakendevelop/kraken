using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Serialization;
using log4net;

namespace BusinessLogic.Communities
{
  public class CommunityManager
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly ICommunityRepo _repo;
    private readonly ICommunitySubscriptionRepo _subscriptionRepo;

    public CommunityManager(ICommunityRepo repo, ICommunitySubscriptionRepo subscriptionRepo)
    {
      Logger.Debug("Initializing...");

      _repo = repo;
      _subscriptionRepo = subscriptionRepo;

      Logger.DebugFormat("Initialized with {0}, {1}", repo, subscriptionRepo);
    }

    public List<Community> GetUserCommunities(int userId)
    {
      var subscriptions = _subscriptionRepo.ReadAllByUserId(userId);
      if (subscriptions == null)
      {
        Logger.DebugFormat("No subscriptions found for user with Id: {0}", userId);
        return null;
      }

      var communities = _repo
        .ReadAll(subscriptions.Select(s => s.CommunityId))
        .AssertNotNull();

      Logger.DebugFormat("Loaded {0} community subscriptions for user with Id: {1}", communities.Count, userId);
      return communities;
    }

    public Community Create(int userId, string name, string pictureUrl)
    {
      var community = new Community(userId, name, pictureUrl);
      var id = _repo.Save(community);
      community.SetId(id);

      Logger.DebugFormat("Saved community: {0}", community.ToJson());

      return community;
    }

    public CommunitySubscription Subscribe(int userId, int communityId)
    {
      var subscription = new CommunitySubscription(userId, communityId);
      var id = _subscriptionRepo.Save(subscription);
      subscription.SetId(id);

      Logger.DebugFormat("Saved user subscription to community. UserId: {0}, CommunityId: {1}", userId, communityId);

      return subscription;
    }

    public void Unsubscribe(int userId, int communityId)
    {
      var subscription = new CommunitySubscription(userId, communityId);
      _subscriptionRepo
        .Delete(subscription)
        .AssertJustOne();

      Logger.DebugFormat("Deleted user subscription to community. UserId: {0}, CommunityId: {1}", userId, communityId);
    }
  }
}