var TIMER = 0;
var SPEED = 1000;
var WRAPPER = '';
var Direction = null;
//var OffsetWidth = null;

// شناسه مربوط به المنتی که قرار است باکس اعلان نمایش داده شود را مشخص میکند میکند
function SetWrapper_Alert_Box(content) {
    WRAPPER = content;
}

//function SetDialogMaskOffsetWidth_Alert_Box(offsetWidth) {
//    OffsetWidth = offsetWidth;
//}

// جهت باکس اعلان را مشخص می کند
function SetDirection_Alert_Box(dir) {
    Direction = dir;
}

function pageWidth() {
  return window.innerWidth != null ? window.innerWidth : document.documentElement && document.documentElement.clientWidth ? document.documentElement.clientWidth : document.body != null ? document.body.clientWidth : null;
}

function pageHeight() {
  return window.innerHeight != null? window.innerHeight : document.documentElement && document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body != null? document.body.clientHeight : null;
}

function topPosition() {
  return typeof window.pageYOffset != 'undefined' ? window.pageYOffset : document.documentElement && document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop ? document.body.scrollTop : 0;
}

function leftPosition() {
  return typeof window.pageXOffset != 'undefined' ? window.pageXOffset : document.documentElement && document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft ? document.body.scrollLeft : 0;
}

function showDialog(title,message,type,autohide,offsetWidth) {
  if(!type) {
    type = 'error';
  }
  var dialog;
  var dialogheader;
  var dialogclose;
  var dialogtitle;
  var dialogcontent;
  var dialogmask;
  if (!document.getElementById('dialog')) {
    dialog = document.createElement('div');
    dialog.id = 'dialog';
    dialogheader = document.createElement('div');
    dialogheader.id = 'dialog-header';
    dialogtitle = document.createElement('div');
    dialogtitle.id = 'dialog-title';
    dialogclose = document.createElement('div');
    dialogclose.id = 'dialog-close';
    dialogcontent = document.createElement('div');
    dialogcontent.id = 'dialog-content';
    dialogcontent.style.overflow = 'auto';
    dialogmask = document.createElement('div');
    dialogmask.id = 'dialog-mask';
    if (offsetWidth != null)
        dialogmask.style.width = offsetWidth;
    document.body.appendChild(dialogmask);
    document.body.appendChild(dialog);
    dialog.appendChild(dialogheader);
    dialogheader.appendChild(dialogtitle);
    dialogheader.appendChild(dialogclose);
    dialog.appendChild(dialogcontent);;
    dialogclose.setAttribute('onclick','hideDialog()');
    dialogclose.onclick = hideDialog;
  } else {
    dialog = document.getElementById('dialog');
    dialogheader = document.getElementById('dialog-header');
    dialogtitle = document.getElementById('dialog-title');
    dialogclose = document.getElementById('dialog-close');
    dialogcontent = document.getElementById('dialog-content');
    dialogmask = document.getElementById('dialog-mask');
    dialogmask.style.visibility = "visible";
    if (offsetWidth != null)
        dialogmask.style.width = offsetWidth;
    dialog.style.visibility = "visible";
  }
  dialog.style.zIndex = 250000000000000;
  if (Direction != null)
      document.getElementById('dialog').dir = Direction;  
  dialog.style.opacity = .00;
  dialog.style.filter = 'alpha(opacity=0)';
  dialog.alpha = 0;
  var width = pageWidth();
  var height = pageHeight();
  var left = leftPosition();
  var top = topPosition();
  var dialogwidth = dialog.offsetWidth;
  var dialogheight = dialog.offsetHeight;
  var topposition = top + (height / 3) - (dialogheight / 2);
  var leftposition = left + (width / 2) - (dialogwidth / 2);
  if (topposition >= 0)
      dialog.style.top = topposition + "px";
  else {
      dialog.style.top = "0px";
      dialogcontent.style.height = '100px';
  }
  dialog.style.left = leftposition + "px";
  dialogheader.className = type + "header";
  dialogtitle.innerHTML = title;
  dialogcontent.className = type;
  dialogcontent.innerHTML = message;
  var content = document.getElementById(WRAPPER);
  dialogmask.style.height = content.offsetHeight + 'px';
  dialog.timer = setInterval("fadeDialog(1)", TIMER);
  if(autohide) {
    dialogclose.style.visibility = "hidden";
    window.setTimeout("hideDialog()", (autohide * 1000));
  } else {
    dialogclose.style.visibility = "visible";
  }
}

function hideDialog() {
  var dialog = document.getElementById('dialog');
//  clearInterval(dialog.timer);
//  dialog.timer = setInterval("fadeDialog(0)", TIMER);
  dialog.style.visibility = "hidden";
  document.getElementById('dialog-mask').style.visibility = "hidden";
  document.getElementById('dialog-close').style.visibility = "hidden";
}

function fadeDialog(flag) {
  if(flag == null) {
    flag = 1;
  }
  var dialog = document.getElementById('dialog');
  var value;
  if(flag == 1) {
    value = dialog.alpha + SPEED;
  } else {
    value = dialog.alpha - SPEED;
  }
  dialog.alpha = value;
  dialog.style.opacity = (value / 100);
  dialog.style.filter = 'alpha(opacity=' + value + ')';
  if(value >= 99) {
    clearInterval(dialog.timer);
    dialog.timer = null;
  } else if(value <= 1) {
    dialog.style.visibility = "hidden";
    document.getElementById('dialog-mask').style.visibility = "hidden";
    clearInterval(dialog.timer);
  }
}