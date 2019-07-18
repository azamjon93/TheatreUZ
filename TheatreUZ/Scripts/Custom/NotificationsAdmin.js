var viewModel = {
    notifications: ko.observableArray()
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

class notificationsViewModel {
    constructor(notifications) {
        var self = this;
        self.notifications = ko.observableArray(notifications);

        self.getAllNotifications = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.notifications.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.notifications.push(returnedData[i]);
                }
            }, "Notifications/AllNotifications");
        }.bind(self);
    }
}


$(document).ready(function () {

    var nm = new notificationsViewModel();

    nm.getAllNotifications();

    viewModel.notifications = nm.notifications;

    ko.applyBindings(viewModel);
});