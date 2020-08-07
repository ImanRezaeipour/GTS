
$(document).ready
        (
            function () {
                document.body.dir = document.SystemMessageForm.dir;
                SetWrapper_Alert_Box(document.SystemMessageForm.id);
                GetBoxesHeaders_SystemMessage();
                GetMessage_SystemMessage();
            }
        );