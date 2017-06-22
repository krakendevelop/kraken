using System.Collections.Generic;
using BusinessLogic.Ratings;

namespace BusinessLogic.Comments
{
  public class CommentManager
  {
    private readonly ICommentRepo _commentRepo;
    private readonly IRatingRepo _ratingRepo;

    public CommentManager(ICommentRepo commentRepo, IRatingRepo ratingRepo)
    {
      _commentRepo = commentRepo;
      _ratingRepo = ratingRepo;
    }

    public Comment Create(int userId, int postId, string text, string imageUrl, int? commentId = null)
    {
      var comment = new Comment(postId, userId, text, imageUrl);

      if (commentId.HasValue)
        comment.SetAsReply(commentId.Value);

      _commentRepo.Save(comment);
      return comment;
    }

    public void Update(Comment comment)
    {
      _commentRepo.Update(comment.Id, comment);
    }

    public List<Comment> GetAll(int postId)
    {
      return _commentRepo.ReadPostComments(postId);
    }

    public Rating Like(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Like, RatingTargetKindId.Comment, id);

      _ratingRepo.Save(rating);
      return rating;
    }

    public Rating Dislike(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Dislike, RatingTargetKindId.Comment, id);
      
      _ratingRepo.Save(rating);
      return rating;
    }

    public List<Rating> GetRatings(int commentId)
    {
      return _ratingRepo.ReadByCommentId(commentId);
    }

    public void RemoveRating(int userId, int id)
    {
      var rating = new Rating(userId, RatingTargetKindId.Comment, id);
      _ratingRepo.Delete(rating);
    }
  }
}