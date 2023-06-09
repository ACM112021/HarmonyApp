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

        var url = 'http://localhost:5174/UpdateEmployee?employeeid=' + textEmployeeId.value+ '&lastName=' + textLastName.value  + '&firstName=' + textFirstName.value + '&salary=' + textSalary.value;   //An excercise for the reader...

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


    //Invoke searchEmployees() on load
    searchEmployees();
}

webapp_02();


