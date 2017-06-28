using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace TestConsole.Download
{
  public class RedditDownloader
  {
    private readonly HashSet<DownloadedPost> _downloadedPosts;
    private int _count;
    private int _step;

    public RedditDownloader()
    {
      _downloadedPosts = new HashSet<DownloadedPost>();
    }

    public RedditDownloader WithStep(int step)
    {
      _step = step;
      return this;
    }

    public RedditDownloader StopWhenCountEquals(int count)
    {
      _count = count;
      return this;
    }

    public RedditDownloader Download()
    {
      var client = new WebClient();
      var pathPattern = "https://www.reddit.com/?count={0}&after={1}";

      string after = "t3_6jk18t";

      var total = 0;
      int current = 1;

      while (true)
      {
        var htmlText = client.DownloadString(string.Format(pathPattern, current, after));

        var htmlDoc = new HtmlDocument
        {
          OptionFixNestedTags = true,
          OptionAutoCloseOnEnd = true
        };

        htmlDoc.LoadHtml(htmlText);

        foreach (var img in htmlDoc.DocumentNode.SelectNodes("//a"))
        {
          if (total == _count)
            return this;

          var att = img.Attributes["href"];
          if (att == null)
            continue;

          if (att.Value.Contains("after="))
          {
            after = att.Value.Substring(att.Value.IndexOf("after=") + 6, 9);
            continue;
          }

          if ((!att.Value.Contains("i.imgur")) ||
             (!att.Value.EndsWith(".jpg")
            && !att.Value.EndsWith(".png")
            && !att.Value.EndsWith(".gif")
            ))
            continue;

          if (string.IsNullOrWhiteSpace(img.InnerText))
            continue;

          var downloadedPost = new DownloadedPost(DownloadSource.Reddit, img.InnerText, att.Value);
          if (_downloadedPosts.Add(downloadedPost))
            total++;
        }

        current += _step;
      }
    }

    public void WriteResultTo(out HashSet<DownloadedPost> result)
    {
      result = _downloadedPosts;
    }
  }
}