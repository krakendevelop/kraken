using System.Collections.Generic;
using BusinessLogic.Data;

namespace BusinessLogic.Users.Friends
{
  public interface IFriendshipRepo : IRepo
  {
    int Save(Friendship friendship);
    Friendship Read(int initiatorUserId, int targetUserId);
    List<Friendship> ReadAcceptedFriends(int userId);
    List<Friendship> ReadFriendRequests(int userId);
  }
}