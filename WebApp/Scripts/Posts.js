var pageSize = 20;
var pageIndex = 0;

$(document).ready(function () {
  var lastLoadTime = Date.now();
  ko.applyBindings(new ViewModel());

  var loading = false;
  $(window).scroll(function () {
    var currentScroll = $(window).scrollTop();
    var docHeight = $(document).height();
    var windowHeight = $(window).height();
    var timeValid = (Date.now() - lastLoadTime) > 2000;
    var preloadBefore = 3000;

    if (!loading && timeValid && currentScroll >= docHeight - windowHeight - preloadBefore) {
      loading = true;
      getData();
      loading = false;
      lastLoadTime = Date.now();
    }
  });
});

function ViewModel() {
  posts = ko.observableArray([]);
  getData();
}

function getData() {
  $.ajax({
    type: 'GET',
    url: '/post/GetNext',
    data: { "pageindex": pageIndex, "pagesize": pageSize },
    dataType: 'json',
    success: function(nextPosts) {
      if (nextPosts == null)
        return;

      for (var i = 0; i < nextPosts.length; i++) {
        var post = nextPosts[i];

        post.LikeCount = ko.observable(post.LikeCount);
        post.DislikeCount = ko.observable(post.DislikeCount);
        post.CommentCount = ko.observable(post.CommentCount); // todo vkoshman fix this

        posts.push(post);
      }

      pageIndex++;
    },
    beforeSend: function () {
      $("#progress").show();
    },
    complete: function () {
      $("#progress").hide();
    },
    error: function () {
      alert("Error while retrieving data!");
    }
  });
}

function likePost(post) {
  $.ajax({
    type: 'POST',
    url: '/post/Like',
    data: { "postId": post.Id },
    dataType: 'json',
    success: function (data) {
      var currentLikes = post.LikeCount();
      var currentDislikes = post.DislikeCount();
      post.LikeCount(currentLikes + 1);
      post.DislikeCount(currentDislikes - 1);
    },
    error: function (error) {
      alert("Error while retrieving data!");
    }
  });
}

function dislikePost(post) {
  $.ajax({
    type: 'POST',
    url: '/post/Dislike',
    data: { "postId": post.Id },
    dataType: 'json',
    success: function (data) {
      var currentDislikes = post.DislikeCount();
      var currentLikes = post.LikeCount();
      post.DislikeCount(currentDislikes + 1);
      post.LikeCount(currentLikes - 1);
    },
    error: function (error) {
      alert("Error while retrieving data!");
    }
  });
}