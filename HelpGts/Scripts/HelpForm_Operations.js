function LoadHelpPage(formKeyHelp) {

    window.open("Help.aspx?formKeyHelp=" + formKeyHelp, "help", "width=1024,heigth=650,menubar=yes,resizable=yes")
}
function Loadtree() {
    CallBack_TreeViewHelpForm_HelpForm.callback();
}
function CallBack_TreeViewHelpForm_HelpForm_onCallbackComplete(sender, eventArgs) {
    TreeViewHelpForm_HelpForm.selectNodeById(document.getElementById('hf_TreeViewFormKey_HelpForm').value);
}
function TreeViewHelpForm_HelpForm_onNodeSelect(sender, eventArgs) {

    var formkey = eventArgs.get_node().get_id();
    $("#divContentHelp_HelpForm").slideUp(1000, function () {
        document.getElementById('HelpForm_iFrame').src = "HelpContent.aspx?formKeyHelp=" + formkey + '&dt=' + (new Date()).getTime();
    });
    $("#divContentHelp_HelpForm").slideDown(1000);
}

