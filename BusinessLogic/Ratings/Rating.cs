using System;
using Common.Exceptions;

namespace BusinessLogic.Ratings
{
  public class Rating : IEntity
  {
    public int Id { get; set; }

    public int UserId { get; private set; }
    public RatingKindId KindId { get; private set; }
    public RatingTargetKindId TargetKindId { get; private set; }
    public int TargetId { get; private set; }
    public DateTime Time { get; private set; }

    public Rating(int userId, RatingKindId kindId, RatingTargetKindId targetKindId, int targetId)
    {
      UserId = userId;
      KindId = kindId;
      TargetKindId = targetKindId;
      TargetId = targetId;

      Time = DateTime.UtcNow;
    }

    public Rating(int userId, RatingTargetKindId targetKindId, int targetId)
    {
      UserId = userId;
      KindId = RatingKindId.Unknown;
      TargetKindId = targetKindId;
      TargetId = targetId;

      Time = DateTime.UtcNow;
    }

    public void SwitchKind()
    {
      if (KindId == RatingKindId.Unknown)
        throw new KrakenException("There must not be ratings with unknown KindId at this point");

      if (KindId == RatingKindId.Like)
        KindId = RatingKindId.Dislike;

      if (KindId == RatingKindId.Dislike)
        KindId = RatingKindId.Like;
    }
  }
}