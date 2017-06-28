using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Common.Exceptions;
using TestConsole.Download;

namespace TestConsole
{
  class Program
  {
    private static readonly PostManager PostManager = new PostManager(new PostRepo(), new RatingRepo());
    private static HashSet<DownloadedPost> _downloadedPosts;

    static void Main(string[] args)
    {
      ConsoleCommand cmd;

      do
      {
        PrintHelp();
        cmd = new ConsoleCommand(Console.ReadLine());

        string result;
        switch (cmd.Name)
        {
          case "dwn":
            result = DownloadFromReddit(cmd.Params);
            break;
          case "dwn_save":
            result = DwnSave(cmd.Params);
            break;
          case "dwn_show":
            result = DwnShow(cmd.Params);
            break;
          case "show_posts":
            result = Show(cmd.Params);
            break;
          default:
            continue;
        }

        Console.WriteLine(result);

      } while (cmd.Name != "quit");

      Console.WriteLine("Finished");
      Console.ReadKey();
    }

    private static string DownloadFromReddit(string [] @params)
    {
      if (@params.Length != 3)
        throw new KrakenException();

      new RedditDownloader()
        .WithStep(int.Parse(@params[1]))
        .StopWhenCountEquals(int.Parse(@params[2]))
        .Download()
        .WriteResultTo(out _downloadedPosts);

      return "Downloaded " + _downloadedPosts.Count + " posts!";
    }

    private static string DwnSave(string[] @params)
    {
      if (_downloadedPosts == null || _downloadedPosts.Count == 0)
        return "Nothing to save!";

      foreach (var post in _downloadedPosts)
      {
        PostManager.Create(-1, post.Content, post.ImageUrl);
      }

      return "Saved " + _downloadedPosts.Count + " posts!";
    }

    private static string DwnShow(string[] @params)
    {
      if (_downloadedPosts == null || _downloadedPosts.Count == 0)
        return "Nothing to show!";

      return string.Join(Environment.NewLine, _downloadedPosts
        .Select(p => $"{p.Source}\t{p.Content}\t{p.ImageUrl}"));
    }

    private static string Show(string[] @params)
    {
      int i = 1;

      Post post;
      while ((post = PostManager.Get(i++)) != null)
      {
        Console.WriteLine($"Id: {post.Id} UserId: {post.UserId} CommunityId: {post.CommunityId} Text: {post.Text} Image: {post.ImageUrl} CreateTime: {post.CreateTime} UpdateTime {post.UpdateTime} IsDeleted {post.IsDeleted}");
      }

      return "";
    }

    private static void PrintHelp()
    {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("dwn \t -source -step -count");
      Console.WriteLine("dwn_save");
      Console.WriteLine("dwn_show");
      Console.WriteLine("show_posts");
      Console.WriteLine("quit");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
