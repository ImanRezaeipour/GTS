<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="AdminHelp.aspx.cs" Inherits="HelpGTS.AdminHelp" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="Styles/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/HelpDesign.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function Loadtree() {
            CallBack1.callback();
        }
        function CallBack1_onCallbackComplete(sender, eventArgs) {
           
        }
        function TreeView1_onNodeSelect(sender, eventArgs) {

            var formkey = eventArgs.get_node().get_id();
            var formID = eventArgs.get_node().get_value();
            document.getElementById('iFrameMain').src = "AdminContent.aspx?formID=" + formID + '&formkey=' + formkey + '&dt=' + (new Date()).getTime();
          }
    </script>
</head>
<body onload="Loadtree();">
    <form id="form1" runat="server">
    <div class="roundedcornr_box_590516" style="background-image: url('image/111.jpg')">
        <div class="roundedcornr_top_590516">
            <div>
            </div>
        </div>
        <div class="roundedcornr_content_590516">
            <table class="tblAdmin" align="center">
                <tr>
                    <td class="tdAdmin" align="right" dir="rtl" valign="top">
                           <ComponentArt:CallBack runat="server" ID="CallBack1" OnCallback="CallBack1_onCallBack" >
                    <Content>
                          <ComponentArt:TreeView id="TreeView1" Width="300" Height="500px" 
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
      <NodeSelect EventHandler="TreeView1_onNodeSelect" />

      </ClientEvents>
      </ComponentArt:TreeView>
                       </Content>
                       <ClientEvents>
    <CallbackComplete EventHandler="CallBack1_onCallbackComplete" />
    </ClientEvents>
                        </ComponentArt:CallBack>
                      
                    </td>
                    <td class="tdAdmin" width="70%" ><iframe id="iFrameMain" width="100%" src="" runat="server" height="600" 
                        scrolling="auto" frameborder="1" style="background-color: #FFFFFF"></iframe> </td>
                </tr>
              
                <tr>
                    <td class="tdAdmin">
                       
                    </td>
                    <td class="tdAdmin">
                     
                    </td>
                </tr>
            </table>
        </div>
        <div class="roundedcornr_bottom_590516">
            <div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
