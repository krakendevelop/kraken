using System;
using System.Data.SqlClient;
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

    public Post(int id, int userId, int? communityId, string text, string imageUrl, DateTime createTime, DateTime updateTime, bool isDeleted)
      : this(userId, text, imageUrl)
    {
      SetId(id);
      CommunityId = communityId;
      CreateTime = createTime;
      UpdateTime = updateTime;
      IsDeleted = isDeleted;
    }

    public Post(int userId, string text, string imageUrl)
    {
      UserId = userId;

      Text = text;
      ImageUrl = imageUrl;

      CreateTime = DateTime.UtcNow;
      UpdateTime = CreateTime;
    }

    public static Post Read(SqlDataReader reader)
    {
      return new Post(
        reader.GetInt32(0),
        reader.GetInt32(1),
        reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
        reader.IsDBNull(3) ? "" : reader.GetString(3),
        reader.IsDBNull(4) ? "" : reader.GetString(4),
        reader.GetDateTime(5),
        reader.GetDateTime(6),
        reader.GetBoolean(7)
      );
    }

    public Post SetAsCommunityPost(int communityId)
    {
      CommunityId = communityId;
      return this;
    }

    public Post Update(string text, string imageUrl)
    {
      Text = text;
      ImageUrl = imageUrl;
      UpdateTime = DateTime.UtcNow;

      return this;
    }

    public Post Delete()
    {
      if (IsDeleted)
        throw new KrakenException("Post is deleted already");

      IsDeleted = true;

      return this;
    }

    public Post AlterCreateTime(DateTime newTime)
    {
      CreateTime = newTime;
      UpdateTime = CreateTime;

      return this;
    }
  }
}