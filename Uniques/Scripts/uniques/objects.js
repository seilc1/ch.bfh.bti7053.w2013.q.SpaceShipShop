var Uniques = Uniques || {};

Uniques.User = function ()
{
    this.Id = ko.observable();
    this.Loginname = ko.observable();
    this.Displayname = ko.observable();
    this.Password = ko.observable();
    this.Email = ko.observable();
    this.IsAdmin = ko.observable();
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

Uniques.Page = function ()
{
    this.currentUser = ko.observable();
    this.targetedUser = ko.observable();

    this.ProfileHeaderTemplate = ko.computed(function ()
    {
        var currentUser = this.currentUser();
        var targetedUser = this.targetedUser();
        
        var templateName = currentUser != null && currentUser.Id() != null ? "LoggedIn" : "LoggedOut";
        templateName += targetedUser != null ? "Targeting" : "NotTargeting";

	    Uniques.TemplateEngine.EnsureTemplateSync(templateName);
        return templateName;
    }, this);

    this.MainNavigation = ko.computed(function ()
    {
        var currentUser = this.currentUser();
        var nodes = new Array();

        nodes.push("Search");
        nodes.push("Profile");
        
        if (currentUser != null && currentUser.IsAdmin())
        {
            nodes.push("Search");
        }
    }, this);
}