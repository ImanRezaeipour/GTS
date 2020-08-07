<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="AdminContent.aspx.cs"
    Inherits="HelpGTS.AdminContent" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="Styles/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/HelpDesign.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        okRegist();
            
            
        });
        function okRegist()
        {
         var d=document.getElementById('<%= HFMassegeOk.ClientID %>').value;
         if (document.getElementById('<%= HFMassegeOk.ClientID %>').value == "ok") {
             alert("اطلاعات با موفقیت ثبت شد.");

         }
         document.getElementById('<%= HFMassegeOk.ClientID %>').value = "";
     }
    
        
    </script>
</head>
<body bgcolor="#6c83c9">
    <form id="form1" runat="server">
   
    <div dir="rtl" align="center" >
    
    <FTB:FreeTextBox ID="FreeTextBox1" ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
                            runat="Server" DesignModeCss="designmode.css" 
            ButtonDownImage="False" ImageGalleryPath="~/images/Help/HelpImage"
                            SupportFolder="/images/Help/FreeTextBox/" 
            TextDirection="RightToLeft" ToolbarStyleConfiguration="Office2003"
                            Width="700px" PasteMode="Text" ToolbarBackColor="Transparent" 
                            Language="fa-IR" RenderMode="NotSet"  
            Height="400px" /><br />
                            <div align="center">
                            <asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" Font-Names="tahoma"
                            Font-Size="9pt" Width="80px" /></div>
                            
    </div>
    <asp:HiddenField ID="HFMassegeOk" runat="server" />
    <asp:HiddenField ID="HFFormID" runat="server" />
    </form>
</body>
</html>
