


function  CurrentProgram() {
    window.location.href = "CurrentProgram.html";
}


function CreateNewProgram() {

        var SQL = "INSERT INTO [PROGRAM] ('UserID') VALUES('" + getCookie("pid") + "');";
        var rsXML = myDB.query(SQL, {xml: true});

        if(rsXML)
        {
            alert("New Program created!");
        }



}