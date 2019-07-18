var viewModel = {
    users: ko.observableArray()
};

function sendAjaxGetRequest(callback, url) {
    $.ajax(url ? "/" + url : "", {
        type: "GET",
        success: callback
    });
}

function sendAjaxPostRequest(url, postData, callback) {
    alert(postData);
    $.ajax(url ? "/" + url : "", {
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: callback,
        data: postData
    });
}

class usersViewModel {
    constructor(users) {
        var self = this;
        self.users = ko.observableArray(users);
        
        self.getAllUsers = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.users.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.users.push(returnedData[i]);
                }
            }, "Users/AllUsers");
        }.bind(self);
    }
}


$(document).ready(function () {

    var um = new usersViewModel();

    um.getAllUsers();
    
    viewModel.users = um.users;

    ko.applyBindings(viewModel);
});