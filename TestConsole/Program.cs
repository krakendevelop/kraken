using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using HtmlAgilityPack;
using Tests.Repos;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      /*var commentManager = new CommentManager(new CommentRepo(), new TestRatingRepo());
      var repo = new CommentRepo();
      int id = repo.Save(new Comment(3, 2, "This is Text ", "http://www.hello.com"));
      var comment = repo.Read(id);
      repo.Update(id, comment.Update("Text is new now", "image is the same?"));
      var comments = repo.ReadPostComments(2);*/
      var titlesByImage = LoadImages(500000);

      var repo = new PostRepo();
      foreach (var kvp in titlesByImage)
      {
        Console.WriteLine(kvp.Key + "\t\t\t\t" + kvp.Value);

        var post = new Post(-1, kvp.Value, kvp.Key);

        repo.Save(post);
      }

      Console.WriteLine("Finished");
      Console.ReadKey();
    }

    private static Dictionary<string, string> LoadImages(int count)
    {
      var titlesByImage = new Dictionary<string, string>();
      var client = new WebClient();
      var pathPattern = "https://www.reddit.com/?count={0}&after={1}";

      string after = "t3_6jk18t";
      for (int i = 0; i < count/50; i += 50)
      {
        var htmlText = client.DownloadString(string.Format(pathPattern, i, after));

        var htmlDoc = new HtmlDocument
        {
          OptionFixNestedTags = true,
          OptionAutoCloseOnEnd = true
        };

        htmlDoc.LoadHtml(htmlText);

        foreach (var img in htmlDoc.DocumentNode.SelectNodes("//a"))
        {
          var att = img.Attributes["href"];
          if (att == null)
            continue;

          if (att.Value.Contains("after="))
          {
            after = att.Value.Substring(att.Value.IndexOf("after=") + 6, 9);
            continue;
          }

          if ((!att.Value.Contains("i.imgur")) ||
             (!att.Value.Contains(".jpg")
            && !att.Value.Contains(".png")
            && !att.Value.Contains(".gif")
            ))
            continue;

          if (string.IsNullOrWhiteSpace(img.InnerText))
            continue;

          if (!titlesByImage.ContainsKey(att.Value))
            titlesByImage.Add(att.Value, img.InnerText);
        }
      }

      return titlesByImage;
    }
  }
}
