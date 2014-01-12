var Uniques = Uniques || { };
Uniques.Classes = Uniques.Classes || {};

Uniques.Classes.UserAttributes = function (baseUrl, categories)
{
    var _baseUrl = baseUrl;
    var _this = this;

    this.Loaded = ko.observable(false);

    this.AttributeList = ko.observableArray();
    this.Categories = categories;

    this.Load = function ()
    {
        $.ajax({
            url: _baseUrl,
            success: function (data)
            {
                _this.Loaded(true);

                _this.AttributeList.removeAll();
                $(data).each(function ()
                {
                    var item = this;
                    this.CategoryName = ko.computed(function ()
                    {
                        var cat = _this.ResolveCategory(item.CategoryId);
                        
                        if (cat != null) {
                            return cat.TextKey();
                        }
                        else
                        {
                            return "-- Not Set --";
                        }
                    });
                   _this.AttributeList.push(new Uniques.Editable(ko.mapping.fromJS(this)));
                });
            }
        });
    };

    this.Attributes = ko.computed(function ()
    {
        if (!_this.Loaded())
        {
            _this.Load();
        }

        return _this.AttributeList();
    });


    this.ResolveCategory = function(categoryId) {
        var res;
        $(this.Categories.Categories()).each(function ()
        {
            if (this.Id() == categoryId) {
                res = this;
            }
        });

        return res;
    };

    this.SaveNew = function (newNode)
    {
        $.ajax({
            url: _baseUrl,
            data: newNode,
            type: "PUT",
            success: function (data)
            {
                data.CategoryName = ko.computed(function ()
                {
                    var cat = _this.ResolveCategory(item.CategoryId);

                    if (cat != null)
                    {
                        return cat.TextKey();
                    }
                    else
                    {
                        return "-- Not Set --";
                    }
                });
                _this.AttributeList.push(new Uniques.Editable(ko.mapping.fromJS(data)));
                newNode = new Uniques.UserAttribute();
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

        _this.AttributeList.remove(dataItem);
    };

    this.Save = function (dataItem)
    {
        $.ajax({
            url: _baseUrl,
            data: dataItem.Data,
            type: "PUT"
        });
    };
};