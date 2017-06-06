using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLogic.Ratings;
using Common.Exceptions;

namespace BusinessLogic.Posts
{
  public static class PostManager
  {
    public static List<Post> TestPosts = new List<Post>();

    static PostManager()
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
        TestPosts.Add(post);
        post.Id = i;
      }
    }

    public static Post Create(int userId, string title, string content)
    {
      var post = new Post(userId, title, content);
      post.Id = Repositories.Posts.Save(post);

      return post;
    }

    public static void Update(Post post)
    {
      Repositories.Posts.Update(post.Id, post);
    }

    public static Post Get(int id)
    {
      return Repositories.Posts.Read(id);
    }

    public static List<Post> GetAll(int idFrom, int count)
    {
      //return Repositories.Posts.ReadAll(idFrom, count);
      var lastShown = TestPosts.ElementAt(idFrom);
      return TestPosts.SkipWhile(p => p != lastShown).Take(count).ToList();
    }

    public static Rating Like(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Like, RatingTargetKindId.Post, id);

      Repositories.Ratings.Delete(rating);
      Repositories.Ratings.Save(rating);
      return rating;
    }

    public static List<Rating> GetRatings(int postId)
    {
      return Repositories.Ratings.ReadByPostId(postId);
    }

    public static Rating Dislike(int userId, int id)
    {
      var rating = new Rating(userId, RatingKindId.Dislike, RatingTargetKindId.Post, id);

      Repositories.Ratings.Delete(rating);
      Repositories.Ratings.Save(rating);
      return rating;
    }

    public static void RemoveRating(int userId, int id)
    {
      var rating = new Rating(userId, RatingTargetKindId.Post, id);
      Repositories.Ratings.Delete(rating);
    }
  }
}