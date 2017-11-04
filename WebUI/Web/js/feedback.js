function ShowAllFeedbacks() {

    var myDB = new ACCESSdb(FileN);
    var SQL;


    var gameID, score, timePlayed, video, i, txt, feedback;

    SQL = "SELECT GameID FROM [PATIENTDATA] WHERE UserID='" + getCookie("pid") + "';";
    var gameIDXML = myDB.query(SQL, { xml: true });

    SQL = "SELECT Score FROM [PATIENTDATA] WHERE UserID='" + getCookie("pid") + "';";
    var scoreDXML = myDB.query(SQL, { xml: true });

    SQL = "SELECT TimePlayed FROM [PATIENTDATA] WHERE UserID='" + getCookie("pid") + "';";
    var timePlayedDXML = myDB.query(SQL, { xml: true });

    SQL = "SELECT VideoLocation FROM [PATIENTDATA] WHERE UserID='" + getCookie("pid") + "';";
    var videoXML = myDB.query(SQL, { xml: true });

    gameID = gameIDXML.getElementsByTagName("GameID");
    score = scoreDXML.getElementsByTagName("Score");
    timePlayed = timePlayedDXML.getElementsByTagName("TimePlayed");
    video = videoXML.getElementsByTagName("VideoLocation");

    var xLen = gameID.length;

    for (i = 0; i < xLen; i++) {
        feedback +=
            "<tr>" +
                "<td>" +
                    "<p>" + gameID[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
                "<td>" +
                    "<p>" + score[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
                "<td>" +
                    "<p>" + timePlayed[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
                "<td>" +
                    "<p>" + video[i].childNodes[0].nodeValue + "</p>" +
                "</td>" +
            "</tr>";
    }


    var FeedbacksTable = "<table align='center' id='current_table' >" +
                    "<tr>" +
                        "<td>" +
                        "<P>" + "Game ID" + "</P>" +
                        "</td>" +

                        "<td>" +
                        "<P>" + "Score" + "</P>" +
                        "</td>" +

                        "<td>" +
                        "<P>" + "Time Played" + "</P>" +
                        "</td>" +

                        "<td>" +
                        "<P>" + "Video" + "</P>" +
                        "</td>" +
                    "</tr>" +
                        feedback +
                    "</table>";

    document.getElementById("show_feedback").innerHTML = FeedbacksTable;
}

