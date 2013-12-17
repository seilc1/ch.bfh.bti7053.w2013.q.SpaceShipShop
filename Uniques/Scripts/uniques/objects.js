var Uniques = Uniques || {};

Uniques.User = function ()
{
    this.Loginname = ko.observable();
    this.Displayname = ko.observable();
    this.Password = ko.observable();
    this.Email = ko.observable();
};

Uniques.UserAttributeCategory = function ()
{
    this.Id = ko.observable();
    this.TextKey = ko.observable();
};

Uniques.UserAttribute = function ()
{
    this.Id = ko.observable();
    this.TextKey = ko.observable();
    this.DefaultText = ko.observable();
    this.DefaultDescription = ko.observable();
    this.Searchable = ko.observable();
    this.CategoryId = ko.observable();
};

Uniques.Editable = function(data) {
	this.Editable = ko.observable(false);
	this.IsActive = ko.observable(false);
    this.Data = data;
};