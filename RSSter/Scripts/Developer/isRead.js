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
    $("tr#" + userItemId).slideToggle();
};

function MarkAsRead(e) {
    PostIsRead(e);
}

function ShowReadItems() {
    $("tr").each(function(ind, e) {
        var tr = $(e);
        if (tr.data("item-read") == "True") {
            tr.show();            
        };
    });
}

function HideReadItems() {
    $("tr").each(function (ind, e) {
        var tr = $(e);
        if (tr.data("item-read") == "True") {
            tr.hide();            
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
