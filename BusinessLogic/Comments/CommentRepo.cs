using System.Collections.Generic;
using Data;

namespace BusinessLogic.Comments
{
  public class CommentRepo : ICommentRepo
  {
    public int Save(Comment comment)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("INSERT INTO [Comments]" +
          "([UserId], [PostId], [CommentId], [Text], [ImageUrl], [AcceptTime], [UpdateTime], [IsDeleted]) " +
          "VALUES(@UserId, @PostId, @CommentId, @Text, @ImageUrl, @AcceptTime, @UpdateTime, @IsDeleted)")
          .SetParam("@UserId", comment.UserId)
          .SetParam("@PostId", comment.PostId)
          .SetParam("@CommentId", comment.CommentId)
          .SetParam("@Text", comment.Text)
          .SetParam("@ImageUrl", comment.ImageUrl)
          .SetParam("@AcceptTime", comment.CreateTime)
          .SetParam("@UpdateTime", comment.UpdateTime)
          .SetParam("@IsDeleted", comment.IsDeleted)
          .Execute();
      }
    }

    public int Update(int id, Comment comment)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("UPDATE [Comments]" +
          " SET [UserId]=@UserId, [PostId]=@PostId, [CommentId]=@CommentId, [Text]=@Text, [ImageUrl]=@ImageUrl, [AcceptTime]=@AcceptTime," +
          "[UpdateTime]=@UpdateTime, [IsDeleted]=@IsDeleted WHERE [Id]=@Id")
          .SetParam("@Id", comment.Id)
          .SetParam("@UserId", comment.UserId)
          .SetParam("@PostId", comment.PostId)
          .SetParam("@CommentId", comment.CommentId)
          .SetParam("@Text", comment.Text)
          .SetParam("@ImageUrl", comment.ImageUrl)
          .SetParam("@AcceptTime", comment.CreateTime)
          .SetParam("@UpdateTime", comment.UpdateTime)
          .SetParam("@IsDeleted", comment.IsDeleted)
          .Execute();
      }
    }

    public Comment Read(int id)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Comments] WHERE [Id]=@Id")
          .SetParam("@Id", id)
          .ExecuteReader(r =>
          {
            r.Read();
            return Comment.Read(r);
          });
      }
    }

    public List<Comment> ReadPostComments(int postId)
    {
      using (var cx = new DataContext())
      {
        return cx.Query("SELECT * FROM [Comments] WHERE [PostId]=@PostId")
          .SetParam("@PostId", postId)
          .ExecuteReader(reader =>
          {
            List<Comment> comments = null;
            while (reader.Read())
            {
              if (comments == null)
                comments = new List<Comment>();

              comments.Add(Comment.Read(reader));
            }

            return comments;
          });
      }
    }
  }
}