$(document).ready(
 function () {
     document.body.dir = document.conceptEditorForm.dir;
     SetWrapper_Alert_Box(document.conceptEditorForm.id);
     GetBoxesHeaders_Concepts();
     SetActionMode_Concepts('Edit');
     SetCurrentConceptOrRuleIdentification();
     Fill_trvConceptExpression_Concept();
     //FillByDialogValue_trvDetails_Concept();
 });