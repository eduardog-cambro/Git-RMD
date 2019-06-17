//show proper entry form for machine
function showForm(path, type, id, action) {
    var url = path + 'runData' + type + '.aspx?id=' + id + '&action=' + action;
    popWindow(url, '_blank', 'resizable,scrollbars,width=1200,height=1000');
    return false;
}

//open new window
function popWindow(url, name, options) {
    var _win = window.open(url, name, options);
    if (_win !== null) {
        if (_win.focus) { _win.focus(); }
    }
    return false;
}

//find client Id
function $$(id, context) {
    var el = $("#" + id, context);
    if (el.length < 1)
        el = $("[id$=_" + id + "]", context);

    return el;
}

function verifyDelete() {
    var flag;
    flag = confirm("Are you sure you want to delete this entry?");
    return flag;
}

//Numbers only (all browsers)
// onKeyPress="return CheckNumericInput(event.keyCode, event.which, true, false);"
function CheckNumericInput($char, $mozChar, AllowDot, AllowMinus) {
    if ($mozChar !== null) {	// Look for a Mozilla-compatible browser

        if (($mozChar >= 48 && $mozChar <= 57) || ($mozChar === 0) || ($char === 8) || ($mozChar === 13) || (AllowDot === true && $mozChar === 46) || (AllowMinus === true && $mozChar === 45)) $RetVal = true;
        else {
            $RetVal = false;
        }
    }
    else {	 // Must be an IE-compatible Browser
        if (($char >= 48 && $char <= 57) || ($char === 13) || (AllowDot === true && $char === 46) || (AllowMinus === true && $char === 45)) $RetVal = true;
        else {
            $RetVal = false;
        }
    }
    return $RetVal;
}