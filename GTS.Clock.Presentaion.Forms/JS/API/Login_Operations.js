
function initControls_Login() {
    try {
        if (document.getElementById('theLogincontrol_UserName') != null) {
            document.getElementById('theLogincontrol_UserName').focus();
            document.getElementById('theLogincontrol_UserName').onclick = function () {
                this.select();
            };
        }
        if (document.getElementById('theLogincontrol_UserName') != null) {
            document.getElementById('theLogincontrol_UserName').onfocus = function () {
                this.select();
            };
        }
        if (document.getElementById('theLogincontrol_Password') != null) {
            document.getElementById('theLogincontrol_Password').onclick = function () {
                this.select();
            };
        }
        if (document.getElementById('theLogincontrol_Password') != null) {
            document.getElementById('theLogincontrol_Password').onfocus = function () {
                this.select();
            };
        }
    } catch (e) {

    }
}

function ShowKeyboard() {
    VKI_show(document.getElementById('theLogincontrol_Password'));
}

function CheckReferer() {
    if (this.location.search != '' && this.location.search.indexOf('.aspx') >= 0 && this.location.search.indexOf('MainPage.aspx') < 0) {
        if (parent.window == this.window)
            return;
        var parentWindow = parent.window;
        while (true) {
            if (parentWindow.document.getElementById('MainForm') != null) {
                parentWindow.location = 'Login.aspx';
                break;
            }
            else
                parentWindow = parentWindow.parent;
        };
    }    
}
