using System.Collections.Generic;

namespace BusinessLogic.Posts
{
  public static class PostManager
  {
    public static Post Create(int userId, string title, string content)
    {
      var post = new Post(userId, title, content);
      post.Id = Repositories.PostRepo.Save(post);

      return post;
    }

    public static void Update(Post post)
    {
      Repositories.PostRepo.Update(post.Id, post);
    }

    public static Post Get(int id)
    {
      return Repositories.PostRepo.Read(id);
    }

    public static List<Post> GetAll(int idFrom, int count)
    {
      return Repositories.PostRepo.ReadAll(idFrom, count);
    }
  }
}