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
    var db=null;

    var insetString="INSERT INTO USER (UserID, Password, IsSupervisor, Firstname, LastName) VALUES ('123','123',0,'123','123')";

    if(document.forms["CreatePatient"]["supervisor"].checked)
    {
        var sup=document.forms["CreatePatient"]["supervisor"].value;
    }

    var NewString=pid+","+fn+","+ln+","+psw+","+sup;
    //alert(insetString);

    var myDB = new ACCESSdb("KinesisArcade.mdb", {showErrors:true});

    var SQL = "INSERT INTO USER VALUES('123', 'abc 123', 0,'xyz','123')";
    var resultSet = myDB.query(SQL);

    if(myDB.query(SQL))
    {
        alert("Yes");
    }

}

function CreateDatabase()
{
    //Create the database
    var db = new SQL.Database();
    // Run a query without reading the results
    db.run("CREATE TABLE test (col1, col2);");
    // Insert two rows: (1,111) and (2,222)
    db.run("INSERT INTO test VALUES (?,?), (?,?)", [1,111,2,222]);

    // Prepare a statement
    var stmt = db.prepare("SELECT * FROM test WHERE col1 BETWEEN $start AND $end");
    stmt.getAsObject({$start:1, $end:1}); // {col1:1, col2:111}

    // Bind new values
    stmt.bind({$start:1, $end:2});
    while(stmt.step()) { //
        var row = stmt.getAsObject();
        // [...] do something with the row of result
    }
}