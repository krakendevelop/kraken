using System.Collections.Generic;
using BusinessLogic.Ratings;

namespace BusinessLogic.Comments
{
  public class CommentManager
  {
    public Comment Create(int userId, int postId, string content)
    {
      var comment = new Comment(postId, userId, content);
      Repositories.Comments.Save(comment);
      return comment;
    }

    public void Update(Comment comment)
    {
      Repositories.Comments.Update(comment.Id, comment);
    }

    public List<Comment> GetAll(int postId)
    {
      return Repositories.Comments.ReadPostComments(postId);
    }

    public Rating Like(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Like, RatingTargetKindId.Comment, id);

      Repositories.Ratings.Delete(rating);
      Repositories.Ratings.Save(rating);
      return rating;
    }

    public Rating Dislike(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Dislike, RatingTargetKindId.Comment, id);

      Repositories.Ratings.Delete(rating);
      Repositories.Ratings.Save(rating);
      return rating;
    }

    public void RemoveRating(int userId, int id)
    {
      var rating = new Rating(userId, RatingTargetKindId.Comment, id);
      Repositories.Ratings.Delete(rating);
    }
  }
}