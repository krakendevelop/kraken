using System;
using Common.Exceptions;

namespace BusinessLogic.Comments
{
  public class Comment : IEntity
  {
    public int Id { get; set; }

    public int UserId;
    public int PostId;

    public string Content;

    public DateTime CreateTime;
    public DateTime UpdateTime;

    public bool IsDeleted;

    public Comment(int userId, int postId, string content)
    {
      UserId = userId;
      PostId = postId;

      Content = content;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public void Update(string content)
    {
      Content = content;
    }

    public void Delete()
    {
      if (IsDeleted)
        throw new KrakenException("Comment is deleted already");

      IsDeleted = true;
    }
  }
}