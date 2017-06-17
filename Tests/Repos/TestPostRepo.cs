using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLogic.Posts;
using log4net;
using log4net.Config;

namespace Tests.Repos
{
  public class TestPostRepo : IPostRepo
  {
    private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private int _lastPostId;
    private readonly List<Post> _posts;

    public TestPostRepo()
    {
      _lastPostId = 0;
      _posts = new List<Post>();

      InitPosts();
    }

    private void InitPosts()
    {
      var images = new List<string>
      {
        Path.Combine("https://www.w3schools.com/css/img_fjords.jpg"),
        Path.Combine("https://www.smashingmagazine.com/wp-content/uploads/2015/06/10-dithering-opt.jpg"),
        Path.Combine("https://ichef.bbci.co.uk/news/976/media/images/83351000/jpg/_83351965_explorer273lincolnshirewoldssouthpicturebynicholassilkstone.jpg"),
        Path.Combine("https://c2.staticflickr.com/2/1520/24330829813_944c817720_b.jpg"),
        Path.Combine("http://coolwildlife.com/wp-content/uploads/galleries/post-3004/Fox%20Picture%20006.jpg"),
        Path.Combine("https://s-media-cache-ak0.pinimg.com/originals/bc/a6/9b/bca69b8816de00251b539a2d16575353.jpg"),
        Path.Combine("https://apod.nasa.gov/apod/image/1611/ChicagoClouds_Hersch_3600.jpg"),
        Path.Combine("https://www.edmonton.ca/attractions_events/documents/Images/BigPicturePrize2_DickTam.jpg"),
        Path.Combine("http://news.nationalgeographic.com/content/dam/news/2016/08/10/lion-day-gallery/01-lion-day-gallery.jpg"),
        Path.Combine("http://www.theamazingpics.com/wp-content/uploads/2014/06/Amazing-Picture-of-a-Newborn-Elephant-in-Africa.jpg"),
        Path.Combine("http://news.nationalgeographic.com/content/dam/news/2017/04/11/black-hole/black-hole-event-horizon-01.ngsversion.1491940808945.jpg")
      };

      for (int i = 1; i < 5000; i++)
      {
        var post = new Post(1, "Post title number " + i, images[i % images.Count]);
        _posts.Add(post);
        post.Id = ++_lastPostId;
      }

      Logger.Debug("Initialized posts");
    }

    public int Save(Post post)
    {
      post.Id = _lastPostId++;
      _posts.Add(post);
      return _lastPostId;
    }

    public void Update(int id, Post post)
    {
      var postToUpdate = _posts.Single(p => p.Id == id);
      postToUpdate.Update(post.Title, post.Content);
    }

    public Post Read(int id)
    {
      return _posts.SingleOrDefault(p => p.Id == id);
    }

    public List<Post> ReadAll(int idFrom, int count)
    {
      var posts = _posts
        .SkipWhile(p => p.Id < idFrom)
        .Take(count)
        .ToList();

      Logger.Debug("Loaded next posts");
      return posts;
    }

    public void Delete(int id)
    {
      _posts.RemoveAll(p => p.Id == id);
    }
  }
}