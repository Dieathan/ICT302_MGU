function ShowAllPorgrams() {

    var myDB = new ACCESSdb(FileN);
    var pidXML, pnXML, ptXML, prid, pn, pt;
    var SQL
    var x, i, txt;
    var programs = "";

    SQL = "SELECT ProgramID FROM [PROGRAM] WHERE UserID='"+getCookie("pid")+"';";
    pidXML = myDB.query(SQL, { xml: true });
    prid = pidXML.getElementsByTagName("ProgramID");

    SQL = "SELECT ProgramName FROM [PROGRAM] WHERE UserID='" + getCookie("pid") + "';";
    pnXML = myDB.query(SQL, { xml: true });
    pn = pnXML.getElementsByTagName("ProgramName");

    SQL = "SELECT DateCreated FROM [PROGRAM] WHERE UserID='" + getCookie("pid") + "';";
    ptXML = myDB.query(SQL, { xml: true });
    pt = ptXML.getElementsByTagName("DateCreated");


    var xLen = prid.length;

    for (i = 0; i <xLen; i++) {
        programs+=
            "<tr>"+
                "<td>"+
                    "<p>" + prid[i].childNodes[0].nodeValue + "</p>" +
                "</td>"+
                 "<td>" +
                     "<p>" + pn[i].childNodes[0].nodeValue + "</p>" +
                 "</td>" +
                 "<td>" +
                     "<p>" + pt[i].childNodes[0].nodeValue + "</p>" +
                 "</td>" +
                 "<td>" +
                     "<p><button class='button3' onclick='NewTaskWindow(" + prid[i].childNodes[0].nodeValue + ")'>" +
                    "New Task</button>" +
                    "<button class='button3' onclick='EditTask(" + prid[i].childNodes[0].nodeValue + ")'>" +
                    "Edit Task</button>" +
                    "<button class='button3' onclick='DeleteProgram(" + prid[i].childNodes[0].nodeValue + ")'>" +
                    "Delete Program</button></p>" +
                 "</td>" +
             "</tr>";
    }


    var PorgramTable = "<table align='center' id='program_table' border='2px'>" +
                    "<tr>"+
                        "<td>"+
                        "<P>"+"Program ID"+"</P>"+
                        "</td>" +
                        "<td>" +
                        "<P>" + "Program Name" + "</P>" +
                        "</td>" +
                        "<td>" +
                        "<P>" + "Date Created" + "</P>" +
                        "</td>" +
                        "<td>" +
                        "<P>" + "Tasks" + "</P>" +
                        "</td>" +
                    "</tr>"+
                        programs+
                    "</table>";

    document.getElementById("show_programs").innerHTML=PorgramTable;

}


function SetCurrentProgram(prid)
{
    createCookie("prid", prid, 2);
}

function NewTaskWindow(prid)
{
    
    SetCurrentProgram(prid);
    document.getElementById('id01').style.display = 'block';
    //alert(getCookie("prid"));
}

function EditTask(prid)
{
    SetCurrentProgram(prid);
    window.location.href = "Tasks.html";
}

function DeleteProgram(prid)
{
    SetCurrentProgram(prid);
    var myDB = new ACCESSdb(FileN);
    var SQL, rsXML

    SQL = "DELETE * FROM [PROGRAM] WHERE ProgramID=" + getCookie("prid") + ";";
    rsXML = myDB.query(SQL, { xml: true });

    SQL = "DELETE * FROM [GAMEINSTANCE] WHERE ProgramID=" + getCookie("prid") + ";";
    rsXML = myDB.query(SQL, { xml: true });

    alert("Deleted!");
    window.location.href = "CurrentProgram.html";
}


function CreateNewTask() {
    //alert("New Task Added!");
    var gid = document.forms["CreateTask"]["gid"].value;
    var di = document.forms["CreateTask"]["di"].value;
    var du = document.forms["CreateTask"]["du"].value;
    var com = 0;

    if (document.forms["CreateTask"]["completed"].checked) {
        var com = document.forms["CreateTask"]["completed"].value;
    }

    if ((gid == "") || (di == "") || (du == "")) {
        alert("Content must be filled out");
        return false;
    }

    NewTask(gid, di, du, com);
    CreateDatabase();
}

function NewTask(gid, di, du, com) {
    
    var myDB = new ACCESSdb(FileN);


    var SQL = "INSERT INTO [GAMEINSTANCE](ProgramID,GameID,Difficulty,Duration,Completed)"+
    "VALUES(" + getCookie('prid') + ", " + gid + ", " + di + "," + du + "," + com + ");";

    var rsXML = myDB.query(SQL, { xml: true });
    alert("New Task Added!");
    
    

}