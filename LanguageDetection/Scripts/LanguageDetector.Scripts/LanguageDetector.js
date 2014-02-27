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
	    //var pattern = /[\wА-Яа-я]+/g;
        //return text.match(pattern);
        return text.split(" ");
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
		                + wordsList[item] + "&nbsp" + "</span>";
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
		    if (currentWord === "" ||currentWord.length < 10 ) {
		        return;
		    }

		    var resultFromServer = data.data.searchWord(currentWord.substr(0, currentWord.length - 6));
		    console.log("Result from server" + resultFromServer);

		    var tableString = "";
		    for (var i in resultFromServer) {
		        tableString += "<tr><td>" + resultFromServer[i]["Language"] + "</td>" +
                    "<td>" + resultFromServer[i]["Score"] + "</td></tr>";
		    }
		    toolTip.children().filter("tbody").html(tableString);
		    toolTip.css("top", y).css("left", x).css("visibility", "visible");
		}, 1000);
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
		toolTip.css("visibility", "hidden");
	});

	//$(".LanguageDetectorArea").bind("input", function (data) {
        
	//    var text = data.target.value;
	//    if (text.length < 2) {
	//        return;
	//    }

	//    if (text[text.length - 1] === text[text.length - 2] & text[text.length - 2] === " ") {
	//        data.target.value = text.substr(0, text.length - 1);
	//    }
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