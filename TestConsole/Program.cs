using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using BusinessLogic.Users.Following;
using Common.Exceptions;
using TestConsole.CommandProcessing;
using TestConsole.Download;

namespace TestConsole
{
  class Program
  {
    protected static readonly PostManager PostManager = new PostManager(new PostRepo(), new RatingRepo(), new FollowRepo());
    private static HashSet<DownloadedPost> _downloadedPosts;

    static void Main(string[] args)
    {
      PrintHelp();
      ConsoleCommand cmd;

      do
      {
        cmd = new ConsoleCommand(Console.ReadLine());

        Console.ForegroundColor = ConsoleColor.Yellow;
        string result = null;
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

          case "p_show":
            result = new PostShowProcessor().Process(cmd.Params);
            break;
          case "p_hot":
            result = new PostHotProcessor().Process(cmd.Params);
            break;
          case "p_like":
            result = new PostLikeProcessor().Process(cmd.Params);
            break;
          case "p_dislike":
            result = new PostDislikeProcessor().Process(cmd.Params);
            break;
          case "p_comment":
            result = new PostCommentProcessor().Process(cmd.Params);
            break;
          case "p_delete":
            result = new PostDeleteProcessor().Process(cmd.Params);
            break;
          case "p_feed":
            result = new PostUserFeedProcessor().Process(cmd.Params);
            break;
          case "p_by_user":
            result = new PostByUserProcessor().Process(cmd.Params);
            break;

          case "u_followers":
            result = new UserFollowersProcessor().Process(cmd.Params);
            break;
          case "u_follows":
            result = new UserFollowsProcessor().Process(cmd.Params);
            break;
          case "u_follow":
            result = new UserFollowProcessor().Process(cmd.Params);
            break;
          case "u_unfollow":
            result = new UserUnfollowProcessor().Process(cmd.Params);
            break;

          case "help":
            PrintHelp();
            break;

          default:
            continue;
        }

        if (!string.IsNullOrEmpty(result))
          Console.WriteLine(result);
        Console.ForegroundColor = ConsoleColor.White;

      } while (cmd.Name != "quit");

      Console.WriteLine("Finished");
      Console.ReadKey();
    }

    private static string DownloadFromReddit(string [] @params)
    {
      new RedditDownloader()
        .WithStep(int.Parse(@params[0]))
        .StopWhenCountEquals(int.Parse(@params[1]))
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

    private static void PrintHelp()
    {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("dwn -source -step -count");
      Console.WriteLine("dwn_save");
      Console.WriteLine();
      Console.WriteLine("p_show <-1,2,3,4,5,6,7....>");
      Console.WriteLine("p_hot");
      Console.WriteLine("p_like -postId <-count>");
      Console.WriteLine("p_dislike-postId <-count>");
      Console.WriteLine("p_comment -postId -text");
      Console.WriteLine("p_delete -postId");
      Console.WriteLine("p_feed -userId");
      Console.WriteLine("p_by_user -userId");
      Console.WriteLine();
      Console.WriteLine("u_followers -userId");
      Console.WriteLine("u_follows -userId");
      Console.WriteLine("u_follow -initiatorUserId -targetUserId");
      Console.WriteLine("u_unfollow -initiatorUserId -targetUserId");
      Console.WriteLine();
      Console.WriteLine("help");
      Console.WriteLine("quit");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
