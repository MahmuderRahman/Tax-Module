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
    public class TaxAssetTypeController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult TaxAssetType()
        {
            return View();
        }

        public ActionResult GetTaxAssetTypeList()
        {
            ActionResult rtr = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                var list = (from ta in db.DbSetTaxAssetTypes

                    select new
                    {
                        ta.Id,
                        ta.Name,
                        ta.Description,
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

        public ActionResult InsertTaxAssetType(TaxAssetType taxAssetType)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    db.DbSetTaxAssetTypes.Add(taxAssetType);
                    taxAssetType.CmnCompanyId = 1;
                    taxAssetType.CreatedBy = 1;
                    taxAssetType.CreatedDate = DateTime.Now;
                    taxAssetType.ModifiedBy = null;
                    taxAssetType.ModifiedDate = null;
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

        public ActionResult UpdateTaxAssetType(TaxAssetType taxAssetType)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxAssetType.CmnCompanyId = 1;
                    taxAssetType.CreatedBy = null;
                    taxAssetType.CreatedDate = null;
                    taxAssetType.ModifiedBy = 1;
                    taxAssetType.ModifiedDate = DateTime.Now;
                    db.Entry(taxAssetType).State = EntityState.Modified;
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

        public ActionResult DeleteTaxAssetType(int taxAssetTypeId)
        {
            try
            {
                using (ConnectionDatabase dbContext = new ConnectionDatabase())
                {
                    TaxAssetType taxAssetType = new TaxAssetType { Id = taxAssetTypeId };
                    dbContext.Entry(taxAssetType).State = EntityState.Deleted;
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