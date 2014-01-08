var CategoryListModel = function (baseUrl)
{
	var _baseUrl = baseUrl;
	var _this = this;
	
	this.ActiveNode = ko.observable();
	this.NewNode = ko.observable(new Uniques.UserAttributeCategory());
	this.Categories = ko.observableArray();

	this.Load = function ()
	{
		$.ajax({
			url: _baseUrl,
			success: function (data)
			{
				$(data).each(function ()
				{
					_this.Categories.push(new Uniques.Editable(this));
				});
			}
		});

		return _this;
	};

	this.SaveNew = function ()
	{
		$.ajax({
			url: _baseUrl,
			data: _this.NewNode(),
			type: "PUT",
			success: function (data)
			{
				_this.Categories.push(new Uniques.Editable(data));
				_this.NewNode(new Uniques.UserAttributeCategory());
			}
		});
	};

	this.Delete = function (dataItem)
	{
		$.ajax({
			url: _baseUrl,
			data: dataItem.Data,
			type: "DELETE"
		});

		_this.Categories.remove(dataItem);
	};

	this.Save = function (dataItem)
	{
		$.ajax({
			url: _baseUrl,
			data: dataItem.Data,
			type: "PUT"
		});
	};

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
};