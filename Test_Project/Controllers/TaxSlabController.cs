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
    public class TaxSlabController : Controller
    {
        TaxSlabGateway taxSlabGateway = new TaxSlabGateway();
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult TaxSlab()
        {
            return View();
        }
        public ActionResult GetTaxSlabList()
        {
            var list = taxSlabGateway.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertTaxSlabType(TaxSlabType taxSlabType)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxSlabType.CmnCompanyId = 1;
                    taxSlabType.CreatedBy = 1;
                    taxSlabType.CreatedDate = DateTime.Now;
                    taxSlabGateway.InsertTaxSlabType(taxSlabType);
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
        public ActionResult GetTaxSlabTypeDropDown()
        {
            try
            {
                using (ConnectionDatabase database = new ConnectionDatabase())
                {
                    var list = (from st in database.DbSetTaxSlabTypes
                        select new
                        {
                            st.Id,
                            st.Name

                        }).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.DenyGet);
            }
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
        public ActionResult InsertTaxSlab(TaxSlab taxSlab)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxSlab.CmnCompanyId = 1;
                    taxSlab.CreatedBy = 1;
                    taxSlab.CreatedDate = DateTime.Now;
                    taxSlabGateway.InsertTaxslab(taxSlab);
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
        public ActionResult UpdateTaxSlab(TaxSlab taxSlab)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxSlab.CmnCompanyId = 1;
                    taxSlab.ModifiedBy = 1;
                    taxSlab.ModifiedDate = DateTime.Now;
                    taxSlabGateway.UpdateTaxslab(taxSlab);
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
        public ActionResult DeleteTaxSlab(int taxSlabId)
        {
            try
            {
                taxSlabGateway.DeleteTaxslab(taxSlabId);
                return Json(taxSlabGateway, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetTaxSlabNextOrderNo()
        {
            var data = taxSlabGateway.GetTaxSlabNextOrderNo();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}