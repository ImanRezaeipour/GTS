
$(document).ready(
 function () {
     document.body.dir = document.RulesManagementForm.dir;
     SetWrapper_Alert_Box(document.RulesManagementForm.id);
     GetBoxesHeaders_Rules();
     SetActionModeRule_Rules('View');
     SetPageIndex_GridRules_Rules(0);
     SetEnumTypes();

     SetActionModeRuleParameter_Rules('View');

 });
