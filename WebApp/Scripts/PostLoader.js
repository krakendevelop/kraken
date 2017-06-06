var pageSize = 10;
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

  this.like = function (post) {
    likePost(post);
  }

  this.dislike = function (post) {
    dislikePost(post);
  }

  this.likeCount = ko.observable("LikeCount");
  this.dislikeCount = ko.observable("DislikeCount");

  this.rating = ko.computed(function () {
    return this.likeCount - this.dislikeCount;
  });
}

function getData() {
  $.ajax({
    type: 'GET',
    url: '/post/LoadNextPosts',
    data: { "pageindex": pageIndex, "pagesize": pageSize },
    dataType: 'json',
    success: function(nextPosts) {
      if (nextPosts == null)
        return;

      for (var i = 0; i < nextPosts.length; i++) {
        var post = nextPosts[i];

        

        

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