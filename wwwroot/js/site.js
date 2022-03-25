// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var newBook = 0;
var newStudent = 0;

function searchBook(value) {

    $.ajax({
        type: "POST",
        url: "/Ausleihe/GetBook/" + value, // the URL of the controller action method
        data: null, // optional data
        success: function (result) {
            if (result != null) {
                document.getElementById("bookAutor").disabled = true;
                document.getElementById("bookAutor").value = result.autor;
                document.getElementById("bookTitel").disabled = true;
                document.getElementById("bookTitel").value = result.titel;
                document.getElementById("bookSachgebiet").disabled = true;
                document.getElementById("bookSachgebiet").value = result.sachgebiet;
                document.getElementById("bookOrt").disabled = true;
                document.getElementById("bookOrt").value = result.ort;
                document.getElementById("bookErscheinungsjahr").disabled = true;
                document.getElementById("bookErscheinungsjahr").value = result.erscheinungsjahr;

                newBook = 0;
            } else {
                document.getElementById("bookAutor").required = true;
                document.getElementById("bookAutor").disabled = false;
                document.getElementById("bookAutor").value = "";
                document.getElementById("bookTitel").required = true;
                document.getElementById("bookTitel").disabled = false;
                document.getElementById("bookTitel").value = "";
                document.getElementById("bookSachgebiet").required = true;
                document.getElementById("bookSachgebiet").disabled = false;
                document.getElementById("bookSachgebiet").value = "";
                document.getElementById("bookOrt").required = true;
                document.getElementById("bookOrt").disabled = false;
                document.getElementById("bookOrt").value = "";
                document.getElementById("bookErscheinungsjahr").required = true;
                document.getElementById("bookErscheinungsjahr").disabled = false;
                document.getElementById("bookErscheinungsjahr").value = "";

                newBook = 1;
            }
        },
        error: function (req, status, error) {
            // do something with error   
        }
    });
}

function searchStudent(value) {

    $.ajax({
        type: "POST",
        url: "/Ausleihe/GetStudent/" + value, // the URL of the controller action method
        data: null, // optional data
        success: function (result) {
            if (result != null) {
                document.getElementById("studentVorname").disabled = true;
                document.getElementById("studentVorname").value = result.vorname;
                document.getElementById("studentNachname").disabled = true;
                document.getElementById("studentNachname").value = result.nachname;

                newStudent = 0;
            } else {
                document.getElementById("studentVorname").disabled = false;
                document.getElementById("studentVorname").value = "";
                document.getElementById("studentNachname").disabled = false;
                document.getElementById("studentNachname").value = "";

                newStudent = 1;
            }
        },
        error: function (req, status, error) {
            // do something with error   
        }
    });
}

function checkIfNew() {
    if (newBook == 1) {

        var buchnummer = document.getElementById('bookNumber').value;
        var autor = document.getElementById('bookAutor').value;
        var titel = document.getElementById('bookTitel').value;
        var sachgebiet = document.getElementById('bookSachgebiet').value;
        var ort = document.getElementById('bookOrt').value;
        var erscheinungsjahr = document.getElementById('bookErscheinungsjahr').value;


        $.ajax({
            type: "POST",
            url: "/Buch/CreateNewBook?buchnummer=" + buchnummer + "&autor=" + autor + "&titel=" + titel + "&sachgebiet=" + sachgebiet + "&ort=" + ort + "&erscheinungsjahr=" + erscheinungsjahr, // the URL of the controller action method
            data: null, // optional data
            success: function (result) {
                console.log(result);
            },
            error: function (req, status, error) {
                // do something with error   
            }
        });
    }

    if (newStudent == 1) {

        var studentNummer = document.getElementById('studentNummer').value;
        var studentNachname = document.getElementById('studentNachname').value;
        var studentVorname = document.getElementById('studentVorname').value;


        $.ajax({
            type: "POST",
            url: "/SchuelerIn/CreateNewSchuelerIn?ausweisnummer=" + studentNummer + "&nachname=" + studentNachname + "&vorname=" + studentVorname, // the URL of the controller action method
            data: null, // optional data
            success: function (result) {
                console.log(result);
            },
            error: function (req, status, error) {
                // do something with error   
            }
        });
    }

    delay(1000).then(() => console.log('Waited 1 Second'));
}
