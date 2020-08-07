
var CurrentLangID = null;
var CurrentPageState_PersonnelMainInformation = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjexpandingOrgPostNode_DialogPersonnelMainInformation = null;
var CurrentStateObj_Personnel = null;
var ObjPersonnel_DialogPersonnelMainInformation = null;
var ObjPersonnelImage_DialogPersonnelMainInformation = null;

function GetWorkingPersonnelID_PersonnelMainInformation() {
    if (parent.DialogPersonnelMainInformation.get_value().PageState == 'Add')
        GetWorkingPersonnelID_PersonnelMainInformationPage("");
    else
        SetActionMode_DialogPersonnelMainInformation();
}

function GetWorkingPersonnelID_PersonnelMainInformationPage_onCallBack(Response) {
    if (Response != null) {
        parent.DialogPersonnelMainInformation.get_value().PersonnelID = Response;
        SetActionMode_DialogPersonnelMainInformation();
    }
}

function ChangeComboTreeDirection_DialogPersonnelMainInformation(CurrentLangID) {
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('cmbDepartment_DialogPersonnelMainInformation_DropDownContent').style.direction = 'rtl';
        document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_DropDownContent').style.direction = 'rtl';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('cmbDepartment_DialogPersonnelMainInformation_DropDownContent').style.direction = 'ltr';
        document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_DropDownContent').style.direction = 'ltr';
    }
}

function CharToKeyCode_Personnel(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

function ImageUploader_DialogPersonnelMainInformation_OnAfterFileUpload(StrPersonnelImage) {
    //var obj = document.getElementById('Subgurim_ImageUploader_DialogPersonnelMainInformation');
    //Callback_ImageUploader_DialogPersonnelMainInformation.callback(CharToKeyCode_Personnel(parent.DialogPersonnelMainInformation.get_value().PersonnelID), CharToKeyCode_Personnel(FileName));
    //ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation();

    var message = null;
    if (ObjPersonnelImage_DialogPersonnelMainInformation == null)
        ObjPersonnelImage_DialogPersonnelMainInformation = new Object();
    ObjPersonnelImage_DialogPersonnelMainInformation = eval('(' + StrPersonnelImage + ')');
    if (ObjPersonnelImage_DialogPersonnelMainInformation.IsErrorOccured){
        document.getElementById('tdImageName_DialogPersonnelMainInformation').innerHTML = ObjPersonnelImage_DialogPersonnelMainInformation.Message;
        ObjPersonnelImage_DialogPersonnelMainInformation = null;
    }
    ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation(ObjPersonnelImage_DialogPersonnelMainInformation.PersonnelImageSavedName);
    Callback_ImageUploader_DialogPersonnelMainInformation.callback();
}

function Callback_ImageUploader_DialogPersonnelMainInformation_onCallBackComplete(sender, e) {
    //var error = document.getElementById('ErrorHiddenField_ImageUploader_DialogPersonnelMainInformation').value;
    //if (error != "") {
    //    var errorParts = eval('(' + error + ')');
    //    showDialog(errorParts[0], errorParts[1], errorParts[2]);
    //}

    Subgurim_ImageUploader_DialogPersonnelMainInformationadd('1', '4');
}

function tlbItemDeletePersonnelImage_TlbDeletePersonnelImage_DialogPersonnelMainInformation_onClick() {
    DeletePersonnelImage_DialogPersonnelMainInformation();
}

function DeletePersonnelImage_DialogPersonnelMainInformation() {
    var imageFileName = null;
    var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
    var personnelID = CurrentStateObj_PersonnelMainInformation.PersonnelID;
    if (ObjPersonnelImage_DialogPersonnelMainInformation != null && ObjPersonnelImage_DialogPersonnelMainInformation.PersonnelImageSavedName != null && ObjPersonnelImage_DialogPersonnelMainInformation.PersonnelImageSavedName != '')
        imageFileName = 'None';
    else{
        if (CurrentStateObj_PersonnelMainInformation.Image != undefined)
            imageFileName = 'None';
    }
    ObjPersonnelImage_DialogPersonnelMainInformation = 'None';
    ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation(null);
}

//function ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation() {
//    document.getElementById('PersonnelImage_DialogPersonnelMainInformation').src = 'ImageViewer.aspx?reload=""' + (new Date()).getTime() + '"&PersonnelID="' + parent.DialogPersonnelMainInformation.get_value().PersonnelID + '"';
//}

function ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation(imageFile) {
      document.getElementById('PersonnelImage_DialogPersonnelMainInformation').src = "ImageViewer.aspx?reload=" + (new Date()).getTime() + "&AttachmentType=Personnel&ClientAttachment=" + CharToKeyCode_Personnel(imageFile);
}


function init_DialogPersonnelMainInformation() {
    CurrentLangID = parent.CurrentLangID;
    GetBoxesHeaders_PersonnelMainInformation();
    //ViewCurrentLangCalendars_DialogPersonnelMainInformation(parent.SysLangID);
    ChangeComboTreeDirection_DialogPersonnelMainInformation(CurrentLangID);
}

function GetBoxesHeaders_PersonnelMainInformation() {
    parent.document.getElementById('Title_DialogPersonnelMainInformation').innerHTML = document.getElementById('hfTitle_DialogPersonnelMainInformation').value;
}

function ShowDialogPersonnelExtraInformation() {
    var ObjDialogPersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
    DialogPersonnelExtraInformation.set_value(ObjDialogPersonnelMainInformation);
    DialogPersonnelExtraInformation.Show();
}

function trvDepartment_DialogPersonnelMainInformation_onNodeSelect(sender, e) {
    document.getElementById('cmbDepartment_DialogPersonnelMainInformation_TextBox').innerHTML = e.get_node().get_text();
    cmbDepartment_DialogPersonnelMainInformation.collapse();
}


function trvOrganizationPost_DialogPersonnelMainInformation_onNodeSelect(sender, e) {
    document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_TextBox').innerHTML = e.get_node().get_text();
    cmbOrganizationPost_DialogPersonnelMainInformation.collapse();
}

function trvOrganizationPost_DialogPersonnelMainInformation_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_DialogPersonnelMainInformation != null)
        ObjexpandingOrgPostNode_DialogPersonnelMainInformation = null;
    ObjexpandingOrgPostNode_DialogPersonnelMainInformation = new Object();
    ObjexpandingOrgPostNode_DialogPersonnelMainInformation.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_DialogPersonnelMainInformation.HasChild = true;
        trvOrganizationPost_DialogPersonnelMainInformation.beginUpdate();
        ObjexpandingOrgPostNode_DialogPersonnelMainInformation.Node.get_nodes().remove(0);
        trvOrganizationPost_DialogPersonnelMainInformation.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_DialogPersonnelMainInformation.HasChild = false;
        else
            ObjexpandingOrgPostNode_DialogPersonnelMainInformation.HasChild = true;
    }
}

function trvOrganizationPost_DialogPersonnelMainInformation_onCallbackComplete() {
    if (ObjexpandingOrgPostNode_DialogPersonnelMainInformation != null) {
        if (ObjexpandingOrgPostNode_DialogPersonnelMainInformation.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_DialogPersonnelMainInformation.HasChild) {
            ObjexpandingOrgPostNode_DialogPersonnelMainInformation = null;
            GetLoadonDemandError_PersonnelMainInformationPage();
        }
        else
            ObjexpandingOrgPostNode_DialogPersonnelMainInformation = null;
    }
}

function GetLoadonDemandError_PersonnelMainInformationPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function ViewCurrentLangCalendars_DialogPersonnelMainInformation(SysLangID) {
    switch (SysLangID) {
        case 'en-US':
            document.getElementById("pdpBirthDate_DialogPersonnelMainInformation").parentNode.removeChild(document.getElementById("pdpBirthDate_DialogPersonnelMainInformation"));
            document.getElementById("pdpBirthDate_DialogPersonnelMainInformationimgbt").parentNode.removeChild(document.getElementById("pdpBirthDate_DialogPersonnelMainInformationimgbt"));
            document.getElementById("pdpEmployDate_WorkStart_DialogPersonnelMainInformation").parentNode.removeChild(document.getElementById("pdpEmployDate_WorkStart_DialogPersonnelMainInformation"));
            document.getElementById("pdpEmployDate_WorkStart_DialogPersonnelMainInformationimgbt").parentNode.removeChild(document.getElementById("pdpEmployDate_WorkStart_DialogPersonnelMainInformationimgbt"));
            document.getElementById("pdpEmployEndDate_DialogPersonnelMainInformation").parentNode.removeChild(document.getElementById("pdpEmployEndDate_DialogPersonnelMainInformation"));
            document.getElementById("pdpEmployEndDate_DialogPersonnelMainInformationimgbt").parentNode.removeChild(document.getElementById("pdpEmployEndDate_DialogPersonnelMainInformationimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_BirthDateCalendars_DialogPersonnelMainInformation").removeChild(document.getElementById("Container_gCalBirthDate_DialogPersonnelMainInformation"));
            document.getElementById("Container_EmployDateCalendars_WorkStart_DialogPersonnelMainInformation").removeChild(document.getElementById("Container_gCalEmployDate_WorkStart_DialogPersonnelMainInformation"));
            document.getElementById("Container_EmployEndDateCalendars_DialogPersonnelMainInformation").removeChild(document.getElementById("Container_gCalEmployEndDate_DialogPersonnelMainInformation"));
            break;
    }
}

///////////////////// gdpBirthDate & gCalBirthDate ////////////////////////
function gdpBirthDate_DialogPersonnelMainInformation_OnDateChange(sender, eventArgs) {
    var BirthDate = gdpBirthDate_DialogPersonnelMainInformation.getSelectedDate();
    gCalBirthDate_DialogPersonnelMainInformation.setSelectedDate(BirthDate);
}
function gCalBirthDate_DialogPersonnelMainInformation_OnChange(sender, eventArgs) {
    var BirthDate = gCalBirthDate_DialogPersonnelMainInformation.getSelectedDate();
    gdpBirthDate_DialogPersonnelMainInformation.setSelectedDate(BirthDate);
}
function btn_gdpBirthDate_DialogPersonnelMainInformation_OnClick(event) {
    if (gCalBirthDate_DialogPersonnelMainInformation.get_popUpShowing()) {
        gCalBirthDate_DialogPersonnelMainInformation.hide();
    }
    else {
        gCalBirthDate_DialogPersonnelMainInformation.setSelectedDate(gdpBirthDate_DialogPersonnelMainInformation.getSelectedDate());
        gCalBirthDate_DialogPersonnelMainInformation.show();
    }
}
function btn_gdpBirthDate_DialogPersonnelMainInformation_OnMouseUp(event) {
    if (gCalBirthDate_DialogPersonnelMainInformation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalBirthDate_DialogPersonnelMainInformation_onLoad(sender, e) {
    window.gCalBirthDate_DialogPersonnelMainInformation.PopUpObject.z = 25000000;
}


///////////////////// gdpEmployDate_WorkStart& gCalEmployDate_WorkStart ////////////////////////
function gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnDateChange(sender, eventArgs) {
    var EmployDate = gdpEmployDate_WorkStart_DialogPersonnelMainInformation.getSelectedDate();
    gCalEmployDate_WorkStart_DialogPersonnelMainInformation.setSelectedDate(EmployDate);
}
function gCalEmployDate_WorkStart_DialogPersonnelMainInformation_OnChange(sender, eventArgs) {
    var EmployDate = gCalEmployDate_WorkStart_DialogPersonnelMainInformation.getSelectedDate();
    gdpEmployDate_WorkStart_DialogPersonnelMainInformation.setSelectedDate(EmployDate);
}
function btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnClick(event) {
    if (gCalEmployDate_WorkStart_DialogPersonnelMainInformation.get_popUpShowing()) {
        gCalEmployDate_WorkStart_DialogPersonnelMainInformation.hide();
    }
    else {
        gCalEmployDate_WorkStart_DialogPersonnelMainInformation.setSelectedDate(gdpEmployDate_WorkStart_DialogPersonnelMainInformation.getSelectedDate());
        gCalEmployDate_WorkStart_DialogPersonnelMainInformation.show();
    }
}
function btn_gdpEmployDate_WorkStart_DialogPersonnelMainInformation_OnMouseUp(event) {
    if (gCalEmployDate_WorkStart_DialogPersonnelMainInformation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalEmployDate_WorkStart_DialogPersonnelMainInformation_onLoad(sender, e) {
    window.gCalEmployDate_WorkStart_DialogPersonnelMainInformation.PopUpObject.z = 25000000;
}


///////////////////// gdpEmployEndDate & gCalEmployEndDate ////////////////////////
function gdpEmployEndDate_DialogPersonnelMainInformation_OnDateChange(sender, eventArgs) {
    var EmployEndDate = gdpEmployEndDate_DialogPersonnelMainInformation.getSelectedDate();
    gCalEmployEndDate_DialogPersonnelMainInformation.setSelectedDate(EmployEndDate);
}
function gCalEmployEndDate_DialogPersonnelMainInformation_OnChange(sender, eventArgs) {
    var EmployEndDate = gCalEmployEndDate_DialogPersonnelMainInformation.getSelectedDate();
    gdpEmployEndDate_DialogPersonnelMainInformation.setSelectedDate(EmployEndDate);
}
function btn_gdpEmployEndDate_DialogPersonnelMainInformation_OnClick(event) {
    if (gCalEmployEndDate_DialogPersonnelMainInformation.get_popUpShowing()) {
        gCalEmployEndDate_DialogPersonnelMainInformation.hide();
    }
    else {
        gCalEmployEndDate_DialogPersonnelMainInformation.setSelectedDate(gdpEmployEndDate_DialogPersonnelMainInformation.getSelectedDate());
        gCalEmployEndDate_DialogPersonnelMainInformation.show();
    }
}
function btn_gdpEmployEndDate_DialogPersonnelMainInformation_OnMouseUp(event) {
    if (gCalEmployEndDate_DialogPersonnelMainInformation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalEmployEndDate_DialogPersonnelMainInformation_onLoad(sender, e) {
    window.gCalEmployEndDate_DialogPersonnelMainInformation.PopUpObject.z = 25000000;
}

function ShowDialogCalculationRange() {
    parent.DialogCalculationRange.Show();
}

function tlbItemWorkGroupDefinition_TlbWorkGroupDefinition_DialogPersonnelMainInformation_onClick() {
    ShowDialogPersonnelSingleDateFeatures('WorkGroups');
}

function tlbItemCalculationRangeGroupDefinition_TlbCalculationRangeGroupDefinition_DialogPersonnelMainInformation_onClick() {
    ShowDialogPersonnelSingleDateFeatures('CalculationRangesGroups');
}

function ShowDialogPersonnelSingleDateFeatures(Caller) {
    var ObjDialogPersonnelSingleDateFeatures = new Object();
    ObjDialogPersonnelSingleDateFeatures.Caller = Caller;
    DialogPersonnelSingleDateFeatures.set_value(ObjDialogPersonnelSingleDateFeatures);
    DialogPersonnelSingleDateFeatures.Show();
}

function ShowDialogPersonnelRulesGroups() {
    DialogPersonnelRulesGroups.Show();
}

function tlbItemSave_TlbPersonnelMainInformation_onClick() {
    CollapseControls_PersonnelMainInformation(null);
    UpdatePersonnel_DialogPersonnelMainInformation();
}

function UpdatePersonnel_DialogPersonnelMainInformation() {
    CurrentStateObj_Personnel = parent.DialogPersonnelMainInformation.get_value();

    ObjPersonnel_DialogPersonnelMainInformation = new Object();
    ObjPersonnel_DialogPersonnelMainInformation.PersonnelID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.IsActive = false;
    ObjPersonnel_DialogPersonnelMainInformation.FirstName = null;
    ObjPersonnel_DialogPersonnelMainInformation.LastName = null;
    ObjPersonnel_DialogPersonnelMainInformation.SexID = '-1';
    ObjPersonnel_DialogPersonnelMainInformation.FatherName = null;
    ObjPersonnel_DialogPersonnelMainInformation.NationalCode = null;
    ObjPersonnel_DialogPersonnelMainInformation.MilitaryState = '-1';
    ObjPersonnel_DialogPersonnelMainInformation.IdentityCertificate = null;
    ObjPersonnel_DialogPersonnelMainInformation.IssuanceLocation = null;
    ObjPersonnel_DialogPersonnelMainInformation.Education = null;
    ObjPersonnel_DialogPersonnelMainInformation.MarriageStateID = '-1';
    ObjPersonnel_DialogPersonnelMainInformation.Tel = null;
    ObjPersonnel_DialogPersonnelMainInformation.MobileNumber = null;
    ObjPersonnel_DialogPersonnelMainInformation.EmailAddress = null;
    ObjPersonnel_DialogPersonnelMainInformation.Address = null;
    ObjPersonnel_DialogPersonnelMainInformation.BirthLocation = null;
    ObjPersonnel_DialogPersonnelMainInformation.BirthDate = null;
    ObjPersonnel_DialogPersonnelMainInformation.PersonnelNumber = null;
    ObjPersonnel_DialogPersonnelMainInformation.CardNumber = null;
    ObjPersonnel_DialogPersonnelMainInformation.DepartmentID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostName = null;
    ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostCustomCode = null;
    ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostParentPath = null;
    ObjPersonnel_DialogPersonnelMainInformation.ParentOrganizationPostID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveWorkGroup = null;
    ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveRuleGroup = null;
    ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveCalculationRangeGroup = null;
    ObjPersonnel_DialogPersonnelMainInformation.ControlStationID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.EmployNumber = null;
    ObjPersonnel_DialogPersonnelMainInformation.EmployTypeID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.EmployDate = null;
    ObjPersonnel_DialogPersonnelMainInformation.EmployEndDate = null;
    ObjPersonnel_DialogPersonnelMainInformation.UserInterfaceRuleGroupID = '0';
    ObjPersonnel_DialogPersonnelMainInformation.Grade = null;
    ObjPersonnel_DialogPersonnelMainInformation.Image = null;
    ObjPersonnel_DialogPersonnelMainInformation.StrPersonnelExtraInformation = null;

    ObjPersonnel_DialogPersonnelMainInformation.PersonnelID = CurrentStateObj_Personnel.PersonnelID;
    if (document.getElementById('chbActive_DialogPersonnelMainInformation').checked)
        ObjPersonnel_DialogPersonnelMainInformation.IsActive = 'true';
    else
        ObjPersonnel_DialogPersonnelMainInformation.IsActive = 'false';
    ObjPersonnel_DialogPersonnelMainInformation.FirstName = document.getElementById('txtFirstName_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.LastName = document.getElementById('txtLastName_DialogPersonnelMainInformation').value;
    if (cmbSex_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.SexID = cmbSex_DialogPersonnelMainInformation.getSelectedItem().get_value();
    else {
        if (CurrentStateObj_Personnel.SexID != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.SexID = CurrentStateObj_Personnel.SexID;
    }
    ObjPersonnel_DialogPersonnelMainInformation.FatherName = document.getElementById('txtFatherName_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.NationalCode = document.getElementById('txtNationalCode_DialogPersonnelMainInformation').value;
    if (cmbMilitaryState_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.MilitaryState = cmbMilitaryState_DialogPersonnelMainInformation.getSelectedItem().get_value();
    else {
        if (CurrentStateObj_Personnel.MilitaryState != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.MilitaryState = CurrentStateObj_Personnel.MilitaryState;
    }
    ObjPersonnel_DialogPersonnelMainInformation.IdentityCertificate = document.getElementById('txtIdentityCertificate_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.IssuanceLocation = document.getElementById('txtIssuanceLocation_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.Education = document.getElementById('txtEducation_DialogPersonnelMainInformation').value;
    if (cmbMarriageState_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.MarriageStateID = cmbMarriageState_DialogPersonnelMainInformation.getSelectedItem().get_value();
    else {
        if (CurrentStateObj_Personnel.MarriageStateID != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.MarriageStateID = CurrentStateObj_Personnel.MarriageStateID;
    }
    ObjPersonnel_DialogPersonnelMainInformation.Tel = document.getElementById('txtTel_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.MobileNumber = document.getElementById('txtMobileNumber_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.Address = document.getElementById('txtAddress_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.EmailAddress = document.getElementById('txtEmailAddress_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.BirthLocation = document.getElementById('txtBirthLocation_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.PersonnelNumber = document.getElementById('txtPersonnelNumber_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.CardNumber = document.getElementById('txtCardNumber_DialogPersonnelMainInformation').value;
    if (trvDepartment_DialogPersonnelMainInformation.get_selectedNode() != undefined) {
        if (trvDepartment_DialogPersonnelMainInformation.get_selectedNode().get_parentNode() != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.DepartmentID = trvDepartment_DialogPersonnelMainInformation.get_selectedNode().get_id();
    }
    else {
        if (CurrentStateObj_Personnel.DepartmentID != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.DepartmentID = CurrentStateObj_Personnel.DepartmentID;
    }
    if (trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode() != undefined) {
        ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostID = trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode().get_id();
        ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostName = trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode().get_text();
        var organizationPostNodeValue = trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode().get_value();
        organizationPostNodeValue = eval('(' + organizationPostNodeValue + ')');
        ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostCustomCode = organizationPostNodeValue.CustomCode;
        ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostParentPath = organizationPostNodeValue.ParentPath;
        if (trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode().get_parentNode() != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.ParentOrganizationPostID = trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode().get_parentNode().get_id();
    }
    else {
        if (CurrentStateObj_Personnel.OrganizationPostID != undefined) {
            ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostID = CurrentStateObj_Personnel.OrganizationPostID;
            ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostName = CurrentStateObj_Personnel.OrganizationPostName;
            ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostCustomCode = CurrentStateObj_Personnel.OrganizationPostCustomCode;
            ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostParentPath = CurrentStateObj_Personnel.OrganizationPostParentPath;
            ObjPersonnel_DialogPersonnelMainInformation.ParentOrganizationPostID = CurrentStateObj_Personnel.ParentOrganizationPostID;
        }
    }
    ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveWorkGroup = document.getElementById('txtCurrentActiveWorkGroup_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveRuleGroup = document.getElementById('txtCurrentActiveRuleGroup_DialogPersonnelMainInformation').value;
    ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveCalculationRangeGroup = document.getElementById('txtCurrentActiveCalculationRangeGroup_DialogPersonnelMainInformation').value;
    if (cmbControlStation_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.ControlStationID = cmbControlStation_DialogPersonnelMainInformation.getSelectedItem().get_value();
    else {
        if (CurrentStateObj_Personnel.ControlStationID != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.ControlStationID = CurrentStateObj_Personnel.ControlStationID;
    }
    ObjPersonnel_DialogPersonnelMainInformation.EmployNumber = document.getElementById('txtEmployNumber_DialogPersonnelMainInformation').value;
    if (cmbEmployType_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.EmployTypeID = cmbEmployType_DialogPersonnelMainInformation.getSelectedItem().get_value();
    else {
        if (CurrentStateObj_Personnel.EmployTypeID != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.EmployTypeID = CurrentStateObj_Personnel.EmployTypeID;
    }
    if (cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.UserInterfaceRuleGroupID = cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.getSelectedItem().get_value();
    else {
        if (CurrentStateObj_Personnel.UserInterfaceRuleGroupID != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.UserInterfaceRuleGroupID = CurrentStateObj_Personnel.UserInterfaceRuleGroupID;
    }
    if (cmbGrade_DialogPersonnelMainInformation.getSelectedItem() != undefined)
        ObjPersonnel_DialogPersonnelMainInformation.Grade = cmbGrade_DialogPersonnelMainInformation.getSelectedItem().get_text();
    else {
        if (CurrentStateObj_Personnel.Grade != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.Grade = CurrentStateObj_Personnel.Grade;
    }
    if (ObjPersonnelImage_DialogPersonnelMainInformation != null) {
        var personImage = null;
        if (ObjPersonnelImage_DialogPersonnelMainInformation != 'None')
            personImage = ObjPersonnelImage_DialogPersonnelMainInformation.PersonnelImageSavedName;
        ObjPersonnel_DialogPersonnelMainInformation.Image = personImage;
    }
    else {
        if (CurrentStateObj_Personnel.Image != undefined)
            ObjPersonnel_DialogPersonnelMainInformation.Image = CurrentStateObj_Personnel.Image;
    }
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ObjPersonnel_DialogPersonnelMainInformation.BirthDate = document.getElementById('pdpBirthDate_DialogPersonnelMainInformation').value;
            ObjPersonnel_DialogPersonnelMainInformation.EmployDate = document.getElementById('pdpEmployDate_WorkStart_DialogPersonnelMainInformation').value;
            ObjPersonnel_DialogPersonnelMainInformation.EmployEndDate = document.getElementById('pdpEmployEndDate_DialogPersonnelMainInformation').value;
            break;
        case 'en-US':
            ObjPersonnel_DialogPersonnelMainInformation.BirthDate = document.getElementById('gdpBirthDate_DialogPersonnelMainInformation_picker').value;
            ObjPersonnel_DialogPersonnelMainInformation.EmployDate = document.getElementById('gdpEmployDate_WorkStart_DialogPersonnelMainInformation_picker').value;
            ObjPersonnel_DialogPersonnelMainInformation.EmployEndDate = document.getElementById('gdpEmployEndDate_WorkStart_DialogPersonnelMainInformation_picker').value;
            break;
    }

    ObjPersonnel_DialogPersonnelMainInformation.StrPersonnelExtraInformation = CurrentStateObj_Personnel.PageState == 'Edit' && CurrentStateObj_Personnel.StrPersonnelExtraInformation == '' ? UpdatePersonnelExtraInformation_PersonnelMainInformation(CurrentStateObj_Personnel) : CurrentStateObj_Personnel.StrPersonnelExtraInformation;

    UpdatePersonnel_PersonnelMainInformationPage(CharToKeyCode_Personnel(CurrentStateObj_Personnel.PageState), CharToKeyCode_Personnel(CurrentStateObj_Personnel.PageSize), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.PersonnelID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.IsActive.toString()), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.FirstName), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.LastName), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.SexID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.FatherName), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.NationalCode), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.MilitaryState), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.IdentityCertificate), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.IssuanceLocation), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.Education), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.MarriageStateID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.Tel), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.MobileNumber), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.Address), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.EmailAddress), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.BirthLocation), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.BirthDate), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.PersonnelNumber), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.CardNumber), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.DepartmentID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostName), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostCustomCode), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.OrganizationPostParentPath), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.ParentOrganizationPostID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveWorkGroup), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveRuleGroup), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.CurrentActiveCalculationRangeGroup), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.ControlStationID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.EmployNumber), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.EmployTypeID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.EmployDate), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.EmployEndDate), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.UserInterfaceRuleGroupID), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.StrPersonnelExtraInformation), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.Grade), CharToKeyCode_Personnel(ObjPersonnel_DialogPersonnelMainInformation.Image));
    DialogWaiting.Show();
}

function UpdatePersonnel_PersonnelMainInformationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            parent.document.getElementById('pgvPersonnelIntroduction_iFrame').contentWindow.Fill_GridPersonnel_Personnel_onPersonnelOperationCompleted(CurrentPageState_PersonnelMainInformation, RetMessage);
            CloseDialogPersonnelMainInformation();
        }
        else {
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
    }
}

function UpdatePersonnelExtraInformation_PersonnelMainInformation(CurrentStateObj_Personnel) {
    var StrPersonnelExtraInformation = '';
    var splitter = ',';
    for (var i = 1; i <= 20; i++) {
        var text = "";
        if (i >= 16 && i <= 20)
            text = eval('CurrentStateObj_Personnel.Reserve' + i + 'Text');
        if (i == 20)
            splitter = '';
        StrPersonnelExtraInformation += '{"Name":"R' + i + '","Text":"' + text + '","Value":"' + eval('CurrentStateObj_Personnel.Reserve' + i + '') + '"}' + splitter + '';
    }
    StrPersonnelExtraInformation = '[' + StrPersonnelExtraInformation + ']';
    UpdatePersonnel_onAfterPersonnelExtraInformation(StrPersonnelExtraInformation);
    return StrPersonnelExtraInformation;
}

function tlbItemExit_TlbPersonnelMainInformation_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DialogPersonnelMainInformation').value;
    DialogConfirm.Show();
}

function tlbItemExtraInformation_TlbPersonnelMainInformation_onClick() {
    ShowDialogPersonnelExtraInformation();
}

function cmbSex_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbSex_DialogPersonnelMainInformation);
    if (cmbSex_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_DialogPersonnelMainInformation = true;
        CallBack_cmbSex_DialogPersonnelMainInformation.callback();
    }
}

function cmbSex_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbSex_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.SexID != null && CurrentStateObj_PersonnelMainInformation.SexID != undefined)
            document.getElementById('cmbSex_DialogPersonnelMainInformation_Input').value = GetSex_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.SexID);
    }
}

function CallBack_cmbSex_DialogPersonnelMainInformation_onBeforeCallback() {
    cmbSex_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbSex_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Sex_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbSex_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSex_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbSex_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbSex_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function cmbMilitaryState_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbMilitaryState_DialogPersonnelMainInformation);
    if (cmbMilitaryState_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_DialogPersonnelMainInformation = true;
        CallBack_cmbMilitaryState_DialogPersonnelMainInformation.callback();
    }
}

function cmbMilitaryState_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbMilitaryState_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.MilitaryState != null && CurrentStateObj_PersonnelMainInformation.MilitaryState != undefined)
            document.getElementById('cmbMilitaryState_DialogPersonnelMainInformation_Input').value = GetMilitaryState_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.MilitaryState);
    }
}

function CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onBeforeCallback(sender, e) {
    cmbMilitaryState_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MilitaryState_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbMilitaryState_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMilitaryState_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbMilitaryState_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMilitaryState_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function cmbMarriageState_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbMarriageState_DialogPersonnelMainInformation);
    if (cmbMarriageState_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_DialogPersonnelMainInformation = true;
        CallBack_cmbMarriageState_DialogPersonnelMainInformation.callback();
    }
}

function cmbMarriageState_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbMarriageState_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.MarriageStateID != null && CurrentStateObj_PersonnelMainInformation.MarriageStateID != undefined)
            document.getElementById('cmbMarriageState_DialogPersonnelMainInformation_Input').value = GetMarriageState_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.MarriageStateID);
    }
}

function CallBack_cmbMarriageState_DialogPersonnelMainInformation_onBeforeCallback() {
    cmbMarriageState_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbMarriageState_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MarriageState_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbMarriageState_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMarriageState_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbMarriageState_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMarriageState_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function Refresh_cmbDepartment_DialogPersonnelMainInformation() {
    CallBack_cmbDepartment_DialogPersonnelMainInformation.callback();
}

function cmbDepartment_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbDepartment_DialogPersonnelMainInformation);
    if (trvDepartment_DialogPersonnelMainInformation.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_DialogPersonnelMainInformation = true;
        CallBack_cmbDepartment_DialogPersonnelMainInformation.callback();
    }
}

function cmbDepartment_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (trvDepartment_DialogPersonnelMainInformation.get_selectedNode() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if ((CurrentStateObj_PersonnelMainInformation.DepartmentName == null || CurrentStateObj_PersonnelMainInformation.DepartmentName == undefined))
            document.getElementById('cmbDepartment_DialogPersonnelMainInformation_Input').value = document.getElementById('hfcmbAlarm_DialogPersonnelMainInformation').value;
        else {
            if (CurrentStateObj_PersonnelMainInformation.DepartmentName != null && CurrentStateObj_PersonnelMainInformation.DepartmentName != undefined)
                document.getElementById('cmbDepartment_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.DepartmentName;
        }
    }
}

function CallBack_cmbDepartment_DialogPersonnelMainInformation_onBeforeCallback() {
    cmbDepartment_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbDepartment_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    ChangeComboTreeDirection_DialogPersonnelMainInformation(parent.CurrentLangID);
    var error = document.getElementById('ErrorHiddenField_Department_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbDepartment_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDepartment_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbDepartment_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDepartment_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function Refresh_cmbOrganizationPost_DialogPersonnelMainInformation() {
    CallBack_cmbOrganizationPost_DialogPersonnelMainInformation.callback();
}

function tlbItemClear_TlbClear_cmbOrganizationPost_DialogPersonnelMainInformation_onClick() {
    cmbOrganizationPost_DialogPersonnelMainInformation.unSelect();
    trvOrganizationPost_DialogPersonnelMainInformation.clearMultipleSelected();
    document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_Input').value = '';
    var CurrentStateObj_Personnel = parent.DialogPersonnelMainInformation.get_value();
    CurrentStateObj_Personnel.OrganizationPostID = undefined;
    CurrentStateObj_Personnel.OrganizationPostName = null;
}


function cmbOrganizationPost_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbOrganizationPost_DialogPersonnelMainInformation);
    if (trvOrganizationPost_DialogPersonnelMainInformation.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_DialogPersonnelMainInformation = true;
        ObjexpandingOrgPostNode_DialogPersonnelMainInformation = null;
        CallBack_cmbOrganizationPost_DialogPersonnelMainInformation.callback();
    }
}

function cmbOrganizationPost_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (trvOrganizationPost_DialogPersonnelMainInformation.get_selectedNode() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.OrganizationPostName != null && CurrentStateObj_PersonnelMainInformation.OrganizationPostName != undefined)
            document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.OrganizationPostName;
    }
}

function CallBack_cmbOrganizationPost_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    ChangeComboTreeDirection_DialogPersonnelMainInformation(parent.CurrentLangID);
    var error = document.getElementById('ErrorHiddenField_OrganizationPost_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOrganizationPost_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbOrganizationPost_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function Refresh_cmbControlStation_DialogPersonnelMainInformation() {
    CallBack_cmbControlStation_DialogPersonnelMainInformation.callback();
}

function tlbItemClear_TlbClear_cmbControlStation_DialogPersonnelMainInformation_onClick() {
    cmbControlStation_DialogPersonnelMainInformation.unSelect();
    document.getElementById('cmbControlStation_DialogPersonnelMainInformation_Input').value = '';
    var CurrentStateObj_Personnel = parent.DialogPersonnelMainInformation.get_value();
    CurrentStateObj_Personnel.ControlStationID = undefined;
    CurrentStateObj_Personnel.ControlStationName = null;
}

function cmbControlStation_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbControlStation_DialogPersonnelMainInformation);
    if (cmbControlStation_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_DialogPersonnelMainInformation = true;
        CallBack_cmbControlStation_DialogPersonnelMainInformation.callback();
    }
}

function cmbControlStation_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbControlStation_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.ControlStationName != null && CurrentStateObj_PersonnelMainInformation.ControlStationName != undefined)
            document.getElementById('cmbControlStation_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.ControlStationName;
    }
}

function CallBack_cmbControlStation_DialogPersonnelMainInformation_onBeforeCallback() {
    cmbControlStation_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbControlStation_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ControlStation_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbControlStation_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbControlStation_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbControlStation_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbControlStation_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function Refresh_cmbEmployType_DialogPersonnelMainInformation() {
    CallBack_cmbEmployType_DialogPersonnelMainInformation.callback();
}

function cmbEmployType_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbEmployType_DialogPersonnelMainInformation);
    if (cmbEmployType_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_DialogPersonnelMainInformation = true;
        CallBack_cmbEmployType_DialogPersonnelMainInformation.callback();
    }
}

function cmbEmployType_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbEmployType_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if ((CurrentStateObj_PersonnelMainInformation.EmployTypeName == null || CurrentStateObj_PersonnelMainInformation.EmployTypeName == undefined))
            document.getElementById('cmbEmployType_DialogPersonnelMainInformation_Input').value = document.getElementById('hfcmbAlarm_DialogPersonnelMainInformation').value;
        else {
            if (CurrentStateObj_PersonnelMainInformation.EmployTypeName != null && CurrentStateObj_PersonnelMainInformation.EmployTypeName != undefined)
                document.getElementById('cmbEmployType_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.EmployTypeName;
        }
    }
}

function CallBack_cmbEmployType_DialogPersonnelMainInformation_onBeforeCallback(sender, e) {
    cmbEmployType_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbEmployType_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_EmployType_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbEmployType_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbEmployType_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbEmployType_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbEmployType_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation);
    if (cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation = true;
        CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.callback();
    }
}

function cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.UserInterfaceRuleGroupName != null && CurrentStateObj_PersonnelMainInformation.UserInterfaceRuleGroupName != undefined)
            document.getElementById('cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.UserInterfaceRuleGroupName;
    }
}

function CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onBeforeCallback(sender, e) {
    cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UserInterfaceRuleGroup_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function Refresh_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation() {
    CallBack_cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation.callback();
}

function GetBoxesHeaders_DialogPersonnelMainInformation() {
    parent.document.getElementById('Title_DialogPersonnelMainInformation').innerHTML = document.getElementById('hfTitle_DialogPersonnelMainInformation').value;
    document.getElementById('cmbSex_DialogPersonnelMainInformation_Input').value = document.getElementById('cmbMarriageState_DialogPersonnelMainInformation_Input').value = document.getElementById('cmbDepartment_DialogPersonnelMainInformation_Input').value = document.getElementById('cmbEmployType_DialogPersonnelMainInformation_Input').value = document.getElementById('hfcmbAlarm_DialogPersonnelMainInformation').value;
}

function tlbItemClear_TlbClear_BirthDateCalendars_DialogPersonnelMainInformation_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpBirthDate_DialogPersonnelMainInformation').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpBirthDate_DialogPersonnelMainInformation_picker').value = "";
            break;
    }
}

function tlbItemClear_TlbClear_EmployDateCalendars_DialogPersonnelMainInformation_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpEmployDate_WorkStart_DialogPersonnelMainInformation').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpEmployDate_WorkStart_DialogPersonnelMainInformation_picker').value = "";
            break;
    }
}

function tlbItemClear_TlbClear_EmployEndDateCalendars_DialogPersonnelMainInformation_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpEmployEndDate_DialogPersonnelMainInformation').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpEmployEndDate_DialogPersonnelMainInformation_picker').value = "";
            break;
    }
}

function SetActionMode_DialogPersonnelMainInformation() {
    var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
    CurrentPageState_PersonnelMainInformation = CurrentStateObj_PersonnelMainInformation.PageState;
    document.getElementById('ActionMode_DialogPersonnelMainInformation').innerHTML = document.getElementById('hf' + CurrentPageState_PersonnelMainInformation + '_DialogPersonnelMainInformation').value;
    if (CurrentStateObj_PersonnelMainInformation.PageState == 'Edit')
        NavigatePersonnel_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation);
    if(CurrentStateObj_PersonnelMainInformation.PageState == 'Add')
       ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation(null);
}

function NavigatePersonnel_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation) {
    if (CurrentStateObj_PersonnelMainInformation.State != 'Add') {
        document.getElementById('chbActive_DialogPersonnelMainInformation').checked = GetChecked_chbActive_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.IsActive);
        document.getElementById('txtFirstName_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.FirstName;
        document.getElementById('txtLastName_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.LastName;
        document.getElementById('cmbSex_DialogPersonnelMainInformation_Input').value = GetSex_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.SexID);
        document.getElementById('txtFatherName_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.FatherName;
        document.getElementById('txtNationalCode_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.NationalCode;
        var MilitaryStateTitle = GetMilitaryState_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.MilitaryState);
        document.getElementById('cmbMilitaryState_DialogPersonnelMainInformation_Input').value = MilitaryStateTitle == undefined ? '' : MilitaryStateTitle;
        document.getElementById('txtIdentityCertificate_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.IdentityCertificate;
        document.getElementById('txtIssuanceLocation_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.IssuanceLocation;
        document.getElementById('txtEducation_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.Education;
        var MarriageStateTitle = GetMarriageState_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.MarriageStateID);
        document.getElementById('cmbMarriageState_DialogPersonnelMainInformation_Input').value = MarriageStateTitle == undefined ? '' : MarriageStateTitle;
        document.getElementById('txtTel_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.Tel;
        document.getElementById('txtMobileNumber_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.MobileNumber;
        document.getElementById('txtAddress_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.Address;
        document.getElementById('txtEmailAddress_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.EmailAddress;
        document.getElementById('txtBirthLocation_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.BirthLocation;
        document.getElementById('txtPersonnelNumber_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.PersonnelNumber;
        document.getElementById('txtCardNumber_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.CardNumber;
        document.getElementById('cmbDepartment_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.DepartmentName;
        document.getElementById('cmbOrganizationPost_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.OrganizationPostName;
        document.getElementById('txtCurrentActiveWorkGroup_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.CurrentActiveWorkGroup;
        document.getElementById('txtCurrentActiveRuleGroup_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.CurrentActiveRuleGroup;
        document.getElementById('txtCurrentActiveCalculationRangeGroup_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.CurrentActiveCalculationRangeGroup;
        document.getElementById('txtEmployNumber_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.EmployNumber;
        document.getElementById('cmbControlStation_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.ControlStationName;
        document.getElementById('cmbEmployType_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.EmployTypeName;
        document.getElementById('cmbUserInterfaceRuleGroup_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.UserInterfaceRuleGroupName;
        document.getElementById('cmbGrade_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.Grade;
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpBirthDate_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.BirthDate;
                document.getElementById('pdpEmployDate_WorkStart_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.EmployDate;
                document.getElementById('pdpEmployEndDate_DialogPersonnelMainInformation').value = CurrentStateObj_PersonnelMainInformation.EmployEndDate;
                break;
            case 'en-US':
                if (CurrentStateObj_PersonnelMainInformation.BirthDate != "") {
                    var gBirthDate = new Date(CurrentStateObj_PersonnelMainInformation.BirthDate);
                    gdpBirthDate_DialogPersonnelMainInformation.setSelectedDate(gBirthDate);
                    gCalBirthDate_DialogPersonnelMainInformation.setSelectedDate(gBirthDate);
                    var gEmployDate = new Date(CurrentStateObj_PersonnelMainInformation.EmployDate);
                    gdpEmployDate_WorkStart_DialogPersonnelMainInformation.setSelectedDate(gEmployDate);
                    gCalEmployDate_WorkStart_DialogPersonnelMainInformation.setSelectedDate(gEmployDate);
                    var gEmployEndDate = new Date(CurrentStateObj_PersonnelMainInformation.EmployEndDate);
                    gdpEmployEndDate_DialogPersonnelMainInformation.setSelectedDate(gEmployEndDate);
                    gCalEmployEndDate_DialogPersonnelMainInformation.setSelectedDate(gEmployEndDate);
                    break;
                }
        }
        ShowUploadedImage_ImageUploader_DialogPersonnelMainInformation(CurrentStateObj_PersonnelMainInformation.Image);
    }
}

function GetSex_DialogPersonnelMainInformation(sexID) {
    var sexList = document.getElementById('hfSexList_DialogPersonnelMainInformation').value;
    var sexListParts = sexList.split('#');
    for (var i = 0; i < sexListParts.length; i++) {
        if (sexListParts[i] != '') {
            var sexListPartObj = sexListParts[i].split(':');
            if (sexID == sexListPartObj[1]) {
                parent.DialogPersonnelMainInformation.get_value().SexTitle = sexListPartObj[0];
                return sexListPartObj[0];
            }
        }
    }
}

function GetMilitaryState_DialogPersonnelMainInformation(militaryStateID) {
    var militaryStateList = document.getElementById('hfMilitaryStateList_DialogPersonnelMainInformation').value;
    var militaryStateListParts = militaryStateList.split('#');
    for (var i = 0; i < militaryStateListParts.length; i++) {
        if (militaryStateListParts[i] != '') {
            var militaryStateListPartObj = militaryStateListParts[i].split(':');
            if (militaryStateID == militaryStateListPartObj[1]) {
                parent.DialogPersonnelMainInformation.get_value().MilitaryStateTitle = militaryStateListPartObj[0];
                return militaryStateListPartObj[0];
            }
        }
    }
}

function GetMarriageState_DialogPersonnelMainInformation(marriageStateID) {
    var marriageStateList = document.getElementById('hfMarriageStateList_DialogPersonnelMainInformation').value;
    var marriageStateListParts = marriageStateList.split('#');
    for (var i = 0; i < marriageStateListParts.length; i++) {
        if (marriageStateListParts[i] != '') {
            var marriageStateListPartObj = marriageStateListParts[i].split(':');
            if (marriageStateID == marriageStateListPartObj[1]) {
                parent.DialogPersonnelMainInformation.get_value().MarriageStateTitle = marriageStateListPartObj[0];
                return marriageStateListPartObj[0];
            }
        }
    }
}

function GetChecked_chbActive_DialogPersonnelMainInformation(IsActive) {
    var checked = null;
    switch (IsActive) {
        case 'true':
            checked = true;
            break;
        case 'false':
            checked = false;
            break;
    }
    return checked;
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogPersonnelMainInformation();
}

function CloseDialogPersonnelMainInformation() {
    parent.document.getElementById('DialogPersonnelMainInformation_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelMainInformation.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function ChangePersonnel_onPersonnelSingleDateFeaturesOperationCompleted(caller, currentActiveSingleDateFeature) {
    switch (caller) {
        case 'WorkGroups':
            parent.DialogPersonnelMainInformation.get_value().CurrentActiveWorkGroup = document.getElementById('txtCurrentActiveWorkGroup_DialogPersonnelMainInformation').value = currentActiveSingleDateFeature;
            break;
        case 'CalculationRangesGroups':
            parent.DialogPersonnelMainInformation.get_value().CurrentActiveCalculationRangeGroup = document.getElementById('txtCurrentActiveCalculationRangeGroup_DialogPersonnelMainInformation').value = currentActiveSingleDateFeature;
            break;
    }
}

function ChangePersonnel_onPersonnelRulesGroupsOperationCompleted(currentActiveRuleGroup) {
    parent.DialogPersonnelMainInformation.get_value().CurrentActiveRuleGroup = document.getElementById('txtCurrentActiveRuleGroup_DialogPersonnelMainInformation').value = currentActiveRuleGroup;
}

function tlbItemFormReconstruction_TlbPersonnelMainInformation_onClick() {
    ReconstructForm_DialogPersonnelMainInformation();
}

function ReconstructForm_DialogPersonnelMainInformation() {
    CloseDialogPersonnelMainInformation();
    parent.document.getElementById('pgvPersonnelIntroduction_iFrame').contentWindow.ShowDialogPersonnelMainInformation();
}

function ResetCalendars_DialogPersonnelMainInformation() {
    var currentDate_PersonnelMainInformation = document.getElementById('hfCurrentDate_PersonnelMainInformation').value;
    switch (parent.SysLangID) {
        case 'en-US':
            currentDate_PersonnelMainInformation = new Date(currentDate_PersonnelMainInformation);
            break;
        case 'fa-IR':
            document.getElementById('pdpBirthDate_DialogPersonnelMainInformation').value = '';
            document.getElementById('pdpEmployDate_WorkStart_DialogPersonnelMainInformation').value = '';
            document.getElementById('pdpEmployEndDate_DialogPersonnelMainInformation').value = '';
            break;
    }
}

function CallBack_cmbSex_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function CallBack_cmbMilitaryState_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function CallBack_cmbMarriageState_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function CallBack_cmbDepartment_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function CallBack_cmbControlStation_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function CallBack_cmbEmployType_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function CallBack_cmbOrganizationPost_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function Callback_ImageUploader_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function ShowConnectionError_PersonnelMainInformation() {
    var error = document.getElementById('hfErrorType_PersonnelMainInformation').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelMainInformation').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_PersonnelMainInformation(exception) {
    if (exception == null || exception != cmbSex_DialogPersonnelMainInformation)
        cmbSex_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbMilitaryState_DialogPersonnelMainInformation)
        cmbMilitaryState_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbMarriageState_DialogPersonnelMainInformation)
        cmbMarriageState_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbDepartment_DialogPersonnelMainInformation)
        cmbDepartment_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbOrganizationPost_DialogPersonnelMainInformation)
        cmbOrganizationPost_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbControlStation_DialogPersonnelMainInformation)
        cmbControlStation_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbEmployType_DialogPersonnelMainInformation)
        cmbEmployType_DialogPersonnelMainInformation.collapse();
    if (exception == null || exception != cmbGrade_DialogPersonnelMainInformation)
        cmbGrade_DialogPersonnelMainInformation.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpBirthDate_DialogPersonnelMainInformation');
}

function tlbItemHelp_TlbPersonnelMainInformation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnelMainInformation');
}

function tlbItemHelp_TlbVPersonnelMainInformation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbVPersonnelMainInformation');
}

function UpdatePersonnel_onAfterPersonnelExtraInformation(StrPersonnelExtraInformation) {
    CurrentStateObj_Personnel = parent.DialogPersonnelMainInformation.get_value();
    CurrentStateObj_Personnel.StrPersonnelExtraInformation = StrPersonnelExtraInformation;
}

function tlbItemParentDepartments_TlbParentDepartments_cmbDepartment_DialogPersonnelMainInformation_onClick() {
    var IsDepartmentExist = Fill_trvParentDepartments_PersonnelMainInformation();
    if (IsDepartmentExist)
        ShowDialogParentDepartments();
}

function ShowDialogParentDepartments() {
    DialogParentDepartments.Show();
}

function Fill_trvParentDepartments_PersonnelMainInformation() {
    var IsDepartmentExist = false;
    var DepartmentID = 0;
    CurrentStateObj_Personnel = parent.DialogPersonnelMainInformation.get_value();
    if (trvDepartment_DialogPersonnelMainInformation.get_selectedNode() != undefined) {
        if (trvDepartment_DialogPersonnelMainInformation.get_selectedNode().get_parentNode() != undefined) {
            IsDepartmentExist = true;
            DepartmentID = trvDepartment_DialogPersonnelMainInformation.get_selectedNode().get_id();
        }
    }
    else {
        if (CurrentStateObj_Personnel.DepartmentID != undefined) {
            IsDepartmentExist = true;
            DepartmentID = CurrentStateObj_Personnel.DepartmentID;
        }
    }

    if (IsDepartmentExist) {
        document.getElementById('loadingPanel_trvParentDepartments_PersonnelMainInformation').innerHTML = document.getElementById('hfloadingPanel_trvParentDepartments_PersonnelMainInformation').value;
        CallBack_trvParentDepartments_PersonnelMainInformation.callback(CharToKeyCode_Personnel(DepartmentID.toString()));
    }
    return IsDepartmentExist;
}

function trvParentDepartments_PersonnelMainInformation_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvParentDepartments_PersonnelMainInformation').innerHTML = "";
}

function CallBack_trvParentDepartments_PersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ParentDepartments_PersonnelMainInformation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvParentDepartments_PersonnelMainInformation();
    }
}

function CallBack_trvParentDepartments_PersonnelMainInformation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvParentDepartments_PersonnelMainInformation').innerHTML = '';
    ShowConnectionError_PersonnelMainInformation();
}

function tlbItemExit_tlbExit_ParentDepartments_PersonnelMainInformation_onClick() {
    CloseDialogParentDepartments();
}

function CloseDialogParentDepartments() {
    DialogParentDepartments.Close();
}

function cmbGrade_DialogPersonnelMainInformation_onExpand(sender, e) {
    CollapseControls_PersonnelMainInformation(cmbGrade_DialogPersonnelMainInformation);
    if (cmbGrade_DialogPersonnelMainInformation.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGrade_DialogPersonnelMainInformation == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGrade_DialogPersonnelMainInformation = true;
        CallBack_cmbGrade_DialogPersonnelMainInformation.callback();
    }
}

function cmbGrade_DialogPersonnelMainInformation_onCollapse(sender, e) {
    if (cmbGrade_DialogPersonnelMainInformation.getSelectedItem() == undefined) {
        var CurrentStateObj_PersonnelMainInformation = parent.DialogPersonnelMainInformation.get_value();
        if (CurrentStateObj_PersonnelMainInformation.Grade != null && CurrentStateObj_PersonnelMainInformation.Grade != undefined)
            document.getElementById('cmbGrade_DialogPersonnelMainInformation_Input').value = CurrentStateObj_PersonnelMainInformation.Grade;
    }
}

function CallBack_cmbGrade_DialogPersonnelMainInformation_onBeforeCallback(sender, e) {
    cmbGrade_DialogPersonnelMainInformation.dispose();
}

function CallBack_cmbGrade_DialogPersonnelMainInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Grade_DialogPersonnelMainInformation').value;
    if (error == "") {
        document.getElementById('cmbGrade_DialogPersonnelMainInformation_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbGrade_DialogPersonnelMainInformation_DropImage').mousedown();
        cmbGrade_DialogPersonnelMainInformation.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbGrade_DialogPersonnelMainInformation_DropDown').style.display = 'none';
    }
}

function CallBack_cmbGrade_DialogPersonnelMainInformation_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelMainInformation();
}

function tlbItemClear_TlbClear_cmbGrade_DialogPersonnelMainInformation_onClick() {
    cmbGrade_DialogPersonnelMainInformation.unSelect();
    document.getElementById('cmbGrade_DialogPersonnelMainInformation_Input').value = '';
    var CurrentStateObj_Personnel = parent.DialogPersonnelMainInformation.get_value();
    CurrentStateObj_Personnel.Grade = undefined;
}

function tlbItemRefresh_TlbVPersonnelMainInformation_onClick() {
    ReconstructForm_DialogPersonnelMainInformation();
}








