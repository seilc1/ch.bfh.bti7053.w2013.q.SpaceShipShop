var AttributeListModel = function (userAttributes)
{
    var _userAttributes = userAttributes;
	var _this = this;
	
	this.ActiveNode = ko.observable();
	this.NewNode = ko.observable(new Uniques.UserAttribute());

    this.Attributes = ko.computed(function() { return _userAttributes.Attributes(); });
    this.Categories = ko.computed(function ()
    {
         return _userAttributes.Categories.Categories();
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

    this.SaveNew = function() {
        _userAttributes.SaveNew(this.NewNode());
        this.NewNode(new Uniques.UserAttribute());
    };

    this.Save = function (dataItem)
    {
        _userAttributes.Save(dataItem);
    };

    this.Delete = function (dataItem)
    {
        _userAttributes.Delete(dataItem);
    };
};