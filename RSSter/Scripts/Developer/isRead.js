/// <reference path="../jquery-2.1.3.intellisense.js" />
function PostIsRead(e) {
    var icon = $(e.target);
    var userItemId = icon.data("item-id");
    $("#ratingError").hide();
    $.ajax({
        url: "/RssReader/Read/?userItemId=" + userItemId,
        method: "POST",
        success: HideTr(userItemId),
        error: errorToggle()
    });
};

function HideTr(userItemId) {
    $("tr#" + userItemId).slideToggle();
};

function MarkAsRead(e) {
    PostIsRead(e);
}