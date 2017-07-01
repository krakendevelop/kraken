namespace TestConsole.CommandProcessing
{
  public class PostCommentProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var id = int.Parse(cmdParams[0]);
      var text = cmdParams[1];

      CommentManager.Create(-1, id, text, null, null);
      return "Done!";
    }
  }
}