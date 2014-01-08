var AttributeListModel = function (baseUrl, categoryUrl)
{
	var _baseUrl = baseUrl;
	var _categoryUrl = categoryUrl;
	var _this = this;
	
	this.ActiveNode = ko.observable();
	this.NewNode = ko.observable(new Uniques.UserAttribute());
	this.Categories = ko.observableArray();
	this.Attributes = ko.observableArray();

	this.Load = function() {
		_this.LoadCategories();

		$.ajax({
			url: _baseUrl,
			success: function (data) {
				$(data).each(function () {
					_this.Attributes.push(new Uniques.Editable(this));
				});
			}
		});
	};

	this.ResolveCategory = function (categoryId)
	{
		var res;
		$(this.Categories()).each(function() {
			if (this.Id == categoryId) {
				res = this;
			}
		});

		return res;
	};

	this.LoadCategories = function ()
	{
		$.ajax({
			url: _categoryUrl,
			success: function (data)
			{
				$(data).each(function ()
				{
					_this.Categories.push(this);
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
				_this.Attributes.push(new Uniques.Editable(data));
				_this.NewNode(new Uniques.UserAttribute());
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

		_this.Attributes.remove(dataItem);
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