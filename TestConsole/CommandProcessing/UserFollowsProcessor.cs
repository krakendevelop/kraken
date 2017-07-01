using System;

namespace TestConsole.CommandProcessing
{
  public class UserFollowsProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var id = int.Parse(cmdParams[0]);
      var follows = FollowManager.GetFollows(id);

      if (follows == null || follows.Count == 0)
        return "User doesn't follow anybody";

      foreach (var follow in follows)
      {
        Console.WriteLine($"{follow.TargetUserId}");
      }

      return "";
    }
  }
}