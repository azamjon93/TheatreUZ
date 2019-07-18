var viewModel = {
    sales: ko.observableArray()
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

class salesViewModel {
    constructor(sales) {
        var self = this;
        self.sales = ko.observableArray(sales);

        self.getAllSales = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.sales.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.sales.push(returnedData[i]);
                }
            }, "Sales/AllSales");
        }.bind(self);
    }
}


$(document).ready(function () {

    var sm = new salesViewModel();

    sm.getAllSales();

    viewModel.sales = sm.sales;

    ko.applyBindings(viewModel);
});