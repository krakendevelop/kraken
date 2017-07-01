namespace TestConsole.CommandProcessing
{
  public class UserUnfollowProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var initiatorId = int.Parse(cmdParams[0]);
      var targetId = int.Parse(cmdParams[2]);
      FollowManager.Unfollow(initiatorId, targetId);

      return "Done!";
    }
  }
}