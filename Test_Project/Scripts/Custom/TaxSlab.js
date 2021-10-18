var id = null;
var dTable = null;
var slabTypeId = null;
var newTypeName = null;

$(document).ready(function () {

    manager.GetDataForTable(0);
    manager.LoadSlabTypeDropDown();
    manager.LoadYears();

    $('#saveBtnSlabType').click(function () {
        manager.SaveSlabType();
    });

    $('#saveBtn').click(function () {
        manager.Save();
    });

    $("#editBtn").click(function () {
        manager.Update();
    });

    $("#deleteBtn").click(function () {
        manager.Delete();
    });

});

var manager = {

    updateOrderNo: function () {
        debugger;
        var url = '/TaxSlab/GetTaxSlabNextOrderNo/';
        var data = '';
        JsHelper.ajaxGet(url,data).then(function (response) {
            $('#Order').val(response);
            $('#frmModal').modal('show');
        }).catch(function (error) {
            cl(error);
        });
    },

    LoadSlabTypeDropDown: function () {

        $.ajax({
            url: '/TaxSlab/GetTaxSlabTypeDropDown/',
            type: "POST",
            dataType: "json",
            success: function (data) {

                $("#TaxSlabTypeId").empty();
                var TaxSlabTypeId = '<option value="" selected>Select Type</option>';
                $("#TaxSlabTypeId").prepend(TaxSlabTypeId);

                $.each(data, function (i, item) {
                    var Id = item.Id;
                    var Name = item.Name;
                    var TaxSlabTypeId = '<option value="' + Id + '" >' + Name + '</option>';
                    $("#TaxSlabTypeId").append(TaxSlabTypeId);
                });
                if (newTypeName != null) {
                    $('#TaxSlabTypeId option:contains(' + newTypeName + ')').attr('selected', true);
                    newTypeName = null;
                }
                else {
                    $("#TaxSlabTypeId").val('Selected', true);
                }
            }

        });
    },

    LoadYears: function () {
        var url = "/TaxSlab/GetHrmFinancialYearsInfo/";
        JsHelper.ajaxGet(url).then(function (response) {
            JsHelper.dropdownOptionLoader('#ValidFromHrmFinancialYearId', response, 'Id', 'Name', '-Select-', true);
        });

    },

    SaveSlabType: function () {
        var name = $("#Name").val();

        if (name == "") {
            Message.Warning("Name is required!");
            return;
        }

        var remarks = $("#Remarks").val();
        if (Message.Prompt()) {
            var data = {
                Name: name,
                Remarks: remarks
            };
            var url = '/TaxSlab/InsertTaxSlabType/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    manager.GetDataForTable(1);
                    newTypeName = $("#Name").val();
                    manager.Reset();
                    manager.LoadSlabTypeDropDown(0);
                    Message.CustomSuccess('Tax slab type saved successfully');
                    $('#frmModalType').modal('hide');
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

    Save: function () {
        debugger;
        var slabType = $("#TaxSlabTypeId").val();

        if (slabType == "") {
            Message.Warning("Slab type is required!");
            return;
        }
        var year = $("#ValidFromHrmFinancialYearId").val();

        if (year == "") {
            Message.Warning("Hrm financial year is required!");
            return;
        }

        var limitAbove = $("#LimitAbove").val();
        var taxRate = $("#TaxRate").val();
        var taxAmount = $("#TaxAmount").val();
        var isActive = $("#IsActive").is(':checked');
        var order = $("#Order").val();
        if (Message.Prompt()) {
            var data = {
                TaxSlabTypeId: slabType,
                ValidFromHrmFinancialYearId: year,
                LimitAbove: limitAbove,
                TaxRate: taxRate,
                TaxAmount: taxAmount,
                IsActive: isActive,
                Order: order,
            };
            var url = '/TaxSlab/InsertTaxSlab/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    manager.updateOrderNo();
                    manager.GetDataForTable(1);
                    manager.ResetForm();
                    Message.CustomSuccess('Tax slab saved successfully');
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
        var slabType = $("#TaxSlabTypeId").val();

        if (slabType == "") {
            Message.Warning("Slab type is required!");
            return;
        }
        var year = $("#ValidFromHrmFinancialYearId").val();

        if (year == "") {
            Message.Warning("Hrm financial year is required!");
            return;
        }

        var limitAbove = $("#LimitAbove").val();
        var taxRate = $("#TaxRate").val();
        var taxAmount = $("#TaxAmount").val();
        var isActive = $("#IsActive").is(':checked');
        var order = $("#Order").val();
        if (Message.Prompt()) {
            var data = {
                Id: id,
                TaxSlabTypeId: slabType,
                ValidFromHrmFinancialYearId: year,
                LimitAbove: limitAbove,
                TaxRate: taxRate,
                TaxAmount: taxAmount,
                IsActive: isActive,
                Order: order,
            };
            var url = '/TaxSlab/UpdateTaxSlab/';
            JsHelper.ajaxPost(url, data).then(function (response) {
                if (response == 401) {
                    Message.CustomError('Sorry! You are not authorized to perform this operation.');
                    return;
                }
                if (response.Succeeded) {
                    manager.GetDataForTable(1);
                    manager.ResetForm();
                    Message.CustomSuccess('Tax slab saved successfully');
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
            var jsonParam = { taxSlabId: id };
            var serviceURL = '/TaxSlab/DeleteTaxSlab/';
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
        var serviceURL = "/TaxSlab/GetTaxSlabList/";
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

                    text: '<i class="fas fa-plus-square"></i> Add New Tax Slab',
                    className: 'btn-blue showModalBtn',
                    action: function (e, bt, node, config) {

                        $("#editBtn").hide();
                        $("#saveBtn").show();
                        manager.ResetForm();
                        manager.updateOrderNo();

                    }
                },
                    {
                        text: '<i class="far fa-file-pdf"></i> PDF',
                        className: 'btn-info',
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        title: 'Tax Slab'
                    },
                    {
                        text: '<i class="fas fa-print"></i> Print',
                        className: 'btn-warning',
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        title: 'Tax Slab'
                    },

                    {
                        text: '<i class="far fa-file-excel"></i> Excel',
                        className: 'btn-success',
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        title: 'Tax Slab'
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
                        data: 'SlabType',
                        name: 'SlabType',
                        title: 'Slab Type'
                    },

                     {
                         data: 'LimitAbove',
                         name: 'LimitAbove',
                         title: 'Limit Above'

                     }, {
                         data: 'TaxRate',
                         name: 'TaxRate',
                         title: 'Tax Rate'

                     }, {
                         data: 'TaxAmount',
                         name: 'TaxAmount',
                         title: 'Tax Amount'

                     },
                    //{
                    //    data: 'ValidFromHrmFinancialYearId',
                    //    name: 'ValidFromHrmFinancialYearId',
                    //    title: 'Year'

                    //},
                    {
                        data: 'IsActive',
                        name: 'IsActive',
                        title: 'IsActive',
                        render: function (data, type, row) {
                            return data == true ? "Yes" : "No";
                        }

                    }, {
                        data: 'Order',
                        name: 'Order',
                        title: 'Order'

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

    Reset: function () {
        $("#taxSlabTypeForm")[0].reset();
    },

    ResetForm: function () {
        $('#TaxSlabTypeId').val('').trigger('chosen:updated');
        $("#taxSlabForm")[0].reset();
    }
}

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    id = rowData.Id;
    $('#TaxSlabTypeId').val(rowData.TaxSlabTypeId);
    $('#LimitAbove').val(rowData.LimitAbove);
    $('#TaxRate').val(rowData.TaxRate);
    $('#TaxAmount').val(rowData.TaxAmount);
    $('#ValidFromHrmFinancialYearId').val(rowData.ValidFromHrmFinancialYearId);
    $('#Order').val(rowData.Order);
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

$('.taxSlabTypeAdd').click(function () {
    $('#frmModalType').modal('show');
});