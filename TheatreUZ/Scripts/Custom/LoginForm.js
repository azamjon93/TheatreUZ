var interval = setInterval(function () {
    var momentNow = moment();

    $('#time').html(momentNow.format('DD.MM.YYYY') + ' ' + momentNow.format('HH:mm:ss'));
}, 100);

$('.message a').click(function () {
    $('form').animate({ height: "toggle", opacity: "toggle" }, "slow");
});

