var deleteFolderUrl = '';
var windowWidth = $(window).width();
var AjaxManager = {
    GetJson: function (serviceUrl, jsonParams, successCallback, errorCallback) {

        $.ajax({
            url: serviceUrl,
            data: jsonParams,
            type: "POST",
            processData: false,
            async: false,
            contentType: "application/json",
            dataType: "JSON",
            success: successCallback,
            error: errorCallback
        });
    },
    GetJsonAsynTrue: function (serviceUrl, jsonParams, successCallback, errorCallback) {

        $.ajax({
            url: serviceUrl,
            data: jsonParams,
            type: "POST",
            processData: false,
            async: true,
            contentType: "application/json",
            dataType: "JSON",
            success: successCallback,
            error: errorCallback
        });
    },

    //ZM Form Validation
    FormValidation: function () {
        var status = true;
        for (var i = 0; i < arguments.length; i++) {
            if (arguments[i] != null) {
                $.each($('.' + arguments[i] + ''), function (key, value) {
                    if ($(this).val() == "" || $(this).val() == "0") {
                        Message.Warning("Please enter " + $(this).attr('label') + "");
                        status = false;
                        return false;
                    }
                });
            }

        }
        return status;
    },

    showPopUpMsg: function (type, title, msg) {
        var style = 'width:50%;margin-left:25%;';

        if (windowWidth < 768)
            style = 'width:80%;margin-left:10%;';

        var wrap = $('<div class="alert alert-' + type + ' alert-dismissible" role="alert" style="z-index:99999;position:fixed;' + style + '">' +
                        '<strong>' + title + '</strong> ' + msg +
                    '</div>');
        var wrapClose = $('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
        wrapClose.click(function () {
            wrap.remove();
        });
        wrap.prepend(wrapClose);
        $('body').append(wrap);

        wrap.animate({ 'top': '20%' }, 1000);
    },

    SendJsonWithFile: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            processData: false,
            contentType: false,
            success: successCalback,
            error: errorCallback
        });
    },

    SendJson: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            success: successCalback,
            error: errorCallback
        });

    },


    SendJsonTraditionlTrue: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            traditional:true,
            success: successCalback,
            error: errorCallback
        });

    },

    SendJsonArray: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url: serviceUrl,
            data: jsonParams,
            async: false,
            traditional:true,
            success: successCalback,
            error: errorCallback
        });

    },

    SendJsonAsyncON: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            success: successCalback,
            error: errorCallback
        });

    },
    getGridConfig: function (opt, urllink, sortColumnName, orderBy) {
        return $.extend(true, {
            url: urllink,
            datatype: 'json',
            mtype: 'GET',
            pager: '#pager',
            rownumbers: true,
            rowNum: 50,
            rowList: [50, 100, 200, 'All'],
            sortname: sortColumnName,
            sortorder: orderBy,
            shrinkToFit: false,
            viewrecords: true,
            jsonReader: {
                root: "Data",
                page: "PageIndex",
                total: "TotalPages",
                records: "TotalCount",
                startrow: "startRow",
                endrow: "endRow",
                repeatitems: false
            },
            loadBeforeSend: function (xhr) {
                xhr.setRequestHeader("content-type", "application/json");
            },
            prmNames: { page: 'pageIndex', rows: 'pageSize', sort: 'orderByField', order: 'orderByType' },
            height: '200'
        }, opt);
    },
    getGridConfig2: function (opt, urllink, sortColumnName, orderBy) {
        return $.extend(true, {
            url: urllink,
            datatype: 'json',
            mtype: 'GET',
            pager: '#pager2',
            rownumbers: true,
            rowNum: 50,
            rowList: [50, 100, 200, 'All'],
            sortname: sortColumnName,
            sortorder: orderBy,
            shrinkToFit: false,
            viewrecords: true,
            jsonReader: {
                root: "Data",
                page: "PageIndex",
                total: "TotalPages",
                records: "TotalCount",
                startrow: "startRow",
                endrow: "endRow",
                repeatitems: false
            },
            loadBeforeSend: function (xhr) {
                xhr.setRequestHeader("content-type", "application/json");
            },
            prmNames: { page: 'pageIndex', rows: 'pageSize', sort: 'orderByField', order: 'orderByType' },
            height: '200'
        }, opt);
    },

    disablePopup: function (popupDivName, backgroundDivName) {
        $(popupDivName).fadeOut("slow");
    },
    centerPopup: function (popupDivName) {
        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        var popupHeight = $(popupDivName).height();
        var popupHeight = popupHeight;
        var popupWidth = $(popupDivName).width();

        $(popupDivName).css({
            "position": "absolute",
            "top": windowHeight / 2 - popupHeight / 2,
            "left": windowWidth / 2 - popupWidth / 2,
            "height": popupHeight
        });

        $(backgroundDivName).css({
            "height": windowHeight
        });

    },

    showlink: function (el, cellval, opts) {
        var op = { baseLinkUrl: opts.baseLinkUrl, showAction: opts.showAction, addParam: opts.addParam };
        if (!isUndefined(opts.colModel.formatoptions)) {
            op = $.extend({}, op, opts.colModel.formatoptions);
        }
        idUrl = op.baseLinkUrl + op.showAction + '?id=' + opts.rowId + op.addParam;
        if (isString(cellval)) {	//add this one even if its blank string
            $(el).html("<a class=\"aColumn\" href=\"#\"" + "onclick=\"Page.Test(' " + opts.rowId + "')\">" + cellval + "</a>");
        } else {
            $.fn.fmatter.defaultFormat(el, cellval);
        }
    },

    CsharpDateToTimeStamp:function(value) {
        return value.replace(/\/Date\(/g, "").replace(/\)\//g, "");  
    },

    changeDateFormat: function (value, isTime) {
        var dateFormat = "";
        if (value != "" && value!=null) {
            if (isTime != 0) {
                dateFormat=AjaxManager.BDDateTime(value).format("mm/dd/yyyy HH:MM:ss");
            }
            else {
                dateFormat=AjaxManager.BDDateTime(value).format("mm/dd/yyyy");
            }
        }
        return dateFormat;
    },

    changeToSQLDateFormat: function(value, isTime) {
        var rtr = "";
        if (value != "" && value != null) {
            if (isTime != 0 && isTime != null) {
                rtr = AjaxManager.BDDateTime(value).format("mm/dd/yyyy HH:MM:ss");
            } else {
                rtr = AjaxManager.BDDateTime(value).format("mm/dd/yyyy");
            }
        }
        return rtr;
    },

    changeToSQLDateFormat1: function (value, isTime) {
        var rtr = "";
        if (value != "" && value!=null) {
            rtr = AjaxManager.BDDateTime(value).format("dd-mm-yyyy");
        }
        return rtr;
    },

    changeToSQLDateTimeFormat: function(value, isTime) {
        var rtr = "";
        if (value != "" && value != null) {
            if (isTime != 0) {
                rtr = AjaxManager.BDDateTime(value).format("dd-mm-yyyy HH:MM:ss");
            } else {
                rtr = AjaxManager.BDDateTime(value).format("dd-mm-yyyy");
            }
        }
        return rtr;
    },

    changeToSQLDateTimeFormatMMddyyyy: function(value, isTime) {
        var rtr = "";
        if (value != "" && value != null) {
            if (isTime != 0) {
                rtr = AjaxManager.BDDateTime(value).format("mm/dd/yyyy HH:MM:ss");
            } else {
                rtr = AjaxManager.BDDateTime(value).format("mm/dd/yyyy");
            }
            return rtr;
        }
    },

    changeToSQLYearFormat: function(value, isTime) {
        var rtr = "";
        if (value != "" && value != null) {
            if (isTime != 0) {
                rtr = AjaxManager.BDDateTime(value).format("dd-mm-yyyy HH:MM:ss");
            } else {
                rtr = AjaxManager.BDDateTime(value).format("yyyy");
            }
            return rtr;
        }
    },

    changeToSQLTimeFormat: function(value) {
        var rtr = "";
        if (value != "" && value != null) {
            rtr = AjaxManager.BDDateTime(value).format("HH:MM:ss");
        }
        return rtr;
    },

    jqGridDate: function (el, cellval, opts) {
        //
        if (cellval != "" && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")
            //            
            $(el).html(AjaxManager.changeToSQLDateFormat(cellval, 0));
    },
    jqGridDateTime: function (el, cellval, opts) {
        if (cellval != "" && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")

            $(el).html(AjaxManager.changeDateFormat(cellval, 1));
    },
    changeTimeFormat: function (value) {
        var time = value.Hours + ':' + value.Minutes;
        return time;
    },
    jqGridTime: function (el, cellval, opts) {
        if (cellval != "" && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")

            $(el).html(AjaxManager.changeTimeFormat(cellval));
    },

    MDYToMDY: function (value) {
        if (value != '' && value != null) {
            var datePart = value.match(/\d+/g);
            var day = datePart[1];
            var month = datePart[0];
            var year = datePart[2];
            return ((month.length==1?("0"+month):month )+ '/' + (day.length==1?("0"+day):day) + '/' + year);
        }
    },

    DMYToMDY: function (value) {
        cl(value);
        var datePart = value.match(/\d+/g);
        var day = datePart[0];
        var month = datePart[1];
        var year = datePart[2];
        return month + '/' + day + '/' + year;
    },
    MDYToDMY: function (value) {
        var datePart = value.match(/\d+/g);
        var month = datePart[0];
        var day = datePart[1];
        var year = datePart[2];
        return day + '/' + month + '/' + year;
    },

    DMYToYMD: function (value) {
        var datePart = value.match(/\d+/g);
        var day = datePart[0];
        var month = datePart[1];
        var year = datePart[2];
        return year + '/' + month + '/' + day;
    },

    MDYToDashDMY: function (value) {
        if (value != "") {
            var datePart = value.match(/\d+/g);
            var month = datePart[0];
            var day = datePart[1];
            var year = datePart[2];
            return day + '-' + month + '-' + year;
        }
    },

    dateDiff: function (value) {
        var dte = new Date(value.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) / (1000 * 60 * 60 * 24);
        return dte;
    },
    HourDiff: function (value) {
        var dte = new Date(value.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) / (1000 * 60 * 60);
        return dte;
    },

    populateCombo: function (container, data, defaultText) {
        var cbmOptions = "<option value=\"0\">" + defaultText + "</option>";
        $.each(data, function () {
            cbmOptions += '<option value=\"' + this.Id + '\">' + this.Name + '</option>';
        });

        $('#' + container).html(cbmOptions);

    },

    populateComboDefaultValueBlank: function (container, data, defaultText) {
        var cbmOptions = "<option value=\"\">" + defaultText + "</option>";
        $.each(data, function () {
            cbmOptions += '<option value=\"' + this.Id + '\">' + this.Name + '</option>';
        });
        $('#' + container).html(cbmOptions);
    },

    populateComboWithoutDefault: function (container, data) {
        var cbmOptions = "";
        $.each(data, function () {
            cbmOptions += '<option value=\"' + this.Id + '\">' + this.Name + '</option>';
        });

        $('#' + container).html(cbmOptions);

    },

    populateComboByClass: function (container, data, defaultText) {
        var cbmOptions = "<option value=\"0\">" + defaultText + "</option>";
        $.each(data, function () {
            cbmOptions += '<option value=\"' + this.Id + '\">' + this.Name + '</option>';
        });
        $('.' + container).html(cbmOptions);

    },
    BDDateTime: function(timeStr) {
        timeStr = timeStr.replace(/\/Date\(/g, "").replace(/\)\//g, "");
        var userTimeZone = moment.tz.guess();
        var timeStrD = new Date('January 1, 1970 00:00:00');
        return moment.tz(timeStrD, userTimeZone).seconds((parseInt(timeStr) / 1000)).tz(serverTimeZone)._d;

    },
    validate: function(obj) {
        if (obj.length > 0) {
            for (var object of obj) {
                for (var property in object) {
                    if (property.toString() != "Id") {
                        if (object[property] === "" || object[property] === "0" || object[property] === null) {
                            notif({
                                msg: property.toString() + " is required.",
                                type: "warning",
                                position: 'center',
                                autohide: false
                            });
                            return false;
                        }
                    }
                }
        }
        return true;
    }
            return false;

},
}