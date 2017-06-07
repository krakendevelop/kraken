function likePost(post) {
  $.ajax({
    type: 'GET',
    url: '/post/Like',
    data: { "postId": post.Id },
    dataType: 'json',
    success: function (data) {
      var current = post.LikeCount();
      post.LikeCount(current + 1);
    },
    error: function (error) {
      alert("Error while retrieving data!");
    }
  });
}

function dislikePost(post) {
  $.ajax({
    type: 'GET',
    url: '/post/Dislike',
    data: { "postId": post.Id },
    dataType: 'json',
    success: function (data) {
      var current = post.DislikeCount();
      post.DislikeCount(current + 1);
    },
    error: function (error) { 
      alert("Error while retrieving data!");
    }
  });
}