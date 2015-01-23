/// <reference path="../jquery-2.1.3.intellisense.js" />
$(function () {
    $('.left.sidebar')
        .sidebar('attach events', '.toggle.button');

    $('.sidebar')
        .sidebar('attach events', '.toggle.thinbutton');
})