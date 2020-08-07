<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="GTS.Clock.Presentaion.WebForms.Help" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GTS Help</title>
    <link href="css/treeHelpStyle.css" runat="server" rel="stylesheet" type="text/css" />
    <link href="css/HelpDesign.css" runat="server" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#6c83c9">
    <script src="JS/jquery.js" type="text/javascript"></script>

    <form id="ClientHelpForm" runat="server" meta:resourcekey="ClientHelpForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="roundedcornr_box_590516" style="background-image: url('image/111.jpg')">
        <div class="roundedcornr_top_590516">
            <div>
            </div>
        </div>
        <div class="roundedcornr_content_590516">
        </div>
        <div style="width: 100%; font-family: tahoma; font-size: 14pt; font-weight: bold;
            color: #FFFFFF; margin-bottom: 20px;" align="center" >
            <asp:Label ID="lblHeader_Help" runat="server" Text="راهنمای جامع نرم افزار&nbsp; GTS"
                Font-Size="14pt" Font-Bold="True" Font-Names="tahoma" meta:resourcekey="lblHeader_Help"></asp:Label></div>
        <table style="width: 100%;">
            <tr>
                <td runat="server" style="width: 300px; padding-right: 10px; font-family: tahoma; font-size: 9pt;"
                     valign="top" meta:resourcekey="AlignObj">
                    <ComponentArt:CallBack runat="server" ID="CallBack_TreeViewHelpForm_HelpForm" OnCallback="CallBack_TreeViewHelpForm_HelpForm_Callback">
                        <Content>
                            <ComponentArt:TreeView ID="TreeViewHelpForm_HelpForm" Width="300" Height="500px"
                                NodeLabelPadding="2" ExtendNodeCells="true" DragAndDropEnabled="false" NodeEditingEnabled="false"
                                KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode" NodeRowCssClass="TreeNodeRow"
                                HoverNodeCssClass="HoverTreeNode" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true"
                                LineImagesFolderUrl="images/Help/treeview/lines/" LineImageWidth="21" LineImageHeight="21"
                                EnableViewState="false" runat="server">
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
                <td style="padding-left: 10px; width: auto;" align="center" valign="top">
                    <div id="divContentHelp_HelpForm" style="display: none">
                        <iframe id="HelpForm_iFrame" width="100%" src="" runat="server" height="500" scrolling="auto"
                            frameborder="1"></iframe>
                    </div>
                </td>                
            </tr>
        </table>
        <div class="roundedcornr_bottom_590516">
            <div>
            </div>
        </div>
    </div>
    <br />
    <asp:HiddenField ID="hf_TreeViewFormKey_HelpForm" runat="server" />
    </form>
</body>
</html>
