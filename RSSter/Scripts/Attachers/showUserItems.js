/// <reference path="../jquery-2.1.3.intellisense.js" />
/// <reference path="~/Scripts/Developer/ratings.js" />
/// <reference path="../Developer/PostIsReadFunctionality.js" />

$(function() {
    $(".up").on("click", RateUp);
    $(".down").on("click", RateDown);
    $(".hide").on("click", PostIsRead);
    $("a").on("click", MarkAsRead);
    $("i").each(RateIconsInitialization);
    $(".itemRow").each(HideReadItems);
    $(".ui.checkbox")
        .checkbox();
    $("#unreadOrAll").on("change", CheckboxAllChange);    
})