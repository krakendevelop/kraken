using System;

namespace BusinessLogic.Posts
{
  public class Post : IEntity
  {
    // todo victor: come up with a good way of updating Id upon save
    public int Id { get; set; }

    public int UserId { get; private set; }

    public string Title;
    public string Content;

    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public bool IsDeleted { get; private set; }

    public Post(int userId, string title, string content)
    {
      UserId = userId;

      Title = title;
      Content = content;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }
  }
}