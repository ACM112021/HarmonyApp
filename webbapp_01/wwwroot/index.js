function webapp_02() {

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

    buttonSearchMusicSheets.addEventListener("click", searchMusicSheets);
    buttonSearchClearMusicSheets.addEventListener("click", searchClear);

    buttonInsertMusicSheet.addEventListener("click", insertMusicSheet);
    buttonInsertMusicSheetCancel.addEventListener("click", insertMusicSheetCancel);

    buttonDeleteMusicSheet.addEventListener("click", deleteMusicSheet);
    buttonDeleteMusicSheetCancel.addEventListener("click", deleteMusicSheetCancel);

    buttonUpdateMusicSheet.addEventListener("click", updateMusicSheet);
    buttonUpdateMusicSheetCancel.addEventListener("click", updateMusicSheetCancel);




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



    function showMusicSheets(musicSheets) {
        var musicSheetTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Music Sheet ID</th><th scope="col">Song Title</th><th scope="col">Start Date</th><th scope="col">Completed Date</th></tr></thead><tbody>';

        for (var i = 0; i < musicSheets.length; i++) {
            var musicSheet = musicSheets[i];

            musicSheetTableText = musicSheetTableText + '<tr><th scope="row">' + musicSheet.musicSheetId + '</th><td>' + musicSheet.songTitle + '</td><td>' + musicSheet.startDate + '</td><td>' + musicSheet.completedDate + '</td></tr>';
        }

        musicSheetTableText = musicSheetTableText + '</tbody></table>';

        musicSheetTable.innerHTML = musicSheetTableText;
    }




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


