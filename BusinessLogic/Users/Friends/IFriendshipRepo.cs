using System.Collections.Generic;

namespace BusinessLogic.Users.Friends
{
  public interface IFriendshipRepo
  {
    int Save(Friendship friendship);
    Friendship Read(int initiatorUserId, int targetUserId);
    List<Friendship> ReadAcceptedFriends(int userId);
    List<Friendship> ReadFriendRequests(int userId);
  }
}