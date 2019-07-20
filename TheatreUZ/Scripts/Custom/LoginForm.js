var interval = setInterval(function () {
    var momentNow = moment();

    $('#time').html(momentNow.format('DD.MM.YYYY') + ' ' + momentNow.format('HH:mm:ss'));
}, 100);

$('.message a').click(function () {
    $('form').animate({ height: "toggle", opacity: "toggle" }, "slow");
});


function sendPostRequest(url, postData, callback) {

    $.ajax(url, {
        type: "POST",
        dataType: "text",
        data: "user=" + JSON.stringify(postData),
        success: callback,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        }
    });
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

$('#crBtn').click(function () {
    var name = $('#crName').val();
    var email = $('#crEmail').val();
    var password = $('#crPassword').val();
    
    var user = {
        Name: name,
        Email: email,
        Password: password
    }

    if (name === '' || password === '' || !isEmail(email)) {
        alert('toliqmas');
    }
    else {
        $('#closeModalBtn').click();
        sendPostRequest("Users/AddUser", user, function (data) {
            alert(data);
        })
    }
});

