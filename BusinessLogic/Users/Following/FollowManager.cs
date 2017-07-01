using System.Collections.Generic;
using Common.Exceptions;
using Common.Serialization;
using log4net;

namespace BusinessLogic.Users.Following
{
  public class FollowManager
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly IFollowRepo _repo;

    public FollowManager(IFollowRepo repo)
    {
      Logger.Debug("Initializing...");

      _repo = repo;

      Logger.DebugFormat("Initialized with {0}", repo);
    }

    public List<Follow> GetFollowers(int userId)
    {
      var followers = _repo.ReadFollowers(userId);
      if (followers == null || followers.Count == 0)
        return null;

      Logger.DebugFormat("Loaded {0} followers for user with Id: {1}", followers.Count, userId);
      return followers;
    }

    public List<Follow> GetFollows(int userId)
    {
      var follows = _repo.ReadFollows(userId);
      if (follows == null || follows.Count == 0)
        return null;

      Logger.DebugFormat("Loaded {0} follows for user with Id: {1}", follows.Count, userId);
      return follows;
    }

    public Follow Follow(int initiatorUserId, int targetUserId)
    {
      var follow = new Follow(initiatorUserId, targetUserId);

      var id = _repo.Save(follow);
      follow.SetId(id);

      Logger.DebugFormat("Saved follow: {0}", follow.ToJson());
      return follow;
    }

    public void Unfollow(int initiatorUserId, int targetUserId)
    {
      if (_repo.Delete(initiatorUserId, targetUserId) != 1)
        throw new KrakenException("Something went wrong");

      Logger.DebugFormat("User {0} unfollowed user {1}", initiatorUserId, targetUserId);
    }
  }
}