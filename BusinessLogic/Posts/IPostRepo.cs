using System.Collections.Generic;
using BusinessLogic.Data;

namespace BusinessLogic.Posts
{
  public interface IPostRepo : IRepo
  {
    int Save(Post post);
    int Update(int id, Post post);
    Post Read(int id);
    List<Post> ReadAll();
    List<Post> ReadAll(IEnumerable<int> ids);
    void Delete(int id);
  }
}