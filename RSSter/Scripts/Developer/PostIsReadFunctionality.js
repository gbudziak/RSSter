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
    itemRow.transition('fade left');
    //$(".itemRow#" + userItemId).slideToggle();
    itemRow.data("item-read", "True");
};

function MarkAsRead(e) {
    PostIsRead(e);
}

function ShowReadItems() {
    $(".itemRow").each(function(ind, e) {
        var itemRow = $(e);
        if (itemRow.data("item-read") == "True") {
            itemRow.show();            
        };
    });
}

function HideReadItems() {
    $(".itemRow").each(function (ind, e) {
        var itemRow = $(e);
        if (itemRow.data("item-read") == "True") {
            itemRow.hide();            
        };
    });
}

function CheckboxAllChange(e) {
    var checkbox = $(e.target);
    if (checkbox.prop('checked')) {
        ShowReadItems();
    } else {
        HideReadItems();
    }
}
