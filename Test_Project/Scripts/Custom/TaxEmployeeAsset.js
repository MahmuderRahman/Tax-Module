var id = null;
var dTable = null;

$(document).ready(function () {

    manager.LoadYears();
    manager.LoadEmployees();

    $('#findBtn').click(function () {
        debugger;
        var yearId = $('#HrmFinancialYearId').val();
        var empId = $('#HrmEmployeeId').val();

        if ($.isNumeric(yearId) && yearId > 0 && $.isNumeric(empId) && empId > 0) {
            manager.GetEmpAssetInfo(yearId, empId);
        }
        else {
            manager.GetDataForTable();
        }
        manager.UpdateTotalAmount();
    });

    $('#saveBtn').click(function () {
        manager.Save();
    });

});

var manager = {

    Reset: function () {
        $("#tableElement").each(function () {
            $(this).find(':input').val('').trigger('chosen:updated');
            $("#TotalAmount").html('0.00');
        });
    },

    Save: function () {

        var amount = [];

        var year = $("#HrmFinancialYearId").val();
        if (year == "") {
            Message.Warning("Year is required!");
            return;
        }
        var employee = $('#HrmEmployeeId').val();
        if (employee == "") {
            Message.Warning("Employee is required!");
            return;
        }

        $.each($('#tableElement tbody tr'), function (index, row) {
            var amountList = new Object();
            if ($(row).find('.amountCls').val() != "" && $(row).find('.amountCls').val() != "0") {
                amountList.TaxAssetTypeId = dTable.row(row).data().AssetTypeId;
                amountList.Amount = $(row).find('.amountCls').val();

                amountList.HrmFinancialYearId = year;
                amountList.HrmEmployeeId = employee;
                amount.push(amountList);
            }

        });

        if (Message.Prompt()) {
            debugger;
            var url = '/TaxEmployeeAsset/InsertTaxEmployeeAsset/';
            var jsonParam = { employeeAssets: amount }
            JsHelper.ajaxPost(url, jsonParam).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {

                    Message.CustomSuccess('Tax employee asset saved successfully');
                    manager.Reset();

                } else {
                    var errorMsg = 'ERROR:';
                    $.each(response.Errors,
                        function (index, error) {
                            errorMsg += '</br>' + error;
                        });
                    Message.Warning(errorMsg);
                }
            }).catch(function (error) {
                cl(error);
            });
        }
    },

    GetTotalAmount: function () {

        var totalAmount = 0;
        $('.amountCls').each(function (key, val) {
            if ($(this).val() && $.isNumeric($(this).val())) {
                totalAmount += parseFloat($(this).val());
            }
        });
        return totalAmount.toFixed(2);
    },

    UpdateTotalAmount: function () {

        var totalAmount = manager.GetTotalAmount();
        if (totalAmount > 0) {
            $('#TotalAmount').html(totalAmount);
        } else {
            $('#TotalAmount').html('0.00');
        }
    },

    LoadYears: function () {
        var url = "/TaxEmployeeAsset/GetHrmFinancialYearsInfo/";
        JsHelper.ajaxGet(url).then(function (response) {
            JsHelper.dropdownOptionLoader('#HrmFinancialYearId', response, 'Id', 'Name', 'Select Financial Year', true);
        });
    },

    LoadEmployees: function () {
        var url = "/TaxEmployeeAsset/GetEmployeeInfo/";
        JsHelper.ajaxGet(url).then(function (response) {
            JsHelper.dropdownOptionLoader('#HrmEmployeeId', response, 'Id', 'Name', 'Select Employee', true);
        });
    },

    GetEmpAssetInfo: function (year, employee) {
        debugger;
        var jsonParam = { yearId: year, empId: employee };
        var serviceURL = "/TaxEmployeeAsset/GetAssetTypeInfoByEmployee/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            manager.LoadDataTable(jsonData);
            manager.UpdateTotalAmount();
            //manager.Reset();
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function () {
        var jsonParam = "";
        var serviceURL = "/TaxEmployeeAsset/GetAssetTypeInfoByEmployee/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            manager.LoadDataTable(jsonData);
            //manager.Reset();
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data) {

        if (dTable == null) {
            dTable = $('#tableElement').DataTable({
                dom: '<"col-sm-4 p0 margin-bottom5"B><"col-sm-5 margin-bottom5 "l><"col-sm-3 p0 margin-bottom5"f>rtip',
                fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    var index = iDisplayIndexFull + 1;
                    $("td:first", nRow).html(index);
                    return nRow;
                },

                initComplete: function () {
                    $("#tableElement").parent().css({
                        'background': '#fff',
                        'minHeight': '100px',
                        'borderLeft': '1px solid #dddddd',
                        'borderRight': '1px solid #dddddd',
                        'borderBottom': '1px solid #dddddd'
                    });
                    $('#tableElement_length').css({ 'float': 'right' });
                },
                "searching": false,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "bInfo": false,

                buttons: [
                ],
                scrollY: "450px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[50, 150, -1], [50, 150, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { "className": "dt-center", "targets": [0] }
                ],

                columns: [
                    {
                        data: null,
                        name: '',
                        title: '#SL',
                        width: 10
                    },
                    {
                        data: 'Name',
                        name: 'Name',
                        title: 'Asset Type',
                        width: 700
                    },
                    {
                        data: 'TotalAmount',
                        name: 'TotalAmount',
                        title: 'Amount',
                        width: 200,
                        render: function (data, type, row, meta) {
                            return "<input type='number' id='row-" +
                                meta.row +
                                "-Amount' name='row-" +
                                meta.row +
                                "-Amount'  value='" +
                                data +
                                "'class='form-control amountCls input-sm w-80 text-right inputs numberField' placeholder='0.00'/>";
                        }
                    },
                ],
                fixedColumns: true,
                data: data

            });

        } else {
            dTable.clear().rows.add(data).draw();
        }
    },
}

$(document).on('keyup', '.amountCls', function () {
    manager.UpdateTotalAmount();
});
