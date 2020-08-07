/* Variables */
var CurrentPageIndex_GridConcepts_Concepts = 0;
var box_ConceptSearch_Concepts_IsShown = false;
var LoadState_Concepts = 'Normal';
var CurrentPageCombosCallBackStateObj = new Object();
var CurrentPageState_Concepts = 'View';
var SelectedConcepts_Concept = new Object();
var ConfirmState_Concepts = null;

// Parent Section
var box_SearchByConcept_Concepts_IsShown = false;
var LoadState_cmbConcept_Concepts = 'Normal';
var CurrentPageIndex_cmbConcept_Concepts = 0;
var AdvancedSearchTerm_cmbConcept_Concepts = '';
var CurrentPageCombosCallBcakStateObj = new Object();
/* /Variables */

/*    */

function ChangePageState_Concepts(State) {
    CurrentPageState_Concepts = State;
    SetActionMode_Concepts(State);

    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbConcepts.get_items().getItemById('tlbItemNew_TlbConcepts') != null) {
            TlbConcepts.get_items().getItemById('tlbItemNew_TlbConcepts').set_enabled(false);
            TlbConcepts.get_items().getItemById('tlbItemNew_TlbConcepts').set_imageUrl('add_silver.png');
        }
        if (TlbConcepts.get_items().getItemById('tlbItemEdit_TlbConcepts') != null) {
            TlbConcepts.get_items().getItemById('tlbItemEdit_TlbConcepts').set_enabled(false);
            TlbConcepts.get_items().getItemById('tlbItemEdit_TlbConcepts').set_imageUrl('edit_silver.png');
        }
        if (TlbConcepts.get_items().getItemById('tlbItemDelete_TlbConcepts') != null) {
            TlbConcepts.get_items().getItemById('tlbItemDelete_TlbConcepts').set_enabled(false);
            TlbConcepts.get_items().getItemById('tlbItemDelete_TlbConcepts').set_imageUrl('remove_silver.png');
        }
        TlbConcepts.get_items().getItemById('tlbItemSave_TlbConcepts').set_enabled(true);
        TlbConcepts.get_items().getItemById('tlbItemSave_TlbConcepts').set_imageUrl('save.png');
        TlbConcepts.get_items().getItemById('tlbItemCancel_TlbTlbConcepts').set_enabled(true);
        TlbConcepts.get_items().getItemById('tlbItemCancel_TlbTlbConcepts').set_imageUrl('cancel.png');
        TlbConcepts.get_items().getItemById('tlbItemDefine_TlbTlbConcepts').set_enabled(true);
        TlbConcepts.get_items().getItemById('tlbItemDefine_TlbTlbConcepts').set_imageUrl('view_detailed.png');

        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemRefresh_TlbPaging_GridConcepts_Concepts').set_enabled(false);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemRefresh_TlbPaging_GridConcepts_Concepts').set_imageUrl('refresh_silver.png');
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemLast_TlbPaging_GridConcepts_Concepts').set_enabled(false);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemLast_TlbPaging_GridConcepts_Concepts').set_imageUrl('last_silver.png');
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemNext_TlbPaging_GridConcepts_Concepts').set_enabled(false);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemNext_TlbPaging_GridConcepts_Concepts').set_imageUrl('Next_silver.png');
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemBefore_TlbPaging_GridConcepts_Concepts').set_enabled(false);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemBefore_TlbPaging_GridConcepts_Concepts').set_imageUrl('Before_silver.png');
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemFirst_TlbPaging_GridConcepts_Concepts').set_enabled(false);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemFirst_TlbPaging_GridConcepts_Concepts').set_imageUrl('first_silver.png');
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbConceptQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbConceptQuickSearch').set_enabled(false);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbConceptQuickSearch').set_imageUrl('search_silver.png');
        }
        document.getElementById('txtSerchTerm_Concept').disabled = true;
        document.getElementById('txtCnptName').disabled = false;
        document.getElementById('txtCnptCode').disabled = false;
        cmbPeriodicTypeField_Concepts.enable();
        ChangeColorPickerEnabled_Concepts('enable');
        cmbTypeField_Concepts.enable();
        cmbCallSituationTypeField_Concepts.enable();
        document.getElementById('txtCnpKeyColumnName').disabled = false;
        cmbPersistSituationTypeField_Concepts.enable();
        cmbConceptCustomeCategoryCodeField_Concepts.enable();
        ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'enabled');

        if (State == 'Edit') {
            NavigateConcepts_Concepts(GridConcepts_Concepts.getSelectedItems()[0]);
        }
        if (State == 'Delete') {
            Concepts_onSave();
            Hide_ConceptSearchBox_Concepts();
        }
    }

    if (State == 'View') {
        if (TlbConcepts.get_items().getItemById('tlbItemNew_TlbConcepts') != null) {
            TlbConcepts.get_items().getItemById('tlbItemNew_TlbConcepts').set_enabled(true);
            TlbConcepts.get_items().getItemById('tlbItemNew_TlbConcepts').set_imageUrl('add.png');
        }
        if (TlbConcepts.get_items().getItemById('tlbItemEdit_TlbConcepts') != null) {
            TlbConcepts.get_items().getItemById('tlbItemEdit_TlbConcepts').set_enabled(true);
            TlbConcepts.get_items().getItemById('tlbItemEdit_TlbConcepts').set_imageUrl('edit.png');
        }
        if (TlbConcepts.get_items().getItemById('tlbItemDelete_TlbConcepts') != null) {
            TlbConcepts.get_items().getItemById('tlbItemDelete_TlbConcepts').set_enabled(true);
            TlbConcepts.get_items().getItemById('tlbItemDelete_TlbConcepts').set_imageUrl('remove.png');
        }
        TlbConcepts.get_items().getItemById('tlbItemSave_TlbConcepts').set_enabled(false);
        TlbConcepts.get_items().getItemById('tlbItemSave_TlbConcepts').set_imageUrl('save_silver.png');
        TlbConcepts.get_items().getItemById('tlbItemCancel_TlbTlbConcepts').set_enabled(false);
        TlbConcepts.get_items().getItemById('tlbItemCancel_TlbTlbConcepts').set_imageUrl('cancel_silver.png');
        TlbConcepts.get_items().getItemById('tlbItemDefine_TlbTlbConcepts').set_enabled(false);
        TlbConcepts.get_items().getItemById('tlbItemDefine_TlbTlbConcepts').set_imageUrl('view_detailed_silver.png');

        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemRefresh_TlbPaging_GridConcepts_Concepts').set_enabled(true);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemRefresh_TlbPaging_GridConcepts_Concepts').set_imageUrl('refresh.png');
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemLast_TlbPaging_GridConcepts_Concepts').set_enabled(true);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemLast_TlbPaging_GridConcepts_Concepts').set_imageUrl("last.png");
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemNext_TlbPaging_GridConcepts_Concepts').set_enabled(true);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemNext_TlbPaging_GridConcepts_Concepts').set_imageUrl("Next.png");
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemBefore_TlbPaging_GridConcepts_Concepts').set_enabled(true);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemBefore_TlbPaging_GridConcepts_Concepts').set_imageUrl("Before.png");
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemFirst_TlbPaging_GridConcepts_Concepts').set_enabled(true);
        TlbPaging_GridConcepts_Concepts.get_items().getItemById('tlbItemFirst_TlbPaging_GridConcepts_Concepts').set_imageUrl("first.png");
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbConceptQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbConceptQuickSearch').set_enabled(true);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbConceptQuickSearch').set_imageUrl('search.png');
        }

        document.getElementById('txtSerchTerm_Concept').disabled = false;
        document.getElementById('txtCnptName').disabled = true;
        document.getElementById('txtCnptCode').disabled = true;
        cmbPeriodicTypeField_Concepts.disable();
        ChangeColorPickerEnabled_Concepts('disable');
        cmbTypeField_Concepts.disable();
        cmbCallSituationTypeField_Concepts.disable();
        document.getElementById('txtCnpKeyColumnName').disabled = true;
        cmbPersistSituationTypeField_Concepts.disable();
        cmbConceptCustomeCategoryCodeField_Concepts.enable();
    }
}
function ChangeColorPickerEnabled_Concepts(state) {
    switch (state) {
        case 'disable':
            document.getElementById('a_ColorPicker').onclick = " ";
            break;
        case 'enable':
            document.getElementById('a_ColorPicker').onclick = function () {
                DialogColors.Show();
            };
            break;
    }
}
function CollapseControls_Concepts() {
    cmbPeriodicTypeField_Concepts.collapse();
    cmbTypeField_Concepts.collapse();
    cmbCallSituationTypeField_Concepts.collapse();
    cmbPersistSituationTypeField_Concepts.collapse();
    cmbConceptCustomeCategoryCodeField_Concepts.collapse();


}
function CollapseControls_Concepts(exception) {
    if (exception == null || exception != cmbConcept_Concepts)
        cmbConcept_Concepts.collapse();
}

/* /   */

/* Type Field */
function cmbPeriodicTypeField_Concepts_onExpand(sender, e) {
    if (cmbPeriodicTypeField_Concepts.get_itemCount() == 0 && CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbPeriodicTypeField_Concepts == undefined) {
        CurrentPageCombosCallBackStateObj.cmbPeriodicTypeField_Concepts_Text = document.getElementById('cmbPeriodicTypeField_Concepts_Input').value;
        CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbPeriodicTypeField_Concepts = true;
        CallBack_cmbPeriodicTypeField_Concepts.callback();
    } else {
        document.getElementById('cmbPeriodicTypeField_Concepts_Input').value = CurrentPageCombosCallBackStateObj.cmbPeriodicTypeField_Concepts_Text;
    }
}
function cmbPeriodicTypeField_Concepts_onBeforeCallback(sender, e) {
    cmbPeriodicTypeField_Concepts.dispose();
}
function cmbPeriodicTypeField_Concepts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_TypeFields').value;
    if (error == "") {
        document.getElementById('cmbPeriodicTypeField_Concepts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPeriodicTypeField_Concepts_DropImage').mousedown();
        cmbPeriodicTypeField_Concepts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPeriodicTypeField_Concepts_DropDown').style.display = 'none';
    }
}
function cmbPeriodicTypeField_Concepts_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = '';
    ShowConnectionError_Concepts();
}
function cmbPeriodicTypeField_Concepts_onChange(sender, e) {

    if (cmbPeriodicTypeField_Concepts.getSelectedItem() != undefined &&
        cmbPeriodicTypeField_Concepts.getSelectedItem().get_value() != undefined &&
        cmbPeriodicTypeField_Concepts.getSelectedItem().get_value() != '1') {
        ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'disabled');
        Hide_ConceptSearchBox_Concepts();
    } else {
        ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'enabled');
        ShowHide_SearchByConcept_Concepts();
    }

}
/* /Type Field */

/* Matt Field */
function cmbTypeField_Concepts_onExpand(sender, e) {
    if (cmbTypeField_Concepts.get_itemCount() == 0 && CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbTypeField_Concepts == undefined) {
        CurrentPageCombosCallBackStateObj.cmbTypeField_Concepts_Text = document.getElementById('cmbTypeField_Concepts_Input').value;
        CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbTypeField_Concepts = true;
        CallBack_cmbTypeField_Concepts.callback();
    } else {
        document.getElementById('cmbTypeField_Concepts_Input').value = CurrentPageCombosCallBackStateObj.cmbTypeField_Concepts_Text;
    }
}
function cmbTypeField_Concepts_onBeforeCallback(sender, e) {
    cmbTypeField_Concepts.dispose();
}
function cmbTypeField_Concepts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MattFields').value;
    if (error == "") {
        document.getElementById('cmbTypeField_Concepts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbTypeField_Concepts_DropImage').mousedown();
        cmbTypeField_Concepts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbTypeField_Concepts_DropDown').style.display = 'none';
    }
}
function cmbTypeField_Concepts_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = '';
    ShowConnectionError_Concepts();
}
/* /Matt Field */

/* ExecuteTime Field */
function cmbCallSituationTypeField_Concepts_onExpand(sender, e) {
    if (cmbCallSituationTypeField_Concepts.get_itemCount() == 0 && CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbCallSituationTypeField_Concepts == undefined) {
        CurrentPageCombosCallBackStateObj.cmbCallSituationTypeField_Concepts_Text = document.getElementById('cmbCallSituationTypeField_Concepts_Input').value;
        CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbCallSituationTypeField_Concepts = true;
        CallBack_ExecuteTimeField_Concepts.callback();
    } else {
        document.getElementById('cmbCallSituationTypeField_Concepts_Input').value = CurrentPageCombosCallBackStateObj.cmbCallSituationTypeField_Concepts_Text;
    }
}
function cmbCallSituationTypeField_Concepts_onBeforeCallback(sender, e) {
    cmbCallSituationTypeField_Concepts.dispose();
}
function cmbCallSituationTypeField_Concepts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ExecuteFields').value;
    if (error == "") {
        document.getElementById('cmbCallSituationTypeField_Concepts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbCallSituationTypeField_Concepts_DropImage').mousedown();
        cmbCallSituationTypeField_Concepts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbCallSituationTypeField_Concepts_DropDown').style.display = 'none';
    }
}
function cmbCallSituationTypeField_Concepts_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = '';
    ShowConnectionError_Concepts();
}
/* /ExecuteTime Field */

/* PersistSituationType Field */
function cmbPersistSituationTypeField_Concepts_onExpand(sender, e) {
    if (cmbPersistSituationTypeField_Concepts.get_itemCount() == 0 && CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbPersistSituationTypeField_Concepts == undefined) {
        CurrentPageCombosCallBackStateObj.cmbPeriodicTypeField_Concepts_Text = document.getElementById('cmbPersistSituationTypeField_Concepts_Input').value;
        CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbPersistSituationTypeField_Concepts = true;
        CallBack_cmbPersistSituationTypeField_Concepts.callback();
    } else {
        document.getElementById('cmbPersistSituationTypeField_Concepts_Input').value = CurrentPageCombosCallBackStateObj.cmbPeriodicTypeField_Concepts_Text;
    }
}
function cmbPersistSituationTypeField_Concepts_onBeforeCallback(sender, e) {
    cmbPersistSituationTypeField_Concepts.dispose();
}
function cmbPersistSituationTypeField_Concepts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ExecuteFields').value;
    if (error == "") {
        document.getElementById('cmbPersistSituationTypeField_Concepts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersistSituationTypeField_Concepts_DropImage').mousedown();
        cmbPersistSituationTypeField_Concepts.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersistSituationTypeField_Concepts_DropDown').style.display = 'none';
    }
}
function cmbPersistSituationTypeField_Concepts_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = '';
    ShowConnectionError_Concepts();
}
/* /PersistSituationType Field */

/* CustomeCategoryCode */
function cmbConceptCustomeCategoryCodeField_Concepts_onExpand(sender, e) {
    if (cmbConceptCustomeCategoryCodeField_Concepts.get_itemCount() == 0 && CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbConceptCustomeCategoryCodeField_Concepts == undefined) {
        CurrentPageCombosCallBackStateObj.cmbPeriodicTypeField_Concepts_Text = document.getElementById('cmbConceptCustomeCategoryCodeField_Concepts_Input').value;
        CurrentPageCombosCallBackStateObj.IsExpandOccured_cmbConceptCustomeCategoryCodeField_Concepts = true;
        CallBack_cmbConceptCustomeCategoryCodeField_Concepts.callback();
    } else {
        document.getElementById('cmbConceptCustomeCategoryCodeField_Concepts_Input').value = CurrentPageCombosCallBackStateObj.cmbPeriodicTypeField_Concepts_Text;
    }
}
function cmbConceptCustomeCategoryCodeField_Concepts_onBeforeCallback(sender, e) {
    cmbConceptCustomeCategoryCodeField_Concepts.dispose();
}
function cmbConceptCustomeCategoryCodeField_Concepts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ExecuteFields').value;
    if (error == "") {
        document.getElementById('cmbConceptCustomeCategoryCodeField_Concepts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbConceptCustomeCategoryCodeField_Concepts_DropImage').mousedown();
        cmbConceptCustomeCategoryCodeField_Concepts.expand();
    } else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbConceptCustomeCategoryCodeField_Concepts_DropDown').style.display = 'none';
    }
}
function cmbConceptCustomeCategoryCodeField_Concepts_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = '';
    ShowConnectionError_Concepts();
}
/* /CustomeCategoryCode */

/* GridConcepts Field */
function SetPageIndex_GridConcepts_Concepts(pageIndex) {
    CurrentPageIndex_GridConcepts_Concepts = pageIndex;
    Fill_GridConcepts_Concepts(pageIndex);
}
function Fill_GridConcepts_Concepts(pageIndex) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = document.getElementById('hfloadingPanel_GridConcepts_Concepts').value;
    var pageSize = parseInt(document.getElementById('hfConceptsPageSize_Concepts').value);
    var searchKey = 'NotSpecified';
    var searchTerm = '';
    if (box_ConceptSearch_Concepts_IsShown) {
        searchTerm = document.getElementById('txtSerchTerm_Concept').value;
    }
    CallBack_GridConcepts_Concept.callback(CharToKeyCode_Concepts(LoadState_Concepts), CharToKeyCode_Concepts(pageSize.toString()), CharToKeyCode_Concepts(pageIndex.toString()), CharToKeyCode_Concepts(searchKey), CharToKeyCode_Concepts(searchTerm));
}
function tlbItemSearch_TlbConceptQuickSearch_onClick(sender, e) {
    box_ConceptSearch_Concepts_IsShown = true;
    LoadState_Concepts = 'Search';
    SetPageIndex_GridConcepts_Concepts(0);
}
function GridConcepts_Concepts_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridConcepts_Concepts').innerHTML = '';
}
function GridConcepts_Concepts_onItemSelect(sender, e) {
    if (CurrentPageState_Concepts != 'Add')
        NavigateConcepts_Concepts(e.get_item());
}
function CallBack_GridConcepts_Concept_OnCallbackComplete(sender, e) {
    var test = 'test';
}
function CallBack_GridConcepts_Concept_onCallbackError(sender, e) {
    var test = 'test';

}
/* /GridConcepts Field */

/* Grid Toolbar Button Events */
function tlbConceptRefresh_TlbPaging_GridConcepts_Concepts_onClick(sender, e) {
    ChangeLoadState_GridConcepts_Concepts('Normal');
}
function ChangeLoadState_GridConcepts_Concepts(state) {
    LoadState_Concepts = state;
    SetPageIndex_GridConcepts_Concepts(0);
}
function tlbConceptFirst_TlbPaging_GridConcepts_Concepts_onClick(sender, e) {
    SetPageIndex_GridConcepts_Concepts(0);
}
function tlbConceptBefore_TlbPaging_GridConcepts_Concepts_onClick(sender, e) {
    if (CurrentPageIndex_GridConcepts_Concepts != 0) {
        CurrentPageIndex_GridConcepts_Concepts = CurrentPageIndex_GridConcepts_Concepts - 1;
        SetPageIndex_GridConcepts_Concepts(CurrentPageIndex_GridConcepts_Concepts);
    }
}
function tlbConceptNext_TlbPaging_GridConcepts_Concepts_onClick(sender, e) {
    if (CurrentPageIndex_GridConcepts_Concepts < parseInt(document.getElementById('hfConceptsPageCount_Concepts').value) - 1) {
        CurrentPageIndex_GridConcepts_Concepts = CurrentPageIndex_GridConcepts_Concepts + 1;
        SetPageIndex_GridConcepts_Concepts(CurrentPageIndex_GridConcepts_Concepts);
    }
}
function tlbConceptLast_TlbPaging_GridConcepts_Concepts_onClick(sender, e) {
    SetPageIndex_GridConcepts_Concepts(parseInt(document.getElementById('hfConceptsPageCount_Concepts').value) - 1);
}
/* /Grid Toolbar Button Events */

/* Search Monthly Concept */
function imgbox_SearchByConcept_Concepts_onClick() {
    //CollapseControls_Concepts();
    setSlideDownSpeed(200);
    slidedown_showHide('box_SearchByConcept_Concepts');
    if (box_SearchByConcept_Concepts_IsShown) {
        box_SearchByConcept_Concepts_IsShown = false;
        document.getElementById('box_SearchByConcept_Concepts').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByConcept_Concepts_IsShown = true;
        document.getElementById('box_SearchByConcept_Concepts').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function Hide_ConceptSearchBox_Concepts() {
    if (box_SearchByConcept_Concepts_IsShown) imgbox_SearchByConcept_Concepts_onClick();
}

function tlbItemRefresh_TlbPaging_ConceptSearch_Concepts_onClick(sender, e) {
    Refresh_cmbConcept_Concepts();
}
function Refresh_cmbConcept_Concepts() {
    LoadState_cmbConcept_Concepts = 'Normal';
    SetPageIndex_cmbConcept_Concepts(0);
}
function SetPageIndex_cmbConcept_Concepts(pageIndex) {
    CurrentPageIndex_cmbConcept_Concepts = pageIndex;
    Fill_cmbConcept_Concepts(pageIndex);
}
function Fill_cmbConcept_Concepts(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfConceptsPageSize_Concepts').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbConcept_Concepts) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbConcept_Concepts = SearchTermConditions = document.getElementById('txtSerchTerm_Concept').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbConcept_Concepts;
            break;
    }
    CallBack_cmbConcept_Concepts.callback(CharToKeyCode_Concepts(LoadState_cmbConcept_Concepts), CharToKeyCode_Concepts(pageSize.toString()), CharToKeyCode_Concepts(pageIndex.toString()), CharToKeyCode_Concepts(SearchTermConditions));
}
function tlbItemFirst_TlbPaging_ConceptSearch_Concepts_onClick(sebder, e) {
    SetPageIndex_cmbConcept_Concepts(0);
}
function tlbItemBefore_TlbPaging_ConceptSearch_Concepts_onClick(sender, e) {
    if (CurrentPageIndex_cmbConcept_Concepts != 0) {
        CurrentPageIndex_cmbConcept_Concepts = CurrentPageIndex_cmbConcept_Concepts - 1;
        SetPageIndex_cmbConcept_Concepts(CurrentPageIndex_cmbConcept_Concepts);
    }
}
function tlbItemNext_TlbPaging_ConceptSearch_Concepts_onClick(sender, e) {
    if (CurrentPageIndex_cmbConcept_Concepts < parseInt(document.getElementById('hfConceptPageCount_Concepts').value) - 1) {
        CurrentPageIndex_cmbConcept_Concepts = CurrentPageIndex_cmbConcept_Concepts + 1;
        SetPageIndex_cmbConcept_Concepts(CurrentPageIndex_cmbConcept_Concepts);
    }
}
function tlbItemLast_TlbPaging_ConceptSearch_Concepts_onClick(sender, e) {
    SetPageIndex_cmbConcept_Concepts(parseInt(document.getElementById('hfConceptPageCount_Concepts').value) - 1);
}

function cmbConcept_Concepts_onExpand(sender, e) {
    //CollapseControls_Concepts(cmbConcept_Concepts);
    //SetPosition_cmbConcept_Concepts();
    if (cmbConcept_Concepts.get_itemCount() == 0 &&
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbConcept_Concepts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbConcept_Concepts = true;
        SetPageIndex_cmbConcept_Concepts(0);
    }
}

function CallBack_cmbConcept_Concepts_onBeforeCallback(sender, e) {
    cmbConcept_Concepts.dispose();
}
function CallBack_cmbConcept_Concepts_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbConcept_Concepts').innerHTML = document.getElementById('hfclmnName_cmbConcept_Concepts').value;
    document.getElementById('clmnCode_cmbConcept_Concepts').innerHTML = document.getElementById('hfclmnCode_cmbConcept_Concepts').value;

    //SetPosition_cmbConcept_Concepts();

    var error = document.getElementById('hfErrorHiddenField_cmbConcept_Concepts').value;
    if (error == "") {
        document.getElementById('cmbConcept_Concepts_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbConcept_Concepts_DropImage').mousedown();
        else
            cmbConcept_Concepts.expand();
        var personnelCount = document.getElementById('hfConceptCount_Concepts').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbConcept_Concepts_DropDown').style.display = 'none';
    }
}
function CallBack_cmbConcept_Concepts_onCallbackError(sender, e) {
    ShowConnectionError_Concepts();
}

function tlbItemSearch_TlbSearchConcept_Concepts_onClick(sender, e) {
    LoadState_cmbConcept_Concepts = 'Search';
    SetPageIndex_cmbConcept_Concepts(0);
}

function ShowHide_SearchByConcept_Concepts() {

}
/* /Search Monthly Concept */


/* SelectedConcepts_Concept and it's functions */
function ClearControls_Concepts() {
    document.getElementById('txtCnptCode').value = '';
    document.getElementById('txtCnptName').value = '';
    document.getElementById('txtCnpKeyColumnName').value = '';
    document.getElementById("clr_ColorPicker").style.backgroundColor = '#FFFFFF';
    //document.getElementById('txtCnpDescription').value = '';

    document.getElementById('cmbPeriodicTypeField_Concepts_Input').value = '';
    document.getElementById('cmbTypeField_Concepts_Input').value = '';
    document.getElementById('cmbCallSituationTypeField_Concepts_Input').value = '';
    document.getElementById('cmbPersistSituationTypeField_Concepts_Input').value = '';
    document.getElementById('cmbConceptCustomeCategoryCodeField_Concepts_Input').value = '';


    if (cmbPeriodicTypeField_Concepts.getSelectedItem() != undefined)
        cmbPeriodicTypeField_Concepts.unSelect();
    if (cmbTypeField_Concepts.getSelectedItem() != undefined)
        cmbTypeField_Concepts.unSelect();
    if (cmbCallSituationTypeField_Concepts.getSelectedItem() != undefined)
        cmbCallSituationTypeField_Concepts.unSelect();
    if (cmbPersistSituationTypeField_Concepts.getSelectedItem() != undefined)
        cmbPersistSituationTypeField_Concepts.unSelect();

    if (cmbConceptCustomeCategoryCodeField_Concepts.getSelectedItem() != undefined)
        cmbConceptCustomeCategoryCodeField_Concepts.unSelect();



    ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'disabled');
    Hide_ConceptSearchBox_Concepts();


}

function NavigateConcepts_Concepts(selectedConcept) {
    if (selectedConcept == undefined) return;

    /*
    var typeTitle = selectedConcept.getMember('TypeTitle').get_text();
    var periodicTypeTitle = selectedConcept.getMember('PeriodicTypeTitle').get_text();
    var calcSituationTypeTitle = selectedConcept.getMember('CalcSituationTypeTitle').get_text();
    var persistSituationTypeTitle = selectedConcept.getMember('PersistSituationTypeTitle').get_text();
    */
    RefreshConcept_Concepts();

    SelectedConcepts_Concept_Fill(
        selectedConcept.getMember('ID').get_text(),
        selectedConcept.getMember('IdentifierCode').get_text(),
        selectedConcept.getMember('Name').get_text(),
        selectedConcept.getMember('Color').get_text(),
        selectedConcept.getMember('KeyColumnName').get_text(),
        selectedConcept.getMember('Type').get_text(),
        selectedConcept.getMember('PeriodicType').get_text(),
        selectedConcept.getMember('CalcSituationType').get_text(),
        selectedConcept.getMember('PersistSituationType').get_text(),
        selectedConcept.getMember('CustomCategoryCode').get_text(),
        selectedConcept.getMember('UserDefined').get_text(),
        selectedConcept.getMember('Script').get_text(),
        selectedConcept.getMember('CSharpCode').get_text(),
        selectedConcept.getMember('JsonObject').get_text()
    );

    SelectedConcepts_Concept_Fill_Fields();

}
function RefreshConcept_Concepts() {
    SelectedConcepts_Concept_Fill(-1, -1, null, null, null, -1, -1, -1, -1, -1, null, null, null, null);
}
function Concepts_onSave() {
    if (CurrentPageState_Concepts != 'Delete')
        UpdateConcept_Concepts();
    else
        ShowDialogConfirm('Delete');
}
function Concepts_Cancel() {
    ChangePageState_Concepts('View');
    RefreshConcept_Concepts();
    ClearControls_Concepts();
}

function SelectedConcepts_Concept_Fill(id, identifierCode, name, color, keyColumnName, periodicType, type, calcSituationType, persistSituationType, customeCategoryCode, userDefined, script, cSharpCode, JsonObject) {
    SelectedConcepts_Concept = new Object();

    SelectedConcepts_Concept.ID = id;
    SelectedConcepts_Concept.IdentifierCode = identifierCode;
    SelectedConcepts_Concept.Name = name;
    SelectedConcepts_Concept.Color = color;
    SelectedConcepts_Concept.KeyColumnName = keyColumnName;
    SelectedConcepts_Concept.PeriodicType = parseInt(periodicType);
    SelectedConcepts_Concept.Type = parseInt(type);
    SelectedConcepts_Concept.CalcSituationType = parseInt(calcSituationType);
    SelectedConcepts_Concept.PersistSituationType = parseInt(persistSituationType);
    SelectedConcepts_Concept.CustomeCategoryCode = parseInt(customeCategoryCode);
    SelectedConcepts_Concept.UserDefined = userDefined;
    SelectedConcepts_Concept.Script = script;
    SelectedConcepts_Concept.CSharpCode = cSharpCode;
    if (JsonObject != undefined && JsonObject != "")
        SelectedConcepts_Concept.JsonObject = JSON.parse(JsonObject);
    else SelectedConcepts_Concept.JsonObject = "";

    //document.getElementById('cmbConcept_Concepts_Input').Value = SelectedConcepts_Concept.PeriodicConcept;

}
function SelectedConcepts_Concept_Fill_Fields() {

    document.getElementById('txtCnptCode').value = SelectedConcepts_Concept.IdentifierCode;
    document.getElementById('txtCnptName').value = SelectedConcepts_Concept.Name;
    document.getElementById('txtCnpKeyColumnName').value = SelectedConcepts_Concept.KeyColumnName;

    if (SelectedConcepts_Concept.Color != null && SelectedConcepts_Concept.Color != '' && SelectedConcepts_Concept.Color != "") {
        var colorToApply = SelectedConcepts_Concept.Color;
        document.getElementById("clr_ColorPicker").style.backgroundColor = colorToApply;
    }
    //document.getElementById('txtCnpDescription').value = SelectedConcepts_Concept.Description;

    document.getElementById('cmbPeriodicTypeField_Concepts_Input').value = Enum_PerioricType[SelectedConcepts_Concept.PeriodicType];
    document.getElementById('cmbTypeField_Concepts_Input').value = Enum_Type[SelectedConcepts_Concept.Type];

    if (SelectedConcepts_Concept.Type != undefined &&
        SelectedConcepts_Concept.Type != 1) {
        ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'disabled');
        Hide_ConceptSearchBox_Concepts();
    }

    document.getElementById('cmbCallSituationTypeField_Concepts_Input').value = Enum_CalSituationType[SelectedConcepts_Concept.CalcSituationType];
    document.getElementById('cmbPersistSituationTypeField_Concepts_Input').value = Enum_PersistSituationType[SelectedConcepts_Concept.PersistSituationType];
    document.getElementById('cmbConceptCustomeCategoryCodeField_Concepts_Input').value = Enum_CustomeCategoryCode[SelectedConcepts_Concept.CustomeCategoryCode];

}
/* /SelectedConcepts_Concept and it's functions */

/* Utilities */
function CharToKeyCode_Concepts(str) {
    if (str == null) return '';

    str = str.toString();

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
function ClearList_Concepts() {
    if (CurrentPageState_Concepts != 'Edit') {
        RefreshConcept_Concepts();
    }
}
function SetActionMode_Concepts(state) {
    document.getElementById('ActionMode_Concepts').innerHTML = document.getElementById("hfState" + state + "_Concepts").value;
}
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != 1 || navigator.userAgent.indexOf('Chrome') != 1)
        return true;
    return false;
}

var Enum_PerioricType = null;
var Enum_Type = null;
var Enum_CalSituationType = null;
var Enum_PersistSituationType = null;
var Enum_CustomeCategoryCode = null;

function SetEnumTypes() {
    Enum_PerioricType = JSON.parse($('#hfJsonEnum_PeriodicType').val());
    Enum_Type = JSON.parse($('#hfJsonEnum_Type').val());
    Enum_CalSituationType = JSON.parse($('#hfJsonEnum_CalSituationType').val());
    Enum_PersistSituationType = JSON.parse($('#hfJsonEnum_PersistSituationType').val());
    Enum_CustomeCategoryCode = JSON.parse($('#hfJsonEnum_CustomeCategoryCode').val());
}

function GetPeriodicTypeTitle_Concept(enumId) {
    return Enum_PerioricType[enumId];
}
function GetTypeTitle_Concept(enumId) {
    return Enum_Type[enumId];
}
function GetCalSituationTypeTitle_Concept(enumId) {
    return Enum_CalSituationType[enumId];
}
function GetPersistSituationTypeTitle_Concept(enumId) {
    return Enum_PersistSituationType[enumId];
}
function GetCustomeCategoryCodeTitle_Concept(enumId) {
    return Enum_CustomeCategoryCode[enumId];
}

function SetPosition_DropDownDives_Concepts() {
    switch (parent.CurrentLangID) {
        case "fa-IR":
            document.getElementById('box_SearchByConcept_Concepts').style.right = '700px';
            break;
        case "en-US":
            document.getElementById('box_SearchByConcept_Concepts').style.left = '700px';
            break;
    }
}
function SetPosition_cmbConcept_Concepts() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbConcept_Concepts_DropDown').style.left = document.getElementById('tblConcepts_ConceptsForm').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbConcept_Concepts_DropDown').style.left = '30px';
    }
}

function CloseDialogConceptsManagemen() {
    parent.document.getElementById('DialogConceptsManagement_IFrame').src = 'WhitePage.aspx';
    parent.DialogConceptsManagement.Close();
}
function ShowDialogConfirm(confirmState) {
    ConfirmState_Concepts = confirmState;
    if (CurrentPageState_Concepts == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Concepts').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Concepts').value;
    DialogConfirm.Show();
    CollapseControls_Concepts();
}

function ChangeEnabled_DropDownDive_Concepts(dropdownDive, state) {
    switch (state) {
        case 'enabled':
            switch (dropdownDive) {
                case 'imgbox_SearchByConcept_Concepts':
                    {
                        document.getElementById(dropdownDive).onclick = function () { imgbox_SearchByConcept_Concepts_onClick(); };
                        Hide_ConceptSearchBox_Concepts();
                    }
                    break;
            }
            document.getElementById(dropdownDive).src = 'Images/Ghadir/arrowDown.jpg';
            break;
        case 'disabled':
            document.getElementById(dropdownDive).onclick = '';
            document.getElementById(dropdownDive).src = 'Images/Ghadir/arrowDown_silver.jpg';
            break;
    }
}
/* /Utilities */

/* Error */
function ShowConnectionError_Concepts() {
    var error = document.getElementById('hfErrorType_Concepts').value;
    var errorBody = document.getElementById('hfConnectionError_Concepts').value;
    showDialog(error, errorBody, 'error');
}
/* /Error */

/* Toolbar Item Clicks */
function tlbConceptNew_TlbConcepts_onClick(sender, e) {
    ClearControls_Concepts();
    ChangePageState_Concepts('Add');
}
function tlbConceptEdit_TlbConcepts_onClick(sender, e) {
    ChangePageState_Concepts('Edit');
}
function tlbConceptDelete_TlbConcepts_onClick(sender, e) {
    ChangePageState_Concepts('Delete');
}
//function tlbConceptHelp_TlbConcepts_onClick(sender, e) {

//}
function tlbConceptSave_TlbConcepts_onClick(sender, e) {
    CollapseControls_Concepts();
    Concepts_onSave();
}
function tlbConceptCancel_TlbTlbConcepts_onClick(sender, e) {
    Concepts_Cancel();
}
function tlbConceptDefine_TlbTlbConcepts_onClick(sender, e) {

    var ConceptJsonObjectEditor = new Object();
    ConceptJsonObjectEditor.ID = SelectedConcepts_Concept.ID;
    ConceptJsonObjectEditor.DetailsJsonObject = SelectedConcepts_Concept.JsonObject;
    ConceptJsonObjectEditor.ScriptEn = SelectedConcepts_Concept.CSharpCode;
    ConceptJsonObjectEditor.ScriptFa = SelectedConcepts_Concept.Script;

    ConceptJsonObjectEditor.CallerDialog = "ConceptManagement";

    parent.DialogConceptRuleEditor.set_value(ConceptJsonObjectEditor);
    parent.DialogConceptRuleEditor.Show();
}
function tlbItemFormReconstruction_TlbConcept_onClick(sender, e) {
    CloseDialogConceptsManagemen();
    parent.DialogConceptsManagement.Show();
}
function tlbConceptExit_TlbConcepts_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}

function Apply_Object_CSharp_Script_FromConceptRuleEditor(recivedObject) {
    SelectedConcepts_Concept.Script = recivedObject.ScriptFa;
    SelectedConcepts_Concept.CSharpCode = recivedObject.ScriptEn;
    SelectedConcepts_Concept.JsonObject = recivedObject.DetailsJsonObject;
}

/* /Toolbar Item Clicks */

function UpdateConcept_Concepts() {

    ObjConcept_Concepts = new Object();

    ObjConcept_Concepts.ID = 0;
    ObjConcept_Concepts.IdentifierCode = "";
    ObjConcept_Concepts.Name = "";
    ObjConcept_Concepts.Color = "";
    ObjConcept_Concepts.KeyColumnName = "";
    ObjConcept_Concepts.Description = "";

    ObjConcept_Concepts.PeriodicType = -1;
    ObjConcept_Concepts.Type = -1;
    ObjConcept_Concepts.CalcSituationType = -1;
    ObjConcept_Concepts.PersistSituationType = -1;
    ObjConcept_Concepts.CustomeCategoryCodpe = -1;
    
    ObjConcept_Concepts.UserDefined = null;
    ObjConcept_Concepts.Script = "";
    ObjConcept_Concepts.CSharpCode = "";
    ObjConcept_Concepts.JsonObject = "";

    var SelectedItems_GridConcepts_Concepts = GridConcepts_Concepts.getSelectedItems();
    if (SelectedItems_GridConcepts_Concepts.length > 0)
        ObjConcept_Concepts.ID = SelectedItems_GridConcepts_Concepts[0].getMember("ID").get_text();
    else ObjConcept_Concepts.ID = 0;

    if (CurrentPageState_Concepts != 'Delete') {

        ObjConcept_Concepts.IdentifierCode = document.getElementById('txtCnptCode').value;
        ObjConcept_Concepts.Name = document.getElementById('txtCnptName').value;
        // ObjConcept_Concepts.Description = document.getElementById('txtCnpDescription').value;

        //if (ColorGrid.get_selectedColor() != undefined)
        //    ObjConcept_Concepts.Color = '#' + ColorGrid.get_selectedColor().get_hex();

        ObjConcept_Concepts.Color = document.getElementById("clr_ColorPicker").style.backgroundColor;

        ObjConcept_Concepts.KeyColumnName = document.getElementById('txtCnpKeyColumnName').value;

        if (cmbPeriodicTypeField_Concepts.getSelectedItem() != undefined) {
            ObjConcept_Concepts.PeriodicType = parseInt(cmbPeriodicTypeField_Concepts.getSelectedItem().Value);
        } else if (SelectedConcepts_Concept.PeriodicType != undefined) {
            ObjConcept_Concepts.PeriodicType = SelectedConcepts_Concept.PeriodicType;
        }

        if (cmbTypeField_Concepts.getSelectedItem() != undefined) {
            ObjConcept_Concepts.Type = parseInt(cmbTypeField_Concepts.getSelectedItem().Value);
        } else if (SelectedConcepts_Concept.Type != undefined) {
            ObjConcept_Concepts.Type = SelectedConcepts_Concept.Type;
        }

        if (cmbCallSituationTypeField_Concepts.getSelectedItem() != undefined) {
            ObjConcept_Concepts.CalcSituationType = parseInt(cmbCallSituationTypeField_Concepts.getSelectedItem().Value);
        } else if (SelectedConcepts_Concept.CalcSituationType != undefined) {
            ObjConcept_Concepts.CalcSituationType = SelectedConcepts_Concept.CalcSituationType;
        }

        if (cmbPersistSituationTypeField_Concepts.getSelectedItem() != undefined) {
            ObjConcept_Concepts.PersistSituationType = parseInt(cmbPersistSituationTypeField_Concepts.getSelectedItem().Value);
        } else if (SelectedConcepts_Concept.PersistSituationType != undefined) {
            ObjConcept_Concepts.PersistSituationType = SelectedConcepts_Concept.PersistSituationType;
        }

        if (cmbConceptCustomeCategoryCodeField_Concepts.getSelectedItem() != undefined) {
            ObjConcept_Concepts.CustomeCategoryCodpe = parseInt(cmbConceptCustomeCategoryCodeField_Concepts.getSelectedItem().Value);
        } else if (SelectedConcepts_Concept.CustomeCategoryCodpe != undefined) {
            ObjConcept_Concepts.CustomeCategoryCodpe = SelectedConcepts_Concept.CustomeCategoryCodpe;
        }

        if (SelectedConcepts_Concept.UserDefined != undefined)
            ObjConcept_Concepts.UserDefined = SelectedConcepts_Concept.UserDefined;
        else ObjConcept_Concepts.UserDefined = true;

        if (SelectedConcepts_Concept.Script != undefined)
            ObjConcept_Concepts.Script = SelectedConcepts_Concept.Script;
        else ObjConcept_Concepts.Script = '';

        if (SelectedConcepts_Concept.CSharpCode != undefined)
            ObjConcept_Concepts.CSharpCode = SelectedConcepts_Concept.CSharpCode;
        else ObjConcept_Concepts.CSharpCode = '';

        ObjConcept_Concepts.JsonObject = JSON.stringify(SelectedConcepts_Concept.JsonObject);

    }

    UpdateConcept_ConceptsPage(
        CharToKeyCode_Concepts(ObjConcept_Concepts.ID),
		CharToKeyCode_Concepts(ObjConcept_Concepts.IdentifierCode),
		CharToKeyCode_Concepts(ObjConcept_Concepts.Name),
		CharToKeyCode_Concepts(ObjConcept_Concepts.Color),
		CharToKeyCode_Concepts(ObjConcept_Concepts.KeyColumnName),
        CharToKeyCode_Concepts(ObjConcept_Concepts.CSharpCode),
        CharToKeyCode_Concepts(ObjConcept_Concepts.Script),
        CharToKeyCode_Concepts(ObjConcept_Concepts.UserDefined),
        CharToKeyCode_Concepts(ObjConcept_Concepts.PeriodicType.toString()),
		CharToKeyCode_Concepts(ObjConcept_Concepts.Type.toString()),
		CharToKeyCode_Concepts(ObjConcept_Concepts.CalcSituationType.toString()),
		CharToKeyCode_Concepts(ObjConcept_Concepts.PersistSituationType.toString()),
		CharToKeyCode_Concepts(ObjConcept_Concepts.CustomeCategoryCodpe.toString()),
		CharToKeyCode_Concepts(ObjConcept_Concepts.JsonObject),
        CharToKeyCode_Concepts(CurrentPageState_Concepts)
    );

}
function UpdateConcept_ConceptsPage_onCallBack(Response) {
    //Console.log(JSON.stringify(Response));

    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Posts').value;
            Response[1] = document.getElementById('hfConnectionError_Posts').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Fill_GridConcepts_Concepts(CurrentPageIndex_GridConcepts_Concepts);
            ClearList_Concepts();
            RefreshConcept_Concepts();
            ClearControls_Concepts();
            ChangePageState_Concepts('View');
        }
        else {
            if (CurrentPageState_Concepts == 'Delete')
                ChangePageState_Concepts('View');
        }
    }
}


/* Conform Dialog Exit | Delete functions */
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Concepts('View');
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Concepts) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateConcept_Concepts();
            break;
        case 'Exit':
            RefreshConcept_Concepts();
            parent.DialogConceptsManagement.Close();
            break;
        default:
    }
}
/* /Conform Dialog Exit | Delete functions */

/* Color Picker */
function color_changed(sender, args) {
    if (sender.get_selectedColor() && sender.get_selectedColor().get_hex()) {
        var c = "#" + sender.get_selectedColor().get_hex();
        document.getElementById("chip").style.backgroundColor = c;
        document.getElementById("hex").innerHTML = c;
        document.getElementById("clr_ColorPicker").style.backgroundColor = c;
    }
}
function DialogColors_OnClose(sender, e) {
    document.getElementById("chip").style.backgroundColor = '';
    document.getElementById("hex").innerHTML = '';
}
function DialogColors_OnShow(sender, e) {
    document.getElementById('DialogColors').style.zIndex = 25000000;
}
/* /Color Picker */


