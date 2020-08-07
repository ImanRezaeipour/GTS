$(document).ready(
 function () {
     document.body.dir = document.ExpressionsManagement.dir;
     SetWrapper_Alert_Box(document.ExpressionsManagement.id);
     GetBoxesHeader_Expressions();
     SetActionMode_Expressions('View');
     Fill_GridExpressions_Expressions(0);
     CallBack_trvExpressions_Expressions.callback();
 });