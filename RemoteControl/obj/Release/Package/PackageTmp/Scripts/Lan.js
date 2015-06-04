
    // Скрипт контроллера
    var ipadress = 'http://178.172.245.5';// 'http://82.209.194.198';

    var timeOutMS = 5000; //ms
    var ajaxList = new Array();

    function newAJAXCommand(url, container, repeat, data) {
        var newAjax = new Object();
        var theTimer = new Date();
        newAjax.url = url;
        newAjax.container = container;
        newAjax.repeat = repeat;
        newAjax.ajaxReq = null;
        if (window.XMLHttpRequest) {
            newAjax.ajaxReq = new XMLHttpRequest();
            newAjax.ajaxReq.open((data == null) ? "GET" : "POST", newAjax.url, true);
            newAjax.ajaxReq.send(data);
        } else if (window.ActiveXObject) {
            newAjax.ajaxReq = new ActiveXObject("Microsoft.XMLHTTP");
            if (newAjax.ajaxReq) {
                newAjax.ajaxReq.open((data == null) ? "GET" : "POST", newAjax.url, true);
                newAjax.ajaxReq.send(data);
            }
        }
        newAjax.lastCalled = theTimer.getTime();
        ajaxList.push(newAjax);
    }

    function pollAJAX() {

        var curAjax = new Object();
        var theTimer = new Date();
        var elapsed;

        for (i = ajaxList.length; i > 0; i--) {
            curAjax = ajaxList.shift();
            if (!curAjax)
                continue;
            elapsed = theTimer.getTime() - curAjax.lastCalled;

            if (curAjax.ajaxReq.readyState == 4 && curAjax.ajaxReq.status == 200) {
                if (typeof (curAjax.container) == 'function') {
                    if (curAjax.ajaxReq.responseXML) curAjax.container(curAjax.ajaxReq.responseXML.documentElement);
                    else if (curAjax.ajaxReq.response) curAjax.container(curAjax.ajaxReq.response);
                } else if (typeof (curAjax.container) == 'string') {
                    document.getElementById(curAjax.container).innerHTML = curAjax.ajaxReq.responseText;
                }

                curAjax.ajaxReq.abort();
                curAjax.ajaxReq = null;

                if (curAjax.repeat)
                    newAJAXCommand(curAjax.url, curAjax.container, curAjax.repeat);
                continue;
            }

            if (elapsed > timeOutMS) {
                if (typeof (curAjax.container) == 'function') {
                    curAjax.container(null);
                } else {
                    //alert("Command failed.\nConnection to Jerome board was lost.");
                }

                curAjax.ajaxReq.abort();
                curAjax.ajaxReq = null;

                if (curAjax.repeat)
                    newAJAXCommand(curAjax.url, curAjax.container, curAjax.repeat);
                continue;
            }
            ajaxList.push(curAjax);
        }
        setTimeout("pollAJAX()", 100);
    }

    function getXMLValue(xmlData, field) {
        try {
            if (xmlData.getElementsByTagName(field)[0].firstChild.nodeValue)
                return xmlData.getElementsByTagName(field)[0].firstChild.nodeValue;
            else
                return null;
        } catch (err) { return null; }
    }

    setTimeout("pollAJAX()", 500);

    /////////////////////////////////////////////////////////////////////
    // Types of SLOW information
    const SLOW_INFO_SN = 1;
    const SLOW_INFO_FW = 2;
    const SLOW_INFO_JIOTABLE = 3;
    const SLOW_INFO_IP = 4;
    const SLOW_INFO_MAC = 5;
    const SLOW_INFO_MSK = 6;
    const SLOW_INFO_GTW = 7;
    const SLOW_INFO_SPB = 8;
    const SLOW_INFO_EXTSRV = 9;
    const SLOW_INFO_BITSET = 10;
    const SLOW_INFO_PSW = 11;
    const SLOW_INFO_COUNT_CYCLES = 12;
    const SLOW_INFO_TOTAL = 13;
    /////////////////////////////////////////////////////////////////////

    function CatcherObject() {
        this.eCatType = 0;
        this.bEnabled = 0;
        this.bExtServer = 0;

        this.timer = 0;
        this.uiInLine = 0;
        this.LineEvtType = 0;

        this.uiOutLineNumber = 0;
        this.uiOutValue = 0;
        this.uiOutDelay = 0;

        this.cnt = 0;
    }
    const CAT_ARRAY_SIZE = 10;

    function JeromeObject() {
        this.Connected = 0;      // 0 - disconnected, 1 - connected
        this.time = 0;      // SystemTime
        this.sn = "";      // Serial Number
        this.fw = "";      // Firmware version
        this.pwm = 0;       // PWM level

        this.IOTableSave = new Array(); // States of IO lines (in/out)
        this.IOValueSave = new Array(); // Values for IO lines (0/1)

        this.ADC = new Array();        // ADC, 4 channels
        this.INT = new Array();        // Impuls counter, 4 channels
        this.INTCycle = new Array();   // Impuls counter cycles, 4 channels

        // Catcher ARRAY
        this.cat = new Array();
        for (i = 0; i < CAT_ARRAY_SIZE; i++) {
            this.cat[i] = new CatcherObject();
        }
        var bCatcherInfoNew = 0;
        var bCatcherCntNew = 0;

        this.pass = "";
        this.ip = "";
        this.mac = "";
        this.mask = "";
        this.gate = "";
        this.baud = "";
        this.security = true;  // Security is ON
        this.fw_load = false; // FW upload is disabled
        this.sav = false; // Values saving is disabled
        this.rmt_srv = "";    // Remote Server adress
        this.kevt = false; // $KE,EVT is OFF by default

        for (i = 0; i < 22; i++) {
            this.IOTableSave[i] = 0;
            this.IOValueSave[i] = 0;
        }

        for (i = 0; i < 4; i++) {
            this.ADC[i] = 0;
            this.INT[i] = 0;
            this.INTCycle[i] = 0;
        }

        this.IsConnected = function () { return this.Connected; }
        this.GetTime = function () { return this.time; }
        this.GetSN = function () { return this.sn; }
        this.GetFW = function () { return this.fw; }
        this.GetPWM = function () { return this.pwm; }
        this.GetADC = function (n) { return this.ADC[n]; }
        this.GetINT = function (n) { return this.INT[n]; }
        this.GetINTCycle = function (n) { return this.INTCycle[n]; }
        this.GetIoTable = function (n) { return this.IOTableSave[n]; }
        this.GetIoValue = function (n) { return this.IOValueSave[n]; }

        this.GetPass = function () { return this.pass; }
        this.GetIP = function () { return this.ip; }
        this.GetMAC = function () { return this.mac; }
        this.GetMask = function () { return this.mask; }
        this.GetGate = function () { return this.gate; }
        this.GetBaud = function () { return this.baud; }

        this.GetSEC = function () { return this.security; }
        this.GetFWL = function () { return this.fw_load; }
        this.GetSAV = function () { return this.sav; }
        this.GetKEVT = function () { return this.kevt; }

        var srv = ipadress + "/server.cgi?data";

        this.Ready = function () {
            newAJAXCommand(srv + '=OKK,');
        }

        this.SetIO = function (LineIdx) {
            newAJAXCommand(srv + '=SIO,' + LineIdx);
        }

        this.SetOUT = function (LineIdx) {
            newAJAXCommand(srv + '=OUT,' + LineIdx);
        }

        this.SetPWM = function (NewPwm) {
            newAJAXCommand(srv + '=PWM,' + NewPwm);
        }

        this.SetPass = function (value) {
            newAJAXCommand(srv + '=PAS,' + value);
        }

        this.SetIP = function (value) {
            newAJAXCommand(srv + '=IPX,' + value + '$');
        }

        this.SetMAC = function (value) {
            newAJAXCommand(srv + '=MAC,' + value + '$');
        }

        this.SetMSK = function (value) {
            newAJAXCommand(srv + '=MSK,' + value + '$');
        }

        this.SetGTW = function (value) {
            newAJAXCommand(srv + '=GTW,' + value + '$');
        }

        this.SetSEC = function (value) {
            newAJAXCommand(srv + '=SEC,' + value);
        }

        this.SetSAV = function (value) {
            newAJAXCommand(srv + '=SAV,' + value);
        }

        this.SetKEVT = function (value) {
            newAJAXCommand(srv + '=KEVT,' + value);
        }

        this.SetBaud = function (value) {
            newAJAXCommand(srv + '=BAU,' + value);
        }

        this.SendUSART = function (value, cr_lf) {
            newAJAXCommand(srv + '=USR,' + cr_lf + ',' + value.length + ',' + value);
        }

        this.Reset = function () {
            newAJAXCommand(srv + '=RST');
        }

        this.Default = function () {
            newAJAXCommand(srv + '=DEF');
        }

        this.SetCAT_ALL_Action = function (action) {
            // All CAT
            newAJAXCommand(srv + '=ACAT,' + action);
        }

        this.SetCAT_I_Action = function (idx, action) {
            // CAT for particulr slot
            newAJAXCommand(srv + '=ICAT,' + idx + ',' + action);
        }

        this.SetCAT_New = function (mes) {
            // NEW CAT
            newAJAXCommand(srv + '=NCAT,' + mes);
        }


        this.UpdateStatus = function (xmlData) {
            if (!xmlData) { this.Connected = 0; return; }

            this.Connected = 1;

            var TempLoc;

            TempLoc = getXMLValue(xmlData, 'systime0');
            if (TempLoc) this.time = TempLoc;

            var IoTempValue = getXMLValue(xmlData, 'iovalue0');
            if (IoTempValue) {
                for (i = 0; i < 22; i++) this.IOValueSave[i] = IoTempValue.charAt(i);
            }

            for (i = 0; i < 4; i++) {
                TempLoc = getXMLValue(xmlData, 'adc' + i);
                if (TempLoc) this.ADC[i] = TempLoc;
            }
            for (i = 0; i < 4; i++) {
                TempLoc = getXMLValue(xmlData, 'count' + i);
                if (TempLoc) this.INT[i] = TempLoc;
            }

            TempLoc = getXMLValue(xmlData, 'pwm0');
            if (TempLoc) this.pwm = TempLoc;

            // Slow information...
            TempLoc = getXMLValue(xmlData, 'slow_info0');
            if (TempLoc) {
                // <data id>,<data>
                var TempArr = TempLoc.split(',');
                if (TempArr.length != 2) return;
                switch (parseInt(TempArr[0])) {
                    case SLOW_INFO_SN: {
                        this.sn = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_FW: {
                        this.fw = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_JIOTABLE: {
                        for (i = 0; i < 22; i++) {
                            this.IOTableSave[i] = TempArr[1].charAt(i);
                        }
                        break;
                    }
                    case SLOW_INFO_IP: {
                        this.ip = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_MAC: {
                        this.mac = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_MSK: {
                        this.mask = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_GTW: {
                        this.gate = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_SPB: {
                        this.baud = parseInt(TempArr[1]);
                        break;
                    }
                    case SLOW_INFO_EXTSRV: {
                        this.rmt_srv = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_BITSET: {
                        // Security, SAV, EVT
                        this.security = false;
                        this.sav = false;
                        this.kevt = false;
                        var ch = TempArr[1].charAt(0);
                        if (ch == '1') this.security = true;
                        ch = TempArr[1].charAt(1);
                        if (ch == '1') this.sav = true;
                        ch = TempArr[1].charAt(2);
                        if (ch == '1') this.kevt = true;
                        break;
                    }
                    case SLOW_INFO_PSW: {
                        this.pass = TempArr[1];
                        break;
                    }
                    case SLOW_INFO_COUNT_CYCLES: {
                        // INT Cycle Counts, 4 x items
                        // cyc1;cyc2;cyc3;cyc4

                        var TempCycArr = TempArr[1].split(';');
                        // No more than 4 x int ciunters are possible
                        if (TempCycArr.length > 4) return;
                        for (i = 0; i < TempCycArr.length; i++) {
                            this.INTCycle[i] = TempCycArr[i];
                        }
                        break;
                    }
                }   // end Switch
            } // IF valid data

            ////////////////////////////////////////////////////////////
            // Catcher information...
            TempLoc = getXMLValue(xmlData, 'cat_inf0');
            if (TempLoc) {
                //if( pCAT->eCatType == 'L' ) {
                //    sprintf( Buff, "%d,%d,%c,%d,%d,%d,%d,%d", bo.bCATInfoChanged-1, pCAT->bEnabled, pCAT->eCatType,
                //        pCAT->evt.uiInLine, pCAT->evt.LineEvtType, pCAT->uiOutLineNumber, pCAT->uiOutValue, pCAT->bExtServer );
                //}


                var TempArr = TempLoc.split(',');
                var len = TempArr.length;

                var idx = parseInt(TempArr[0]);

                if (idx > CAT_ARRAY_SIZE - 1) return;

                this.cat[idx].bEnabled = parseInt(TempArr[1]);
                this.cat[idx].eCatType = TempArr[2];
                if (TempArr[2] == "L") {
                    this.cat[idx].uiInLine = parseInt(TempArr[3]);
                    this.cat[idx].LineEvtType = parseInt(TempArr[4]);

                    this.cat[idx].uiOutLineNumber = parseInt(TempArr[5]);
                    this.cat[idx].uiOutValue = parseInt(TempArr[6]);
                    //this.cat[idx].bExtServer = parseInt(TempArr[7]);
                }
                else if (TempArr[2] == "T") {
                    this.cat[idx].timer = parseInt(TempArr[3]);
                    this.cat[idx].uiOutLineNumber = parseInt(TempArr[4]);
                    this.cat[idx].uiOutValue = parseInt(TempArr[5]);
                    //this.cat[idx].bExtServer = parseInt(TempArr[6]);
                    this.cat[idx].uiOutDelay = parseInt(TempArr[6]);
                }
                else if (TempArr[2] == "Z") {
                    // Slot is ZERO
                    this.cat[idx].eCatType = 0;
                    this.cat[idx].bEnabled = 0;
                    this.cat[idx].bExtServer = 0;
                    this.cat[idx].timer = 0;
                    this.cat[idx].uiInLine = 0;
                    this.cat[idx].LineEvtType = 0;
                    this.cat[idx].uiOutLineNumber = 0;
                    this.cat[idx].uiOutValue = 0;
                    this.cat[idx].uiOutDelay = 0;
                    this.cat[idx].cnt = 0;
                }

                // Raise the flag... somethig new has just arrived
                this.bCatcherInfoNew = 1;
            } // IF valid data
            ////////////////////////////////////////////////////////////

            // Catcher counters...
            TempLoc = getXMLValue(xmlData, 'cat_cnt0');
            if (TempLoc) {
                //sprintf( Buff, "%d,%d", bo.bCATCounterChanged-1, bo.CatcherCounter[bo.bCATCounterChanged-1] );
                var TempArr = TempLoc.split(',');
                var len = TempArr.length;
                var idx = parseInt(TempArr[0]);
                this.cat[idx].cnt = parseInt(TempArr[1]);

                // Raise the flag... somethig new has just arrived
                this.bCatcherCntNew = 1;

            } // IF valid data

            this.fw_load = false;
        }

        this.Run = function () {
            //setTimeout( "newAJAXCommand('"+ipadress+"/status.xml', updateStatusJerome, true)", 1000 );
        }
    }


    var bJeromeExists_XX = false;
    var ObjJerome_XX = null;

    function GetJeromeObject() {
        if (!bJeromeExists_XX) {
            ObjJerome_XX = new JeromeObject();
            bJeromeExists_XX = true;
        }
        return ObjJerome_XX;
    }

    function updateStatusJerome(xmlData) {
        ObjJerome_XX.UpdateStatus(xmlData);
    }

    function JsButtonClick(but_id) {
        // but_id: starts from +1
        if (but_id < 1 || but_id > NUMBER_OF_BUTTONS) return;

        // If already pressed -> get out from here
        if (ButtonBlock[but_id - 1] == 1) return;

        // First, set up the button blocked in order to avoid several consequal clicks
        ButtonBlock[but_id - 1] = 1;
        // Checnge IO line state to opposite (turn it ON)
        ModuleObj.SetOUT(but_id);
        // Emulate button click (background color change)
        //document.getElementById("BUTTON_" + but_id).style.backgroundColor = "yellow";
        document.getElementById('myTune').play();

        // 300 ms pause for button blink
        setTimeout(getFunctionForTimeout(but_id), 300);

    }


    var bDocumentComplete = false;
    const NUMBER_OF_BUTTONS = 9;
    var ButtonBlock = new Array();

    var ModuleObj = GetJeromeObject();

    function DisplayDIVFull(div_id, bEnable) {
        if (bEnable == 1) {
            document.getElementById(div_id).style.width = document.body.scrollWidth;
            document.getElementById(div_id).style.height = document.body.scrollHeight;
            document.getElementById(div_id).style.display = "block";
        }
        else {
            document.getElementById(div_id).style.display = "none";
        }
    }

    function UpdateInformation() {
        // Check if a timeout occurred
        if (!ModuleObj.IsConnected()) {
            DisplayDIVFull("FonPic", 1);
            return;
        }

        DisplayDIVFull("FonPic", 0);
        if (bDocumentComplete == false) return;

        document.getElementById("SysTime").innerHTML = ModuleObj.GetTime();
        document.getElementById("SN").innerHTML = ModuleObj.GetSN();
        document.getElementById("FW").innerHTML = ModuleObj.GetFW();

        UpdateSettings();
    }

    function UpdateSettings() {
        // Do not try to update these data if the window is open
        if (document.getElementById("Settings").style.display == "block") return;

        document.getElementById("SET_PASS").value = ModuleObj.GetPass();
        document.getElementById("SET_IP").value = ModuleObj.GetIP();
        document.getElementById("SET_MAC").value = ModuleObj.GetMAC();
        document.getElementById("SET_MSK").value = ModuleObj.GetMask();
        document.getElementById("SET_GTW").value = ModuleObj.GetGate();

        document.getElementById("SEC_LEVEL").checked = ModuleObj.GetSEC();
        document.getElementById("SAVE_STATE").checked = ModuleObj.GetSAV();
        document.getElementById("KEVT_STATE").checked = ModuleObj.GetKEVT();
    }

    function PreInit() {
        // All buttons are unblocked
        for (i = 0; i < NUMBER_OF_BUTTONS; i++) {
            ButtonBlock[i] = 0;
        }

        bDocumentComplete = true;
        ModuleObj.Run();
        ModuleObj.Ready();
        setInterval("UpdateInformation()", 250);
    }

    function DisplayDIV(div_id) {
        var current = document.getElementById(div_id).style.display;
        if (current == "none")
            document.getElementById(div_id).style.display = "block";
        else
            document.getElementById(div_id).style.display = "none";
    }

    function CloseSettings() {
        document.getElementById("Settings").style.display = "none";
    }

    function JsUpdatePass() {
        value = document.getElementById("SET_PASS").value;
        if (value.length < 1 || value.length > 9) {
            alert("Пароль указан некорректно.\nДлина пароля должна быть от 1 до 9 символов");
            return;
        }
        result = confirm("Изменить пароль модуля?");
        if (result == false) return;
        ModuleObj.SetPass(value);
        alert("Пароль модуля обновлен");
        CloseSettings();
    }

    function JsUpdateIP() {
        value = document.getElementById("SET_IP").value;
        if (!CheckDotString(value, 3)) {
            alert("IP адрес указан некорректно");
            return;
        }
        result = confirm("Изменить IP адрес модуля?");
        if (result == false) return;
        ModuleObj.SetIP(value);
        alert("Новое значение IP адреса сохранено.\nОбновление вступит в силу после перезагрузки модуля");
        CloseSettings();
    }

    function JsUpdateMAC() {
        value = document.getElementById("SET_MAC").value;
        if (!CheckDotString(value, 5)) {
            alert("MAC адрес указан некорректно");
            return;
        }
        result = confirm("Изменить MAC адрес модуля?");
        if (result == false) return;
        ModuleObj.SetMAC(value);
        alert("Новое значение MAC адреса сохранено.\nОбновление вступит в силу после перезагрузки модуля");
        CloseSettings();
    }

    function JsUpdateMSK() {
        value = document.getElementById("SET_MSK").value;
        if (!CheckDotString(value, 3)) {
            alert("Маска подсети указана некорректно");
            return;
        }
        result = confirm("Изменить маску подсети?");
        if (result == false) return;
        ModuleObj.SetMSK(value);
        alert("Новое значение маски подсети сохранено.\nОбновление вступит в силу после перезагрузки модуля");
        CloseSettings();
    }

    function JsUpdateGTW() {
        value = document.getElementById("SET_GTW").value;
        if (!CheckDotString(value, 3)) {
            alert("Основной шлюз указан некорректно");
            return;
        }
        result = confirm("Изменить основной шлюз?");
        if (result == false) return;
        ModuleObj.SetGTW(value);
        alert("Новое значение основного шлюза сохранено.\nОбновление вступит в силу после перезагрузки модуля");
        CloseSettings();
    }

    function CheckDotString(str_value, DotCounter) {
        var arr = new Array()
        arr = str_value.split('.');
        if (arr.length != (DotCounter + 1)) return false;
        for (i = 0; i < arr.length; i++) {
            if (arr[i].length < 1 || arr[i].length > 3) return false;
            for (j = 0; j < arr[i].length; j++) {
                Ch = arr[i].charAt(j);
                if (Ch < '0' || Ch > '9') return false;
            }
            if (arr[i] > 255) return false;
        }
        return true;
    }

    function JsUpdateReset() {
        result = confirm("Выполнить сброс модуля?");
        if (result == true) ModuleObj.Reset();
        CloseSettings();
    }

    function JsUpdateDefault() {
        result = confirm("Установить настройки модуля в значения по умолчанию?");
        if (result == true) ModuleObj.Default();
        CloseSettings();
    }

    function JsUpdateSecLevel() {
        state = document.getElementById("SEC_LEVEL").checked;
        document.getElementById("SEC_LEVEL").checked = !state;
        state = !state;

        str = "";
        if (state == true) str = "Выключить защиту модуля?"
        else str = "Включить защиту модуля?"

        result = confirm(str);
        if (result == true) {
            if (state == false) state = '1';
            else state = '0';
            ModuleObj.SetSEC(state);
            alert("Настройка изменена");
            CloseSettings();
        }
    }

    function JsUpdateSaveState() {
        state = document.getElementById("SAVE_STATE").checked;
        document.getElementById("SAVE_STATE").checked = !state;
        state = !state;

        str = "";
        if (state == true) str = "Выключить сохранение значений аппаратных ресурсов?";
        else str = "Сохранять значения аппаратных ресурсов?";

        result = confirm(str);
        if (result == true) {
            if (state == false) state = '1';
            else state = '0';
            ModuleObj.SetSAV(state);
            alert("Настройка изменена");
            CloseSettings();
        }
    }

    function JsUpdateKevtState() {
        state = document.getElementById("KEVT_STATE").checked;
        document.getElementById("KEVT_STATE").checked = !state;
        state = !state;

        str = "";
        if (state == true) str = "Выключить режим 'Сторож'?";
        else str = "Включить режим 'Сторож'?";

        result = confirm(str);
        if (result == true) {
            if (state == false) state = '1';
            else state = '0';
            ModuleObj.SetKEVT(state);
            alert("Настройка изменена");
            CloseSettings();
        }
    }

    //**************************************************************
    var getFunctionForTimeout = function (j) {
        var fn = function () {
            // Set up IO to OFF state
            ModuleObj.SetOUT(j);
            // Turn to default button color
            document.getElementById("BUTTON_" + j).style.removeProperty('background-color');
            // unblock the button
            ButtonBlock[j - 1] = 0;
        }
        return fn;
    }


