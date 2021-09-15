
var JsHelper = {

    ajaxGet: function (url) {
        return fetch(url)
            .then(response => response.json());
    },

    ajaxGetHtml: function (url) {
        return fetch(url)
            .then(response => response.text());
    },

    ajaxPost: function (url, data) {
        var request = new Request(url, {
            method: 'POST',
            headers: new Headers({
                'content-type': 'application/json'
            }),
            body: JSON.stringify(data)
        });
        return fetch(request).then(response => response.json()).catch(response => cl(response));
    },

    ajaxPostFile: function (url, data) {
        var request = new Request(url, {
            method: 'POST',
            body: data
        });
        return fetch(request).then(response => response.json());
    },

    cSharpDateToString: function (csharpDate) {
        var date = new Date(Date.parse(csharpDate));
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        hours = hours % 12;
        hours = hours === 0 ? 12 : hours;
        hours = hours < 10 ? '0' + hours : hours;
        minutes = minutes < 10 ? '0' + minutes : minutes;
        seconds = seconds < 10 ? '0' + seconds : seconds;
        var strTime = hours + ':' + minutes + ':' + seconds;
        return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + " " + strTime;
    },

    getFormData: function (formId) {
        var data = {};
        $.each($(formId).serializeArray(), function (index, item) {
            data[item.name] = item.value;
        });
        return data;
    },

    dropdownOptionLoader: function (element, data, valueMember, displayMember, placeholder, useChosen) {
        var options = [$('<option/>', {
            'value': '',
            'text': placeholder
        })];
        data.forEach(s => {
            options.push($('<option/>', {
                'value': s[valueMember],
                'text': s[displayMember]
            }))});

        $(element).empty();
        if (options.length > 1) {
            $(element).append(options);
            if (useChosen) {
                $(element).chosen({ width: '100%' });
            }
        }
        else {
            if ($(element).data("chosen")) {
                $(element).chosen('destroy'); 
            }
        }
    },

    momentDateTimeFormatter: function (value) {
        var dateTime = moment(moment(value, 'DD-MM-YYYY HH:mm:ss')).format('MM-DD-YYYY HH:mm:ss');
        return dateTime;
    },

    convertToDayMonthYearDash:function(dateTime, addTime) {
        if (!dateTime)
            return '';

        var dtFormat = addTime ? 'DD-MM-YYYY HH:mm:ss' : 'DD-MM-YYYY';
        return moment(dateTime).format(dtFormat);
    },

    convertToDayMonthYearWithoutSecondDash: function (dateTime, addTime) {
        if (!dateTime)
            return '';

        var dtFormat = addTime ? 'DD-MM-YYYY HH:mm' : 'DD-MM-YYYY';
        return moment(dateTime).format(dtFormat);
    },

    isEqual : function (value, other) {

        // Get the value type
        var type = Object.prototype.toString.call(value);

        // If the two objects are not the same type, return false
        if (type !== Object.prototype.toString.call(other)) return false;

        // If items are not an object or array, return false
        if (['[object Array]', '[object Object]'].indexOf(type) < 0) return false;

        // Compare the length of the length of the two items
        var valueLen = type === '[object Array]' ? value.length : Object.keys(value).length;
        var otherLen = type === '[object Array]' ? other.length : Object.keys(other).length;
        if (valueLen !== otherLen) return false;

        // Compare two items
        var compare = function (item1, item2) {

            // Get the object type
            var itemType = Object.prototype.toString.call(item1);

            // If an object or array, compare recursively
            if (['[object Array]', '[object Object]'].indexOf(itemType) >= 0) {
                if (!JsHelper.isEqual(item1, item2)) return false;
            }

                // Otherwise, do a simple comparison
            else {

                // If the two items are not the same type, return false
                if (itemType !== Object.prototype.toString.call(item2)) return false;

                // Else if it's a function, convert to a string and compare
                // Otherwise, just compare
                if (itemType === '[object Function]') {
                    if (item1.toString() !== item2.toString()) return false;
                } else {
                    if (item1 !== item2) return false;
                }

            }
        };


        // Compare properties
        if (type === '[object Array]') {
            for (var i = 0; i < valueLen; i++) {
                if (compare(value[i], other[i]) === false) return false;
            }
        } else {
            for (var key in value) {
                if (value.hasOwnProperty(key)) {
                    if (compare(value[key], other[key]) === false) return false;
                }
            }
        }

        // If nothing failed, return true
        return true;
    },

    readExcel:function (file, isAllSheet) {
        return new Promise(function(resolve, reject) {

            var reader = new FileReader();
            reader.onload = function(e) {
                var data = e.target.result;
                var workbook = XLSX.read(data, {
                    type: 'binary'
                });

                if (isAllSheet) {
                    var result = {};
                    workbook.SheetNames.forEach(function(sheetName) {
                        var sheetData = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], {
                            header: 1
                        });
                        if (sheetData.length) result[sheetName] = sheetData;
                    });
                    resolve(JSON.stringify(result, 2, 2));
                } else {
                    var sheetData = XLSX.utils.sheet_to_json(workbook.Sheets[workbook.SheetNames[0]], {
                        header: 1
                    });
                    resolve(JSON.stringify(sheetData, 2, 2));
                }
            };
            reader.readAsBinaryString(file);
        });
        
    }

}
