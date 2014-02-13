(function ($) {

	function ResponseToServr(text) {
		$.getJSON("http://localhost:6000/api/words/" + text, function(data) {

			for (var i = 0; i < data.length; i++) {
				console.log(data[i]["Text"] + ' ' + data[i]["Language"]);
			}
		});
	}

	var toolTip = $("#toolTip");
	var timerId;
	var x, y;

	$(".LanguageDetectorArea").bind("mouseover", function(data) {
		console.log("enter");

		var oldX = 0;
		var oldY = 0;

		timerId = setInterval(function () {
			if (oldX === x && oldY === y) {
				return;
			}

			oldX = x;
			oldY = y;

			toolTip.css("top", y).css("left", x).css("visibility", "visible");
		}, 2000);
	});

	$(".LanguageDetectorArea").bind("mouseout", function() {
		console.log("out");

		toolTip.css("visibility", "hidden");
		clearInterval(timerId);
	});

	$(".LanguageDetectorArea").bind("mousemove", function (data) {
		x = data.pageX;
		y = data.pageY;
	});

	console.log($(".LanguageDetectorArea"));
})(jQuery);