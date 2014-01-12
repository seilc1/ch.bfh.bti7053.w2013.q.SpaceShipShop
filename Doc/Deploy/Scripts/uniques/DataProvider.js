var Uniques = Uniques || {};
Uniques.Classes = Uniques.Classes || { };

Uniques.Classes.DataProvider = function() {
	this.Data = new Array();

	this.GetData = function(key) {
		return this.Data[key];
	};

	this.SetData = function(key, data) {
		this.Data[key] = data;
	};
};
Uniques.DataProvider = new Uniques.Classes.DataProvider();