$(document).ready(function() {
    $("#btnGoToChannel").mousedown(GoTo);
    //$("#createSubmit").hide();
    //$("#btnGoToChannel").hide();
    $("#Link").keyup(function() {
        var link = $("#Link").val();
        $.ajax({
            url: "/Validation/LinkValidation/?link=" + link,
            success: function(result) {
                if (result) {
                    $("#createSubmit").show();                    
                } else {
                    $("#btnGoToChannel").show();
                }
            }
    });
});
});

function GoTo() {
    var link = $("#Link").val();
    window.location.assign("RssListView/?link=" + link);
}