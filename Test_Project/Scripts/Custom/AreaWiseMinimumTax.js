var id = null;
var dTable = null;


$(document).ready(function () {

    manager.GetDataForTable(0);

    $('#saveBtn').click(function () {
        manager.Save();
    });

    $("#editBtn").click(function () {
        manager.Update();
    });

    $("#deleteBtn").click(function () {
        manager.Delete();
    });

    manager.LoadTaxArea();
    manager.LoadYears();

});

var manager = {

    LoadTaxArea: function () {
        var url = "/AreaWiseMinimumTax/GetTaxAreaInfo/";
        JsHelper.ajaxGet(url).then(function (response) {
            JsHelper.dropdownOptionLoader('#TaxAreaId', response, 'Id', 'Name', '-Select-', true);
        });

    },

    LoadYears: function () {
        var url = "/AreaWiseMinimumTax/GetHrmFinancialYearsInfo/";
        JsHelper.ajaxGet(url).then(function (response) {
            JsHelper.dropdownOptionLoader('#ValidFromHrmFinancialYearId', response, 'Id', 'Name', '-Select-', true);
        });

    },

    Save: function () {
        var areaName = $("#TaxAreaId").val();

        if (areaName == "") {
            Message.Warning("Area name is required!");
            return;
        }
        var year = $("#ValidFromHrmFinancialYearId").val();

        if (year == "") {
            Message.Warning("Hrm financial year is required!");
            return;
        }

        var minimumTax = $("#MinimumTax").val();
        var isActive = $("#IsActive").is(':checked');
        if (Message.Prompt()) {
            var data = {
                TaxAreaId: areaName,
                ValidFromHrmFinancialYearId: year,
                MinimumTax: minimumTax,
                IsActive: isActive
            };
            var url = '/AreaWiseMinimumTax/InsertAreaWiseMinimumTax/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    manager.GetDataForTable(1);
                    manager.ResetForm();
                    Message.CustomSuccess('Area wise minimum tax saved successfully');
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
        var areaName = $("#TaxAreaId").val();

        if (areaName == "") {
            Message.Warning("Area name is required!");
            return;
        }
        var year = $("#ValidFromHrmFinancialYearId").val();

        if (year == "") {
            Message.Warning("Hrm financial year is required!");
            return;
        }

        var minimumTax = $("#MinimumTax").val();
        var isActive = $("#IsActive").is(':checked');
        if (Message.Prompt()) {
            var data = {
                Id: id,
                TaxAreaId: areaName,
                ValidFromHrmFinancialYearId: year,
                MinimumTax: minimumTax,
                IsActive: isActive
            };
            var url = '/AreaWiseMinimumTax/UpdateAreaWiseMinimumTax/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    manager.GetDataForTable(1);
                    manager.ResetForm();
                    Message.CustomSuccess('Area wise minimum tax saved successfully');
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
            var jsonParam = { areaWiseMinimumTaxId: id };
            var serviceURL = '/AreaWiseMinimumTax/DeleteAreaWiseMinimumTax/';
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }
        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                manager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function (refresh) {

        var jsonParam = '';
        var serviceURL = "/AreaWiseMinimumTax/GetAreaWiseMinimumTaxList/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            manager.LoadDataTable(jsonData, refresh);
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

                    text: '<i class="fas fa-plus-square"></i> Add New Area Wise Tax',
                    className: 'btn-blue showModalBtn',
                    action: function (e, bt, node, config) {

                        $("#editBtn").hide();
                        $("#saveBtn").show();
                        manager.ResetForm();
                        $('#frmModal').modal('show');
                    }
                },
                    {
                        text: '<i class="far fa-file-pdf"></i> PDF',
                        className: 'btn-info',
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        },
                        title: 'Area Wise Minimum Tax'
                    },
                    {
                        text: '<i class="fas fa-print"></i> Print',
                        className: 'btn-warning',
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        },
                        title: 'Area Wise Minimum Tax'
                    },

                    {
                        text: '<i class="far fa-file-excel"></i> Excel',
                        className: 'btn-success',
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3]
                        },
                        title: 'Area Wise Minimum Tax'
                    }
                ],
                scrollY: "350px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[50, 150, -1], [50, 150, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { "className": "dt-center", "targets": [4] }
                ],
                columns: [

                    {
                        data: 'AreaName',
                        name: 'AreaName',
                        title: 'Area Name'
                    },
                     {
                         data: 'MinimumTax',
                         name: 'MinimumTax',
                         title: 'Minimum Tax'

                     },
                    {
                        data: 'YearName',
                        name: 'YearName',
                        title: 'Year'

                    },
                    {
                        data: 'IsActive',
                        name: 'IsActive',
                        title: 'Is Active',
                        render: function (data, type, row) {
                            return data == true ? "Yes" : "No";
                        }
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
        $("#areaWiseMinimumTaxForm")[0].reset();
    }
}

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    id = rowData.Id;
    $('#TaxAreaId').val(rowData.TaxAreaId);
    $('#MinimumTax').val(rowData.MinimumTax);
    $('#ValidFromHrmFinancialYearId').val(rowData.ValidFromHrmFinancialYearId);
    var isActive = rowData.IsActive;

    if (isActive == 1) {
        $('#IsActive').prop('checked', true);
    } else {
        $('#IsActive').prop('checked', false);
    }

    $("#editBtn").show();
    $("#saveBtn").hide();
    $("#frmModal").modal('show');
});

$(document).on('click', '.spnDataTableDelete', function () {
    id = dTable.row($(this).parent()).data().Id;
    manager.Delete(id);
});