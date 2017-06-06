function likePost(post) {
  $.ajax({
    type: 'GET',
    url: '/post/Like',
    data: { "postId": post.Id },
    dataType: 'json',
    success: function (data) {
      post.LikeCount++;
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
      post.DislikeCount++;
    },
    error: function (error) { 
      alert("Error while retrieving data!");
    }
  });
}