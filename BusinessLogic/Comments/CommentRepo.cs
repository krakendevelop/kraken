﻿using System;
using System.Collections.Generic;
using BusinessLogic.Posts;
using Common.Serialization;
using Data;

namespace BusinessLogic.Comments
{
  public class CommentRepo
  {
    public int Save(Comment comment)
    {
      throw new NotImplementedException();
    }

    public void Update(int id, Comment comment)
    {
      throw new NotImplementedException();
    }

    public Comment Read(int id)
    {
      throw new NotImplementedException();
    }

    public List<Comment> ReadPostComments(int postId)
    {
      throw new NotImplementedException();
    }
  }
}