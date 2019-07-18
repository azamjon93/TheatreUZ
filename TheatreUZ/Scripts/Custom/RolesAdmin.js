var viewModel = {
    roles: ko.observableArray()
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

class rolesViewModel {
    constructor(roles) {
        var self = this;
        self.roles = ko.observableArray(roles);
        self.getAllRoles = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.roles.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.roles.push(returnedData[i]);
                }
            }, "Roles/AllRoles");
        }.bind(self);
    }
}

$(document).ready(function () {

    var rm = new rolesViewModel();

    rm.getAllRoles();

    viewModel.roles = rm.roles;

    ko.applyBindings(viewModel);
});
