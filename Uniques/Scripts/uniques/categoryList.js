var CategoryListModel = function (userAttributesCategories)
{
    var _userAttributesCategories = userAttributesCategories;
    var _this = this;
    
	this.ActiveNode = ko.observable();
	this.NewNode = ko.observable(new Uniques.UserAttributeCategory());
	this.Categories = ko.computed(function ()
	{
	    return _userAttributesCategories.Categories();
	});

	this.SetActive = function ()
	{
		if (_this.ActiveNode() != null)
		{
			_this.ActiveNode().IsActive(false);
		}
		
		_this.ActiveNode(this);
		_this.ActiveNode().IsActive(true);
	};

	this.SetInActive = function (ele) {
		_this.Save(ele);
		_this.ActiveNode().IsActive(false);
		_this.ActiveNode(null);
	};

	this.SaveNew = function ()
	{
	    _userAttributesCategories.SaveNew(this.NewNode());
	    this.NewNode(new Uniques.UserAttributeCategory());
	};
    
	this.Save = function (dataItem)
	{
	    _userAttributesCategories.Save(dataItem);
	};

	this.Delete = function (dataItem)
	{
	    _userAttributesCategories.Delete(dataItem);
	};
};