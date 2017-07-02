using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Ratings;
using BusinessLogic.Users.Following;
using Common;
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
    private readonly IFollowRepo _followRepo;

    private readonly PostCache _cache;
    public List<int> HotPostIds { get; }

    public PostManager(IPostRepo postRepo, IRatingRepo ratingRepo, IFollowRepo followRepo)
    {
      Logger.Debug("Initializing...");

      _postRepo = postRepo;
      _ratingRepo = ratingRepo;
      _followRepo = followRepo;
      _cache = new PostCache(_postRepo);

      HotPostIds = GenerateHotPosts();

      Logger.DebugFormat("Initialized with {0}, {1}, {2}", postRepo, ratingRepo, followRepo);
    }

    private List<int> GenerateHotPosts()
    {
      return Enumerable.Range(0, 10000000).ToList();
    }

    public Post Create(int userId, string text, string imageUrl)
    {
      var post = new Post(userId, text, imageUrl);

      Logger.DebugFormat("Creating post: {0}", post.ToJson());

      var id = _postRepo.Save(post);
      post.SetId(id);

      if (!_cache.Add(post))
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
      List<Post> posts = null;
      int current = 0;
      foreach (var id in HotPostIds.SkipWhile(id => id != idFrom))
      {
        if (current == count)
          break;

        var post = _cache.Get(id);
        if (post != null)
        {
          if (!post.CreateTime.IsInPast())
            continue;

          if (posts == null)
            posts = new List<Post>();

          posts.Add(post);
          current++;
          continue;
        }

        post = _postRepo.Read(id);
        if (post == null)
        {
          Logger.DebugFormat("Unable to find post with Id: {0}", id);
          continue;
        }

        _cache.Add(post);
        if (!post.CreateTime.IsInPast())
          continue;

        if (posts == null)
          posts = new List<Post>();

        posts.Add(post);
        current++;
      }

      return posts;
    }

    // todo v.koshman right now all posts are stored in cache, that's why we garantee any post can be acquired,
    // todo but we need to change this assumption, because at some point it might not be valid
    // todo add UserPosts table and store everything there? If so, add transaction logic to update it along with Posts table

    public List<Post> GetUserPosts(int userId)
    {
      var posts = _cache
        .EnumeratePosts()
        .Where(p => p.UserId == userId)
        .ToList();

      Logger.DebugFormat("Selected {0} posts by user {1}", posts.Count, userId);
      return posts;
    }

    public List<Post> GetNextFeedPosts(int userId, DateTime lastPostTime, int count)
    {
      var followIds = _followRepo
        .ReadFollows(userId)
        .Select(f => f.TargetUserId);

      var posts = _cache
        .EnumeratePosts()
        .Where(p => followIds.Contains(p.UserId))
        .OrderBy(p => p.CreateTime)
        .SkipWhile(p => p.CreateTime > lastPostTime)
        .Take(count)
        .ToList();

      Logger.DebugFormat("Selected {0} posts for user {1} feed.", posts.Count, userId);
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

    public int Delete(int postId)
    {
      var deleted = _postRepo.Delete(postId);
      var cachedPost = _cache.Get(postId);
      if (cachedPost == null)
        return deleted;

      cachedPost.Delete();
      _cache.Update(cachedPost);
      return deleted;
    }
  }
}