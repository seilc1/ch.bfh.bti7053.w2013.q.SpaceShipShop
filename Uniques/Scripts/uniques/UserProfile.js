var Uniques = Uniques || {};
Uniques.Classes = Uniques.Classes || {};

Uniques.Classes.UserProfile = function (baseUrl, imageUrl, categories, attributes)
{
    var _categories = categories;
    var _attributes = attributes;

    var userIdLastLoad;
    this.CurrentUserId = ko.observable(1);
    this.Editable = ko.observable(false);
    var _this = this;
    
    
    this.LoadData = function ()
    {
        _this.userIdLastLoad = _this.CurrentUserId();

        $.ajax({
            url: baseUrl.replace("{userId}", _this.CurrentUserId()),
            type: "GET",
            success: function (data)
            {
                _this.UserProfileDataList.removeAll();
                if (data.length > 0)
                {
                    $(data).each(function() {
                        _this.UserProfileDataList.push(new Uniques.Editable(ko.mapping.fromJS(this)));
                    });
                }
            }
        });

        $.ajax({
            url: imageUrl.replace("{userId}", _this.CurrentUserId()),
            type: "GET",
            success: function (data)
            {
                _this.UserProfileImageList.removeAll();
                if (data.length > 0)
                {
                    $(data).each(function ()
                    {
                        this.Url = imageUrl.replace("{userId}", _this.CurrentUserId()) + "/" + this.Id + "/thumbnail";
                        _this.UserProfileImageList.push(new Uniques.Editable(ko.mapping.fromJS(this)));
                    });
                }
            }
        });
    };
    
    this.UserProfileDataList = ko.observableArray();
    this.UserProfileImageList = ko.observableArray();
    this.UserProfileData = ko.computed(function ()
    {
        if (this.userIdLastLoad != this.CurrentUserId())
        {
            this.LoadData(this.CurrentUserId());
        }
        
        return this.UserProfileDataList();
    }, this);
    
    this.UserProfileImage = ko.computed(function ()
    {
        if (this.userIdLastLoad != this.CurrentUserId())
        {
            this.LoadData(this.CurrentUserId());
        }

        return this.UserProfileImageList();
    }, this);

    var DisplayAttribute = function() {
        this.Id = ko.observable();
        this.TextKey = ko.observable();
        this.Value = ko.observable();
        this.RefId = ko.observable();
        this.UpdateValue = _this.UpdateValueDelayed;
        this.UpdateValueImmediate = _this.UpdateValue;
    };

    this.Categories = ko.computed(function ()
    {
        var result = [];
        
        _categories.Categories().forEach(function (cat)
        {
            var curCategory = { Id: cat.Id(), TextKey: cat.TextKey(), Attributes: [] };

            _attributes.Attributes().forEach(function (attr)
            {
                if (attr.CategoryId() == curCategory.Id)
                {
                    var curAttribute = new DisplayAttribute();
                    curAttribute.Id(attr.Id());
                    curAttribute.TextKey(attr.TextKey());
                    
                    _this.UserProfileData().forEach(function (val)
                    {
                        if (val.AttributeTypeId() == curAttribute.Id())
                        {
                            curAttribute.Value(val.Value());
                            curAttribute.RefId(val.Id());
                        }
                    });

                    curCategory.Attributes.push(curAttribute);
                }
            });

            result.push(curCategory);
        });

        return result;
    });

    this.UpdateValue = function (attr)
    {
        _this.delayedTimeout = null;
        $.ajax({
            url: baseUrl.replace("{userId}", _this.CurrentUserId()),
            type: "POST",
            data: { AttributeTypeId: attr.Id(), Id: attr.RefId(), Value: attr.Value() }
        });
    };

    var delayedTimeout = null;
    this.UpdateValueDelayed = function (attr)
    {
        if (_this.delayedTimeout != null)
        {
            clearTimeout(_this.delayedTimeout);
        }

        _this.delayedTimeout = setTimeout(function () { _this.UpdateValue(attr); }, 500);
    };
};