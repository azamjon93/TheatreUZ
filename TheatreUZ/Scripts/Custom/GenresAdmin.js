var viewModel = {
    genres: ko.observableArray()
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

class genresViewModel {
    constructor(genres) {
        var self = this;
        self.genres = ko.observableArray(genres);
        self.getAllGenres = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.genres.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.genres.push(returnedData[i]);
                }
            }, "Genres/AllGenres");
        }.bind(self);
    }
}

$(document).ready(function () {

    var gm = new genresViewModel();

    gm.getAllGenres();

    viewModel.genres = gm.genres;

    ko.applyBindings(viewModel);
});
