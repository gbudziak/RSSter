/// <reference path="../jquery-2.1.3.intellisense.js" />
/// <reference path="~/Scripts/Developer/ratings.js" />
function PostIsRead(e) {
    var icon = $(e.target);
    var userItemId = icon.data("item-id");
    $("#ratingError").hide();
    $.ajax({
        url: "/Ajax/Read/?userItemId=" + userItemId,
        method: "POST",
        success: HideTr(userItemId),
        error: errorToggle
    });
};

function MarkAsRead(e) {
    PostIsRead(e);
}

function HideTr(userItemId) {
    var itemRow = $(".itemRow#" + userItemId);
    //itemRow.transition('fade up');
    itemRow.data("item-read", "True");
    itemRow.removeClass("unread");
};
