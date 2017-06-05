function like(postId) {
  $.ajax({
    type: 'GET',
    url: '/post/Like',
    data: { "postId": postId },
    dataType: 'json',
    success: function (data) {
      $("#points" + postId).text("+1");
    },
    error: function (error) {
      alert("Error while retrieving data!");
    }
  });
}

function dislike(postId) {
  $.ajax({
    type: 'GET',
    url: '/post/Dislike',
    data: { "postId": postId },
    dataType: 'json',
    success: function (data) {
      $("#points" + postId).text("-1");
    },
    error: function (error) {
      alert("Error while retrieving data!");
    }
  });
}