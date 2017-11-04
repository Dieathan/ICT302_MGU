
function  CurrentProgram() {
    window.location.href = "CurrentProgram.html";
}


function CreateNewProgram() {
        var d = new Date();
        var n = d.getDate()+"/"+(d.getMonth()+1)+"/"+d.getFullYear();

        var pn=document.forms["NewProgram"]["pn"].value;

        var myDB = new ACCESSdb(FileN);
        //alert("New Program created!");

        var SQL = "INSERT INTO [PROGRAM](UserID,ProgramName,Time) VALUES('" + getCookie("pid") + "','"+pn+"','"+n+"');";
        var rsXML = myDB.query(SQL, {xml: true});

        if(rsXML)
        {
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