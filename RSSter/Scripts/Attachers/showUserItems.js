/// <reference path="../jquery-2.1.3.intellisense.js" />
$(function() {
    $(".up").on("click", RateUp);
    $(".down").on("click", RateDown);
    $(".hide").on("click", PostIsRead);
    $("a").on("click", MarkAsRead);
    $("i").each(RateIconsInitialization);
    $("tr").each(HideEmoticons);
})