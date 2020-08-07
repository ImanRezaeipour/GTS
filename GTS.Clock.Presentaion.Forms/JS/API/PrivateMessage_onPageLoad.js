$(document).ready
        (
            function () {

                parent.DialogLoading.Close();
                document.body.dir = document.PrivateMessageForm.dir;
                SetWrapper_Alert_Box(document.PrivateMessageForm.id);
                GetBoxesHeadersReceive_PrivateMessage();
                GetBoxesHeadersSend_PrivateMessage();
                SetActionMode_PrivateMessage('View');
                SetPageIndex_GridPrivateMessageReceive_PrivateMessage(0);
                SetPageIndex_GridPrivateMessageSend_PrivateMessage(0);
              //SelectTabDefault();

            }
        );