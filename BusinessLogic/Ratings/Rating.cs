using System;
using System.Data.SqlClient;
using Common.Exceptions;
using Newtonsoft.Json;

namespace BusinessLogic.Ratings
{
  public class Rating : BaseEntity
  {
    public int UserId { get; private set; }
    public RatingKindId KindId { get; private set; }
    public RatingTargetKindId TargetKindId { get; private set; }
    public int TargetId { get; private set; }
    public DateTime CreateTime { get; private set; }

    [JsonIgnore] public bool IsPostRating => TargetKindId == RatingTargetKindId.Post;
    [JsonIgnore] public bool IsCommentRating => TargetKindId == RatingTargetKindId.Comment;
    [JsonIgnore] public bool IsLike => KindId == RatingKindId.Like;
    [JsonIgnore] public bool IsDislike => KindId == RatingKindId.Dislike;

    public Rating(int id, int userId, RatingKindId kindId, RatingTargetKindId targetKindId, int targetId, DateTime createTime)
      :this(userId, kindId, targetKindId, targetId)
    {
      SetId(id);
      CreateTime = createTime;
    }

    public Rating(int userId, RatingKindId kindId, RatingTargetKindId targetKindId, int targetId)
      : this(userId, targetKindId, targetId)
    {
      KindId = kindId;
      CreateTime = DateTime.UtcNow;
    }

    public Rating(int userId, RatingTargetKindId targetKindId, int targetId)
    {
      UserId = userId;
      KindId = RatingKindId.Unknown;
      TargetKindId = targetKindId;
      TargetId = targetId;

      CreateTime = DateTime.UtcNow;
    }

    public static Rating Read(SqlDataReader reader)
    {
      return new Rating(
        reader.GetInt32(0),
        reader.GetInt32(1),
        (RatingKindId) reader.GetInt32(2),
        (RatingTargetKindId) reader.GetInt32(3),
        reader.GetInt32(4),
        reader.GetDateTime(5)
      );
    }

    public void SwitchKind()
    {
      if (KindId == RatingKindId.Unknown)
        throw new KrakenException("There must not be ratings with unknown KindId at this point");

      if (KindId == RatingKindId.Like)
        KindId = RatingKindId.Dislike;
      else if (KindId == RatingKindId.Dislike)
        KindId = RatingKindId.Like;
    }
  }
}