$(document).ready(function () {
    $("#btnGoToChannel").mousedown(GoTo);
    $("#createSubmit").hide();
    $("#btnGoToChannel").hide();
    $("#Link").keyup(IsLinkInDB);
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
});

//Function that assigns destination for Go to channel button.
function GoTo() {
    var link = $("#Link").val();
    window.location.assign("RssListView/?link=" + link);
}

//Feature method for user convenience, it uses ajax call for controller action, if user has the rss channel he tryes to add on his list already button to go to channel is shows, else create button is shown.
function IsLinkInDB() {
    var link = $("#Link").val();
    $.ajax({
        url: "/Validation/IsLinkInUserDatabe/?link=" + link,
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

//Validation method from user side. It axaj calls controller action, if validation is true it unbinds and submits the form, if not is shows a message.
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