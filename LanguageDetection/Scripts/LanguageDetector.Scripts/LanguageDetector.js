(function($) {

    // function which return function wich search text in cache and if doesn't find, do response to server and add info in cache 
    function getFunctionForProccessingWord() { 
        var cache = new Array();
        return function(text) {
            var result = null;

            if (cache[text.toLowerCase()] != undefined) {
                return cache[text.toLowerCase()];
            }

            $.ajax({
                url: "http://localhost:6000/api/words/" + text,
                dataType: "json",
                async: false,
                success: function(data) {
                    result = data;
                }
            });

            cache[text.toLowerCase()] = result;
            return result;
        };
    }

    //return list of word from text
    function getWordsFromText(text) {
	    var pattern = /[\wА-Яа-я]+/g;
	    return text.match(pattern);
	}

	var toolTip = $("#toolTip");
	var timerId;             // timer for text proccessing function
	var x, y;                //current mouse position
    var currentWord = "";


    // evant handlers
    $(".LanguageDetectorArea").bind("mouseover", { searchWord: getFunctionForProccessingWord() }, function(data) {
		console.log("enter");

		var oldX = 0;  // old mouse coordinate
		var oldY = 0;
        var oldInputText = "";

		timerId = setInterval(function () {
			if (oldX === x && oldY === y) {
				return;
			}

			oldX = x;
			oldY = y;

			var hiddenArea = $("#hiddenArea");
			var text = data.target.value;
		    console.log(text);

		    if (oldInputText != text) {                            //if input text change
		        var wordsList = getWordsFromText(text);
		        console.log(wordsList);
		        var resultText = "";

		        for (var item in wordsList) {                      //add behind input tag, elements of span, each span contain one word
		            resultText += "<span class = 'hiddenWord'>"
		                + wordsList[item] + " </span>";
		            console.log(wordsList[item]);
		        }
		        
		        hiddenArea.html(resultText);
		        oldInputText = text;
		    }

		    var spanList = hiddenArea.children();           
		    var hiddenAreaX = hiddenArea.offset().left;
		    var offset = x - hiddenAreaX;

		    for (var span in spanList){                         //detect word under the mouse coursor
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

		    var resultFromServer = data.data.searchWord(currentWord.substr(0, currentWord.length - 1));
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
})(jQuery);