using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.Models;
using Test_Project.Models.Dtos;

namespace Test_Project.Controllers
{
    public class InvestmentRebateSlabController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult InvestmentRebateSlab()
        {
            return View();
        }
        public ActionResult GetTaxInvestmentRebateSlabList()
        {
            ActionResult rtr = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                var list = (from rs in db.DbSetTaxInvestmentRebateSlabs
                    join y in db.DbSetHrmFinancialYears on rs.ValidFromHrmFinancialYearId equals y.Id

                            select new
                    {
                        rs.Id,
                        rs.LimitAbove,
                        rs.RebateRate,
                        rs.Description,
                        rs.Status,
                        rs.ValidFromHrmFinancialYearId,
                        YearName = y.Name

                    }).ToList();
                rtr = Json(list, JsonRequestBehavior.DenyGet);
            }

            catch (Exception ex)
            {
                rtr = Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
            return rtr;

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
        public ActionResult InsertInvestmentRebateSlab(TaxInvestmentRebateSlab investmentRebateSlab)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    db.DbSetTaxInvestmentRebateSlabs.Add(investmentRebateSlab);
                    investmentRebateSlab.CmnCompanyId = 1;
                    investmentRebateSlab.CreatedBy = 1;
                    investmentRebateSlab.CreatedDate = DateTime.Now;
                    investmentRebateSlab.ModifiedBy = null;
                    investmentRebateSlab.ModifiedDate = null;
                    db.SaveChanges();

                    response.Succeeded = true;
                    return Json(response, JsonRequestBehavior.DenyGet);
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

            //response.Errors = AppUtility.GetErrorListFromModelState(ModelState);
            return Json(response, JsonRequestBehavior.DenyGet);
        }
        public ActionResult UpdateInvestmentRebateSlab(TaxInvestmentRebateSlab investmentRebateSlab)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    investmentRebateSlab.CmnCompanyId = 1;
                    investmentRebateSlab.CreatedBy = null;
                    investmentRebateSlab.CreatedDate = null;
                    investmentRebateSlab.ModifiedBy = 1;
                    investmentRebateSlab.ModifiedDate = DateTime.Now;
                    db.Entry(investmentRebateSlab).State = EntityState.Modified;
                    db.SaveChanges();
                    response.Succeeded = true;
                    return Json(response, JsonRequestBehavior.DenyGet);
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

            //response.Errors = AppUtility.GetErrorListFromModelState(ModelState);
            return Json(response, JsonRequestBehavior.DenyGet);
        }
        public ActionResult DeleteInvestmentRebateSlab(int investmentRebateSlabId)
        {
            try
            {
                using (ConnectionDatabase dbContext = new ConnectionDatabase())
                {
                    TaxInvestmentRebateSlab investmentRebateSlab = new TaxInvestmentRebateSlab { Id = investmentRebateSlabId };
                    dbContext.Entry(investmentRebateSlab).State = EntityState.Deleted;
                    int row = dbContext.SaveChanges();
                    return Json(row, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                ResponseDto response = new ResponseDto();
                while (e.InnerException != null)
                    e = e.InnerException;
                response.Errors = new List<string> { e.Message };
                return Json(response, JsonRequestBehavior.DenyGet);
            }

        }
    }
}