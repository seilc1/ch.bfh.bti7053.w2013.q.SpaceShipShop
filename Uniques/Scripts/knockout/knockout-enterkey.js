ko.bindingHandlers.enterkey = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var allBindings = allBindingsAccessor();

		$(element).on('keypress', function (e)
		{
			var keyCode = e.which || e.keyCode;
			if (keyCode !== 13) {
				return true;
			}

			var target = e.target;
			target.blur();

			allBindings.enterkey.call(viewModel, viewModel, target, element);

			return false;
		});
	}
};