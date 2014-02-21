(function($) {

    function responseToServr(text) {
        var result = null;

        $.ajax({
            url: "http://localhost:6000/api/words/" + text,
            dataType: "json",
            async: false,
            success: function (data) {
                result = data;
            }
        });
		return result;
	}

	function getWordsFromText(text) {
	    var pattern = /\w+/g;
	    return text.match(pattern);
	}

	var toolTip = $("#toolTip");
	var timerId;
	var x, y;
    var currentWord = "";

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

			var text = data.target.value;
		    console.log(text);

		    var wordsList = getWordsFromText(text);
		    console.log(wordsList);
		    var resultText = ""; 
		    for (var item in wordsList) {
		        resultText += "<span class = 'hiddenWord'>" + wordsList[item] + " </span>";
		        console.log(wordsList[item]);
		    }

		    var hiddenArea = $("#hiddenArea");
		    hiddenArea.html(resultText);

		    var spanList = hiddenArea.children();
		    var hiddenAreaX = hiddenArea.offset().left;
		    var offset = x - hiddenAreaX;

		    for (var span in spanList){
		        var spanWidth = spanList[span].offsetWidth;

		        if (offset <= spanWidth) {
		            currentWord = spanList[span].innerHTML;
                    break;
		        }
		        offset -= spanWidth;
		    }

		    console.log("Current word =" + currentWord);
		    if (currentWord === "") {
		        return;
		    }

		    var resultFromServer = responseToServr(currentWord.substr(0, currentWord.length - 1));
		    console.log("Result from server" + resultFromServer);

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
	    currentWord = "";
	});

    //$(".LanguageDetectorArea").bind("keyup", function(data) {
    //    var text = data.target.value;
    //});

    //$(".hiddenWord").live("mouseover", function (data) {
    //    console.log("hiddenWord on");
    //    console.log(data);
    //    currentWord = data.target.textContent;
    //});

    //$(".hiddenWord").live("mouseout", function () {
    //    console.log("hiddenWord off");
    //    currentWord = "";
    //});
    

	//console.log($(".LanguageDetectorArea"));
})(jQuery);