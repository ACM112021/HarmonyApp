function webapp_02() {



    //back button:
    window.addEventListener('popstate', handlePopState);

    // Nav Bar Elements first
    var anchorNavSheetMusic = document.getElementById("anchor-nav-sheet-music");
    var anchorNavTutorials = document.getElementById("anchor-nav-tutorials");
    var anchorNavPayments = document.getElementById("anchor-nav-payments");
    // Nav Bar End



    // Nav Bar Event Listeners first
    anchorNavSheetMusic.addEventListener("click", handleClickAnchorNavSheetMusic);
    anchorNavTutorials.addEventListener("click", handleClickAnchorNavTutorials);
    anchorNavPayments.addEventListener("click", handleClickAnchorNavPayments);
    // Nav Bar Event Listeners end



    // Pages

    var pageSheetMusic = document.getElementById("page-sheet-music");
    var pageTutorials = document.getElementById("page-tutorials");
    var pagePayments = document.getElementById("page-payments");

    // Pages end






    // Music Sheet elements

    var textSearchMusicSheets = document.getElementById("text-search-music-sheets");

    var buttonSearchMusicSheets = document.getElementById("button-search-music-sheets");
    var buttonSearchClearMusicSheets = document.getElementById("button-search-clear-music-sheets");

    var musicSheetTable = document.getElementById("music-sheet-table");

    var buttonInsertMusicSheet = document.getElementById("button-insert-music-sheet");
    var buttonInsertMusicSheetCancel = document.getElementById("button-insert-music-sheet-cancel");

    var buttonDeleteMusicSheet = document.getElementById("button-delete-music-sheet");
    var buttonDeleteMusicSheetCancel = document.getElementById("button-delete-music-sheet-cancel");

    var buttonUpdateMusicSheet = document.getElementById("button-update-music-sheet");
    var buttonUpdateMusicSheetCancel = document.getElementById("button-update-music-sheet-cancel");




    // Music Sheet event listeners

    textSearchMusicSheets.addEventListener("keyup", handleTextMusicSearchKeyUp);
    

    buttonSearchMusicSheets.addEventListener("click", searchMusicSheets);
    buttonSearchClearMusicSheets.addEventListener("click", searchClear);

    buttonInsertMusicSheet.addEventListener("click", insertMusicSheet);
    buttonInsertMusicSheetCancel.addEventListener("click", insertMusicSheetCancel);

    buttonDeleteMusicSheet.addEventListener("click", deleteMusicSheet);
    buttonDeleteMusicSheetCancel.addEventListener("click", deleteMusicSheetCancel);

    buttonUpdateMusicSheet.addEventListener("click", updateMusicSheet);
    buttonUpdateMusicSheetCancel.addEventListener("click", updateMusicSheetCancel);







    // Tutorial elements

    var textSearchTutorials = document.getElementById("text-search-tutorials");

    var buttonSearchTutorials = document.getElementById("button-search-tutorials");
    var buttonSearchClearTutorials = document.getElementById("button-search-clear-tutorials");

    var tutorialTable = document.getElementById("tutorial-table");

    // var buttonInsertTutorial = document.getElementById("button-insert-tutorial");
    // var buttonInsertTutorialCancel = document.getElementById("button-insert-tutorial-cancel");

    // var buttonDeleteTutorial = document.getElementById("button-delete-tutorial");
    // var buttonDeleteTutorialCancel = document.getElementById("button-delete-tutorial-cancel");

    // var buttonUpdateTutorial = document.getElementById("button-update-tutorial");
    // var buttonUpdateTutorialCancel = document.getElementById("button-update-tutorial-cancel");




    // Tutorial Event Listeners


    textSearchTutorials.addEventListener("keyup", handleTextTutorialSearchKeyUp);


    buttonSearchTutorials.addEventListener("click", searchTutorials);
    buttonSearchClearTutorials.addEventListener("click", searchClearTutorials);

    // buttonInsertTutorial.addEventListener("click", insertTutorial);
    // buttonInsertTutorialCancel.addEventListener("click", insertTutorialCancel);

    // buttonDeleteTutorial.addEventListener("click", deleteTutorial);
    // buttonDeleteTutorialCancel.addEventListener("click", deleteTutorialCancel);

    // buttonUpdateTutorial.addEventListener("click", updateTutorial);
    // buttonUpdateTutorialCancel.addEventListener("click", updateTutorialCancel);





    // Functions

    // nav bar functions:

    function handleClickAnchorNavSheetMusic(e) {
        window.history.pushState({}, "", "/" + "sheetmusic");
        showPage("sheetmusic");
        e.preventDefault();
    }

    function handleClickAnchorNavTutorials(e) {
        window.history.pushState({}, "", "/" + "tutorials");
        showPage("tutorials");
        e.preventDefault();
    }

    function handleClickAnchorNavPayments(e) {
        window.history.pushState({}, "", "/" + "payments");
        showPage("payments");
        e.preventDefault();
    }

    function showPage(page) {
        if (page.toLowerCase() === "sheetmusic" || page === "") {
            pageSheetMusic.classList.remove("visually-hidden");
            pageTutorials.classList.add("visually-hidden");
            pagePayments.classList.add("visually-hidden");
        } else if (page.toLowerCase() === "tutorials") {
            pageSheetMusic.classList.add("visually-hidden");
            pageTutorials.classList.remove("visually-hidden");
            pagePayments.classList.add("visually-hidden");
        } else if (page.toLowerCase() === "payments") {
            pageSheetMusic.classList.add("visually-hidden");
            pageTutorials.classList.add("visually-hidden");
            pagePayments.classList.remove("visually-hidden");
        }
    }

    function handleNewUrl() {
        var page = window.location.pathname.split('/')[1];

        if (page === "") {
            window.history.replaceState({}, "", "/" + "sheetmusic");
        } else {
        window.history.replaceState({}, "", "/" + page);
        }

        showPage(page);
    }

    // nav bar functions end





    // back button function

    function handlePopState() {
        var page = window.location.pathname.split('/')[1];
        showPage(page);
    }















    // Music Sheet Functions


    function handleTextMusicSearchKeyUp(e) {
        searchMusicSheets();
    }




    function searchMusicSheets() {

        var url = 'http://localhost:5174/SearchMusicSheets?search=' + textSearchMusicSheets.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchMusicSheets;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterSearchMusicSheets() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showMusicSheets(response.musicSheets);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };
    }



    // initial showMusicSheets method as of 6/12/23 5:38 PM:

    // function showMusicSheets(musicSheets) {
    //     var musicSheetTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Music Sheet ID</th><th scope="col">Song Title</th><th scope="col">Start Date</th><th scope="col">Completed Date</th></tr></thead><tbody>';

    //     for (var i = 0; i < musicSheets.length; i++) {
    //         var musicSheet = musicSheets[i];

    //         musicSheetTableText = musicSheetTableText + '<tr><th scope="row">' + musicSheet.musicSheetId + '</th><td>' + musicSheet.songTitle + '</td><td>' + musicSheet.startDate + '</td><td>' + musicSheet.completedDate + '</td></tr>';
    //     }

    //     musicSheetTableText = musicSheetTableText + '</tbody></table>';

    //     musicSheetTable.innerHTML = musicSheetTableText;
    // }



    // under construction showMusicSheets method below:



    function showMusicSheets(musicSheets) {
        var musicSheetTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Music Sheet ID</th><th scope="col">Song Title</th><th scope="col">Start Date</th><th scope="col">Completed Date</th></tr></thead><tbody>';

        for (var i = 0; i < musicSheets.length; i++) {
            var musicSheet = musicSheets[i];

            // initial # placeholder below:

            // musicSheetTableText = musicSheetTableText + '<tr><th scope="row">' + musicSheet.musicSheetId + '</th><td><a href="#" class="song-link" data-id"' + musicSheet.musicSheetId + '">' + musicSheet.songTitle + '</a></td><td>' + musicSheet.startDate + '</td><td>' + musicSheet.completedDate + '</td></tr>';



            // Tuesday 6/13/23 7:37PM: replacing the 'href="#"' attempt:

            // musicSheetTableText = musicSheetTableText + '<tr><th scope="row">' + musicSheet.musicSheetId + '</th><td><a href="D:\\HarmonyApp\\HarmonyApp\\pdfs\\' + musicSheet.pdfFileName + '" class="song-link" data-id="' + musicSheet.musicSheetId + '">' + musicSheet.songTitle + '</a></td><td>' + musicSheet.startDate + '</td><td>' + musicSheet.completedDate + '</td></tr>';



            //2nd attempt, direct PDF file link:
            // okay the link goes directly to the Ode to Joy PDF but it still doesn't work


            // musicSheetTableText = musicSheetTableText + '<tr><th scope="row">' + musicSheet.musicSheetId + '</th><td><a href="D:/HarmonyApp/HarmonyApp/pdfs/8-ode-to-joy.pdf" class="song-link" data-id="' + musicSheet.musicSheetId + '">' + musicSheet.songTitle + '</a></td><td>' + musicSheet.startDate + '</td><td>' + musicSheet.completedDate + '</td></tr>';


            

            // 3rd attempt: moved pdfs folder to wwwroot

            musicSheetTableText = musicSheetTableText + '<tr><th scope="row">' + musicSheet.musicSheetId + '</th><td><a href="http://localhost:5057/pdfs/' + musicSheet.pdfFileName +'.pdf" target="_blank" class="song-links" data-id="' + musicSheet.musicSheetId + '">' + musicSheet.songTitle + '</a></td><td>' + musicSheet.startDate.split('T')[0]  + '</td><td>' + musicSheet.completedDate.split('T')[0] + '</td></tr>';




        }

        musicSheetTableText = musicSheetTableText + '</tbody></table>';

        musicSheetTable.innerHTML = musicSheetTableText;


        // Song Link Event Listener
        // removed Wednesday 6/14/23 1:20pm


        // var songLinks = document.getElementsByClassName('song-link');
        // for (var j = 0; j < songLinks.length; j++) {
        //     songLinks[j].addEventListener('click', onSongLinkClick);
        // }
    }

    // Song Link Click Event Listener
    // removed Wednesday 6/14/23 1:20pm

    // function onSongLinkClick(event) {
    //     console.log('')
    //     event.preventDefault();
    //     var musicSheetId = event.target.getAttribute('data-id');
    //     console.log('Song link linked! Music Sheet ID:', musicSheetId);
    // }




    function searchClear() {
        textSearchMusicSheets.value = "";
        searchMusicSheets();
    }




    function insertMusicSheet() {

        var textSongTitle = document.getElementById("text-insert-song-title");
        var textStartDate = document.getElementById("text-insert-start-date");
        var textCompletedDate = document.getElementById("text-insert-completed-date");



        // allow null in Completed Date 6/10/23 1:08pm

        if (!textCompletedDate.value) {
            textCompletedDate.value = null; 
          }



        var url = 'http://localhost:5174/InsertMusicSheet?songTitle=' + textSongTitle.value + '&startDate=' + textStartDate.value + '&completedDate=' + textCompletedDate.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterInsertMusicSheet;
        xhr.open('GET', url); 
        xhr.send(null);

        function doAfterInsertMusicSheet() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showMusicSheets(response.musicSheets);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textSongTitle.value = "";
        textStartDate.value = "";
        textCompletedDate.value = "";

    };



    function insertMusicSheetCancel() {

        var textSongTitle = document.getElementById("text-insert-song-title");
        var textStartDate = document.getElementById("text-insert-start-date");
        var textCompletedDate = document.getElementById("text-insert-completed-date");

        textSongTitle.value = "";
        textStartDate.value = "";
        textCompletedDate.value = "";

    }






    function updateMusicSheet() {

        var textMusicSheetId = document.getElementById("text-update-music-sheet-id");
        var textSongTitle = document.getElementById("text-update-song-title");
        var textStartDate = document.getElementById("text-update-start-date");
        var textCompletedDate = document.getElementById("text-update-completed-date");

        var url = 'http://localhost:5174/UpdateMusicSheet?musicsheetid=' + textMusicSheetId.value + '&songTitle=' + textSongTitle.value + '&startDate=' + textStartDate.value + '&completedDate=' + textCompletedDate.value; 

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterUpdateMusicSheet;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterUpdateMusicSheet() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showMusicSheets(response.musicSheets);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textMusicSheetId.value = "";
        textStartDate.value = "";
        textSongTitle.value = "";
        textCompletedDate.value = "";

    };

    function updateMusicSheetCancel() {

        var textMusicSheetId = document.getElementById("text-update-music-sheet-id");
        var textStartDate = document.getElementById("text-update-start-date");
        var textSongTitle = document.getElementById("text-update-song-title");
        var textCompletedDate = document.getElementById("text-update-completed-date");

        textMusicSheetId.value = "";
        textStartDate.value = "";
        textSongTitle.value = "";
        textCompletedDate.value = "";
    }













    function deleteMusicSheet() {

        var textMusicSheetId = document.getElementById("text-delete-music-sheet-id");

        var url = 'http://localhost:5174/DeleteMusicSheet?musicsheetid=' + textMusicSheetId.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterDeleteMusicSheet;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterDeleteMusicSheet() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showMusicSheets(response.musicSheets);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textMusicSheetId.value = "";

    };

    function deleteMusicSheetCancel() {
        var textMusicSheetId = document.getElementById("text-delete-music-sheet-id");
        textMusicSheetId.value = "";
    }

































    // Tutorial Functions:


    function handleTextTutorialSearchKeyUp(e) {
        searchTutorials();
    }




    function searchTutorials() {

        var url = 'http://localhost:5174/SearchTutorials?search=' + textSearchTutorials.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchTutorials;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterSearchTutorials() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showTutorials(response.tutorials);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };
    }




    function showTutorials(tutorials) {
        var tutorialTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Tutorial ID</th><th scope="col">Title</th><th scope="col">Description</th><th scope="col">Video Link</th></tr></thead><tbody>';

        for (var i = 0; i < tutorials.length; i++) {
            var tutorial = tutorials[i];


            tutorialTableText += '<tr><th scope="row">' + tutorial.tutorialId + '</th><td>' + tutorial.title + '</td><td>' + tutorial.description + '</td><td><a href="' + tutorial.videoLink + '" target="_blank">Watch Video</a></td></tr>';



        }

        tutorialTableText = tutorialTableText + '</tbody></table>';

        tutorialTable.innerHTML = tutorialTableText;


    }





    function searchClearTutorials() {
        textSearchTutorials.value = "";
        searchTutorials();
    }




    function insertTutorial() {

        var textTitle = document.getElementById("text-insert-tutorial-title");
        var textDescription = document.getElementById("text-insert-description");
        var textVideoLink = document.getElementById("text-insert-video-link");



        // allow null in Video Link 6/17/23 6:40pm

        if (!textVideoLink.value) {
            textVideoLink.value = null; 
          }



        var url = 'http://localhost:5174/InsertTutorial?title=' + textTitle.value + '&description=' + textDescription.value + '&videoLink=' + textVideoLink.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterInsertTutorial;
        xhr.open('GET', url); 
        xhr.send(null);

        function doAfterInsertTutorial() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showTutorials(response.tutorials);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textTitle.value = "";
        textDescription.value = "";
        textVideoLink.value = "";

    };



    function insertTutorialCancel() {

        var textTitle = document.getElementById("text-insert-tutorial-title");
        var textDescription = document.getElementById("text-insert-tutorial-description");
        var textVideoLink = document.getElementById("text-insert-video-link");

        textTitle.value = "";
        textDescription.value = "";
        textVideoLink.value = "";

    }






    function updateTutorial() {

        var textTutorialId = document.getElementById("text-update-tutorial-id");
        var textTitle = document.getElementById("text-update-tutorial-title");
        var textDescription = document.getElementById("text-update-tutorial-description");
        var textVideoLink = document.getElementById("text-update-video-link");

        var url = 'http://localhost:5174/UpdateTutorial?tutorialid=' + textTutorialId.value + '&title=' + textTitle.value + '&description=' + textDescription.value + '&videoLink=' + textVideoLink.value; 

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterUpdateTutorial;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterUpdateTutorial() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showTutorials(response.tutorials);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textTutorialId.value = "";
        textTitle.value = "";
        textDescription.value = "";
        textVideoLink.value = "";

    };

    function updateTutorialCancel() {

        var textTutorialId = document.getElementById("text-update-tutorial-id");
        var textTitle = document.getElementById("text-update-tutorial-title");
        var textDescription = document.getElementById("text-update-tutorial-description");
        var textVideoLink = document.getElementById("text-update-video-link");

        textTutorialId.value = "";
        textTitle.value = "";
        textDescription.value = "";
        textVideoLink.value = "";
    }













    function deleteTutorial() {

        var textTutorialId = document.getElementById("text-delete-tutorial-id");

        var url = 'http://localhost:5174/DeleteTutorial?tutorialid=' + textTutorialId.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterDeleteTutorial;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterDeleteTutorial() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showTutorials(response.tutorials);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textTutorialId.value = "";

    };





    function deleteTutorialCancel() {
        var textTutorialId = document.getElementById("text-delete-tutorial-id");
        textTutorialId.value = "";
    }







    // Payments Getters & Event Listeners:

    // Wait for the document to be fully loaded
document.addEventListener('DOMContentLoaded', function() {

    // Get the payment logos
    var paypalLogo = document.getElementById('paypal-logo');
    var cashappLogo = document.getElementById('cashapp-logo');
    var metaPayLogo = document.getElementById('meta-pay-logo');
  
    // Add click event listeners to the payment logos
    paypalLogo.addEventListener('click', function() {
      window.open('https://paypal.me/AndrewMayes652?country.x=US&locale.x=en_US', '_blank'); // Replace with PayPal payment link
    });
  
    cashappLogo.addEventListener('click', function() {
      window.open('https://cash.app/$ACadeMayes', '_blank'); // Replace with CashApp payment link
    });
  
    metaPayLogo.addEventListener('click', function() {
      window.open('https://m.me/pay/100008649042546/', '_blank'); // Replace with Meta Pay payment link
    });

  });
  



















    //Invoke handleNewUrl() and searchMusicSheets() on load... and searchTutorials()
    handleNewUrl();
    searchMusicSheets();
    searchTutorials();
}


webapp_02();


