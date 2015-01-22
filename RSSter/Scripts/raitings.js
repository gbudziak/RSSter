function RateUp() {
    var userItemId = $("i.rateup").attr("id");
    $.ajax({
        url: "/RssReader/RaitingUp/?userItemId=" + userItemId;
})
}