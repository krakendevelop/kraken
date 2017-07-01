namespace TestConsole.CommandProcessing
{
  public class PostHotProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      return string.Join(",", PostManager.HotPostIds);
    }
  }
}