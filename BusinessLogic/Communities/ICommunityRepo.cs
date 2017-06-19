namespace BusinessLogic.Communities
{
  public interface ICommunityRepo
  {
    int Save(Community community);
    Community Read(int subscriptionCommunityId);
  }
}