using System.Collections.Generic;
using Common.Exceptions;

namespace BusinessLogic.Users.Friends
{
  public class FriendshipManager
  {
    private readonly IFriendshipRepo _repo;

    public FriendshipManager(IFriendshipRepo repo)
    {
      _repo = repo;
    }

    public List<Friendship> GetFriends(int userId)
    {
      return _repo.ReadAcceptedFriends(userId);
    }

    public List<Friendship> GetFriendRequests(int userId)
    {
      return _repo.ReadFriendRequests(userId);
    }

    public Friendship SendFriendshipRequest(int initiatorUserId, int targetUserId)
    {
      var friendship = new Friendship(initiatorUserId, targetUserId);
      var id = _repo.Save(friendship);

      friendship.SetId(id);
      return friendship;
    }

    public Friendship AcceptFriendshipRequest(int initiatorUserId, int targetUserId)
    {
      var request = _repo.Read(initiatorUserId, targetUserId);
      if (request == null)
        throw new KrakenException("Unable to find friendship request from UserId: " + initiatorUserId + " to UserId: " + targetUserId);

      request.Accept();
      _repo.Save(request);

      return request;
    }
  }
}