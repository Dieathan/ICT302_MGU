


function  CurrentProgram() {
    window.location.href = "CurrentProgram.html";

}


function CreateNewProgram() {
        var d = new Date();
        var n = d.getDate()+"/"+(d.getMonth()+1)+"/"+d.getFullYear();

        var pn=document.forms["NewProgram"]["pn"].value;

        var SQL = "INSERT INTO [PROGRAM](UserID,ProgramName,Time) VALUES('" + getCookie("pid") + "','"+pn+"','"+n+"');";
        var rsXML = myDB.query(SQL, {xml: true});

        if(!rsXML)
        {
            alert("New Program created!");
        }


}