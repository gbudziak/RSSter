var content = [
    { title: 'Andorrs' },
    { title: 'United Arab Emirates' },
    { title: 'Afghanistas' },
    { title: 'Antigus' },
    { title: 'Anguills' },
    { title: 'Albanis' },
    { title: 'Armenis' },
    { title: 'Netherlands Antilles' },
    { title: 'Angols' },
    { title: 'Argentins' },
    { title: 'American Samos' },
    { title: 'Austris' },
    { title: 'Australis' },
    { title: 'Arubs' },
    { title: 'Aland Islands' },
    { title: 'Azerbaijas' },
    { title: 'Bosnis' }
    // etc
];


$(document).ready(function () {
    $('.ui.search')
        .search({
            source: content
        });
});

$(document).ready(function () {
    $('#selectRegisterType').on('change', function () {
        alert("ddddddd");
    });
});

