function webapp_02() {

    //Get elements

    var textSearch = document.getElementById("text-search");

    var buttonSearch = document.getElementById("button-search");
    var buttonSearchClear = document.getElementById("button-search-clear");

    var employeeTable = document.getElementById("employee-table");

    var buttonInsert = document.getElementById("button-insert");
    var buttonInsertCancel = document.getElementById("button-insert-cancel");

    var buttonDelete = document.getElementById("button-delete");
    var buttonDeleteCancel = document.getElementById("button-delete-cancel");

    var buttonUpdate = document.getElementById("button-update");
    var buttonUpdateCancel = document.getElementById("button-update-cancel");





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


   


    //Add event listeners

    buttonSearch.addEventListener("click", searchEmployees);
    buttonSearchClear.addEventListener("click", searchClear);

    buttonInsert.addEventListener("click", insertEmployee);
    buttonInsertCancel.addEventListener("click", insertEmployeeCancel);

    buttonDelete.addEventListener("click", deleteEmployee);
    buttonDeleteCancel.addEventListener("click", deleteEmployeeCancel);

    buttonUpdate.addEventListener("click", updateEmployee);
    buttonUpdateCancel.addEventListener("click", updateEmployeeCancel);








    //Functions

    function searchEmployees() {

        var url = 'http://localhost:5174/SearchEmployees?search=' + textSearch.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchEmployees;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterSearchEmployees() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showEmployees(response.employees);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };

    function showEmployees(employees) {
        var employeeTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Empoyee ID</th><th scope="col">First Name</th><th scope="col">Last Name</th><th scope="col">Salary</th></tr></thead><tbody>';

        for (var i = 0; i < employees.length; i++) {
            var employee = employees[i];

            employeeTableText = employeeTableText + '<tr><th scope="row">' + employee.employeeId + '</th><td>' + employee.firstName + '</td><td>' + employee.lastName + '</td><td>' + employee.salary + '</td></tr>';
        }

        employeeTableText = employeeTableText + '</tbody></table>';

        employeeTable.innerHTML = employeeTableText;
    }

    function searchClear() {
        textSearch.value = "";
        searchEmployees();
    }

    function insertEmployee() {

        var textFirstName = document.getElementById("text-insert-first-name");
        var textLastName = document.getElementById("text-insert-last-name");
        var textSalary = document.getElementById("text-insert-salary");

        var url = 'http://localhost:5174/InsertEmployee?lastName=' + textLastName.value + '&firstName=' + textFirstName.value + '&salary=' + textSalary.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterInsertEmployee;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterInsertEmployee() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showEmployees(response.employees);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textFirstName.value = "";
        textLastName.value = "";
        textSalary.value = "";

    };

    function insertEmployeeCancel() {

        var textFirstName = document.getElementById("text-insert-first-name");
        var textLastName = document.getElementById("text-insert-last-name");
        var textSalary = document.getElementById("text-insert-salary");

        textFirstName.value = "";
        textLastName.value = "";
        textSalary.value = "";

    }

    //Update functions go here.

    function updateEmployee() {

        var textEmployeeId = document.getElementById("text-update-employee-id");
        var textFirstName = document.getElementById("text-update-first-name");
        var textLastName = document.getElementById("text-update-last-name");
        var textSalary = document.getElementById("text-update-salary");

        var url = 'http://localhost:5174/UpdateEmployee?employeeid=' + textEmployeeId.value + '&lastName=' + textLastName.value + '&firstName=' + textFirstName.value + '&salary=' + textSalary.value;   //An excercise for the reader...

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterUpdateEmployee;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterUpdateEmployee() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showEmployees(response.employees);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textEmployeeId.value = "";
        textFirstName.value = "";
        textLastName.value = "";
        textSalary.value = "";

    };

    function updateEmployeeCancel() {

        var textEmployeeId = document.getElementById("text-update-employee-id");
        var textFirstName = document.getElementById("text-update-first-name");
        var textLastName = document.getElementById("text-update-last-name");
        var textSalary = document.getElementById("text-update-salary");

        textEmployeeId.value = "";
        textFirstName.value = "";
        textLastName.value = "";
        textSalary.value = "";

    }


    //Delete functions go here.

    function deleteEmployee() {

        var textEmployeeId = document.getElementById("text-delete-employee-id");

        var url = 'http://localhost:5174/DeleteEmployee?employeeid=' + textEmployeeId.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterDeleteEmployee;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterDeleteEmployee() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showEmployees(response.employees);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        textEmployeeId.value = "";

    };

    function deleteEmployeeCancel() {
        var textEmployeeId = document.getElementById("text-delete-employee-id");
        textEmployeeId.value = "";
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
    searchEmployees();

    searchMusicSheets();
}


webapp_02();


