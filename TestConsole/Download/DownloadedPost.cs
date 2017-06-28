namespace TestConsole.Download
{
  public class DownloadedPost
  {
    public DownloadSource Source;
    public string Content;
    public string ImageUrl;

    public DownloadedPost(DownloadSource source, string content, string imageUrl)
    {
      Source = source;
      Content = content;
      ImageUrl = imageUrl;
    }
  }
}