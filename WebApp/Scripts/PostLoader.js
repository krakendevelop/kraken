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
  var self = this;
  posts = ko.observableArray([]);

  getData();
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
        post.LikeCount = ko.observable(post.LikeCount);
        post.DislikeCount = ko.observable(post.DislikeCount);
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