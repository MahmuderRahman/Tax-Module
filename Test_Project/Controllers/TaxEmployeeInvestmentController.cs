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
    public class TaxEmployeeInvestmentController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        TaxEmployeeInvestmentGateway taxEmployeeInvestmentGateway = new TaxEmployeeInvestmentGateway();
        public ActionResult TaxEmployeeInvestment()
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
        public ActionResult GetInvestmentRebateAreaInfo(int? yearId, int? empId)
        {
            if (yearId != null && yearId > 0 && empId != null && empId > 0)
            {
                
                var list = (from area in db.DbSetTaxInvestmentRebateAreas
                            join empInvestment in db.DbSetTaxEmployeeInvestments
                                .Where(c => c.HrmFinancialYearId == yearId && c.HrmEmployeeId == empId)
                                on area.Id equals empInvestment.TaxInvestmentRebateAreaId into tmpMapp
                            from data in tmpMapp.DefaultIfEmpty()
                            select new
                            {
                                EmpInvestmentId = data != null ? data.Id : 0,
                                InvRebateAreaId = data != null ? data.TaxInvestmentRebateAreaId : area.Id,
                                TotalInvestment = data != null ? data.TotalInvestment : 0,
                                area.Name,

                                //EmpInvestmentId = (int?)data.Id,
                                //InvRebateAreaId = (int?)data.TaxInvestmentRebateAreaId,
                                //TotalInvestment = (int?)data.TotalInvestment,
                                //area.Name

                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from rebateArea in db.DbSetTaxInvestmentRebateAreas

                            select new
                            {
                                InvRebateAreaId = rebateArea.Id,
                                rebateArea.Name

                            }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InsertTaxEmployeeInvestment(List<TaxEmployeeInvestment> employeeInvestments)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxEmployeeInvestmentGateway.InsertTaxEmployeeInvestment(employeeInvestments);
                    response.Succeeded = true;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                catch (Exception ex)
                {
                    string message = ex.Message;
                    response.Errors = new List<string> { message };
                    return Json(response, JsonRequestBehavior.DenyGet);
                }
            }

            return Json(response, JsonRequestBehavior.DenyGet);
        }
    }
}