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
        return cx.Query("INSERT INTO `Posts`(`Id`, `Data`) VALUES(@Id, @Data)")
          .SetParam("@Id", post.Id)
          .SetParam("@Data", post.ToJson())
          .Execute();
      }
    }

    public void Update(int id, Post post)
    {
      using (var cx = new DataContext())
      {
        cx.Query("UPDATE `Posts` SET `Data`=@Data")
          .SetParam("Data", post.ToJson())
          .Execute();
      }
    }

    public Post Read(int id)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT `Data` FROM `Posts` WHERE `Id`=@Id")
          .SetParam("Id", id)
          .ExecuteReader(reader =>
          {
            var data = reader.GetString("Data");
            return data.FromJson<Post>();
          });
      }
    }
  }
}