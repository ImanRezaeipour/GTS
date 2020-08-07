
function initControls_Login() {
    document.getElementById('theLogincontrol_UserName').focus();
    document.getElementById('theLogincontrol_UserName').onclick = function () {
        this.select();
    }
    document.getElementById('theLogincontrol_UserName').onfocus = function () {
        this.select();
    }
    document.getElementById('theLogincontrol_Password').onclick = function () {
        this.select();
    }
    document.getElementById('theLogincontrol_Password').onfocus = function () {
        this.select();
    }
}

function ShowKeyboard() {
    VKI_show(document.getElementById('theLogincontrol_Password'));
}
