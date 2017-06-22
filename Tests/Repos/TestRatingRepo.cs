using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Ratings;
using Common.Exceptions;
using Common.Serialization;
using log4net;

namespace Tests.Repos
{
  public class TestRatingRepo : IRatingRepo
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private int _lastId;
    private readonly List<Rating> _ratings;

    public TestRatingRepo()
    {
      _lastId = 0;
      _ratings = new List<Rating>();

      InitRatings();
      Logger.DebugFormat("Initialized with {0} post ratings ({1} likes/{2} dislikes) and {3} comment ratings ({4} likes/{5} dislikes)",
        _ratings.Count(r => r.IsPostRating),
        _ratings.Count(r => r.IsPostRating && r.IsLike),
        _ratings.Count(r => r.IsPostRating && r.IsDislike),
        _ratings.Count(r => r.IsCommentRating),
        _ratings.Count(r => r.IsCommentRating && r.IsLike),
        _ratings.Count(r => r.IsCommentRating && r.IsDislike)
        );
    }

    private void InitRatings()
    {
      for (int i = 0; i < 10000; i++)
      {
        _lastId++;
        var targetId = i % 50;
        var kind = i % 2 == 0 && DateTime.UtcNow.Ticks%5 == 0 ? RatingKindId.Dislike : RatingKindId.Like;
        var targetKind = i % 3 == 0 && DateTime.UtcNow.Ticks % 3 == 0 ? RatingTargetKindId.Post : RatingTargetKindId.Comment;

        var rating = new Rating(0, kind, targetKind, targetId);
        rating.SetId(_lastId);
        _ratings.Add(rating);
      }
    }

    public int Save(Rating rating)
    {
      var existingRating = _ratings
        .Where(r => r.TargetId == rating.TargetId)
        .Where(r => r.TargetKindId == rating.TargetKindId)
        .FirstOrDefault(r => r.UserId == rating.UserId);

      if (existingRating == null)
      {
        rating.SetId(++_lastId);
        _ratings.Add(rating);
        
        Logger.DebugFormat("Rating for {0} with Id {1} by User {2} does not exist, saving: {3}",
          rating.KindId, rating.Id, rating.UserId, rating.ToJson());
        return _lastId;
      }

      if (rating.KindId == existingRating.KindId)
        throw new KrakenException("Unable to save the same rating");

      existingRating.SwitchKind();
      Logger.DebugFormat("Rating for {0} with Id {1} by User {2} exists, switching kind and saving: {3}",
          rating.KindId, rating.Id, rating.UserId, rating.ToJson());
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