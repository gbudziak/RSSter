/// <reference path="../jquery-2.1.3.intellisense.js" />
function PostIsRead(e) {
    var icon = $(e.target);
    var userItemId = icon.data("item-id");
    $("#ratingError").hide();
    $.ajax({
        url: "/Ajax/Read/?userItemId=" + userItemId,
        method: "POST",
        success: HideTr(userItemId),
        error: errorToggle()
    });
};

function HideTr(userItemId) {
    var itemRow = $(".itemRow#" + userItemId);
    //itemRow.transition('fade up');
    itemRow.data("item-read", "True");
    itemRow.removeClass("yellow");
};

function MarkAsRead(e) {
    PostIsRead(e);
}

//function ToggleReadItems(command) {
//    $(".itemRow").each(function(ind, e) {
//        var itemRow = $(e);
//        if (itemRow.data("item-read") == "True") {
//            if (command == "hide") {
//                itemRow.transition("fade up");
//            }
//            if (command == "show") {
//                itemRow.transition("fade down");
//            }
//        };
//    });
//}

//function CheckboxAllChange(e) {
//    var checkbox = $(e.target);
//    if (checkbox.prop("checked")) {
//        ToggleReadItems("hide");
//    } else {
//        ToggleReadItems("show");
//    }
//}
