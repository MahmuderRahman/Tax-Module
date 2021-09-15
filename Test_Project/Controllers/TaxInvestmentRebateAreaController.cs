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
    public class TaxInvestmentRebateAreaController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult TaxInvestmentRebateArea()
        {
            return View();
        }

        public ActionResult GetTaxInvestmentRebateAreaList()
        {
            ActionResult rtr = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                var list = (from ta in db.DbSetTaxInvestmentRebateAreas

                            select new
                            {
                                ta.Id,
                                ta.Name,
                                ta.Remarks,
                                ta.Status

                            }).ToList();
                rtr = Json(list, JsonRequestBehavior.DenyGet);
            }

            catch (Exception ex)
            {
                rtr = Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
            return rtr;

        }

        public ActionResult InsertTaxInvestmentRebateArea(TaxInvestmentRebateArea taxInvestmentRebateArea)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    db.DbSetTaxInvestmentRebateAreas.Add(taxInvestmentRebateArea);
                    taxInvestmentRebateArea.CmnCompanyId = 1;
                    taxInvestmentRebateArea.CreatedBy = 1;
                    taxInvestmentRebateArea.CreatedDate = DateTime.Now;
                    taxInvestmentRebateArea.ModifiedBy = null;
                    taxInvestmentRebateArea.ModifiedDate = null;
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

        public ActionResult UpdateTaxInvestmentRebateArea(TaxInvestmentRebateArea taxInvestmentRebateArea)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxInvestmentRebateArea.CmnCompanyId = 1;
                    taxInvestmentRebateArea.CreatedBy = null;
                    taxInvestmentRebateArea.CreatedDate = null;
                    taxInvestmentRebateArea.ModifiedBy = 1;
                    taxInvestmentRebateArea.ModifiedDate = DateTime.Now;
                    db.Entry(taxInvestmentRebateArea).State = EntityState.Modified;
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

        public ActionResult DeleteTaxInvestmentRebateArea(int investmentRebateAreaId)
        {
            try
            {
                using (ConnectionDatabase dbContext = new ConnectionDatabase())
                {
                    TaxInvestmentRebateArea taxInvestment = new TaxInvestmentRebateArea { Id = investmentRebateAreaId };
                    dbContext.Entry(taxInvestment).State = EntityState.Deleted;
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