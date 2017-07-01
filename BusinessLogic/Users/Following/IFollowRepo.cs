using System.Collections.Generic;
using BusinessLogic.Data;

namespace BusinessLogic.Users.Following
{
  public interface IFollowRepo : IRepo
  {
    int Save(Follow follow);
    List<Follow> ReadFollowers(int userId);
    List<Follow> ReadFollows(int userId);
    int Delete(int initiatorUserId, int targetUserId);
  }
}