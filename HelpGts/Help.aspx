<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="HelpGTS.Help" %>

<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GTS Help</title>
    <link href="Styles/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/HelpDesign.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/HelpForm_onPageLoad.js" type="text/javascript"></script>
    <script src="Scripts/HelpForm_Operations.js" type="text/javascript"></script>
    
</head>

<body bgcolor="#6c83c9">
    <form id="ClientHelpForm" runat="server" meta:resourcekey="ClientHelpForm">
    <div class="roundedcornr_box_590516" 
        style="background-image: url('image/111.jpg')">
   <div class="roundedcornr_top_590516"><div></div></div>
      <div class="roundedcornr_content_590516">
      </div>
      
     <div style="width: 100%; font-family: tahoma; font-size: 14pt; font-weight: bold; color: #FFFFFF; margin-bottom: 20px;" 
            align="center" dir="rtl">
            <asp:Label ID="lblHeader_Help" runat="server" Text="راهنمای جامع نرم افزار&nbsp; GTS" Font-Size="14pt" Font-Bold="True" Font-Names="tahoma" meta:resourcekey="lblHeader_Help"></asp:Label></div>
        
        <table style="width: 100%;">
            <tr >
                <td style="padding-left: 10px; width: auto;" align="center" dir="rtl" 
                    valign="top">
                    <div id="divContentHelp_HelpForm" style="display: none">
                    <iframe id="HelpForm_iFrame" width="100%" src="" runat="server" height="500" 
                        scrolling="auto" frameborder="1"></iframe></div></td>
                <td style="width: 300px; padding-right: 10px; font-family: tahoma; font-size: 9pt;"; 
                    align="right" dir="rtl" valign="top">
                    <ComponentArt:CallBack runat="server" ID="CallBack_TreeViewHelpForm_HelpForm" 
                        OnCallback="CallBack_TreeViewHelpForm_HelpForm_Callback" >
                    <Content>
                          <ComponentArt:TreeView id="TreeViewHelpForm_HelpForm" Width="300" Height="500px" 
      NodeLabelPadding="2"
      ExtendNodeCells="true"
      DragAndDropEnabled="false"
      NodeEditingEnabled="false"
      KeyboardEnabled="true"
      CssClass="TreeView"
      NodeCssClass="TreeNode"
      NodeRowCssClass="TreeNodeRow"
      HoverNodeCssClass="HoverTreeNode"
      SelectedNodeCssClass="SelectedTreeNode"
      ShowLines="true"
      LineImagesFolderUrl="images/Help/treeview/lines/"
      LineImageWidth="21"
      LineImageHeight="21"
      EnableViewState="false"
      runat="server">
      <ClientEvents>
      <NodeSelect EventHandler="TreeViewHelpForm_HelpForm_onNodeSelect" />

      </ClientEvents>
      </ComponentArt:TreeView>
           </Content>

<ClientEvents>
    <CallbackComplete EventHandler="CallBack_TreeViewHelpForm_HelpForm_onCallbackComplete" />
    </ClientEvents>
 </ComponentArt:CallBack>
  
                </td>
                
            </tr>
        </table>
    
     <div class="roundedcornr_bottom_590516"><div></div></div>
</div><br />
<asp:HiddenField ID="hf_TreeViewFormKey_HelpForm" runat="server" />
   
    </form>
</body>
</html>
