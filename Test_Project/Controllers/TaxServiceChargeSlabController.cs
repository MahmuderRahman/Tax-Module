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
    public class TaxServiceChargeSlabController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult TaxServiceChargeSlab()
        {
            return View();
        }
        public ActionResult GetTaxServiceChargeSlabList()
        {
            ActionResult rtr = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                var list = (from sc in db.DbSetTaxServiceChargeSlabs
                    join y in db.DbSetHrmFinancialYears on sc.ValidFromHrmFinancialYearId equals y.Id

                    select new
                    {
                        sc.Id,
                        sc.LimitAbove,
                        sc.TaxRate,
                        sc.MinAmount,
                        sc.Description,
                        sc.Status,
                        sc.ValidFromHrmFinancialYearId,
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
        public ActionResult InsertServiceChargeSlab(TaxServiceChargeSlab serviceChargeSlab)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    db.DbSetTaxServiceChargeSlabs.Add(serviceChargeSlab);
                    serviceChargeSlab.CmnCompanyId = 1;
                    serviceChargeSlab.CreatedBy = 1;
                    serviceChargeSlab.CreatedDate = DateTime.Now;
                    serviceChargeSlab.ModifiedBy = null;
                    serviceChargeSlab.ModifiedDate = null;
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
        public ActionResult UpdateServiceChargeSlab(TaxServiceChargeSlab serviceChargeSlab)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    serviceChargeSlab.CmnCompanyId = 1;
                    serviceChargeSlab.CreatedBy = null;
                    serviceChargeSlab.CreatedDate = null;
                    serviceChargeSlab.ModifiedBy = 1;
                    serviceChargeSlab.ModifiedDate = DateTime.Now;
                    db.Entry(serviceChargeSlab).State = EntityState.Modified;
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
        public ActionResult DeleteServiceChargeSlab(int serviceChargeSlabId)
        {
            try
            {
                using (ConnectionDatabase dbContext = new ConnectionDatabase())
                {
                    TaxServiceChargeSlab serviceChargeSlab = new TaxServiceChargeSlab { Id = serviceChargeSlabId };
                    dbContext.Entry(serviceChargeSlab).State = EntityState.Deleted;
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