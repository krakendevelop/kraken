using System;
using Common.Exceptions;

namespace BusinessLogic.Posts
{
  public class Post : BaseEntity
  {
    public int UserId { get; private set; }
    public int? CommunityId { get; private set; }

    public string Text { get; private set; }
    public string ImageUrl { get; private set; }

    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }

    public bool IsDeleted { get; private set; }

    public Post(int userId, string text, string imageUrl)
    {
      UserId = userId;

      Text = text;
      ImageUrl = imageUrl;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public Post(int userId, string text, string imageUrl, int communityId)
      : this(userId, text, imageUrl)
    {
      CommunityId = communityId;
    }

    public void Update(string text, string imageUrl)
    {
      Text = text;
      ImageUrl = imageUrl;
      UpdateTime = DateTime.UtcNow;
    }

    public void Delete()
    {
      if (IsDeleted)
        throw new KrakenException("Post is deleted already");

      IsDeleted = true;
    }
  }
}