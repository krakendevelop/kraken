using System;

namespace BusinessLogic.Comments
{
  public class Comment : IEntity
  {
    public int Id { get; set; }

    public int CreatorUserId { get; private set; }
    public int PostId { get; private set; }

    public string Content;

    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }

    public Comment(int creatorUserId, int postId, string content)
    {
      CreatorUserId = creatorUserId;
      PostId = postId;

      Content = content;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public void Update(string content = null)
    {
      if (!string.IsNullOrEmpty(content))
        Content = content;

      UpdateTime = DateTime.UtcNow;
    }
  }
}