<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="HelpGTS.test" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

  <script type="text/javascript" src="Scripts/HelpForm_onPageLoad.js"></script>
        
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    <input id="Button1" type="button" value="help" onclick="LoadPageHelp(document.getElementById('txtQuery').value);" />
    <input id="txtQuery" type="text"  />
        <br />
        <br />
     
        <br />
        <br />
    </div>
    </form>
</body>
</html>
