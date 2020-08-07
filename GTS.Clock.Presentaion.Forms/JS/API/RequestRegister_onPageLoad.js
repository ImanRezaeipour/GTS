
$(document).ready
        (
            function () {
                document.body.dir = document.RequestRegisterForm.dir;
                ChangeHideElementsState_RequestRegister(false, false, true);
                GetBoxesHeaders_RequestRegister();
                SetWrapper_Alert_Box(document.RequestRegisterForm.id);
                SetDirection_Alert_Box(parent.parent.document.MainForm.dir);
                //ViewCurrentLangCalendars_RequestRegister();
                initTimePickers_RequestRegister('Load');
                ResetCalendars_RequestRegister();
                GetDialogRequestRegisterObjVal_RequestRegister();
                //CustomizeRequestRegister_RequestRegister();
                ChangeControlDirection_RequestRegister('All');
            }
        );
