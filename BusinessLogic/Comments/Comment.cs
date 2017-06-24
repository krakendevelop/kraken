using System;
using System.Data.SqlClient;
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

    public Comment(int id, int userId, int postId, int? commentId, string text, string imageUrl,
      DateTime createTime, DateTime updateTime, bool isDeleted)
      : this(userId, postId, text, imageUrl)
    {
      SetId(id);
      CommentId = commentId;
      CreateTime = createTime;
      UpdateTime = updateTime;
      IsDeleted = isDeleted;
    }

    public Comment(int userId, int postId, string text, string imageUrl)
    {
      UserId = userId;
      PostId = postId;

      Text = text;
      ImageUrl = imageUrl;

      CreateTime = DateTime.UtcNow;
      UpdateTime = DateTime.UtcNow;
    }

    public static Comment Read(SqlDataReader reader)
    {
      return new Comment(
        reader.GetInt32(0),
        reader.GetInt32(1),
        reader.GetInt32(2),
        reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
        reader.IsDBNull(4) ? "" : reader.GetString(4),
        reader.IsDBNull(5) ? "" : reader.GetString(5),
        reader.GetDateTime(6),
        reader.GetDateTime(7),
        reader.GetBoolean(8)
      );
    }

    public Comment SetAsReply(int commentId)
    {
      CommentId = commentId;
      return this;
    }

    public Comment Update(string text, string imageUrl)
    {
      if (text != null)
        Text = text;

      if (imageUrl != null)
        ImageUrl = imageUrl;

      UpdateTime = DateTime.UtcNow;

      return this;
    }

    public void Delete()
    {
      if (IsDeleted)
        throw new KrakenException("Comment is deleted already");

      IsDeleted = true;
    }
  }
}