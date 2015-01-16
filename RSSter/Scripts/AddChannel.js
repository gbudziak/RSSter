$(document).ready(function () {
    $("#btnGoToChannel").mousedown(GoTo);
    $("#createSubmit").hide();
    $("#btnGoToChannel").hide();
    $("#Link").keyup(IsLinkInDB);
});

function GoTo() {
    var link = $("#Link").val();
    window.location.assign("RssListView/?link=" + link);
}

function IsLinkInDB() {
    var link = $("#Link").val();
    $.ajax({
        url: "/Validation/IsLinkInDb/?link=" + link,
        success: function (result) {
            if (result) {
                $("#createSubmit").show();
            } else {
                $("#btnGoToChannel").show();
            }
        }
    });    
    $("#wrongLink").hide();
};


$("#AddRssForm").submit(function (event) {
    event.preventDefault();
    var link = $("#Link").val();
    $.ajax({
        url: "/Validation/IsLinkValid/?link=" + link,
        success: function (result) {
            if (result) {
                $("#AddRssForm").unbind("submit").submit();
            } else {
                $("#wrongLink").show();
            }

        }
    });
});