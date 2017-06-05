using System.Collections.Generic;
using BusinessLogic.Ratings;

namespace BusinessLogic.Comments
{
  public static class CommentManager
  {
    public static Comment Create(int userId, int postId, string content)
    {
      var comment = new Comment(postId, userId, content);
      Repositories.Comments.Save(comment);
      return comment;
    }

    public static void Update(Comment comment)
    {
      Repositories.Comments.Update(comment.Id, comment);
    }

    public static List<Comment> GetAll(int postId)
    {
      return Repositories.Comments.ReadPostComments(postId);
    }

    public static Rating Like(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Like, RatingTargetKindId.Comment, id);

      Repositories.Ratings.Delete(rating);
      Repositories.Ratings.Save(rating);
      return rating;
    }

    public static Rating Dislike(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Dislike, RatingTargetKindId.Comment, id);

      Repositories.Ratings.Delete(rating);
      Repositories.Ratings.Save(rating);
      return rating;
    }

    public static void RemoveRating(int userId, int id)
    {
      var rating = new Rating(userId, RatingTargetKindId.Comment, id);
      Repositories.Ratings.Delete(rating);
    }
  }
}