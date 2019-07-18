var viewModel = {
    states: ko.observableArray()
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

class statesViewModel {
    constructor(states) {
        var self = this;
        self.states = ko.observableArray(states);
        self.getAllStates = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.states.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.states.push(returnedData[i]);
                }
            }, "States/AllStates");
        }.bind(self);
    }
}

$(document).ready(function () {

    var sm = new statesViewModel();

    sm.getAllStates();

    viewModel.states = sm.states;

    ko.applyBindings(viewModel);
});
