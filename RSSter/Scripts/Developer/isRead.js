function PostIsRead() {
    var userItemId = $(this).attr("id");
    $.ajax({
        url: "/RssReader/Read/?userItemId=" + userItemId,
        success: function (result) {
            if (result) {
                $("div#" + userItemId).slideToggle();
            } else {
                $("#Error").show();
            }
        }
    });
};