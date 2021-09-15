var id = null;
var dTable = null;


$(document).ready(function () {

    Manager.GetDataForTable(0);

    $("#btnSave").click(function () {
        Manager.Save();
    });

    $("#btnEdit").click(function () {
        Manager.Update();
    });

    $("#btnDelete").click(function () {
        Manager.Delete();
    });

    Manager.LoadYears();


});

var Manager = {

    LoadYears: function () {
        var url = "/InvestmentRebateSlab/GetHrmFinancialYearsInfo/";
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
        var rebateRate = $("#RebateRate").val();
        var description = $("#Description").val();
        var status = $("#Status").is(':checked');
        if (Message.Prompt()) {
            var data = {
                ValidFromHrmFinancialYearId: year,
                LimitAbove: limitAbove,
                RebateRate: rebateRate,
                Description: description,
                Status: status
            };
            var url = '/InvestmentRebateSlab/InsertInvestmentRebateSlab/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    Manager.GetDataForTable(1);
                    Manager.Reset();
                    Message.CustomSuccess('Investment rebate slab saved successfully');
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
        var rebateRate = $("#RebateRate").val();
        var description = $("#Description").val();
        var status = $("#Status").is(':checked');
        if (Message.Prompt()) {
            var data = {
                Id: id,
                ValidFromHrmFinancialYearId: year,
                LimitAbove: limitAbove,
                RebateRate: rebateRate,
                Description: description,
                Status: status
            };
            var url = '/InvestmentRebateSlab/UpdateInvestmentRebateSlab/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    Manager.GetDataForTable(1);
                    Manager.Reset();
                    Message.CustomSuccess('Investment rebate slab saved successfully');
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
            var jsonParam = { investmentRebateSlabId: id };
            var serviceURL = '/InvestmentRebateSlab/DeleteInvestmentRebateSlab/';
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }
        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                Manager.GetDataForTable(1);
                Manager.Reset();
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/InvestmentRebateSlab/GetTaxInvestmentRebateSlabList/";
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
                dom: '<"col-sm-5 pad-left0 margin-bottom5"B><"col-sm-4 margin-bottom5 "l><"col-sm-3 p0 margin-bottom5"f>rtip',
                initComplete: function () {
                    $("#tableElement").parent().css({
                        'background': '#fff',
                        //'minHeight': '450px',
                        'borderLeft': '1px solid #dddddd',
                        'borderRight': '1px solid #dddddd',
                        'borderBottom': '1px solid #dddddd'
                    });
                    $('#tableElement_length').css({ 'float': 'right' });
                },
                buttons: [

                     {
                         text: '<i class="fas fa-plus-square "></i> Add New Tax Investment Rebate Slab',
                         className: 'btn-success',
                         action: function (e, bt, node, config) {
                             $('#frmModal').modal('show');

                             Manager.Reset();
                             $("#btnEdit").hide();
                             $("#btnSave").show();

                         }
                     },
                        {
                            text: '<i class="far fa-file-excel"></i> Excel',
                            className: 'btn-primary',
                            extend: 'excelHtml5',
                            exportOptions: {
                                columns: [0, 1, 3, 4]
                            },
                            title: 'Tax Investment Rebate Slab'
                        }
                ],

                scrollY: "450px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[50, 100, 500, -1], [50, 100, 500, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { "className": "dt-center", "targets": [] }
                ],
                columns: [

                    {
                        data: 'LimitAbove',
                        name: 'LimitAbove',
                        title: 'Limit Above'
                    },
                    {
                        data: 'RebateRate',
                        name: 'RebateRate',
                        title: 'Rebate Rate'

                    },
                    {
                        data: 'Description',
                        name: 'Description',
                        title: 'Description'

                    },
                    {
                        data: 'YearName',
                        name: 'YearName',
                        title: 'Year'

                    },
                    {
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 130,
                        render: function (data, type, row) {
                            return data == true ? "Yes" : "No";
                        }
                    },
                    {
                        name: 'Option',
                        title: 'Option', width: 50,
                        render: function (data, type, row) {
                            var deleteBtn = '';
                            if ($("#btnDelete").length > 0) {
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
    Reset: function () {
        $("#TaxInvestmentRebateSlabFormId")[0].reset();
    }

}

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    var status = rowData.Status;
    id = rowData.Id;
    $('#LimitAbove').val(rowData.LimitAbove);
    $('#RebateRate').val(rowData.RebateRate);
    $('#Description').val(rowData.Description);
    $('#ValidFromHrmFinancialYearId').val(rowData.ValidFromHrmFinancialYearId);

    if (status == 1) {
        $('#Status').prop('checked', true);
    } else {
        $('#Status').prop('checked', false);
    }

    $("#btnEdit").show();
    $("#btnSave").hide();
    $("#frmModal").modal('show');
});

$(document).on('click', '.spnDataTableDelete', function () {
    id = dTable.row($(this).parent()).data().Id;
    Manager.Delete(id);
});