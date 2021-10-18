using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.Controllers.Gateway;
using Test_Project.Models;
using Test_Project.Models.Dtos;

namespace Test_Project.Controllers
{
    public class TaxEmployeeAssetController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        TaxEmployeeAssetGateway taxEmployeeAssetGateway = new TaxEmployeeAssetGateway();
        public ActionResult TaxEmployeeAsset()
        {
            return View();
        }
        public ActionResult GetHrmFinancialYearsInfo()
        {
            var list = (from yr in db.DbSetHrmFinancialYears
                        select new
                        {
                            yr.Id,
                            yr.Name
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmployeeInfo()
        {
            var list = (from yr in db.DbSetEmployees
                        select new
                        {
                            yr.Id,
                            yr.Name
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAssetTypeInfoByEmployee(int? yearId, int? empId)
        {
            if (yearId != null && yearId > 0 && empId != null && empId > 0)
            {
                var list = (from assetType in db.DbSetTaxAssetTypes
                    join empAsset in db.DbSetTaxEmployeeAssets.Where(c => c.HrmFinancialYearId == yearId && c.HrmEmployeeId == empId) on assetType.Id equals empAsset.TaxAssetTypeId into tmpMapp
                    from data in tmpMapp.DefaultIfEmpty()
                    select new
                    {
                        EmpAssetId = (int?)data.Id,
                        AssetTypeId = (int?)data.TaxAssetTypeId,
                        TotalAmount = (int?)data.Amount,
                        assetType.Name,
                        //InvRebateAreaId = data != null ? data.TaxInvestmentRebateAreaId : 0,
                        //TotalInvestment = data != null ? data.TotalInvestment : 0,
                        //ct.TotalInvestment

                    }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var list = (from assetType in db.DbSetTaxAssetTypes
                    select new
                    {
                        AssetTypeId = assetType.Id,
                        assetType.Name,

                    }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
       public ActionResult InsertTaxEmployeeAsset(List<TaxEmployeeAsset> employeeAssets)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxEmployeeAssetGateway.InsertTaxEmployeeAsset(employeeAssets);
                    response.Succeeded = true;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;
                    string message = ex.Message;
                    response.Errors = new List<string> { message };
                    return Json(response, JsonRequestBehavior.DenyGet);
                }
            }

            return Json(response, JsonRequestBehavior.DenyGet);
        }
    }
}