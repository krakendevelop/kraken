namespace TestConsole.CommandProcessing
{
  public class UserFollowProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var initiatorId = int.Parse(cmdParams[0]);
      var targetId = int.Parse(cmdParams[1]);
      FollowManager.Follow(initiatorId, targetId);

      return "Done!";
    }
  }
}