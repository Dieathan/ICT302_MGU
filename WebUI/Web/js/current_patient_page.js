
function  CurrentProgram() {
    window.location.href = "CurrentProgram.html";
<<<<<<< HEAD

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


=======
>>>>>>> 382cb223d0bca2f4c5846f7e46d1f7cf8c9790e4
}