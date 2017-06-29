using System.Collections.Generic;
using Data;

namespace BusinessLogic.Posts
{
  public class PostRepo : IPostRepo
  {
    public int Save(Post post)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("INSERT INTO [Posts]" +
                        "([UserId], [CommunityId], [Text], [ImageUrl], [CreateTime], [UpdateTime], [IsDeleted]) " +
                        "OUTPUT INSERTED.Id VALUES(@UserId, @CommunityId, @Text, @ImageUrl, @CreateTime, @UpdateTime, @IsDeleted)")
          .SetParam("@UserId", post.UserId)
          .SetParam("@CommunityId", post.CommunityId)
          .SetParam("@Text", post.Text)
          .SetParam("@ImageUrl", post.ImageUrl)
          .SetParam("@CreateTime", post.CreateTime)
          .SetParam("@UpdateTime", post.UpdateTime)
          .SetParam("@IsDeleted", post.IsDeleted)
          .ExecuteReader(r =>
          {
            r.Read();
            return r.GetInt32(0);
          });
      }
    }

    public int Update(int id, Post post)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("UPDATE [Posts]" +
                        " SET [UserId]=@UserId, [CommunityId]=@CommunityId, [Text]=@Text, [ImageUrl]=@ImageUrl, [CreateTime]=@CreateTime," +
                        "[UpdateTime]=@UpdateTime, [IsDeleted]=@IsDeleted WHERE [Id]=@Id")
          .SetParam("@Id", post.Id)
          .SetParam("@UserId", post.UserId)
          .SetParam("@CommunityId", post.CommunityId)
          .SetParam("@Text", post.Text)
          .SetParam("@ImageUrl", post.ImageUrl)
          .SetParam("@CreateTime", post.CreateTime)
          .SetParam("@UpdateTime", post.UpdateTime)
          .SetParam("@IsDeleted", post.IsDeleted)
          .Execute();
      }
    }

    public Post Read(int id)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Posts] WHERE [Id]=@Id")
          .SetParam("@Id", id)
          .ExecuteReader(r => !r.Read() ? null : Post.Read(r));
      }
    }

    public List<Post> ReadAll()
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Posts]")
          .ExecuteReader(reader =>
          {
            List<Post> posts = null;

            while (reader.Read())
            {
              if (posts == null)
                posts = new List<Post>();

              posts.Add(Post.Read(reader));
            }

            return posts;
          });
      }
    }

    public List<Post> ReadAll(IEnumerable<int> ids)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Posts] WHERE [Id] IN (@Ids)")
          .SetParam("@Ids", string.Join(",", ids))
          .ExecuteReader(reader =>
          {
            List<Post> posts = null;

            while (reader.Read())
            {
              if (posts == null)
                posts = new List<Post>();

              posts.Add(Post.Read(reader));
            }

            return posts;
          });
      }
    }

    public void Delete(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}