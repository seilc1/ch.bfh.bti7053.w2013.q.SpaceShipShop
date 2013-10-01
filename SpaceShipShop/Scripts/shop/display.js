var SpaceShop = SpaceShop || {};

SpaceShop.Display = function() {

	var heightScaler = function ()
	{
		var topNavHeight = $(".top-navigation").height();

		$(".pillar").css("height", $(window).height());
		$(".col-container").css("height", $(window).height() - topNavHeight);
		$(".col-container .slider").css("margin-top", ($(window).height() - topNavHeight - 40) / 2);
	};

	var sliderHandler = function ()
	{
		$(".col-container").toggleClass("collapsed");
		
		if ($(".col-container").hasClass("collapsed"))
		{
			$(".col-container .slider").html("&laquo;");
		} else {
			$(".col-container .slider").html("&raquo;");
		}

		console.log("sliding");
	};

	$(".slider").on("click", function() { sliderHandler(); });
	$(window).on("resize", function () { heightScaler(); });
	
	heightScaler();
	sliderHandler();
};