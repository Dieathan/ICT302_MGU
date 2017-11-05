function ShowUserInfor() {

    //document.getElementById('FN').innerHTML = "Current Patient Name: ";
    //document.getElementById("CP").innerHTML = "Current Patient ID: " + getCookie("pid");
    var SQL, rsXML;
    var myDB = new ACCESSdb(FileN);

    SQL = "SELECT FirstName FROM [USER] WHERE UserID='" + getCookie("pid") + "';";
    rsXML = myDB.query(SQL, { xml: true });
    var fn = rsXML.getElementsByTagName("FirstName")[0].childNodes[0].nodeValue;

    SQL = "SELECT LastName FROM [USER] WHERE UserID='" + getCookie("pid") + "';";
    rsXML = myDB.query(SQL, { xml: true });
    var ln = rsXML.getElementsByTagName("LastName")[0].childNodes[0].nodeValue;

    var UserTable = "<table id='infor_table' align='center'>" +
        "<tr>" +
            "<td>" +
                "<p class='CP'>" + "Patient ID: " + getCookie("pid") + "</p>" +
                "<p class='CP'>" + "Patient Name: " + fn + " " + ln + "</p>" +
            "</td>" +
        "</tr>" +
    "</table>";

    document.getElementById("user_infor").innerHTML = UserTable;
    document.getElementById("current_table").style.display = 'table';
}