

function handleFiles()
{
    var pid=document.forms["CreatePatient"]["pid"].value;
    var fn=document.forms["CreatePatient"]["fn"].value;
    var ln=document.forms["CreatePatient"]["ln"].value;
    var psw=document.forms["CreatePatient"]["psw"].value;
    var sup=0;

    if(document.forms["CreatePatient"]["supervisor"].checked)
    {
        var sup=document.forms["CreatePatient"]["supervisor"].value;
    }

    if( (pid == "")||(fn == "")||(ln == "")||(psw == "") ){
        alert("Content must be filled out");
        return false;
    }

    CreateNewPatient(pid,fn,ln,psw,sup);
    //CreateDatabase();
}

function CreateNewPatient(pid,fn,ln,psw,sup)
{

    var SQL2 = "SELECT UserID FROM [USER] WHERE UserID='"+pid+"';";
    var rsXML2 = myDB.query(SQL2, {xml:true});

    if(rsXML2)
    {
        alert("User Exsit!");
    }
    else
    {
        var SQL = "INSERT INTO [USER] VALUES('"+pid+"', '"+psw+"', "+sup+",'"+fn+"','"+ln+"');";
        var rsXML = myDB.query(SQL, {xml:true});
        alert("New User Added!");
    }

}

function AccessCurrentPatient()
{
    var pid=document.forms["CurrentPatient"]["pid"].value;


    //src = scripts[scripts.length-1].src;
    //alert(scripts);
    //var myDB = new ACCESSdb("C:\\Study/Murdoch/2017_S2/ICT302/ICT302_MGU/WebUI/Web/js/KinesisArcade.mdb");
    var SQL = "SELECT UserID FROM [USER] WHERE UserID='"+pid+"';";
    var rsXML = myDB.query(SQL, {xml:true});

    if(rsXML)
    {
        window.location.href = "CurrentPatient.html";
        createCookie("pid",pid,2);
        //document.getElementById("CP").innerHTML="H";
    }
    else
    {
        alert("No such patient! Try again!");
    }

}
