/*
* @author Kaysar && chanchal
* @since 2018-02-19
*
*/

$(document).ready(function () {

    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
        "date-uk-pre": function (a) {
            if (a == null || a == "") {
                return 0;
            }
            var ukDatea = a.split(a[2]);
            return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
        },

        "date-uk-asc": function (a, b) {
            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
        },

        "date-uk-desc": function (a, b) {
            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        }
    });

    //setInterval(function () {
    //    var jsonParam = '';
    //    var serviceURL = "/Security/ExtendSession/";
    //    AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
    //    function onSuccess(jsonData) {
    //    }
    //    function onFailed(error) {
    //        window.alert(error.statusText);
    //    }
    //},120000);
});


function cl(value) {
    console.log(value);
}

function GetCurrentDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    return mm + '/' + dd + '/' + yyyy;
}

function GetCurrentDateDDMMYYYY() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    return dd + '-' + mm + '-' + yyyy;
}


var utlt = [];
utlt["siteUrl"] = function(addr) {
    addr = typeof addr == "undefined" ? "" : addr;
    return location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/' + addr;
}

var dataTableCustom = {
    serialNumber:function(dTable) {
        dTable.on('order.dt search.dt', function () {
            dTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = '<div align="center">' + (i + 1) + '<div>';
            });
        }).draw();
    }
};
var NotificationManager = {
    StartProcessBar: function () {
        var div = "<div id='ui_waitingbar' style='position: fixed;z-index: 1500;padding-top: 20%;top: 0;width: 100%;height: 100%;background: rgba(0, 0, 0, 0.18);left:0'><p style='width: 82px;-align: center;background: #fff;border-radius: 55px;padding: 4px 5px;margin: 0 auto;box-shadow: 2px 2px 15px #807b79;'><img height='70px' src='/Picture/WaitingProcessBar.gif' /></p></div>";
        $("#process_notifi").append(div);
    },
    EndProcessBar: function () {
        $("#ui_waitingbar").remove();
    }
};


var DTableManager = {

    dTableSerialNumber: function (n) {
        n.on("order.dt search.dt", function () { n.column(0, { search: "applied", order: "applied" }).nodes().each(function (n, t) { n.innerHTML = DTableManager.indexColumn(t + 1) }) }).draw();
    },
    indexColumn: function (n) {
        return '<div class="font-weight" style="" align="center">' + n + "<\/div>";
    }
};

var UtilityManager = {
    GetRandomUniqueId:function() {
        return '_' + Math.random().toString(36).substr(2, 9);
    }
}

var partyTypeEnum = new Map();
partyTypeEnum.set('Customer', 1);
partyTypeEnum.set('ShippingLine', 2);
partyTypeEnum.set('Shipper', 3);
partyTypeEnum.set('Consignee', 4);
partyTypeEnum.set('ClearanceAgent', 5);

var statusTypeEnum = new Map();
statusTypeEnum.set('CustomsStatus', 30);
statusTypeEnum.set('ClearanceStatus', 31);
statusTypeEnum.set('WarehouseSpecialHandlingStatus', 32);
statusTypeEnum.set('ProblemStatus', 33);
statusTypeEnum.set('OveralStatus', 34);
statusTypeEnum.set('ConsignmentStatus', 35);


var freightTypeEnum = new Map();
freightTypeEnum.set(0, 'Flight Number');
freightTypeEnum.set(1, 'Flight Number');
freightTypeEnum.set(2, 'Shipment ID');
freightTypeEnum.set(3, 'Freight ID');

var freightTypeLabelEnum = new Map();
freightTypeLabelEnum.set(0, 'Fl. No');
freightTypeLabelEnum.set(1, 'Fl. No');
freightTypeLabelEnum.set(2, 'Ship. ID');
freightTypeLabelEnum.set(3, 'Fre. ID');

var freightTypeTitleEnum = new Map();
freightTypeTitleEnum.set(0, 'Flight Number');
freightTypeTitleEnum.set(1, 'Flight Number');
freightTypeTitleEnum.set(2, 'Shipment ID');
freightTypeTitleEnum.set(3, 'Freight ID');

var overallStatusEnum = new Map();
overallStatusEnum.set('Created', 19);
overallStatusEnum.set('In Progress', 20);
overallStatusEnum.set('Complete', 21);
overallStatusEnum.set('Closed', 22);
overallStatusEnum.set('Archive', 23);
overallStatusEnum.set('CommonAll', [19, 20, 21, 22]);

var consignmentStatusEnum = new Map();
consignmentStatusEnum.set('Label Printed', 27);

const regexNewLine = /(\r\n|\n|\r)/gm;

const globalDateFormat = 'd-m-Y';
const globalDateTimeFormat = 'd-m-Y H:i';