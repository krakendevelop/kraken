using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Ratings;

namespace BusinessLogic.Posts
{
  public class PostManager
  {
    private readonly IPostRepo _postRepo;
    private readonly IRatingRepo _ratingRepo;

    public PostManager(IPostRepo postRepo, IRatingRepo ratingRepo)
    {
      _postRepo = postRepo;
      _ratingRepo = ratingRepo;
    }

    public Post Create(int userId, string title, string content)
    {
      var post = new Post(userId, title, content);
      post.Id = _postRepo.Save(post);

      return post;
    }

    public void Update(Post post)
    {
      _postRepo.Update(post.Id, post);
    }

    #region Get

    public Post Get(int id)
    {
      return _postRepo.Read(id);
    }

    public List<Post> GetAll(int idFrom, int count)
    {
      return _postRepo.ReadAll(idFrom, count);
    }

    #endregion

    #region Ratings

    public Rating Like(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Like, RatingTargetKindId.Post, id);

      _ratingRepo.Delete(rating);
      _ratingRepo.Save(rating);
      return rating;
    }

    public List<Rating> GetRatings(int postId)
    {
      return _ratingRepo.ReadByPostId(postId);
    }

    public Rating Dislike(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Dislike, RatingTargetKindId.Post, id);

      _ratingRepo.Delete(rating);
      _ratingRepo.Save(rating);
      return rating;
    }

    public void RemoveRating(int userId, int id)
    {
      var rating = new Rating(userId, RatingTargetKindId.Post, id);
      _ratingRepo.Delete(rating);
    }

    #endregion

    public void Delete(int postId)
    {
      _postRepo.Delete(postId);
    }
  }
}