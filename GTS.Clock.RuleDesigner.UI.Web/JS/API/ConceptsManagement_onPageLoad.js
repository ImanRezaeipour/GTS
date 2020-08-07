
$(document).ready(
 function () {
     document.body.dir = document.ConceptsManagement.dir;
     SetWrapper_Alert_Box(document.ConceptsManagement.id);
     //GetBoxesHeaders_Concepts();
     ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'disabled');
     SetActionMode_Concepts('View');
     SetPageIndex_GridConcepts_Concepts(0);
     SetEnumTypes();
     SetPosition_DropDownDives_Concepts();
     SetPosition_cmbConcept_Concepts();
 });