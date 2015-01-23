/// <reference path="../jquery-2.1.3.intellisense.js" />
function RateUp(e) {
    var icon = $(e.target);
    var userItemId = icon.data("item-id");
    $("#ratingError").slideToggle();
    $.ajax({
        url: "/RssReader/RatingUp/?userItemId=" + userItemId,
        method:"POST",
        success: RateUpSuccess(icon),
        error: errorToggle
    });
};

function RateDown(e) {
    var icon = $(e.target);
    var userItemId = icon.data("item-id");
    $("#ratingError").hide();
    $.ajax({
        url: "/RssReader/RatingDown/?userItemId=" + userItemId,
        method: "POST",
        success: RateDownSuccess(icon),
        error: errorToggle
    });
};

function errorToggle() {
    $("#ratingError").show();
}

function RateUpSuccess(icon) {
    var up = icon.parent().find(".up");
    up.hide();
    var smile = icon.parent().find(".smile");
    smile.show();
    var frown = icon.parent().find(".frown");
    frown.hide();
    var down = icon.parent().find(".down");
    down.show();
}

function RateDownSuccess(icon) {
    var up = icon.parent().find(".up");
    up.show();
    var smile = icon.parent().find(".smile");
    smile.hide();
    var frown = icon.parent().find(".frown");
    frown.show();
    var down = icon.parent().find(".down");
    down.hide();
}

function RateIconsInitialization(idx,e) {
    
    var show = $(e).data("item-rate");
    console.log(show);

    if (show == "True") {
        $(e).hide();
    }
}

function HideEmoticons(idx, e) {
    var smile = $(e).find(".smile");
    var frown = $(e).find(".frown");
    if (smile.data("item-rate") == frown.data("item-rate")) {
        smile.hide();
        frown.hide();
    }
}