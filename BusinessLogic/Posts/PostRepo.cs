using System.Collections.Generic;
using Common.Serialization;
using Data;

namespace BusinessLogic.Posts
{
  public class PostRepo
  {
    public int Save(Post post)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("INSERT INTO `Posts`(`Id`, `UserId`, `Data`) VALUES(@Id, @UserId, @Data)")
          .SetParam("@Id", post.Id)
          .SetParam("@UserId", post.UserId)
          .SetParam("@Data", post.ToJson())
          .Execute();
      }
    }

    public void Update(int id, Post post)
    {
      using (var cx = new DataContext())
      {
        cx.Query("UPDATE `Posts` SET  `UserId`=@UserId, `Data`=@Data")
          .SetParam("@UserId", post.UserId)
          .SetParam("@Data", post.ToJson())
          .Execute();
      }
    }

    public Post Read(int id)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT `Data` FROM `Posts` WHERE `Id`=@Id")
          .SetParam("@Id", id)
          .ExecuteReader(reader =>
          {
            var data = reader.GetString("Data");
            return data.FromJson<Post>();
          });
      }
    }

    public List<Post> ReadAll(int idFrom, int count)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT TOP @Count `Data` FROM `Posts` WHERE `Id`>@IdFrom")
          .SetParam("@Count", count)
          .SetParam("@IdFrom", idFrom)
          .ExecuteReader(reader =>
          {
            var result = new List<Post>();

            while (reader.Read())
            {
              var data = reader.GetString("Data");
              result.Add(data.FromJson<Post>());
            }

            return result;
          });
      }
    }

    public void Delete(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}