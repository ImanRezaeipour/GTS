//#region Variables

var CurrentConceptOrRuleIdentification = null;

var CurrentPageState_Concept = 'Edit';
var ConfirmState_Concept = null;
var ObjConcept_Concept = null;
var Obj_CncptExprsn_ExpandedNodes = null;

var ExpressionConcept_Selected = null;
var DetailedConcept_Selected = null;

var DetailsJsonObject = null;
var ScriptEn = null;
var ScriptFa = null;

var CallerDialog = null;

//#endregion


//#region trvConceptExpression client events
function trvConceptExpression_Concept_onLoad(sender, e) {
    //document.getElementById('loadingPanel_trvDetails_Concept').innerHTML = "";
}

function trvConceptExpression_Concept_onNodeSelect(sender, e) {
    Fill_ExpressionConcept_Selected(e.get_node());
}

function Fill_ExpressionConcept_Selected(selectedExpressionConceptNode) {
    if (selectedExpressionConceptNode != undefined) {
        ExpressionConcept_Selected = new Object();
        ExpressionConcept_Selected.ID = selectedExpressionConceptNode.get_id();
        ExpressionConcept_Selected.Value = selectedExpressionConceptNode.get_value();
        ExpressionConcept_Selected.Text = selectedExpressionConceptNode.get_text();
        ExpressionConcept_Selected.ImageUrl = selectedExpressionConceptNode.get_imageUrl();

        var InnerInDepth = JSON.parse(selectedExpressionConceptNode.Value)[0].Value;
        if (InnerInDepth != undefined) {
            if (TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor') != null) {
                if (InnerInDepth.CanAddToFinal) {
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_enabled(true);
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_imageUrl('arrow-left.png');

                } else {
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_enabled(false);
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_imageUrl('arrow-left_silver.png');
                }
            }
        }
    }
}

function trvConceptExpression_Concept_onNodeBeforeExpand(sender, e) {
    Obj_CncptExprsn_ExpandedNodes = new Object();
    Obj_CncptExprsn_ExpandedNodes.Node = e.get_node();
    if (
        e.get_node().get_nodes().get_length() == 1 &&
        (
            e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined ||
            e.get_node().get_nodes().get_nodeArray()[0].get_id() == ''
        )
       ) {
        Obj_CncptExprsn_ExpandedNodes.HasChild = true;
        trvConceptExpression_Concept.beginUpdate();
        Obj_CncptExprsn_ExpandedNodes.Node.get_nodes().remove(0);
        trvConceptExpression_Concept.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            Obj_CncptExprsn_ExpandedNodes.HasChild = false;
        else
            Obj_CncptExprsn_ExpandedNodes.HasChild = true;
    }
}

function trvConceptExpression_Concept_onCallbackComplete(sender, e) {
    if (Obj_CncptExprsn_ExpandedNodes != null) {
        if (Obj_CncptExprsn_ExpandedNodes.Node.get_nodes().get_length() == 0 && Obj_CncptExprsn_ExpandedNodes.HasChild) {
            Obj_CncptExprsn_ExpandedNodes = null;
            GetLoadonDemandError_ConceptsPage();
        }
        else
            Obj_CncptExprsn_ExpandedNodes = null;
    }
}

function trvConceptExpression_Concept_onNodeRename(sender, eventArgs) {
    Fill_ExpressionConcept_Selected(eventArgs.get_node());
}

function GetLoadonDemandError_ConceptsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvConceptExpression_Concept_onNodeMouseDoubleClick(sender, e) {
    Fill_ExpressionConcept_Selected(e.get_node());
    AddExpressionToDetailed();
}

var loadedBefore = false;

function CallBack_trvConceptExpression_Concept_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_trvConceptExpression_Concep').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvConceptExpression_Concept();
    } else if (!loadedBefore) {
        loadedBefore = true;
        FillByDialogValue_trvDetails_Concept();
        SetDetailedRootAsSelected();
    }
}

function CallBack_trvConceptExpression_Concept_onCallbackError(sender, e) {

}

function Fill_trvConceptExpression_Concept() {
    CallBack_trvConceptExpression_Concept.callback();
}

function SetActionMode_Concepts() {

}

function tlbConceptExpressionReferesh_TlbConcepts_onClick() {
    Obj_CncptExprsn_ExpandedNodes = null;
    Fill_trvConceptExpression_Concept();
}

//#endregion


//#region trvDetails client events

function SetCurrentConceptOrRuleIdentification() {
    CurrentConceptOrRuleIdentification = document.getElementById("hfConceptOrRuleIdentification").value;
}

function SetDetailedRootAsSelected() {
    trvDetails_Concept.selectNodeById(CurrentConceptOrRuleIdentification);
}

function trvDetails_Concept_onNodeSelect(sender, e) {
    Fill_DetailedConcept_Selected(e.get_node());
}

function Fill_DetailedConcept_Selected(selectedDetailedConcept) {
    if (selectedDetailedConcept != undefined) {
        DetailedConcept_Selected = new Object();

        DetailedConcept_Selected.ID = selectedDetailedConcept.get_id();
        DetailedConcept_Selected.Value = selectedDetailedConcept.get_value();
        DetailedConcept_Selected.Text = selectedDetailedConcept.get_text();
    }
}

function trvDetails_Concept_onLoad(sender, e) {
    if (
        trvDetails_Concept.get_selectedNode() == undefined &&
        trvDetails_Concept.get_nodes() != undefined &&
            trvDetails_Concept.get_nodes().getNode(0) != undefined
    ) {
        trvDetails_Concept.get_nodes().getNode(0).select();
    }
}

function trvDetails_Concept_onCallbackComplete(sender, e) {

}

function trvDetails_Concept_onNodeBeforeExpand(sender, e) {

}

function CallBack_trvDetails_Concept_onCallbackComplete(sender, e) {

}

function CallBack_trvDetails_Concept_onCallbackError(sender, e) {

}

function TlbConceptsDetails_Delete_TlbConcepts_onClick() {

    var selected = trvDetails_Concept.get_selectedNode();
    var rootNode = trvDetails_Concept.get_nodes().getNode(0);

    RemoveSelectedNodeFromDetailed();

}

function trvDetails_Concept_onNodeSelect(sender, e) {
    DetailedConcept_Selected = e.get_node();
}

function trvDetails_Concept_onNodeBeforeRename(sender, eventArgs) {
    if (eventArgs.get_newText().length < 0) {
        eventArgs.set_cancel(true);
    }

    //if (eventArgs.get_newText().length < eventArgs.get_node().get_text().length) {
    //    eventArgs.set_cancel(true);
    //}

    //var sender_value = eventArgs.get_node().get_value();
    //var outterValueConvertedToObject = JSON.parse(sender_value)[0];

    //if (!outterValueConvertedToObject.CanEditInFinal) {
    //    alert("این آیتم قابل تغیر نیست");
    //    eventArgs.set_cancel(true);
    //}

}

function trvDetails_Concept_onNodeRename(sender, eventArgs) {

}

function TlbConceptsDetails_Refresh_JsonObj_TlbConcepts_onClick() {
    GenerateAllScript();
}

function GenerateAllScript() {
    DetailsJsonObject = new Object();

    if (trvDetails_Concept.get_nodes() != null &&
        trvDetails_Concept.get_nodes().get_length() > 0
    ) {

        if (trvDetails_Concept.get_nodes().getNode(0) != undefined
            && trvDetails_Concept.get_nodes().getNode(0).get_nodes().get_length() > 0) {

            DetailsJsonObject = TreeViewNodesToObject(trvDetails_Concept.get_nodes().getNode(0).get_nodes());

            var stringJson = JSON.stringify(DetailsJsonObject);
            document.getElementById('ConceptExpressioncriptFull').innerHTML = stringJson;
            document.getElementById('ObjectJsonHiddenField_trDetails_Concept').Value = stringJson;

            //Extracted C# Code To ConceptExpressioncript TD tag
            ScriptEn = GenerateScript(DetailsJsonObject, "En");
            document.getElementById('ConceptExpressioncript').innerHTML = ScriptEn;

            // Extract ScriptFa to ConceptExpressioncriptFa TD tag
            ScriptFa = GenerateScript(DetailsJsonObject, "Fa");
            document.getElementById('ConceptExpressioncriptFa').innerHTML = ScriptFa;

        }
    }
}

function tlbConceptSave_TlbConcepts_onClick() {
    try {
        GenerateAllScript();
        if (DetailsJsonObject == null || ScriptEn == null || ScriptFa == null) return;

        //UpdateConcept_ConceptEditorPage(CharToKeyCode(CurrentPageState_Concept), CharToKeyCode(JSON.stringify(DetailsJsonObject)), CharToKeyCode(ScriptEn), CharToKeyCode(ScriptFa));
        UpdateConceptOnParentForm();

    } catch (e) {

    }
}

function UpdateConceptOnParentForm() {

    var ObjectToSendToParent = new Object();
    ObjectToSendToParent.DetailsJsonObject = DetailsJsonObject;
    ObjectToSendToParent.ScriptEn = ScriptEn;
    ObjectToSendToParent.ScriptFa = ScriptFa;

    switch (CallerDialog) {
        case "ConceptManagement":
            parent.document.getElementById('DialogConceptsManagement_IFrame').contentWindow.Apply_Object_CSharp_Script_FromConceptRuleEditor(ObjectToSendToParent);
            break;
        case "RuleManagement":
            parent.document.getElementById('DialogRulesManagement_IFrame').contentWindow.Apply_Object_CSharp_Script_FromRuleRuleEditor(ObjectToSendToParent);
            break;
    }

}

function UpdateConcept_ConceptEditorPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Concepts').value;
            Response[1] = document.getElementById('hfConnectionError_Concepts').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            //Personnel_OnAfterUpdate(Response);
            CurrentPageState_Concept = 'Edit';
        }
        //else {
        //    if (CurrentPageState_Personnel == 'Delete')
        //        CurrentPageState_Personnel = 'View';
        //}
    }
}

function FillByDialogValue_trvDetails_Concept() {
    if (parent.DialogConceptRuleEditor.get_value() != undefined) {

        if (parent.DialogConceptRuleEditor.get_value().ID == undefined) {
            CurrentConceptOrRuleIdentification = "ذخيره نشده";

            DetailsJsonObject = parent.DialogConceptRuleEditor.get_value().DetailsJsonObject;
            ScriptEn = parent.DialogConceptRuleEditor.get_value().ScriptEn;
            ScriptFa = parent.DialogConceptRuleEditor.get_value().ScriptFa;
            CallerDialog = parent.DialogConceptRuleEditor.get_value().CallerDialog;
            return;
        } else {
            CurrentConceptOrRuleIdentification = parent.DialogConceptRuleEditor.get_value().ID;

            DetailsJsonObject = parent.DialogConceptRuleEditor.get_value().DetailsJsonObject;
            ScriptEn = parent.DialogConceptRuleEditor.get_value().ScriptEn;
            ScriptFa = parent.DialogConceptRuleEditor.get_value().ScriptFa;
            CallerDialog = parent.DialogConceptRuleEditor.get_value().CallerDialog;
            Fill_trvDetails_Concept_By_DetailsJsonObject();
        }
    }
}

function Fill_trvDetails_Concept_By_DetailsJsonObject() {
    CleartrvDetails_ConceptAllNodes();

    var ConceptRule;

    if (DetailsJsonObject != undefined && DetailsJsonObject != "") {
        ConceptRule = DetailsJsonObject;// JSON.parse(DetailsJsonObject);
    }

    trvDetails_Concept.beginUpdate();

    var treeViewNodeToAdd = new ComponentArt.Web.UI.TreeViewNode();
    treeViewNodeToAdd.set_expanded(true);
    treeViewNodeToAdd.set_id(CurrentConceptOrRuleIdentification);
    treeViewNodeToAdd.set_value(JSON.stringify(""));
    treeViewNodeToAdd.set_text(CurrentConceptOrRuleIdentification);
    treeViewNodeToAdd.set_imageUrl("Images/TreeView/folder.gif");
    treeViewNodeToAdd.set_editingEnabled(false);
    trvDetails_Concept.get_nodes().add(treeViewNodeToAdd);

    trvDetails_Concept.endUpdate();

    trvDetails_Concept.selectNodeById(treeViewNodeToAdd.get_id());

    if (ConceptRule != undefined && ConceptRule != "") {
        trvDetails_Concept.beginUpdate();
        GenerateNode(ConceptRule, treeViewNodeToAdd.get_id());
        trvDetails_Concept.endUpdate();
        //trvDetails_Concept.get_nodes().get_nodeArray()[0].expand();
    }
}

function CleartrvDetails_ConceptAllNodes() {

    trvDetails_Concept.get_nodes().clear();

    //var trvDetailAllRootNodes = trvDetails.get_nodes();

    //for (var i = 0; i < trvDetailAllRootNodes.length; i++) {
    //    trvDetails_Concept.beginUpdate();
    //    trvDetails_Concept.get_selectedNode().get_nodes().add(newTreeViewNode);
    //    trvDetails_Concept.endUpdate();
    //}

}

//#endregion trvDetails


//#region

function tlbItemAdd_TlbInterAction_ConceptRuleEditor_onClick() {
    AddExpressionToDetailed();
}

function tlbItemDelete_TlbInterAction_ConceptRuleEditor_onClick() {
    RemoveSelectedNodeFromDetailed();
}

function Refresh_trvConceptExpression_Concept() {
    CallBack_trvConceptExpression_Concept.callback();
}

//#endregion


//#region
function tlbConceptHelp_TlbConcepts_onClick(sender, e) {

}

function tlbConceptExit_TlbConcepts_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Concepts = confirmState;
    if (CurrentPageState_Concept == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Concepts').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Concepts').value;
    DialogConfirm.Show();
}

//#endregion


//#region Utilities

function CharToKeyCode(str) {
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

function GetBoxesHeaders_Concepts() {

}

function SetActionMode_Concepts(state) {
    document.getElementById('ActionMode_Concepts').innerHTML = document.getElementById("hf" + state + "_Concepts").value;
}

function AddExpressionToDetailed() {
    if (trvDetails_Concept.get_selectedNode() == null) DetailedConcept_Selected == null;
    if (ExpressionConcept_Selected == null || DetailedConcept_Selected == null) return;

    var newTreeViewNode = new ComponentArt.Web.UI.TreeViewNode();

    var newIntId = 1;
    var newTreeViewNode_Id = DetailedConcept_Selected.ID.toString() + "_" + ExpressionConcept_Selected.ID + "_" + newIntId;

    var nodeCollection = DetailedConcept_Selected.get_nodes();

    for (var i = 0; i < nodeCollection.get_length() ; i++) {

        var curNode = nodeCollection.getNode(i);
        if (curNode.get_id() != newTreeViewNode_Id) continue;

        newIntId++;

        var dd = newTreeViewNode_Id.substr(0, newTreeViewNode_Id.length - 1);
        newTreeViewNode_Id = dd + newIntId;
    }

    newTreeViewNode.set_id(newTreeViewNode_Id);
    newTreeViewNode.set_value(ExpressionConcept_Selected.Value);
    newTreeViewNode.set_text(ExpressionConcept_Selected.Text);
    newTreeViewNode.set_imageUrl(ExpressionConcept_Selected.ImageUrl);

    var outterValueConvertedToObject = //JSON.parse(
        JSON.parse(ExpressionConcept_Selected.Value)[0].Value
    // )
    ;
    newTreeViewNode.set_editingEnabled(outterValueConvertedToObject.CanEditInFinal);

    trvDetails_Concept.beginUpdate();
    trvDetails_Concept.get_selectedNode().get_nodes().add(newTreeViewNode);
    trvDetails_Concept.endUpdate();

    trvDetails_Concept.get_selectedNode().expand();

    CallGetChildrenOnCreation(CharToKeyCode(ExpressionConcept_Selected.ID), CharToKeyCode(newTreeViewNode_Id));

}

function RemoveSelectedNodeFromDetailed() {
    if (DetailedConcept_Selected == undefined || trvDetails_Concept.get_selectedNode() == undefined) return;

    if (trvDetails_Concept.get_selectedNode().get_parentNode() == null ||
        trvDetails_Concept.get_selectedNode().get_parentNode() == undefined) {
        alert("نود ریشه قابل حذف نیست");
        return;
    }

    var parentNode = trvDetails_Concept.get_selectedNode().get_parentNode();

    if ((trvDetails_Concept.get_selectedNode().get_nodes() != null ||
       trvDetails_Concept.get_selectedNode().get_nodes() == undefined) &&
        confirm("آیا مطمئن هستید")
    ) {
        if (trvDetails_Concept.get_selectedNode().get_nodes().get_length() > 0) {
            if (confirm("آیا مطمئن هستید این نود دارای فرزند می باشد")) {
                trvDetails_Concept.get_selectedNode().remove();
                parentNode.select();
                return;
            } else { return; }
        }
        trvDetails_Concept.get_selectedNode().remove();
        parentNode.select();

    } else {
        trvDetails_Concept.get_selectedNode().remove();
        parentNode.select();
    }
}

function TreeViewNodesToObject(treeViewNodeCollection) {
    var jsonObjInner = [];
    for (var index = 0; index < treeViewNodeCollection.get_length() ; index++) {

        var curNode = treeViewNodeCollection.getNode(index);

        if (curNode.get_nodes() == undefined ||
            curNode.get_nodes().get_length() < 1) {
            var hh = {
                id: treeViewNodeCollection.getNode(index).get_id(),
                title: treeViewNodeCollection.getNode(index).get_text(),
                value: treeViewNodeCollection.getNode(index).get_value(),
                nodes: undefined
            };
            jsonObjInner.push(hh);
        } else {
            var oo = {
                id: treeViewNodeCollection.getNode(index).get_id(),
                title: treeViewNodeCollection.getNode(index).get_text(),
                value: treeViewNodeCollection.getNode(index).get_value(),
                nodes: TreeViewNodesToObject(treeViewNodeCollection.getNode(index).get_nodes())
            };
            jsonObjInner.push(oo);
        }
    }
    return jsonObjInner;
}

var lastWasEditable = false;
function GenerateScript(detailsJsonbObject, scriptType) {

    var result = "";
    var i;
    var expressionObject;
    var detail;

    switch (scriptType) {
        case "Fa":
            {
                for (i = 0; i < detailsJsonbObject.length; i++) {
                    detail = detailsJsonbObject[i];

                    expressionObject = SingleNodeToPrepare(detail);

                    if (expressionObject != undefined) {

                        if (expressionObject.CanEditInFinal != undefined && expressionObject.CanEditInFinal == true) {
                            result += " " + expressionObject.ScriptBeginFa;
                        } else {
                            if (expressionObject.ScriptBeginFa != undefined) {
                                result += " " + expressionObject.ScriptBeginFa;
                            }
                            if (detail.nodes != undefined && detail.nodes.length > 0) {
                                result += GenerateScript(detail.nodes, "Fa");
                            }
                            if (expressionObject.ScriptEndFa != undefined) {
                                result += " " + expressionObject.ScriptEndFa;
                            }
                        }


                    }
                }
                break;
            }
        case "En":
        default:
            {

                for (i = 0; i < detailsJsonbObject.length; i++) {
                    detail = detailsJsonbObject[i];

                    expressionObject = SingleNodeToPrepare(detail);

                    if (expressionObject != undefined) {

                        if (expressionObject.CanEditInFinal != undefined && expressionObject.CanEditInFinal == true) {
                            result += "" + expressionObject.ScriptBeginEn;
                            lastWasEditable = true;
                        } else {
                            if (expressionObject.ScriptBeginEn != undefined) {
                                if (lastWasEditable) {
                                    result += "" + expressionObject.ScriptBeginEn;
                                } else {
                                    result += " " + expressionObject.ScriptBeginEn;
                                }
                            }
                            if (detail.nodes != undefined && detail.nodes.length > 0) {
                                result += GenerateScript(detail.nodes, "En");
                            }
                            if (expressionObject.ScriptEndEn != undefined) {
                                if (lastWasEditable) {
                                    result += "" + expressionObject.ScriptEndEn;
                                } else {
                                    result += " " + expressionObject.ScriptEndEn;
                                }
                            }
                            lastWasEditable = false;
                        }

                    }
                }
                break;
            }
    }

    return result;
}

function SingleNodeToPrepare(node) {

    var ValueObject;

    try {
        ValueObject = JSON.parse(JSON.parse(node.value))[0].Value;
        ValueObject.ImageUrl = JSON.parse(JSON.parse(node.value))[0].ImageUrl;
    } catch (e) {
        try {
            ValueObject = JSON.parse(node.value)[0].Value;
            ValueObject.ImageUrl = JSON.parse(node.value)[0].ImageUrl;
        } catch (e) {
            try {
                ValueObject = JSON.parse(JSON.parse(node.value))[0].Value;
                ValueObject.ImageUrl = JSON.parse(JSON.parse(node.value))[0].ImageUrl;
            } catch (e) {

            }
        }
    }


    if (ValueObject != undefined && ValueObject.CanEditInFinal) {
        ValueObject.ScriptBeginEn = node.title;
        ValueObject.ScriptBeginFa = node.title;
    }
    return ValueObject;
}

function CallGetChildrenOnCreation(parentDbId, parentId) {
    GetChildrenOnCreation_ConceptEditorPage(parentDbId, parentId);
}

function GetChildrenOnCreation_ConceptEditorPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Concepts').value;
            Response[1] = document.getElementById('hfConnectionError_Concepts').value;
        }
        if (RetMessage[2] == 'success') {
            var parentID = RetMessage[3];
            var strJson = RetMessage[4];
            AddToDetailedFromCallback(strJson, parentID);
        }
        else {
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
    }
}

function AddToDetailedFromCallback(stringJson, parentId) {

    var treeViewNodeToAdd = trvDetails_Concept.findNodeById(parentId);
    var jsonObjectCollection = JSON.parse(stringJson);

    for (var i = 0; i < jsonObjectCollection.length; i++) {

        var jsonObjectItem = jsonObjectCollection[i];
        var jsonObjectValueObject = jsonObjectItem.Value;

        var newTreeViewNode = new ComponentArt.Web.UI.TreeViewNode();

        newTreeViewNode.set_id(parentId.toString() + "_" + jsonObjectItem.Id.toString());
        newTreeViewNode.set_value(JSON.stringify(jsonObjectItem.Value));
        newTreeViewNode.set_text(jsonObjectValueObject.ScriptBeginFa);
        newTreeViewNode.set_imageUrl(jsonObjectItem.ImageUrl);
        newTreeViewNode.set_editingEnabled(jsonObjectValueObject.CanEditInFinal);

        trvDetails_Concept.beginUpdate();
        treeViewNodeToAdd.get_nodes().add(newTreeViewNode);
        trvDetails_Concept.endUpdate();
    }
    treeViewNodeToAdd.get_selectedNode().expand();
}

function GenerateNode(jsonObjectCollection, parentNodeId) {

    var treeViewNodeToAdd = trvDetails_Concept.findNodeById(parentNodeId);

    if (jsonObjectCollection != undefined) {

        for (var i = 0; i < jsonObjectCollection.length; i++) {

            var ConceptRule = jsonObjectCollection[i];
            if (ConceptRule != undefined) {

                var newTreeViewNode = new ComponentArt.Web.UI.TreeViewNode();

                newTreeViewNode.set_id(parentNodeId + "_" + ConceptRule.id.toString());
                newTreeViewNode.set_value(ConceptRule.value);
                newTreeViewNode.set_text(ConceptRule.title);
                newTreeViewNode.set_imageUrl(SingleNodeToPrepare(ConceptRule).ImageUrl);
                newTreeViewNode.set_editingEnabled(SingleNodeToPrepare(ConceptRule).CanEditInFinal);

                treeViewNodeToAdd.get_nodes().add(newTreeViewNode);

                if (ConceptRule.nodes != undefined && ConceptRule.nodes.length > 0) {
                    GenerateNode(ConceptRule.nodes, newTreeViewNode.get_id());
                }
            }
        }
    }
    //treeViewNodeToAdd.expand();
}

//#endregion Utilities 


//#region Conform Dialog Exit | Delete functions
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    //ChangePageState_Concepts('View');
}
function tlbItemOk_TlbOkConfirm_onClick() {
    //switch (ConfirmState_Concepts) {
    //    case 'Delete':
    //        DialogConfirm.Close();
    //        UpdateConcept_Concepts();
    //        break;
    //    case 'Exit':
    //        RefreshConcept_Concepts();
    parent.DialogConceptRuleEditor.Close();
    //    break;
    //default:
    //}
}
//#endregion Conform Dialog Exit | Delete functions
