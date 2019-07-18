var viewModel = {
    spectacles: ko.observableArray(),
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

class spectacleModel {
    constructor() {
        var self = this;
        self.spectacles = ko.observableArray();

        self.GenreID = ko.observable("");
        self.Name = ko.observable("");
        self.Cost = ko.observable("");
        self.PlayDate = ko.observable("");
        self.RegDate = ko.observable("");

        self.addS = function () {

            var spectacle = {
                GenreID: self.GenreID(),
                //StateID: viewModel.genres[0].StateID,
                Name: self.Name(),
                Cost: self.Cost(),
                PlayDate: Date.now(),
                RegDate: Date.now()
            };

            self.spectacle = ko.observable();

            sendAjaxPostRequest("Spectacles/Create", ko.toJSON(spectacle), function () {
                alert('Create OK !');
            });
        }
    }
}

class spectaclesViewModel {
    constructor(spectacles) {
        var self = this;
        self.spectacles = ko.observableArray(spectacles);

        self.GenreID = ko.observable("");
        self.Name = ko.observable("");
        self.Cost = ko.observable("");
        self.PlayDate = ko.observable("");
        self.RegDate = ko.observable("");

        self.addS = function () {

            var spectacle = {
                GenreID: self.GenreID(),
                //StateID: viewModel.genres[0].StateID,
                Name: self.Name(),
                Cost: self.Cost(),
                PlayDate: Date.now(),
                RegDate: Date.now()
            };

            self.spectacle = ko.observable();

            sendAjaxPostRequest("Spectacles/Create", ko.toJSON(spectacle), function () {
                alert('Create OK !');
            });
        }

        self.getAllSpectacles = function () {
            sendAjaxGetRequest(function (data) {
                var returnedData = JSON.parse(data);
                self.spectacles.removeAll();
                for (var i = 0; i < returnedData.length; i++) {
                    self.spectacles.push(returnedData[i]);
                }
            }, "Spectacles/AllSpectacles");
        }.bind(self);
    }
}


$(document).ready(function () {

    var gm = new genresViewModel();
    var sm = new spectaclesViewModel();

    gm.getAllGenres();
    sm.getAllSpectacles();

    viewModel.genres = gm.genres;
    viewModel.spectacles = sm.spectacles;

    ko.applyBindings(viewModel);
});