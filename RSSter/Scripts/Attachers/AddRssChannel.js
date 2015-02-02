$(document).ready(function () {
    $("#btnGoToChannel").mousedown(GoTo);
    $("#createSubmit").hide();
    $("#btnGoToChannel").hide();
    $("#Url").keyup(IsUrlInUserDatabase);
    $(window).keydown(function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            return false;
        }
    });
});