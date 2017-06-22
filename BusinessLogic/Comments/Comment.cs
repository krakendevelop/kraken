using System;
using Common.Exceptions;
using Newtonsoft.Json;

namespace BusinessLogic.Comments
{
  public class Comment : BaseEntity
  {
    public int UserId { get; private set; }
    public int PostId { get; private set; }
    public int? CommentId { get; private set; }

    public string Text { get; private set; }
    public string ImageUrl { get; private set; }

    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }

    public bool IsDeleted { get; private set; }

    [JsonIgnore] public bool IsReply => CommentId.HasValue;

    public Comment(int userId, int postId, string text, string imageUrl)
    {
      UserId = userId;
      PostId = postId;

      Text = text;
      ImageUrl = imageUrl;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public Comment SetAsReply(int commentId)
    {
      CommentId = commentId;
      return this;
    }

    public void Update(string content)
    {
      Text = content;
    }

    public void Delete()
    {
      if (IsDeleted)
        throw new KrakenException("Comment is deleted already");

      IsDeleted = true;
    }
  }
}