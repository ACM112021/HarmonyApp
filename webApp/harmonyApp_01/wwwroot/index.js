function  harmonyApp_01() {

     //Get elements
 
     var textSearch = document.getElementById("text-search");
 
     var buttonSearch = document.getElementById("button-search");
     var buttonSearchClear = document.getElementById("button-search-clear");
 
     var studentTable = document.getElementById("student-table");
 
     var buttonInsert = document.getElementById("button-insert");
     var buttonInsertCancel = document.getElementById("button-insert-cancel");
 
     var buttonDelete = document.getElementById("button-delete");
     var buttonDeleteCancel = document.getElementById("button-delete-cancel");
 
     var buttonUpdate = document.getElementById("button-update");
     var buttonUpdateCancel = document.getElementById("button-update-cancel");
 
     //Add event listeners
 
     buttonSearch.addEventListener("click", searchStudents);
     buttonSearchClear.addEventListener("click", searchClear);
 
     buttonInsert.addEventListener("click", insertStudent);
     buttonInsertCancel.addEventListener("click", insertStudentCancel);
 
     buttonDelete.addEventListener("click", deleteStudent);
     buttonDeleteCancel.addEventListener("click", deleteStudentCancel);
 
     buttonUpdate.addEventListener("click", updateStudent);
     buttonUpdateCancel.addEventListener("click", updateStudentCancel);
 
     //Functions
 
     function searchStudents() {
 
         var url = 'http://localhost:5028/SearchStudents?search=' + textSearch.value;
 
         var xhr = new XMLHttpRequest();
         xhr.onreadystatechange = doAfterSearchStudents;
         xhr.open('GET', url);
         xhr.send(null);
 
         function doAfterSearchStudents() {
             var DONE = 4; // readyState 4 means the request is done.
             var OK = 200; // status 200 is a successful return.
             if (xhr.readyState === DONE) {
                 if (xhr.status === OK) {
 
                     var response = JSON.parse(xhr.responseText);
 
                     if (response.result === "success") {
                         showStudents(response.students);
                     } else {
                         alert("API Error: " + response.message);
                     }
                 } else {
                     alert("Server Error: " + xhr.status + " " + xhr.statusText);
                 }
             }
         };
 
     };
 
     function showStudents(students) {
         var studentTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Student ID</th><th scope="col">First Name</th><th scope="col">Last Name</th><th scope="col">Student Balance</th></tr></thead><tbody>';
 
         for (var i = 0; i < students.length; i++) {
             var student = students[i];
 
             studentTableText = studentTableText + '<tr><th scope="row">' + student.studentId + '</th><td>' + student.firstName + '</td><td>' + student.lastName + '</td><td>' + student.studentBalance + '</td></tr>';
         }
 
         studentTableText = studentTableText + '</tbody></table>';
 
         studentTable.innerHTML = studentTableText;
     }
 
     function searchClear() {
         textSearch.value = "";
         searchStudents();
     }
 
     function insertStudent() {
 
         var textFirstName = document.getElementById("text-insert-first-name");
         var textLastName = document.getElementById("text-insert-last-name");
         var textStudentBalance = document.getElementById("text-insert-studentBalance");
 
         var url = 'http://localhost:5028/InsertStudent?lastName=' + textLastName.value + '&firstName=' + textFirstName.value + '&studentBalance=' + textStudentBalance.value;
 
         var xhr = new XMLHttpRequest();
         xhr.onreadystatechange = doAfterInsertStudent;
         xhr.open('GET', url);
         xhr.send(null);
 
         function doAfterInsertStudent() {
             var DONE = 4; // readyState 4 means the request is done.
             var OK = 200; // status 200 is a successful return.
             if (xhr.readyState === DONE) {
                 if (xhr.status === OK) {
 
                     var response = JSON.parse(xhr.responseText);
 
                     if (response.result === "success") {
                         showStudents(response.students);
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
         textStudentBalance.value = "";
 
     };
 
     function insertStudentCancel() {
 
         var textFirstName = document.getElementById("text-insert-first-name");
         var textLastName = document.getElementById("text-insert-last-name");
         var textStudentBalance = document.getElementById("text-insert-studentBalance");
 
         textFirstName.value = "";
         textLastName.value = "";
         textStudentBalance.value = "";
 
     }
 
     //Update functions go here.
 
     function updateStudent() {
 
         var textStudentId = document.getElementById("text-update-student-id");
         var textFirstName = document.getElementById("text-update-first-name");
         var textLastName = document.getElementById("text-update-last-name");
         var textStudentBalance = document.getElementById("text-update-studentBalance");
 
         var url = 'http://localhost:5028/UpdateStudent?studentid=' + textStudentId.value+ '&lastName=' + textLastName.value  + '&firstName=' + textFirstName.value + '&studentBalance=' + textStudentBalance.value;   //An excercise for the reader...
 
         var xhr = new XMLHttpRequest();
         xhr.onreadystatechange = doAfterUpdateStudent;
         xhr.open('GET', url);
         xhr.send(null);
 
         function doAfterUpdateStudent() {
             var DONE = 4; // readyState 4 means the request is done.
             var OK = 200; // status 200 is a successful return.
             if (xhr.readyState === DONE) {
                 if (xhr.status === OK) {
 
                     var response = JSON.parse(xhr.responseText);
 
                     if (response.result === "success") {
                         showStudents(response.students);
                     } else {
                         alert("API Error: " + response.message);
                     }
                 } else {
                     alert("Server Error: " + xhr.status + " " + xhr.statusText);
                 }
             }
         };
 
         textStudentId.value = "";
         textFirstName.value = "";
         textLastName.value = "";
         textStudentBalance.value = "";
 
     };
 
     function updateStudentCancel() {
 
         var textStudentId = document.getElementById("text-update-student-id");
         var textFirstName = document.getElementById("text-update-first-name");
         var textLastName = document.getElementById("text-update-last-name");
         var textStudentBalance = document.getElementById("text-update-studentBalance");
 
         textStudentId.value = "";
         textFirstName.value = "";
         textLastName.value = "";
         textStudentBalance.value = "";
 
     }
 
 
     //Delete functions go here.
 
     function deleteStudent() {
 
         var textStudentId = document.getElementById("text-delete-student-id");
 
         var url = 'http://localhost:5028/DeleteStudent?studentid=' + textStudentId.value;
 
         var xhr = new XMLHttpRequest();
         xhr.onreadystatechange = doAfterDeleteStudent;
         xhr.open('GET', url);
         xhr.send(null);
 
         function doAfterDeleteStudent() {
             var DONE = 4; // readyState 4 means the request is done.
             var OK = 200; // status 200 is a successful return.
             if (xhr.readyState === DONE) {
                 if (xhr.status === OK) {
 
                     var response = JSON.parse(xhr.responseText);
 
                     if (response.result === "success") {
                         showStudents(response.students);
                     } else {
                         alert("API Error: " + response.message);
                     }
                 } else {
                     alert("Server Error: " + xhr.status + " " + xhr.statusText);
                 }
             }
         };
 
         textStudentId.value = "";
 
     };
 
     function deleteStudentCancel() {
         var textStudentId = document.getElementById("text-delete-student-id");
         textStudentId.value = "";
     }
 
 
     //Invoke searchStudents() on load
     searchStudents();
 }
 
 harmonyApp_01();