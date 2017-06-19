using System;
using Common.Exceptions;

namespace BusinessLogic.Comments
{
  public class Comment : BaseEntity
  {
    public int UserId;
    public int PostId;

    public string Text;
    public string ImageUrl;

    public DateTime CreateTime;
    public DateTime UpdateTime;

    public bool IsDeleted;

    public Comment(int userId, int postId, string text, string imageUrl)
    {
      UserId = userId;
      PostId = postId;

      Text = text;
      ImageUrl = imageUrl;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
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