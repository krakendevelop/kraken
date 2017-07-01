namespace TestConsole.CommandProcessing
{
  public class PostDeleteProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      var id = int.Parse(cmdParams[0]);
      PostManager.Delete(id);
      return "Deleted post with Id " + id;
    }
  }
}