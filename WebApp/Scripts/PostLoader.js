var pageSize = 10;
var pageIndex = 0;

$(document).ready(function () {
  var lastLoadTime = Date.now();
  getData();

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

function getData() {
  $.ajax({
    type: 'GET',
    url: '/post/LoadNextPosts',
    data: { "pageindex": pageIndex, "pagesize": pageSize },
    dataType: 'json',
    success: function (data) {
      if (data != null) {
        for (var i = 0; i < data.length; i++) {
          $("#container").append(buildPost(data[i]));
        }
        pageIndex++;
      }
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

function buildPost(post) {
  return "<article class=\"post\">" +
					"<header class=\"post-header\">" +
						"<h3 class=\"post-header-title\">" + post.Title + "</h3>" +
					"</header>" +
					"<figure class=\"post-caption\">" +
					  "<img src=\"" + post.Content + "\" class=\"post-media\" alt=\"The Pulpit Rock\" width=\"700\" height=\"525\">" +
					  "<figcaption class=\"post-caption-data\">Fig1. - The Pulpit Rock, Norway.</figcaption>" +
					"</figure>" +
					"<footer class=\"post-footer\">" +
						"<span class=\"post-meta\">" +
							"<a id=\"points" + post.Id + "\" href=\"#\" class=\"post-meta-link\" target=\"_blank\">10525 points</a>" +
							"<a href=\"#\" class=\"post-meta-link\" target=\"_blank\">158 comments</a>" +
						"</span>" +
						"<span class=\"post-actions\">" +
							"<button onclick=\"like(" + post.Id + ")\" class=\"button post-actions-button\">Like</button>" +
							"<button onclick=\"dislike(" + post.Id + ")\" class=\"button post-actions-button\">Dislike</button>" +
							"<button class=\"button post-actions-button\">Comment</button>" +
						"</span>" +
					"</footer>" +
				"</article>";
}