using System.Collections.Generic;

namespace BusinessLogic.Communities
{
  public interface ICommunityRepo : IRepo
  {
    int Save(Community community);
    Community Read(int communityId);
    List<Community> ReadAll(IEnumerable<int> communityIds);
  }
}