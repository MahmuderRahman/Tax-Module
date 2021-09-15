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
    public class TaxAreaController : Controller
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult TaxArea()
        {
            return View();
        }

        public ActionResult GetTaxAreaList()
        {
            ActionResult rtr = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                var list = (from ta in db.DbSetTaxAreas

                    select new
                    {
                        ta.Id,
                        ta.Name,
                        ta.Remarks

                    }).ToList();
                rtr = Json(list, JsonRequestBehavior.DenyGet);
            }

            catch (Exception ex)
            {
                rtr = Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
            return rtr;

        }

        public ActionResult InsertTaxArea(TaxArea taxArea)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    db.DbSetTaxAreas.Add(taxArea);
                    taxArea.CmnCompanyId = 1;
                    taxArea.CreatedBy = 1;
                    taxArea.CreatedDate = DateTime.Now;
                    taxArea.ModifiedBy = null;
                    taxArea.ModifiedDate = null;
                    db.SaveChanges();

                    response.Succeeded = true;
                    return Json(response, JsonRequestBehavior.DenyGet);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                    string message = ex.Message;
                    if (message.Contains("UK_Name"))
                        message = string.Format("Tax area name({0}) already exists!", taxArea.Name);

                    response.Errors = new List<string> { message };
                    return Json(response, JsonRequestBehavior.DenyGet);
                }
            }
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        public ActionResult UpdateTaxArea(TaxArea taxArea)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxArea.CmnCompanyId = 1;
                    taxArea.CreatedBy = null;
                    taxArea.CreatedDate = null;
                    taxArea.ModifiedBy = 1;
                    taxArea.ModifiedDate = DateTime.Now;
                    db.Entry(taxArea).State = EntityState.Modified;
                    db.SaveChanges();
                    response.Succeeded = true;
                    return Json(response, JsonRequestBehavior.DenyGet);
                }

                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                    string message = ex.Message;
                    if (message.Contains("UK_Name"))
                        message = string.Format("Tax area name({0}) already exists!", taxArea.Name);

                    response.Errors = new List<string> { message };
                    return Json(response, JsonRequestBehavior.DenyGet);
                }
            }
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeleteTaxArea(int taxAreaId)
        {
            try
            {
                using (ConnectionDatabase dbContext = new ConnectionDatabase())
                {
                    TaxArea taxArea = new TaxArea { Id = taxAreaId };
                    dbContext.Entry(taxArea).State = EntityState.Deleted;
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