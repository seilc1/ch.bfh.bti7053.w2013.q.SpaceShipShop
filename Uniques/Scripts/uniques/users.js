﻿var Uniques = Uniques || {};

Uniques.User = function (ref)
{
    this.Loginname = ko.observable("Loginname");
    this.Displayname = ko.observable("Displayname");
    this.Password = ko.observable("Password");
    this.Email = ko.observable("Email");

    this.UserLoader = function() {
        return ref;
    };
    
    this.Users = ko.observableArray();
};

Uniques.UserList = function() {
    this.Users = ko.observableArray();
};

Uniques.UserLoader = function() {

};

Uniques.UserLoader.prototype = {
    _user: null,
    LoadUser: function() {
        $.ajax({ url: "/api/users", type: "GET", success: this.LoadUserCallBack });
    },
    LoadUserCallBack: function (data)
    {
    },
    BindNewUser: function (htmlElement)
    {
        _user = new Uniques.User(this);
        ko.applyBindings(_user, htmlElement);
    },
    SaveUser: function ()
    {
        $.ajax({
            url: "/api/users",
            type: "PUT",
            data: _user,
            error: function (response)
            {
                alert(response.responseText);
            },
            success: function (response)
            {
                _user.Users.push(response);
            }
        });
    }
};