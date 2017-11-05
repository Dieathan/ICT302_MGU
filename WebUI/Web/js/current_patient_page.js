
function  CurrentProgram() {
    window.location.href = "CurrentProgram.html";
}


function CreateNewProgram() {
        var d = new Date();
        var n = d.getDate()+"/"+(d.getMonth()+1)+"/"+d.getFullYear();

        var pn=document.forms["NewProgram"]["pn"].value;

        var myDB = new ACCESSdb(FileN);
        //alert("New Program created!");

        var SQL2 = "SELECT ProgramName FROM [PROGRAM] WHERE ProgramName='" + pn + "';";
        var rsXML2 = myDB.query(SQL2, { xml: true });

        if (rsXML2) {
            alert("Porgram Exsit!");
        }
        else {
            var SQL = "INSERT INTO [PROGRAM](UserID,ProgramName,DateCreated) VALUES('" + getCookie("pid") + "','" + pn + "','" + n + "');";
            var rsXML = myDB.query(SQL, {xml: true});
            alert("New Program created!");
       
        }


}

function CurrentFeedBacks() {
    window.location.href = "Feedbacks.html";
}

function CreateRestriction() {

    var re = 0;
    var gid = document.forms["NewRestriction"]["gid"].value;

    if (document.forms["NewRestriction"]["restriction"].checked) {
        var re = document.forms["NewRestriction"]["restriction"].value;
    }

    var myDB = new ACCESSdb(FileN);
    //alert("New Program created!");

    var SQL = "INSERT INTO [RESTRICTION](UserID,GameID,IsRestricted) VALUES('" + getCookie("pid") + "'," + gid + "," + re + ");";
    var rsXML = myDB.query(SQL, { xml: true });

    if (!rsXML) {
        alert("Set!");
    }

}