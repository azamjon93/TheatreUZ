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
        data: "userStr=" + JSON.stringify(postData),
        success: callback,
        error: function (xhr, ajaxOptions, thrownError) {
            //alert('eerroorr');
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
        PasswordHash: password
    }

    if (name === '' || password === '' || !isEmail(email)) {
        alert('toliqmas');
    }
    else {
        $('#closeModalBtn').click();
        sendPostRequest("Auth/AddUser", user, function (data) {
            //alert(data);
        })
    }
});

$('#loginBtn').click(function () {
    var email = $('#loginEmail').val();
    var password = $('#loginPassword').val();

    var user = {
        Email: email,
        PasswordHash: password
    }

    $('#closeModalBtn').click();
    sendPostRequest("Auth/LogIn", user, function (data) {
        //alert(data);
    });
    //location.reload();
});

$('#logoutBtn').click(function () {

    $('#closeModalBtn').click();
    sendPostRequest("Auth/LogOut", "", function (data) {
        //alert(data);
    });
    //location.reload();
});

