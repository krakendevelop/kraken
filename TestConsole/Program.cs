using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Comments;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;
using Common.Exceptions;
using TestConsole.Download;

namespace TestConsole
{
  class Program
  {
    private static readonly PostManager PostManager = new PostManager(new PostRepo(), new RatingRepo());
    private static readonly CommentManager CommentManager = new CommentManager(new CommentRepo(), new RatingRepo());
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
          case "p_show":
            result = ShowPosts(cmd.Params);
            break;
          case "p_hot":
            result = ShowHot(cmd.Params);
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

    private static string ShowPosts(string[] @params)
    {
      if (@params == null || @params.Length == 0)
      {
        int i = 1;

        Post post;
        while ((post = PostManager.Get(i++)) != null)
        {
          Console.WriteLine($"Id: {post.Id} UserId: {post.UserId} CommunityId: {post.CommunityId} Text: {post.Text} Image: {post.ImageUrl} CreateTime: {post.CreateTime} UpdateTime {post.UpdateTime} IsDeleted {post.IsDeleted}");
        }
      }
      else
      {
        var ids = @params[0].Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);

        if (ids.Length == 1)
        {
          var postId = int.Parse(ids[0]);
          var post = PostManager.Get(postId);
          Console.WriteLine($"Id: {post.Id}\r\n" +
                            $"UserId: {post.UserId}\r\n" +
                            $"CommunityId: {post.CommunityId}\r\n" +
                            $"Text: {post.Text}\r\n" +
                            $"Image: {post.ImageUrl}\r\n" +
                            $"CreateTime: {post.CreateTime}\r\n" +
                            $"UpdateTime: {post.UpdateTime}\r\n" +
                            $"IsDeleted: {post.IsDeleted}");

          Console.WriteLine("------------------------");

          int likes;
          int dislikes;
          PostManager
            .GetRatings(postId)
            .CalcRatings(out likes, out dislikes);

          var rating = likes - dislikes;
          Console.WriteLine($"Likes: {likes} Dislikes: {dislikes} Rating: {rating}");

          var comments = CommentManager.GetAllByPostId(postId);
          Console.WriteLine("------------------------");
          if (comments == null)
            return "No comments";

          foreach (var comment in comments)
          {
            Console.WriteLine($"Id: {comment.Id} CommentId: {comment.CommentId} UserId: {comment.Id} Text: {comment.Text} Create Time: {comment.CreateTime}");
          }

          return "";
        }

        foreach (var id in ids)
        {
          var post = PostManager.Get(int.Parse(id));
          Console.WriteLine($"Id: {post.Id} UserId: {post.UserId} CommunityId: {post.CommunityId} Text: {post.Text} Image: {post.ImageUrl} CreateTime: {post.CreateTime} UpdateTime {post.UpdateTime} IsDeleted {post.IsDeleted}");
        }
      }

      return "";
    }

    public static string ShowHot(string[] @params)
    {
      return string.Join(",", PostManager.HotPostIds);
    }

    private static void PrintHelp()
    {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("dwn -source -step -count");
      Console.WriteLine("dwn_save");
      Console.WriteLine("p_show <-1,2,3,4,5,6,7....>");
      Console.WriteLine("p_hot");
      Console.WriteLine("quit");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
