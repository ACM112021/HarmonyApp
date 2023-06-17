function webapp_02() {

    // Music Sheet elements

    // Nav Bar Elements first
    var anchorNavSheetMusic = document.getElementById("anchor-nav-sheet-music");
    var anchorNavDiscussion = document.getElementById("anchor-nav-discussion");
    var anchorNavPayments = document.getElementById("anchor-nav-payments");
    // Nav Bar End

    // Pages

    var pageSheetMusic = document.getElementById("page-sheet-music");
    var pageDiscussion = document.getElementById("page-discussion");
    var pagePayments = document.getElementById("page-payments");

    // Pages end

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


    // Nav Bar Listeners first
    anchorNavSheetMusic.addEventListener("click", handleClickAnchorNavSheetMusic);
    anchorNavDiscussion.addEventListener("click", handleClickAnchorNavDiscussion);
    anchorNavPayments.addEventListener("click", handleClickAnchorNavPayments);
    // Nav Bar Listeners end

    buttonSearchMusicSheets.addEventListener("click", searchMusicSheets);
    buttonSearchClearMusicSheets.addEventListener("click", searchClear);

    buttonInsertMusicSheet.addEventListener("click", insertMusicSheet);
    buttonInsertMusicSheetCancel.addEventListener("click", insertMusicSheetCancel);

    buttonDeleteMusicSheet.addEventListener("click", deleteMusicSheet);
    buttonDeleteMusicSheetCancel.addEventListener("click", deleteMusicSheetCancel);

    buttonUpdateMusicSheet.addEventListener("click", updateMusicSheet);
    buttonUpdateMusicSheetCancel.addEventListener("click", updateMusicSheetCancel);







    // Functions

    function handleClickAnchorNavSheetMusic(e) {
        pageSheetMusic.classList.remove("visually-hidden");
        pageDiscussion.classList.add("visually-hidden");
        pagePayments.classList.add("visually-hidden");
        e.preventDefault();
    }

    function handleClickAnchorNavDiscussion(e) {
        pageSheetMusic.classList.add("visually-hidden");
        pageDiscussion.classList.remove("visually-hidden");
        pagePayments.classList.add("visually-hidden");
        e.preventDefault();
    }

    function handleClickAnchorNavPayments(e) {
        pageSheetMusic.classList.add("visually-hidden");
        pageDiscussion.classList.add("visually-hidden");
        pagePayments.classList.remove("visually-hidden");
        e.preventDefault();
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

    





    //Invoke searchEmployees() on load
    // searchEmployees();

    searchMusicSheets();
}


webapp_02();


