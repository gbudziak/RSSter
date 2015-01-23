/// <reference path="../jquery-2.1.3.intellisense.js" />
function RateUp(e) {
    var icon = $(e.target);
    var userItemId = icon.data("item-id");
    $("#ratingError").slideToggle();
    $.ajax({
        url: "/RssReader/RatingUp/?userItemId=" + userItemId,
        method:"POST",
        success: RateSuccess(icon),
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
        success: RateSuccess(icon),
        error: errorToggle
    });
};

function errorToggle() {
    $("#ratingError").show();
}

function RateSuccess(icon) {
    var up = icon.parent().find(".up");
    up.toggle();
    var smile = icon.parent().find(".smile");
    smile.toggle();
    var frown = icon.parent().find(".frown");
    frown.toggle();
    var down = icon.parent().find(".down");
    down.toggle();
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