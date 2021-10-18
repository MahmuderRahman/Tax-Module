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
    public class TaxSlabTypeController : Controller
    {
        TaxSlabTypeGateway slabType = new TaxSlabTypeGateway();
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult TaxSlabType()
        {
            return View();
        }
        public ActionResult GetTaxSlabTypeList()
        {
            var list = slabType.GetAll();
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
                    slabType.InsertTaxSlabType(taxSlabType);
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
        public ActionResult UpdateTaxSlabType(TaxSlabType taxSlabType)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    taxSlabType.CmnCompanyId = 1;
                    taxSlabType.ModifiedBy = 1;
                    taxSlabType.ModifiedDate = DateTime.Now;
                    slabType.UpdateTaxSlabType(taxSlabType);
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
        public ActionResult DeleteTaxSlabType(int taxSlabTypeId)
        {
            try
            {
                slabType.DeleteTaxSlabType(taxSlabTypeId);
                return Json(slabType, JsonRequestBehavior.AllowGet);
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