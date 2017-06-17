using System.Collections.Generic;

namespace BusinessLogic.Posts
{
  public interface IPostRepo
  {
    int Save(Post post);
    void Update(int id, Post post);
    Post Read(int id);
    List<Post> ReadAll(int idFrom, int count);
    void Delete(int id);
  }
}