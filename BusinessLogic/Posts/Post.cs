using System;
using Common.Exceptions;

namespace BusinessLogic.Posts
{
  public class Post : IEntity
  {
    // todo victor: come up with a good way of updating Id upon save
    public int Id { get; set; }

    public int UserId;

    public string Title;
    public string Content;

    public DateTime CreateTime;
    public DateTime UpdateTime;

    public bool IsDeleted;

    public Post(int userId, string title, string content)
    {
      UserId = userId;

      Title = title;
      Content = content;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public void Update(string title, string content)
    {
      Title = title;
      Content = content;
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