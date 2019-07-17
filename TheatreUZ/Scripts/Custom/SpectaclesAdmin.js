function sendAjaxRequest(httpMethod, callback, url) {
    $.ajax(url ? "/" + url : "", {
        type: httpMethod,
        success: callback
    });
}

var viewModel = {
    spectacles: ko.observableArray(),
    genres: ko.observableArray()
};

//function addSpectacle() {
//    var s = new Object();
//    s.GenreID = ID;
//    s.Name = name;
//    s.Cost = cost;

//    spectacles.push(s);
//}

//function getAllSpectacles() {
//    sendAjaxRequest("GET", function (data) {
//        var returnedData = JSON.parse(data);
//        viewModel.spectacles.removeAll();
//        for (var i = 0; i < returnedData.length; i++) {
//            viewModel.spectacles.push(returnedData[i]);
//        }
//    }, "Spectacles/All");
//}

//function getAllGenres() {
//    sendAjaxRequest("GET", function (data) {
//        var returnedData = JSON.parse(data);
//        viewModel.genres.removeAll();
//        for (var i = 0; i < returnedData.length; i++) {
//            viewModel.genres.push(returnedData[i]);
//        }
//    }, "Genres/AllGenres");
//}

//function removeItem(item) {
//    sendAjaxRequest("DELETE", function () {
//        getAllSpectacles()
//    }, item.ReservationId);
//}

//$(document).ready(function () {
//    getAllGenres();
//    getAllSpectacles();
//    ko.applyBindings(viewModel);
//});



var GenresModel = function (genres) {
    var self = this;
    self.genres = ko.observableArray(genres);

    this.getAllGenres = function () {
        
        sendAjaxRequest("GET", function (data) {
            var returnedData = JSON.parse(data);
            self.genres.removeAll();
            for (var i = 0; i < returnedData.length; i++) {
                self.genres.push(returnedData[i]);
            }
        }, "Genres/AllGenres");
    }.bind(this);
};

var SpectaclesModel = function (spectacles) {
    var self = this;
    self.spectacles = ko.observableArray(spectacles);

    this.getAllSpectacles = function () {
        sendAjaxRequest("GET", function (data) {
            var returnedData = JSON.parse(data);
            self.spectacles.removeAll();
            for (var i = 0; i < returnedData.length; i++) {
                self.spectacles.push(returnedData[i]);
            }
        }, "Spectacles/All");
    }.bind(this);

    this.addSpectacle = function () {
        sendAjaxRequest("GET", function (data) {

        }, "Home/Index");
    }.bind(this);
};

$(document).ready(function () {

    var gm = new GenresModel([
        {
            ID: '23C06379-81A8-E911-90A2-C8D3FFD5B866',
            StateID: '1BC06379-81A8-E911-90A2-C8D3FFD5B866',
            Name: 'Drama',
            RegDate: '2019-07-17 15:56:06'
        }]);

    var sm = new SpectaclesModel([
        {
            ID: '25C06379-81A8-E911-90A2-C8D3FFD5B866',
            GenreID: '23C06379-81A8-E911-90A2-C8D3FFD5B866',
            StateID: '1BC06379-81A8-E911-90A2-C8D3FFD5B866',
            Name: 'Othello',
            Cost: 60000,
            PlayDate: '2019-07-27 19:00:00',
            RegDate: '2019-07-17 15:56:06'
        }]);

    gm.getAllGenres();
    sm.getAllSpectacles();

    viewModel.genres = gm.genres;
    viewModel.spectacles = sm.spectacles;

    ko.applyBindings(viewModel);
});