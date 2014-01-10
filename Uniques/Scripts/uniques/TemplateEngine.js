﻿var Uniques = Uniques || {};
Uniques.Classes = Uniques.Classes || {};

Uniques.Classes.TemplateEngine = function (baseUrl, containerId) 
{
	var _baseUrl = baseUrl;
	var _container;

	var getContainer = function() {
		if (_container == null || _container == undefined) {
			_container = $("#" + containerId);
		}

		return _container;
	};

	this.IsTemplateLoaded = function (templateName)
	{
		var element = document.getElementById(templateName);
		return element != null && element != undefined;
	};

	this.LoadTemplate = function (templateName, onSuccess)
	{
		$.ajax({
			url: _baseUrl + templateName,
			success: function (answer)
			{
				getContainer().append(answer);

				if (onSuccess != null && onSuccess != undefined)
				{
					onSuccess();
				}
			}
		});
	};

	this.LoadDataInto = function (model, templateName, target)
	{
		var binding = function ()
		{
			target.attr("data-bind", "template: { name: '" + templateName + "' }");
			ko.applyBindings(model, target[0]);
		};

		if (this.IsTemplateLoaded(templateName)) {
			binding();
		} else {
			this.LoadTemplate(templateName, binding);
		}
	};
};