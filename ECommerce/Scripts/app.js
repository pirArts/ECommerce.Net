function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();

    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();

    function showError(jqXHR) {
        self.result(jqXHR.status + ': ' + jqXHR.statusText);
    }

    self.getAllAttendance = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/Attendance/GetAttendances',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.addAttendance = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var data = {
            Time: "2015-10-20",
            Type: "Test"
        };

        $.ajax({
            type: 'POST',
            url: '/api/Attendance/AddAttendance',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.getUserAppliedVacation = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/Vacation/GetUserAppliedVacations',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.getUserApproveVacation = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/Vacation/GetUserApproveVacations',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.AddVacation = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var data = {
            Type: 1,
            StartTime: "2015-12-20",
            EndTime: "2015-12-22",
            ApproverId: "65d57d15-fe31-4b56-aa57-f9b663a5147d"
        };

        $.ajax({
            type: 'POST',
            url: '/api/Vacation/AddVacation',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.ApproveVacation = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var data = {
            Id: 4
        };

        $.ajax({
            type: 'POST',
            url: '/api/Vacation/ApproveVacation',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.callApi = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/values',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.register = function () {
        self.result('');

        var data = {
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(showError);
    }

    self.login = function () {
        self.result('');

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
        }).fail(showError);
    }

    self.logout = function () {
        self.user('');
        sessionStorage.removeItem(tokenKey);
    }

    self.getAllUsers = function () {
        self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/Account/GetAllUsers',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }
}

var app = new ViewModel();
ko.applyBindings(app);