$(document)
  .ready(function() {
    $('.left.sidebar')
        .sidebar('attach events', '.toggle.button');

    $('.sidebar')
        .sidebar('attach events', '.toggle.thinbutton');
});