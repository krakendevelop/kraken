using System;

namespace TestConsole.CommandProcessing
{
  public class UserFollowersProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var id = int.Parse(cmdParams[0]);
      var followers = FollowManager.GetFollowers(id);

      if (followers == null || followers.Count == 0)
        return "User has no followers";

      foreach (var follower in followers)
      {
        Console.WriteLine($"{follower.InitiatorUserId}");
      }

      return "";
    }
  }
}