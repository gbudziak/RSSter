function RateUp() {
    var userItemId = $(this).attr("id");
    $.ajax({
        url: "/RssReader/RaitingUp/?userItemId=" + userItemId,
        success: function(result) {
            if (result) {                
                $("#"+userItemId,".up").dimmer("show");
            } else {
                $("#ratingError").show();
            }
        }
    });
};

function RateDown() {
    var userItemId = $(this).attr("id");
    $.ajax({
        url: "/RssReader/RaitingDown/?userItemId=" + userItemId,
        success: function (result) {
            if (result) {
                $("i#" + userItemId).fadeToggle();
            } else {
                $("#ratingError").show();
            }
        }
    });
};