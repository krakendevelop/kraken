using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Ratings;
using Common.Exceptions;

namespace Tests.Repos
{
  public class TestRatingRepo : IRatingRepo
  {
    private int _lastId;
    private readonly List<Rating> _ratings;

    public TestRatingRepo()
    {
      _lastId = 0;
      _ratings = new List<Rating>();
    }

    public int Save(Rating rating)
    {
      var existingRating = _ratings
        .Where(r => r.TargetId == rating.TargetId)
        .Where(r => r.TargetKindId == rating.TargetKindId)
        .FirstOrDefault(r => r.UserId == rating.UserId);

      if (existingRating == null)
      {
        rating.Id = ++_lastId;
        _ratings.Add(rating);
        return _lastId;
      }

      if (rating.KindId == existingRating.KindId)
        throw new KrakenException("Unable to save the same rating");

      existingRating.SwitchKind();
      return _lastId;
    }

    public void Delete(Rating rating)
    {
      _ratings.RemoveAll(r =>
        r.TargetId == rating.TargetId
        && r.TargetKindId == rating.TargetKindId
        && r.UserId == rating.UserId);
    }

    public List<Rating> ReadByUserId(int userId)
    {
      return _ratings
        .Where(r => r.UserId == userId)
        .ToList();
    }

    public List<Rating> ReadByPostId(int postId)
    {
      return _ratings
        .Where(r => r.TargetKindId == RatingTargetKindId.Post)
        .Where(r => r.TargetId == postId)
        .ToList();
    }

    public List<Rating> ReadByCommentId(int commentId)
    {
      return _ratings
        .Where(r => r.TargetKindId == RatingTargetKindId.Comment)
        .Where(r => r.TargetId == commentId)
        .ToList();
    }
  }
}