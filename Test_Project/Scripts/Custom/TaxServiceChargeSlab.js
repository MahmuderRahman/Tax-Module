var id = null;
var dTable = null;


$(document).ready(function () {

    Manager.GetDataForTable(0);

    $('#saveBtn').click(function () {
        Manager.Save();
    });

    $("#editBtn").click(function () {
        Manager.Update();
    });

    $("#deleteBtn").click(function () {
        Manager.Delete();
    });

    Manager.LoadYears();

});

var Manager = {

    LoadYears: function () {
        var url = "/TaxServiceChargeSlab/GetHrmFinancialYearsInfo/";
        JsHelper.ajaxGet(url).then(function (response) {
            JsHelper.dropdownOptionLoader('#ValidFromHrmFinancialYearId', response, 'Id', 'Name', '-Select-', true);
        });

    },

    Save: function () {
        var year = $("#ValidFromHrmFinancialYearId").val();

        if (year == "") {
            Message.Warning("Hrm financial year is required!");
            return;
        }

        var limitAbove = $("#LimitAbove").val();
        var taxRate = $("#TaxRate").val();
        var minAmount = $("#MinAmount").val();
        var status = $("#Status").is(':checked');
        var description = $("#Description").val();
        if (Message.Prompt()) {
            var data = {
                ValidFromHrmFinancialYearId: year,
                LimitAbove: limitAbove,
                TaxRate: taxRate,
                MinAmount: minAmount,
                Status: status,
                Description: description,
            };
            var url = '/TaxServiceChargeSlab/InsertServiceChargeSlab/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    Manager.GetDataForTable(1);
                    Manager.ResetForm();
                    Message.CustomSuccess('Tax service charge saved successfully');
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

    Update: function () {
        var year = $("#ValidFromHrmFinancialYearId").val();

        if (year == "") {
            Message.Warning("Hrm financial year is required!");
            return;
        }

        var limitAbove = $("#LimitAbove").val();
        var taxRate = $("#TaxRate").val();
        var minAmount = $("#MinAmount").val();
        var status = $("#Status").is(':checked');
        var description = $("#Description").val();
        if (Message.Prompt()) {
            var data = {
                Id: id,
                ValidFromHrmFinancialYearId: year,
                LimitAbove: limitAbove,
                TaxRate: taxRate,
                MinAmount: minAmount,
                Status: status,
                Description: description,
            };
            var url = '/TaxServiceChargeSlab/UpdateServiceChargeSlab/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    Manager.GetDataForTable(1);
                    Manager.ResetForm();
                    Message.CustomSuccess('Tax service charge saved successfully');
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

    Delete: function () {
        if (Message.Prompt()) {
            var jsonParam = { serviceChargeSlabId: id };
            var serviceURL = '/TaxServiceChargeSlab/DeleteServiceChargeSlab/';
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }
        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                Manager.GetDataForTable(1);
                //Manager.Reset();
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function (refresh) {

        var jsonParam = '';
        var serviceURL = "/TaxServiceChargeSlab/GetTaxServiceChargeSlabList/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            Manager.LoadDataTable(jsonData, refresh);
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data, refresh) {

        if (refresh == "0") {
            dTable = $('#tableElement').DataTable({
                dom: '<"col-sm-4 p0 margin-bottom5"B><"col-sm-5 margin-bottom5 "l><"col-sm-3 p0 margin-bottom5"f>rtip',
                initComplete: function () {
                    $("#tableElement").parent().css({
                        'background': '#fff',
                        'minHeight': '350px',
                        'borderLeft': '1px solid #dddddd',
                        'borderRight': '1px solid #dddddd',
                        'borderBottom': '1px solid #dddddd'
                    });
                    $('#tableElement_length').css({ 'float': 'right' });
                },

                buttons: [
                {

                    text: '<i class="fas fa-plus-square"></i> Add New Service Charge',
                    className: 'btn-blue showModalBtn',
                    action: function (e, bt, node, config) {
                        debugger;
                        //$('.formRow').find('.spnMinusAttendancePlanningDetails').show();
                        //$('.formRow').find('.spnAddAttendancePlanningDetails').show();

                        $("#editBtn").hide();
                        $("#saveBtn").show();
                        Manager.ResetForm();
                        $('#frmModal').modal('show');
                    }
                },
                    {
                        text: '<i class="far fa-file-pdf"></i> PDF',
                        className: 'btn-info',
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        },
                        title: 'Tax Service Charge Slab'
                    },
                    {
                        text: '<i class="fas fa-print"></i> Print',
                        className: 'btn-warning',
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        },
                        title: 'Tax Service Charge Slab'
                    },

                    {
                        text: '<i class="far fa-file-excel"></i> Excel',
                        className: 'btn-success',
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5]
                        },
                        title: 'Tax Service Charge Slab'
                    }
                ],
                scrollY: "350px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[50, 150, -1], [50, 150, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { "className": "dt-center", "targets": [6] }
                ],
                columns: [

                    {
                        data: 'LimitAbove',
                        name: 'LimitAbove',
                        title: 'LimitAbove'
                    },

                     {
                         data: 'TaxRate',
                         name: 'TaxRate',
                         title: 'TaxRate'

                     },
                    {
                        data: 'MinAmount',
                        name: 'MinAmount',
                        title: 'MinAmount'

                    },
                    {
                        data: 'Description',
                        name: 'Description',
                        title: 'Description'

                    },

                    {
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 140,
                        render: function (data, type, row) {
                            console.log(data);
                            return data == true ? "Yes" : "No";
                        }
                    },
                    {
                        data: 'YearName',
                        name: 'YearName',
                        title: 'Year'

                    },

                    {
                        name: 'Option',
                        title: 'Option',
                        width: '40',
                        render: function (data, type, row) {
                            var deleteBtn = '';
                            if ($("#deleteBtn").length > 0) {
                                deleteBtn = '<span class="glyphicon glyphicon-trash spnDataTableDelete" title="Click to delete"></span>';
                            }
                            return '<span class="glyphicon glyphicon-edit spnDataTableEdit" title="Edit"></span>' + deleteBtn;
                        }
                    }
                ],
                fixedColumns: true,
                data: data

            });

        } else {
            dTable.clear().rows.add(data).draw();
        }
    },
    ResetForm: function () {
        $("#taxServiceChargeSlabForm")[0].reset();
    }
}

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    var status = rowData.Status;
    id = rowData.Id;
    $('#LimitAbove').val(rowData.LimitAbove);
    $('#TaxRate').val(rowData.TaxRate);
    $('#MinAmount').val(rowData.MinAmount);
    $('#Description').val(rowData.Description);
    //$('#Status').val(rowData.Status);
    $('#ValidFromHrmFinancialYearId').val(rowData.ValidFromHrmFinancialYearId);

    if (status == 1) {
        $('#Status').prop('checked', true);
    } else {
        $('#Status').prop('checked', false);
    }

    $("#editBtn").show();
    $("#saveBtn").hide();
    $("#frmModal").modal('show');
});

$(document).on('click', '.spnDataTableDelete', function () {
    id = dTable.row($(this).parent()).data().Id;
    Manager.Delete(id);
});