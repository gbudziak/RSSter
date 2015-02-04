/// <reference path="../jquery-2.1.3.intellisense.js" />
/// <reference path="../Developer/PostIsReadFunctionalityOnShowAllItemsView.js" />
/// <reference path="~/Scripts/Developer/ratings.js" />
$(function () {
    $(".up").on("click", RateUp);
    $(".down").on("click", RateDown);
    $(".hide").on("click", PostIsRead);
    $("a").on("click", MarkAsRead);
    $("i").each(RateIconsInitialization);
    $(".ui.checkbox")
        .checkbox();
    $("#unreadOrAll").on("change", CheckboxAllChange);
    $(".label").on("change", RatingDisplayChange);
})