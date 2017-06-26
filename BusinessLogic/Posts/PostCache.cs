using System.Collections.Concurrent;

namespace BusinessLogic.Posts
{
  public class PostCache
  {
    private readonly IPostRepo _repo;
    private readonly ConcurrentDictionary<int, Post> _postsById;

    public PostCache(IPostRepo repo)
    {
      _repo = repo;
      _postsById = new ConcurrentDictionary<int, Post>();

      Initialize();
    }

    private void Initialize()
    {
      var posts = _repo.ReadAll();
      if (posts == null)
      {
        return;
      }

      foreach (var post in posts)
      {
        _postsById.TryAdd(post.Id, post);
      }
    }

    public bool Add(Post post)
    {
      return _postsById.TryAdd(post.Id, post);
    }

    public Post Get(int id)
    {
      Post post;
      return _postsById.TryGetValue(id, out post) ? post : null;
    }

    public Post Update(Post post)
    {
      return _postsById.AddOrUpdate(post.Id, p => post, (i, p) => post);
    }
  }
}