using System.Collections.Concurrent;
using System.Collections.Generic;

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
        return;

      foreach (var post in posts)
        _postsById.TryAdd(post.Id, post);
    }

    public bool Add(Post post)
    {
      const int limit = 10;
      int current = 0;

      do
      {
        if (_postsById.TryAdd(post.Id, post))
          return true;

        current++;
      } while (current < limit);

      return false;
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

    public IEnumerable<Post> EnumeratePosts()
    {
      return _postsById.Values;
    }
  }
}