using System;

namespace BusinessLogic.Posts
{
  public class Post : IEntity
  {
    public int Id { get; }

    public int CreatorUserId { get; private set; }

    public string Title;
    public string Content;

    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }

    public Post(string title, int creatorUserId, string content)
    {
      Title = title;
      CreatorUserId = creatorUserId;

      Content = content;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public void Update(string title = null, string content = null)
    {
      if (!string.IsNullOrEmpty(title))
        Title = title;

      if (!string.IsNullOrEmpty(content))
        Content = content;

      UpdateTime = DateTime.UtcNow;
    }
  }
}