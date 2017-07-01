using System;
using BusinessLogic.Posts;
using BusinessLogic.Ratings;

namespace TestConsole.CommandProcessing
{
  public class PostShowProcessor : CommandProcessor
  {
    public override string Process(string[] cmdParams)
    {
      if (cmdParams == null || cmdParams.Length == 0)
      {
        int i = 1;

        Post post;
        while ((post = PostManager.Get(i++)) != null)
        {
          Console.WriteLine($"Id: {post.Id} UserId: {post.UserId} CommunityId: {post.CommunityId} Text: {post.Text} Image: {post.ImageUrl} AcceptTime: {post.CreateTime} UpdateTime {post.UpdateTime} IsDeleted {post.IsDeleted}");
        }
      }
      else
      {
        var ids = cmdParams[0].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        if (ids.Length == 1)
        {
          var postId = int.Parse(ids[0]);
          var post = PostManager.Get(postId);
          Console.WriteLine($"Id: {post.Id}\r\n" +
                            $"UserId: {post.UserId}\r\n" +
                            $"CommunityId: {post.CommunityId}\r\n" +
                            $"Text: {post.Text}\r\n" +
                            $"Image: {post.ImageUrl}\r\n" +
                            $"AcceptTime: {post.CreateTime}\r\n" +
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
          Console.WriteLine($"Id: {post.Id} UserId: {post.UserId} CommunityId: {post.CommunityId} Text: {post.Text} Image: {post.ImageUrl} AcceptTime: {post.CreateTime} UpdateTime {post.UpdateTime} IsDeleted {post.IsDeleted}");
        }
      }

      return "";
    }
  }
}