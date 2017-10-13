var modal = document.getElementById('id01');

window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

function handleFiles()
{
    initDatabase();
    //CreateDatabase();
}

function initDatabase()
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

    //var NewString=pid+","+fn+","+ln+","+psw+","+sup;
    //alert(insetString);

    var FileN = "C:\\Study/Murdoch/2017_S2/ICT302/ICT302_MGU/WebUI/Web/js/KinesisArcade.mdb";
    //src = scripts[scripts.length-1].src;
    //alert(scripts);
    //var myDB = new ACCESSdb("C:\\Study/Murdoch/2017_S2/ICT302/ICT302_MGU/WebUI/Web/js/KinesisArcade.mdb");
    var myDB = new ACCESSdb(FileN);
    var SQL = "SELECT FirstName FROM [USER] WHERE UserID='1';";
    var rsXML = myDB.query(SQL, {xml:true});
    var FN=rsXML.getElementsByTagName("FirstName")[0].childNodes[0].nodeValue;
    alert(FN);

}

function CreateDatabase()
{

}