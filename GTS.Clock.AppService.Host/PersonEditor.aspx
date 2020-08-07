<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonEditor.aspx.cs" Inherits="GTS.Clock.AppService.Host.PersonEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function ChangePicture() {
            var myTextField = document.getElementById('personIdTxt');
            if (myTextField.value != "") {
                var src = 'ImageLoader.aspx?personid='+ myTextField.value;
                var myFaramField = document.getElementById('myframe');
                if (myFaramField != "") {
                    myFaramField.src = src;
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            Person Id:
            <input id="personIdTxt" type="text" />
            <input type="button" id="btn1" value="Go!" onclick='ChangePicture();' />
        </tr>
        <tr>
            <iframe id="myframe" width="500" height="500"></iframe>
        </tr>
    </table>
    </form>
</body>
</html>
