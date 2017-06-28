using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Ratings;
using Common.Exceptions;
using Common.Serialization;
using log4net;

namespace BusinessLogic.Posts
{
  public class PostManager
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly IPostRepo _postRepo;
    private readonly IRatingRepo _ratingRepo;

    private readonly PostCache _cache;
    private readonly List<int> _hotPostIds;

    public PostManager(IPostRepo postRepo, IRatingRepo ratingRepo)
    {
      Logger.Debug("Initializing...");

      _postRepo = postRepo;
      _ratingRepo = ratingRepo;
      _cache = new PostCache(_postRepo);

      _hotPostIds = GenerateHotPosts();

      Logger.DebugFormat("Initialized with {0}, {1}", postRepo, ratingRepo);
    }

    private List<int> GenerateHotPosts()
    {
      return Enumerable.Range(0, 490).ToList();
    }

    public Post Create(int userId, string text, string imageUrl)
    {
      var post = new Post(userId, text, imageUrl);

      Logger.DebugFormat("Creating post: {0}", post.ToJson());

      var id = _postRepo.Save(post);
      post.SetId(id);

      if (_cache.Add(post))
        throw new KrakenException("Unable to add post " + post.ToJson() + " to cache");

      return post;
    }

    public void Update(Post post)
    {
      _postRepo.Update(post.Id, post);
      _cache.Update(post);
    }

    #region Get

    public Post Get(int id)
    {
      var post = _cache.Get(id);

      if (post != null)
        return post;

      post = _postRepo.Read(id);
      if (post == null)
      {
        Logger.DebugFormat("Unable to find post with Id: {0}", id);
        return null;
      }

      _cache.Add(post);
      return post;
    }

    public List<Post> GetNextHot(int idFrom, int count)
    {
      var ids = _hotPostIds
        .SkipWhile(id => id != idFrom)
        .Take(count);

      List<Post> posts = null;
      foreach (var id in ids)
      {
        var post = _cache.Get(id);
        if (post != null)
        {
          if (posts == null)
            posts = new List<Post>();

          posts.Add(post);
          continue;
        }

        post = _postRepo.Read(id);
        if (post == null)
        {
          Logger.DebugFormat("Unable to find post with Id: {0}", id);
          continue;
        }

        _cache.Add(post);
        if (posts == null)
          posts = new List<Post>();

        posts.Add(post);
      }

      return posts;
    }

    #endregion

    #region Ratings

    public Rating Like(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Like, RatingTargetKindId.Post, id);
      
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