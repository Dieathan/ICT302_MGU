function ShowAllPorgrams() {

    var x, i, txt,programs;
    var SQL = "SELECT ProgramID FROM [PROGRAM] WHERE UserID='"+getCookie("pid")+"';";
    var rsXML = myDB.query(SQL, {xml:true});


    x = rsXML.getElementsByTagName("ProgramID");
    var xLen = x.length;

    for (i = 0; i <xLen; i++) {
        programs+=
            "<tr>"+
                "<td>"+
                    "<p>"+x[i].childNodes[0].nodeValue+"</p>"+
                "</td>"+
            "</tr>";
    }


    var PorgramTable="<table align='center' border='2px'>"+
                    "<tr>"+
                        "<td>"+
                        "<P>"+"Program ID"+"</P>"+
                        "</td>"+
                    "</tr>"+
                        programs+
                    "</table>";

    document.getElementById("show_programs").innerHTML=PorgramTable;

}