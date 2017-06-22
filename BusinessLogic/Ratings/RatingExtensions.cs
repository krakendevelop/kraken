using System.Collections.Generic;
using Common.Exceptions;

namespace BusinessLogic.Ratings
{
  public static class RatingExtensions
  {
    public static void CalcRatings(this IEnumerable<Rating> ratings, out int likes, out int dislikes)
    {
      likes = 0;
      dislikes = 0;

      if (ratings == null)
        return;

      foreach (var rating in ratings)
      {
        if (rating.IsLike)
          likes++;
        else if (rating.IsDislike)
          dislikes++;
        else
          throw new KrakenException("Unable to determine rating kind");
      }
    }
  }
}