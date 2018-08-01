using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Posts
{
  public class PostCache
  {
    private readonly IPostRepo _repo;
    private readonly ConcurrentDictionary<int, Post> _postById;

    public PostCache(IPostRepo repo)
    {
      _repo = repo;
      _postById = new ConcurrentDictionary<int, Post>();

      Initialize();
    }

    private void Initialize()
    {
      var posts = _repo.ReadAll();
      if (posts == null)
        return;

      foreach (var post in posts)
        _postById.TryAdd(post.Id, post);
    }

    public bool Add(Post post)
    {
      const int limit = 10;
      int current = 0;

      do
      {
        if (_postById.TryAdd(post.Id, post))
          return true;

        current++;
      } while (current < limit);

      return false;
    }

    public Post Get(int id)
    {
      Post post;
      return _postById.TryGetValue(id, out post) ? post : null;
    }

    public Post Update(Post post)
    {
      return _postById.AddOrUpdate(post.Id, p => post, (i, p) => post);
    }

    public IEnumerable<Post> EnumeratePosts(bool includeDeleted = false)
    {
      return _postById
        .Values
        .Where(p => includeDeleted || !p.IsDeleted);
    }
  }
}