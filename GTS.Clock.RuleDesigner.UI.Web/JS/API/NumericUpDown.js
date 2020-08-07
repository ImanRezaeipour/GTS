function hookEvent(element, eventName, callback) {
    if (typeof (element) == "string")
        element = document.getElementById(element);
    if (element == null)
        return;
    if (element.addEventListener) {
        element.addEventListener(eventName, callback, false);
    }
    else if (element.attachEvent)
        element.attachEvent("on" + eventName, callback);
}

function unhookEvent(element, eventName, callback) {
    if (typeof (element) == "string")
        element = document.getElementById(element);
    if (element == null)
        return;
    if (element.removeEventListener)
        element.removeEventListener(eventName, callback, false);
    else if (element.detachEvent)
        element.detachEvent("on" + eventName, callback);
}

function getEventTarget(e) {
    e = e ? e : window.event;
    return e.target ? e.target : e.srcElement;
}

function cancelEvent(e) {
    e = e ? e : window.event;
    if (e.stopPropagation)
        e.stopPropagation();
    if (e.preventDefault)
        e.preventDefault();
    e.cancelBubble = true;
    e.cancel = true;
    e.returnValue = false;
    return false;
}

function SpinControlAcceleration(increment, milliseconds) {
    increment = parseFloat(increment);
    if (isNaN(increment) || increment < 0)
        increment = 0;

    milliseconds = parseInt(milliseconds);
    if (isNaN(milliseconds) || milliseconds < 0)
        milliseconds = 0;

    this.GetIncrement = function ()
    { return increment; }

    this.GetMilliseconds = function ()
    { return milliseconds; }
}

function SpinControlAccelerationCollection() {
    var _array = new Array();

    this.GetCount = function ()
    { return _array.length; }

    this.GetIndex = function (index) {
        if (index < 0 || index >= _array.length)
            return null;

        return _array[index];
    }

    this.RemoveIndex = function (index) {
        if (index < 0 || index >= _array.length)
            return;

        newArray = new Array();
        for (var i = 0; i < _array.length; i++) {
            if (i == index)
                continue;
            newArray.push(_array[i]);
        }
        _array = newArray;
    }

    this.Clear = function () {
        _array = new Array();
    }

    this.Add = function (spa) {
        if (spa.constructor != SpinControlAcceleration)
            return;

        if (_array.length == 0) {
            _array.push(spa);
            return;
        }

        var newSec = spa.GetMilliseconds();
        if (newSec > _array[_array.length - 1].GetMilliseconds()) {
            _array.push(spa);
            return;
        }

        var added = false;
        var newArray = new Array();
        var indexSec;
        for (var i = 0; i < _array.length; i++) {
            if (added) {
                newArray.push(_array[i]);
            }
            else {
                indexSec = _array[i].GetMilliseconds();
                if (indexSec < newSec) {
                    newArray.push(_array[i]);
                }
                else if (indexSec == newSec) {
                    newArray.push(spa);
                    added = true;
                }
                else {
                    newArray.push(_array[i]);
                    newArray.push(spa);
                    added = true;
                }
            }
        }
        _array = newArray;
        return;
    }
}

var FirstExceptionList = ['12', '1'];
var FirstExceptionListIndex = 1;
var LastExceptionList = ['12', '1'];
var LastExceptionListIndex = 0;



function SpinControl(id) {
    var _this = this;

    var _accelerationCollection = new SpinControlAccelerationCollection();
    var _callbackArray = new Array();
    var _currentValue = 1;
    var _maximumVal = 100;
    var _minimumVal = 0;
    var _increment = 1;
    var _width = 40;
    var _id = null;

    var _running = 0;
    var _interval = -1;
    var _timeStart = 0;

    var _bodyEventHooked = false;

    var _IsFirstExceptionOccured = false;
    var _IsLastExceptionOccured = false;

    var _container = document.createElement("DIV");
    _container.className = 'spinContainer';

    var _leftEdge = document.createElement("DIV");
    _leftEdge.className = 'spinLeftRightEdge';
    _leftEdge.style.left = '0px';


    var _bottomEdge = document.createElement("DIV");
    _bottomEdge.className = 'spinTopBottomEdge';
    _bottomEdge.style.top = '0px';

    var _topEdge = document.createElement("DIV");
    _topEdge.className = 'spinTopBottomEdge';
    _topEdge.style.top = '0px';

    var _rightEdge = document.createElement("DIV");
    _rightEdge.className = 'spinLeftRightEdge';
    _rightEdge.style.right = '0px';

    var _textBox = document.createElement("INPUT");
    _textBox.type = 'text';
    _textBox.className = 'spinInput';
    _textBox.value = _currentValue;
    _textBox.id = id;
    _textBox.readOnly = true;

    var _upButton = document.createElement("DIV");
    _upButton.className = 'spinUpBtn';
    _upButton.id = "btnUp_" + id + "";

    var _downButton = document.createElement("DIV");
    _downButton.className = 'spinDownBtn';
    _downButton.id = "btnDown_" + id + "";

    /*
    * Because IE 6 and lower don't support the transparent png background 
    * mask that we use for the buttons.
    * So we use a regular old gif instead.
    * This means that, sadly, the button coloring does not work in IE6 and lower.
    */
    var canChangeBtnColors = true;
    if (document.body.filters) {
        var arVersion = navigator.appVersion.split("MSIE");
        var version = parseFloat(arVersion[1]);
        if (version < 7) {
            canChangeBtnColors = false;
            _downButton.style.backgroundImage = 'url(spin_control_buttons.gif)';
            _upButton.style.backgroundImage = 'url(spin_control_buttons.gif)';
            _downButton.style.backgroundColor = '#FFFFFF';
            _upButton.style.backgroundColor = '#FFFFFF';
        }
    }

    _container.appendChild(_leftEdge);
    _container.appendChild(_bottomEdge);
    _container.appendChild(_topEdge);
    _container.appendChild(_rightEdge);
    _container.appendChild(_textBox);
    _container.appendChild(_upButton);
    _container.appendChild(_downButton);

    function Run() {
        if (_running == 0)
            return;

        var elapsed = new Date().getTime() - _timeStart;
        var inc = _increment;

        if (_accelerationCollection.GetCount() != 0) {
            inc = 0;
            for (var i = 0; i < _accelerationCollection.GetCount(); i++) {
                if (elapsed < _accelerationCollection.GetIndex(i).GetMilliseconds())
                    break;

                inc = _accelerationCollection.GetIndex(i).GetIncrement();
            }
        }
        else if (elapsed < 600) {
            return;
        }

        DoChange(inc);
    }

    function CancelRunning() {
        _running = 0;
        if (_interval != -1) {
            clearInterval(_interval);
            _interval = -1;
        }
    }

    function DoChange(inc) {
        var newVal = _currentValue + inc * _running;
        UpdateCurrentValue(newVal);
    }

    function StartRunning(newState) {
        if (_running != 0)
            CancelRunning();

        _running = newState;

        DoChange(_increment);

        //    _timeStart = new Date().getTime();
        //    _interval = setInterval(Run, 150);
    }

    function UpdateCurrentValue(newVal) {
        if (newVal < _minimumVal)
            newVal = _minimumVal;
        if (newVal > _maximumVal)
            newVal = _maximumVal;

        newVal = Math.round(1000 * newVal) / 1000;

        _textBox.value = newVal;
        if (newVal == _currentValue)
            return;


        _currentValue = newVal;

        for (var i = 0; i < _callbackArray.length; i++)
            _callbackArray[i](_this, _currentValue);
    }

    function UpPress(e) {
        if (isNaN(CheckValidation_Nud(_this, 'Up'))) {
            if (!_this.GetFirstExceptionOccured() && !_this.GetLastExceptionOccured()) {
                _upButton.className = 'spinUpBtnPress';
                _downButton.className = 'spinDownBtn';
                StartRunning(1);
                _textBox.focus();
            }
            else
                Nud_OnExceptionOccured(_this, 1);
            ChnageAnotherPair_onNudChange_Nud(_this, 'Up');
            return cancelEvent(e);
        }
        else
            cancelEvent(e);
    }

    function DownPress(e) {
        if (isNaN(CheckValidation_Nud(_this, 'Down'))) {
            if (!_this.GetFirstExceptionOccured() && !_this.GetLastExceptionOccured()) {
                _upButton.className = 'spinUpBtn';
                _downButton.className = 'spinDownBtnPress';
                StartRunning(-1);
                _textBox.focus();
            }
            else
                Nud_OnExceptionOccured(_this, -1);
            ChnageAnotherPair_onNudChange_Nud(_this, 'Down');
            return cancelEvent(e);
        }
        else
            cancelEvent(e);
    }




    function UpHover(e) {
        if (!_bodyEventHooked)
            hookEvent(document.body, 'mouseover', ClearBtns);

        _upButton.className = 'spinUpBtnHover';
        _downButton.className = 'spinDownBtn';
        CancelRunning();
        return cancelEvent(e);
    }

    function DownHover(e) {
        if (!_bodyEventHooked)
            hookEvent(document.body, 'mouseover', ClearBtns);

        _upButton.className = 'spinUpBtn';
        _downButton.className = 'spinDownBtnHover';
        CancelRunning();
        return cancelEvent(e);
    }


    function ClearBtns(e) {
        var target = getEventTarget(e);
        if (target == _upButton || target == _downButton)
            return;
        _upButton.className = 'spinUpBtn';
        _downButton.className = 'spinDownBtn';
        CancelRunning();

        if (_bodyEventHooked) {
            unhookEvent(document.body, 'mouseover', ClearBtns);
            _bodyEventHooked = false;
        }
        return cancelEvent(e);
    }

    function BoxChange() {
        var val = parseFloat(_textBox.value);
        if (isNaN(val))
            val = _currentValue;

        if (!CheckValidation_Nud_onBoxChange())
            return;

        if (!_this.GetFirstExceptionOccured() && !_this.GetLastExceptionOccured())
            UpdateCurrentValue(val);
        else {
            var NudVal = null;
            if (_this.GetFirstExceptionOccured())
                NudVal = FirstExceptionList[FirstExceptionListIndex];
            if (_this.GetLastExceptionOccured())
                NudVal = LastExceptionList[LastExceptionListIndex];
            _textBox.value = NudVal;
        }
    }


    function CheckValidation_Nud_onBoxChange() {
        switch (_textBox.id.substring(3, 4)) {
            case 'F':
                switch (_textBox.id.substring(4, 5)) {
                    case 'd':
                        var NudFmID_Box = _textBox.id.substring(0, 4) + 'm' + _textBox.id.substring(5, _textBox.id.length)
                        if (parseInt(document.getElementById(NudFmID_Box).value) == parseInt(document.getElementById(NudFmID_Box.substring(0, 3) + 'T' + NudFmID_Box.substring(4, NudFmID_Box.length)).value)) {
                            if (parseInt(_textBox.value) >= parseInt(document.getElementById(_textBox.id.substring(0, 3) + 'T' + _textBox.id.substring(4, _textBox.id.length)).value))
                                _textBox.value = _currentValue;
                            return false;
                        }
                        break;
                }
                break;
            case 'T':
                switch (_textBox.id.substring(4, 5)) {
                    case 'd':
                        if (parseInt(document.getElementById(_textBox.id.substring(0, 3) + 'F' + _textBox.id.substring(4, _textBox.id.length)).value) >= parseInt(_textBox.value))
                            _textBox.value = _currentValue;
                        return false;
                        break;
                    case 'm':
                        if (parseInt(_textBox.value) == parseInt(document.getElementById(_textBox.id.substring(0, 3) + 'F' + _textBox.id.substring(4, _textBox.id.length)).value)) {
                            var NudTdID_Box = _textBox.id.substring(0, 4) + 'd' + _textBox.id.substring(5, _textBox.id.length);
                            if (parseInt(document.getElementById(NudTdID_Box).value) >= parseInt(document.getElementById(NudTdID_Box.substring(0, 3) + 'F' + NudTdID_Box.substring(4, NudTdID_Box.length)).value))
                                return false;
                        }
                        break;
                }
                break;
        }

    }



    function MouseWheel(e) {
        e = e ? e : window.event;
        var movement = e.detail ? e.detail / -3 : e.wheelDelta / 120;
        UpdateCurrentValue(_currentValue + _increment * movement);
        return cancelEvent(e);
    }

    function TextFocused(e) {
        hookEvent(window, 'DOMMouseScroll', MouseWheel);
        hookEvent(document, 'mousewheel', MouseWheel);
        return cancelEvent(e);
    }

    function TextBlur(e) {
        unhookEvent(window, 'DOMMouseScroll', MouseWheel);
        unhookEvent(document, 'mousewheel', MouseWheel);
        return cancelEvent(e);
    }

    this.StartListening = function () {
        hookEvent(_upButton, 'mousedown', UpPress);
        hookEvent(_upButton, 'mouseup', UpHover);
        hookEvent(_upButton, 'mouseover', UpHover);

        hookEvent(_downButton, 'mousedown', DownPress);
        hookEvent(_downButton, 'mouseup', DownHover);
        hookEvent(_downButton, 'mouseover', DownHover);

        hookEvent(_textBox, 'change', BoxChange);
        hookEvent(_textBox, 'focus', TextFocused);
        hookEvent(_textBox, 'blur', TextBlur);
    }

    this.StopListening = function () {
        unhookEvent(_upButton, 'mousedown', UpPress);
        unhookEvent(_upButton, 'mouseup', UpHover);
        unhookEvent(_upButton, 'mouseover', UpHover);

        unhookEvent(_downButton, 'mousedown', DownPress);
        unhookEvent(_downButton, 'mouseup', DownHover);
        unhookEvent(_downButton, 'mouseover', DownHover);

        unhookEvent(_textBox, 'change', BoxChange);
        unhookEvent(_textBox, 'focus', TextFocused);
        unhookEvent(_textBox, 'blur', TextBlur);

        if (_bodyEventHooked) {
            unhookEvent(document.body, 'mouseover', ClearBtns);
            _bodyEventHooked = false;
        }
    }

    this.SetMaxValue = function (value) {
        value = parseFloat(value);
        if (isNaN(value))
            value = 1;
        _maximumVal = value;

        UpdateCurrentValue(_currentValue);
    }

    this.SetMinValue = function (value) {
        value = parseFloat(value);
        if (isNaN(value))
            value = 0;
        _minimumVal = value;

        UpdateCurrentValue(_currentValue);
    }

    this.SetCurrentValue = function (value) {
        value = parseFloat(value);
        if (isNaN(value))
            value = 0;

        UpdateCurrentValue(value);
    }

    this.SetWidth = function (value) {
        value = parseInt(value);
        if (isNaN(value) || value < 25)
            value = 25;

        _width = value;

        _container.style.width = _width + 'px';
        _bottomEdge.style.width = (_width - 1) + 'px';
        _topEdge.style.width = (_width - 1) + 'px';
        _textBox.style.width = (_width - 20) + 'px';
    }

    this.SetIncrement = function (value) {
        value = parseFloat(value);
        if (isNaN(value))
            value = 0;
        if (value < 0)
            value = -value;

        _increment = value;
    }

    this.SetBackgroundColor = function (color) {
        _container.style.backgroundColor = color;
        _textBox.style.backgroundColor = color;
    }

    this.SetButtonColor = function (color) {
        if (!canChangeBtnColors)
            return;

        _upButton.style.backgroundColor = color;
        _downButton.style.backgroundColor = color;
    }

    this.SetFontColor = function (color) {
        _textBox.style.color = color;
    }

    this.SetBorderColor = function (color) {
        _topEdge.style.backgroundColor = color;
        _bottomEdge.style.backgroundColor = color;
        _leftEdge.style.backgroundColor = color;
        _rightEdge.style.backgroundColor = color;
    }

    this.AttachValueChangedListener = function (listener) {
        for (var i = 0; i < _callbackArray.length; i++)
            if (_callbackArray[i] == listener)
                return;

        _callbackArray.push(listener);
    }

    this.DetachValueChangedListener = function (listener) {
        newArray = new Array();
        for (var i = 0; i < _callbackArray.length; i++)
            if (_callbackArray[i] != listener)
                newArray.push(_callbackArray[i]);

        _callbackArray = newArray;
    }

    this.GetContainer = function ()
    { return _container; }

    this.GetCurrentValue = function ()
    { return _currentValue; }

    this.GetMaxValue = function ()
    { return _maximumVal; }

    this.GetMinValue = function ()
    { return _minimumVal; }

    this.GetWidth = function ()
    { return _width; }

    this.GetIncrement = function ()
    { return _increment; }

    this.GetAccelerationCollection = function ()
    { return _accelerationCollection; }

    _this.SetWidth(_width);

    this.GetFirstExceptionOccured = function () {
        return _IsFirstExceptionOccured;
    }

    this.GetLastExceptionOccured = function () {
        return _IsLastExceptionOccured;
    }

    this.SetFirstExceptionOccured = function (IsOccured) {
        _IsFirstExceptionOccured = IsOccured;
    }

    this.SetLastExceptionOccured = function (IsOccured) {
        _IsLastExceptionOccured = IsOccured;
    }

    this.SetAlternativeNudID = function () {
        _alternativeNudID = _textBox.id.substring(0, 4) + 'm' + _textBox.id.substring(5, _textBox.id.length);
    }

    this.GetTextBox = function () {
        return _textBox;
    }

    this.SetTextBoxValue = function (value) {
        _textBox.value = value;
    }

}

function CheckValidation_Nud(_this, action) {
    if (!CheckPairNudsDifferenceValidation_Nud(_this, action))
        return false;
    if (!ChangeAlternativeNud_onNudCahnge(_this, action))
        return false;
    if (!CheckAnotherPairNudsDifferenceValidation_Nud(_this, action))
        return false;
}

function CheckAnotherPairNudsDifferenceValidation_Nud(_this, action) {
    var textBoxID = _this.GetTextBox().id
    if (textBoxID.substring(3, 4) == 'T' && textBoxID.substring(4, 5) == 'd' && action == 'Up' && parseInt(textBoxID.substring(5, textBoxID.length)) != 12) {
        if (parseInt(document.getElementById('NudFm' + (parseInt(textBoxID.substring(5, textBoxID.length)) + 1)).value) == (parseInt(textBoxID.substring(5, textBoxID.length)) + 1) && parseInt(document.getElementById('NudFd' + (parseInt(textBoxID.substring(5, textBoxID.length)) + 1)).value) == parseInt(MonthsDayCol_CalculationRange[parseInt(document.getElementById('NudFm' + (parseInt(textBoxID.substring(5, textBoxID.length)) + 1)).value)]))
            return false;
    }
    return true;
}

function ChangeAlternativeNud_onNudCahnge(_this, action) {
    var alternativeNudID = null;
    var textBoxID = _this.GetTextBox().id;
    var textBoxValue = _this.GetTextBox().value;
    var currentValue = _this.GetCurrentValue();
    if (textBoxID.substring(4, 5) == 'm') {
        if (CheckNudValExtermomEquality_Nud(_this, action))
            return false;
        if (!CheckCurrentNudValExistingInPiars_Nud(_this))
            return false;
        alternativeNudID = textBoxID.substring(0, 4) + 'd' + textBoxID.substring(5, textBoxID.length);
        if (textBoxID.substring(3, 4) == 'T') {
            if (parseInt(textBoxValue) + GetNudActionOperator_Nud(action) <= parseInt(document.getElementById(textBoxID.substring(0, 3) + GetAlternateNudCreatorMeter_Nud(_this) + textBoxID.substring(4, textBoxID.length)).value)) {
                if (parseInt(document.getElementById(alternativeNudID.substring(0, 3) + GetAlternateNudCreatorMeter_Nud(_this) + alternativeNudID.substring(4, alternativeNudID.length)).value) < parseInt(MonthsDayCol_CalculationRange[parseInt(document.getElementById(textBoxID.substring(0, 3) + GetAlternateNudCreatorMeter_Nud(_this) + textBoxID.substring(4, textBoxID.length)).value) - 1]))
                    document.getElementById(alternativeNudID).value = parseInt(document.getElementById(alternativeNudID.substring(0, 3) + GetAlternateNudCreatorMeter_Nud(_this) + alternativeNudID.substring(4, alternativeNudID.length)).value) + 1;
                else
                    return false;
            }
            else
                document.getElementById(alternativeNudID).value = 1;
        }
        else
            document.getElementById(alternativeNudID).value = 1;
    }
    else {
        alternativeNudID = textBoxID.substring(0, 4) + 'm' + textBoxID.substring(5, textBoxID.length);
        if (currentValue != document.getElementById(textBoxID).value)
            _this.SetCurrentValue(textBoxValue);
        _this.SetMaxValue(MonthsDayCol_CalculationRange[parseInt(document.getElementById(alternativeNudID).value) - 1]);
    }
    return true;
}

function CheckCurrentNudValExistingInPiars_Nud(_this) {
    var textBoxID = _this.GetTextBox().id;
    var CurrentNudVal_Nud = textBoxID.substring(5, textBoxID.length);
    if (document.getElementById(textBoxID.substring(0, 3) + GetAlternateNudCreatorMeter_Nud(_this) + textBoxID.substring(4, textBoxID.length)).value != CurrentNudVal_Nud)
        return false;
    else
        return true;
}

function CheckPairNudsDifferenceValidation_Nud(_this, action) {
    var PairsObj = GetNudPairs_Nud(_this);
    var textBoxID = _this.GetTextBox().id;
    var textBoxValue = _this.GetTextBox().value;
    if (document.getElementById(PairsObj.NudFmID_Nud).value == document.getElementById(PairsObj.NudTmID_Nud).value) {
        if (parseInt(textBoxValue) + GetNudActionOperator_Nud(action) == parseInt(document.getElementById(textBoxID.substring(0, 3) + GetAlternateNudCreatorMeter_Nud(_this) + textBoxID.substring(4, textBoxID.length)).value))
            return false;
    }
    return true;
}

function ChnageAnotherPair_onNudChange_Nud(_this, action) {
    var textBoxID = _this.GetTextBox().id;
    var textBoxValue = _this.GetTextBox().value;
    var CurrentNudIndex_Nud = parseInt(textBoxID.substring(5, textBoxID.length));
    var CurrentNudCreator_Nud = textBoxID.substring(4, 5);
    var CurrentNudCreatorMeter_Nud = textBoxID.substring(3, 4);
    switch (CurrentNudCreatorMeter_Nud) {
        case 'F':
            if (CurrentNudIndex_Nud != 1) {
                switch (CurrentNudCreator_Nud) {
                    case 'd':
                        if (parseInt(textBoxValue) != 1) {
                            if (parseInt(document.getElementById("NudFm" + (CurrentNudIndex_Nud - 1) + "").value) != CurrentNudIndex_Nud - 1) {
                                _this.SetTextBoxValue("" + (parseInt(textBoxValue) - GetNudActionOperator_Nud(action)) + "");
                                return;
                            }
                            if (parseInt(document.getElementById("NudFm" + CurrentNudIndex_Nud + "").value) == CurrentNudIndex_Nud - 1 && parseInt(textBoxValue) == parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value) && parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value) - 1 == parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud - 1) + "").value) && action == 'Down') {
                                _this.SetTextBoxValue("" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value) + 1) + "");
                                return;
                            }
                            if (parseInt(document.getElementById("NudFm" + CurrentNudIndex_Nud + "").value) == CurrentNudIndex_Nud)
                                document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + CurrentNudIndex_Nud + "";
                            else
                                document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (CurrentNudIndex_Nud - 1) + "";
                            document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value = "" + (parseInt(textBoxValue) - 1) + "";
                        }
                        else {
                            if (document.getElementById("NudFm" + CurrentNudIndex_Nud + "").value == document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value) {
                                _this.SetTextBoxValue("" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value) + 1) + "");
                                return;
                            }
                            document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (CurrentNudIndex_Nud - 1) + "";
                            document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value = MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 2];
                        }
                        break;
                    case 'm':
                        if (parseInt(textBoxValue) == CurrentNudIndex_Nud - 1) {
                            if (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud) + "").value) >= parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud - 1) + "").value) + 2) {
                                document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (CurrentNudIndex_Nud - 1) + "";
                                document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value = parseInt(document.getElementById("NudFd" + CurrentNudIndex_Nud + "")) - 1;
                            }
                            else {
                                if (document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value != MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 2]) {
                                    if (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud - 1) + "").value) + 2 > MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 2]) {
                                        document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value = "1";
                                        document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value = MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 2];
                                        document.getElementById("NudFm" + CurrentNudIndex_Nud + "").value = "" + CurrentNudIndex_Nud + "";
                                        document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (CurrentNudIndex_Nud - 1) + "";
                                    }
                                    else {
                                        document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value = "" + (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud - 1) + "").value) + 2) + "";
                                        document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (CurrentNudIndex_Nud - 1) + "";
                                        document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value = "" + (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud - 1) + "").value) + 1) + "";
                                    }
                                }
                                else {
                                    _this.SetTextBoxValue("" + CurrentNudIndex_Nud + "");
                                    document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value = "1";
                                }
                            }
                        }
                        else {
                            document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + CurrentNudIndex_Nud + "";
                            document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value = parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value) + 1;
                            if (parseInt(textBoxValue) == CurrentNudIndex_Nud && parseInt(document.getElementById("NudTm" + CurrentNudIndex_Nud + "").value) == CurrentNudIndex_Nud && parseInt(document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value) >= parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value)) {
                                _this.SetTextBoxValue("" + (parseInt(textBoxValue) - GetNudActionOperator_Nud(action)) + "");
                                document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value = "" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value) + 1) + "";
                                document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (CurrentNudIndex_Nud - GetNudActionOperator_Nud(action)) + "";
                                return;
                            }
                            if (document.getElementById("NudFm" + (CurrentNudIndex_Nud - 1) + "").value != CurrentNudIndex_Nud - 1 && document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value != CurrentNudIndex_Nud - 1) {
                                document.getElementById("NudTm" + (CurrentNudIndex_Nud - 1) + "").value = "" + (parseInt(textBoxValue) - GetNudActionOperator_Nud(action)) + "";
                                _this.SetTextBoxValue("" + (parseInt(textBoxValue) - GetNudActionOperator_Nud(action)) + "");
                                return;
                            }
                            if (document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value == document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) {
                                document.getElementById("NudTd" + (CurrentNudIndex_Nud - 1) + "").value = parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) - 2;
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud) + "").value = parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) - 1;
                            }
                        }
                        break;
                }
            }
            break;
        case 'T':
            if (CurrentNudIndex_Nud != 12) {
                switch (CurrentNudCreator_Nud) {
                    case 'd':
                        if (parseInt(document.getElementById("NudTm" + CurrentNudIndex_Nud + "").value) == CurrentNudIndex_Nud) {
                            if (parseInt(textBoxValue) < MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 1]) {
                                if (parseInt(document.getElementById("NudTm" + (CurrentNudIndex_Nud + 1) + "").value) == CurrentNudIndex_Nud + 2) {
                                    _this.SetTextBoxValue("" + parseInt(textBoxValue) - GetNudActionOperator_Nud(action) + "");
                                    return;
                                }
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = parseInt(textBoxValue) + 1;
                                if (parseInt(document.getElementById("NudTm" + CurrentNudIndex_Nud + "").value) == CurrentNudIndex_Nud) {
                                    document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + CurrentNudIndex_Nud + "";
                                }
                                else
                                    document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                            }
                            else {
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = "1";
                                document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                            }
                        }
                        else {
                            if (parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) <= parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 2 || parseInt(document.getElementById("NudTm" + (CurrentNudIndex_Nud + 1) + "").value) == CurrentNudIndex_Nud + 2) {
                                document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = parseInt(textBoxValue) + 1;
                            }
                            else {
                                _this.SetTextBoxValue("" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 2) + "");
                                document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = "" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 1) + "";
                            }
                        }
                        break;
                    case 'm':
                        if (parseInt(textBoxValue) == CurrentNudIndex_Nud) {
                            if (parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) < MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 1]) {
                                if (parseInt(document.getElementById("NudTm" + (CurrentNudIndex_Nud + 1) + "").value) == CurrentNudIndex_Nud + 2 || (CurrentNudIndex_Nud != 1 && document.getElementById("NudTm" + (CurrentNudIndex_Nud + 1) + "").value == '1')) {
                                    _this.SetTextBoxValue("" + (parseInt(textBoxValue) - GetNudActionOperator_Nud(action)) + "");
                                    document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value = "" + (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value) - 1) + "";
                                    return;
                                }
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) + 1;
                                document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + CurrentNudIndex_Nud + "";
                            }
                            else {
                                if (textBoxValue == document.getElementById("NudFm" + (CurrentNudIndex_Nud) + "").value && document.getElementById("NudFd" + CurrentNudIndex_Nud + "").value == MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 1]) {
                                    {
                                        _this.SetTextBoxValue(document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value);
                                        document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value = "" + (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value) - 1) + "";
                                        return;
                                    }
                                }
                                document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value = MonthsDayCol_CalculationRange[CurrentNudIndex_Nud - 1];
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = "1";
                                document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                            }
                        }
                        else {
                            if (parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) <= parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 2 || parseInt(document.getElementById("NudTm" + (CurrentNudIndex_Nud + 1) + "").value) == CurrentNudIndex_Nud + 2) {
                                document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = parseInt(document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value) + 1;
                                document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                            }
                            else {
                                if (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 2 > 0) {
                                    document.getElementById("NudTd" + CurrentNudIndex_Nud + "").value = "" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 2) + "";
                                    document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value = "" + (parseInt(document.getElementById("NudTd" + (CurrentNudIndex_Nud + 1) + "").value) - 1) + "";
                                    document.getElementById("NudFm" + (CurrentNudIndex_Nud + 1) + "").value = "" + (CurrentNudIndex_Nud + 1) + "";
                                }
                                else {
                                    _this.SetTextBoxValue("" + CurrentNudIndex_Nud + "");
                                    document.getElementById("NudTd" + (CurrentNudIndex_Nud) + "").value = "" + (parseInt(document.getElementById("NudFd" + (CurrentNudIndex_Nud + 1) + "").value) - 1) + "";
                                }
                            }
                        }
                        break;
                }
            }
            break;
    }
}

function CheckNudValExtermomEquality_Nud(_this, action) {
    var IsNudValEqualExtermom = false;
    var textBoxValue = _this.GetTextBox().value;
    switch (action) {
        case 'Up':
            if (textBoxValue == _this.GetMaxValue())
                IsNudValEqualExtermom = true;
            break;
        case 'Down':
            if (textBoxValue == _this.GetMinValue())
                IsNudValEqualExtermom = true;
            break;
    }
    return IsNudValEqualExtermom;
}

function GetNudActionOperator_Nud(action) {
    switch (action) {
        case 'Up':
            return 1;
            break;
        case 'Down':
            return -1;
            break;
    }
}

function GetAlternateNudCreatorMeter_Nud(_this) {
    var textBoxID = _this.GetTextBox().id;
    switch (textBoxID.substring(3, 4)) {
        case 'F':
            return 'T';
            break;
        case 'T':
            return 'F';
            break;
    }
}

function GetAlternativeNudCreator_Nud(_this) {
    var textBoxID = _this.GetTextBox().id;
    switch (textBoxID.substring(4, 5)) {
        case 'm':
            return 'd';
            break;
        case 'd':
            return 'm';
            break;
    }
}

function GetNudPairs_Nud(_this) {
    var PairsObj = new Object();
    var NudFmID_Nud = null;
    var NudFdID_Nud = null;
    var NudTmID_Nud = null;
    var NudTdID_Nud = null;
    var NudCreator_Nud = null;
    var NudMeter_Nud = null;
    var textBoxID = _this.GetTextBox().id;

    NudMeter_Nud = textBoxID.substring(3, 4);
    NudCreator_Nud = textBoxID.substring(4, 5);
    switch (NudCreator_Nud) {
        case 'm':
            switch (NudMeter_Nud) {
                case 'F':
                    NudFmID_Nud = textBoxID;
                    NudFdID_Nud = textBoxID.substring(0, 4) + 'd' + textBoxID.substring(5, textBoxID.length);
                    NudTmID_Nud = textBoxID.substring(0, 3) + 'T' + textBoxID.substring(4, textBoxID.length);
                    NudTdID_Nud = NudTmID_Nud.substring(0, 4) + 'd' + NudTmID_Nud.substring(5, NudTmID_Nud.length);
                    break;
                case 'T':
                    NudFmID_Nud = textBoxID.substring(0, 3) + 'F' + textBoxID.substring(4, textBoxID.length);
                    NudFdID_Nud = NudFmID_Nud.substring(0, 4) + 'd' + NudFmID_Nud.substring(5, NudFmID_Nud.length);
                    NudTmID_Nud = textBoxID;
                    NudTdID_Nud = textBoxID.substring(0, 4) + 'd' + textBoxID.substring(5, textBoxID.length);
                    break;
            }
            break;
        case 'd':
            switch (NudMeter_Nud) {
                case 'F':
                    NudFmID_Nud = textBoxID.substring(0, 4) + 'm' + textBoxID.substring(5, textBoxID.length);
                    NudFdID_Nud = textBoxID;
                    NudTmID_Nud = NudFmID_Nud.substring(0, 3) + 'T' + NudFmID_Nud.substring(4, NudFmID_Nud.length);
                    NudTdID_Nud = NudTmID_Nud.substring(0, 4) + 'd' + NudTmID_Nud.substring(5, NudTmID_Nud.length);
                    break;
                case 'T':
                    NudFdID_Nud = textBoxID.substring(0, 3) + 'F' + textBoxID.substring(4, textBoxID.length);
                    NudFmID_Nud = NudFdID_Nud.substring(0, 4) + 'm' + NudFdID_Nud.substring(5, NudFdID_Nud.length);
                    NudTmID_Nud = NudFmID_Nud.substring(0, 3) + 'T' + NudFmID_Nud.substring(4, NudFmID_Nud.length);
                    NudTdID_Nud = textBoxID;
                    break;
            }
            break;
    }
    PairsObj.NudFdID_Nud = NudFdID_Nud;
    PairsObj.NudFmID_Nud = NudFmID_Nud;
    PairsObj.NudTdID_Nud = NudTdID_Nud;
    PairsObj.NudTmID_Nud = NudTmID_Nud;
    return PairsObj;
}

function Nud_OnExceptionOccured(_this, operator) {
    var CurretnExeptionList = null;
    var CurrentExeptionListIndex = null;
    if (_this.GetFirstExceptionOccured()) {
        CurretnExeptionList = FirstExceptionList;
        CurrentExeptionListIndex = FirstExceptionListIndex;
    }
    if (_this.GetLastExceptionOccured()) {
        CurretnExeptionList = LastExceptionList;
        CurrentExeptionListIndex = LastExceptionListIndex;
    }
    ChangeNud_OnExceptionOccured(_this, operator, CurretnExeptionList, CurrentExeptionListIndex);
}

function ChangeNud_OnExceptionOccured(_this, operator, list, index) {
    if ((operator > 0 && index < 1) || (operator < 0 && index > 0)) {
        var textBoxValue = _this.GetTextBox().value;
        var currentValue = _this.GetCurrentValue();
        if (_this.GetFirstExceptionOccured())
            FirstExceptionListIndex = index + operator;
        if (_this.GetLastExceptionOccured())
            LastExceptionListIndex = index + operator;
        _this.SetCurrentValue(list[index + operator]);
        _this.SetTextBoxValue(list[index + operator]);
    }
}



