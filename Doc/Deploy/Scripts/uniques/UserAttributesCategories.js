var Uniques = Uniques || { };
Uniques.Classes = Uniques.Classes || {};

Uniques.Classes.UserAttributesCategories = function (baseUrl)
{
    var _baseUrl = baseUrl;
    var _this = this;
    this.Loaded = ko.observable(false);

    this.CategoryList = ko.observableArray();
    
    this.Load = function ()
    {
        $.ajax({
            url: _baseUrl,
            success: function (data)
            {
                _this.Loaded(true);

                _this.CategoryList.removeAll();
                $(data).each(function ()
                {
                    _this.CategoryList.push(new Uniques.Editable(ko.mapping.fromJS(this)));
                });
            }
        });

        return _this;
    };

    this.Categories = ko.computed(function ()
    {
        if (!_this.Loaded())
        {
            _this.Load();
        }
        return _this.CategoryList();
    });

    this.SaveNew = function (newNode)
    {
        $.ajax({
            url: _baseUrl,
            data: newNode,
            type: "PUT",
            success: function(data) {
                _this.CategoryList.push(new Uniques.Editable(ko.mapping.fromJS(data)));
            }
        });
    };

    this.Delete = function(dataItem) {
        $.ajax({
            url: _baseUrl,
            data: dataItem,
            type: "DELETE"
        });

        _this.CategoryList.remove(dataItem);
    };

    this.Save = function(dataItem) {
        $.ajax({
            url: _baseUrl,
            data: dataItem,
            type: "PUT"
        });
    };
};