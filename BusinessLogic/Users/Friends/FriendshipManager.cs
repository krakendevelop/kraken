using System.Collections.Generic;
using Common.Exceptions;
using Common.Serialization;
using log4net;

namespace BusinessLogic.Users.Friends
{
  public class FriendshipManager
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly IFriendshipRepo _repo;

    public FriendshipManager(IFriendshipRepo repo)
    {
      Logger.Debug("Initializing...");

      _repo = repo;

      Logger.DebugFormat("Initialized with {0}", repo);
    }

    public List<Friendship> GetFriends(int userId)
    {
      var friends = _repo.ReadAcceptedFriends(userId);
      Logger.DebugFormat("Loaded {0} friends for user with Id: {1}", friends.Count, userId);
      return friends;
    }

    public List<Friendship> GetFriendRequests(int userId)
    {
      var requests = _repo.ReadFriendRequests(userId);
      Logger.DebugFormat("Loaded {0} friend requests for user with Id: {1}", requests.Count, userId);
      return requests;
    }

    public Friendship SendFriendshipRequest(int initiatorUserId, int targetUserId)
    {
      var friendship = new Friendship(initiatorUserId, targetUserId);

      var id = _repo.Save(friendship);
      friendship.SetId(id);

      Logger.DebugFormat("Saves friendship request: {0}", friendship.ToJson());
      return friendship;
    }

    public Friendship AcceptFriendshipRequest(int initiatorUserId, int targetUserId)
    {
      var request = _repo.Read(initiatorUserId, targetUserId);
      if (request == null)
        throw new KrakenException("Unable to find friendship request from UserId: " + initiatorUserId + " to UserId: " + targetUserId);

      Logger.DebugFormat("Loaded friend request: {0}", request.ToJson());
      request.Accept();

      _repo.Save(request);
      Logger.DebugFormat("Saved friendship: {0}", request.ToJson());

      return request;
    }
  }
}