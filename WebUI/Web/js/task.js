function ShowAllTask() {

    //alert(getCookie("prid"));
    var myDB = new ACCESSdb(FileN);
    var SQL;


    var gameID, dif, dur, com, i, txt;
    var tasks = "";

    SQL = "SELECT GameID FROM [GAMEINSTANCE] WHERE ProgramID=" + getCookie("prid") + ";";
    var gameIDXML = myDB.query(SQL, { xml: true });

    SQL = "SELECT Difficulty FROM [GAMEINSTANCE] WHERE ProgramID=" + getCookie("prid") + ";";
    var difXML = myDB.query(SQL, { xml: true });

    SQL = "SELECT Duration FROM [GAMEINSTANCE] WHERE ProgramID=" + getCookie("prid") + ";";
    var durXML = myDB.query(SQL, { xml: true });

    SQL = "SELECT Completed FROM [GAMEINSTANCE] WHERE ProgramID=" + getCookie("prid") + ";";
    var comXML = myDB.query(SQL, { xml: true });

    gameID = gameIDXML.getElementsByTagName("GameID");
    dif = difXML.getElementsByTagName("Difficulty");
    dur = durXML.getElementsByTagName("Duration");
    com = comXML.getElementsByTagName("Completed");

    var xLen = gameID.length;

    for (i = 0; i < xLen; i++) {
        tasks +=
            "<tr>" +
                "<td>" +
                    "<p>" + gameID[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
                "<td>" +
                    "<p>" + dif[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
                "<td>" +
                    "<p>" + dur[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
                "<td>" +
                    "<p>" + com[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
            "</tr>";
    }


    var TasksTable = "<table align='center' id='task_table' border='2px'>" +
                    "<tr>" +
                        "<td>" +
                        "<P>" + "Game ID" + "</P>" +
                        "</td>" +

                        "<td>" +
                        "<P>" + "Difficulty" + "</P>" +
                        "</td>" +

                        "<td>" +
                        "<P>" + "Duration" + "</P>" +
                        "</td>" +

                        "<td>" +
                        "<P>" + "Is Completed" + "</P>" +
                        "</td>" +
                    "</tr>" +
                        tasks +
                    "</table>";

    document.getElementById("show_tasks").innerHTML = TasksTable;
}