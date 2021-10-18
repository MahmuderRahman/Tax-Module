using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.Models;
using Test_Project.Models.Dtos;
using Test_Project.Controllers.Gateway;

namespace Test_Project.Controllers
{
    public class AreaWiseMinimumTaxController : Controller
    {
        AreaWiseMinimumTaxGateway area = new AreaWiseMinimumTaxGateway();
        ConnectionDatabase db = new ConnectionDatabase();
        public ActionResult AreaWiseMinimumTax()
        {
            return View();
        }
        public ActionResult GetAreaWiseMinimumTaxList()
        {
            ActionResult rtr = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                var list = (from at in db.DbSetAreaWiseMinimumTaxes
                            join area in db.DbSetTaxAreas on at.TaxAreaId equals area.Id
                            join yr in db.DbSetHrmFinancialYears on at.ValidFromHrmFinancialYearId equals yr.Id

                            select new
                            {
                                at.Id,
                                at.TaxAreaId,
                                AreaName = area.Name,
                                at.MinimumTax,
                                at.IsActive,
                                at.ValidFromHrmFinancialYearId,
                                YearName = yr.Name

                            }).ToList();
                rtr = Json(list, JsonRequestBehavior.DenyGet);
            }

            catch (Exception ex)
            {
                rtr = Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
            return rtr;

        }
        public ActionResult GetTaxAreaInfo()
        {
            var list = (from yr in db.DbSetTaxAreas
                        select new
                        {
                            yr.Id,
                            yr.Name
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
        public ActionResult InsertAreaWiseMinimumTax(AreaWiseMinimumTax areaWiseMinimumTax)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    areaWiseMinimumTax.CmnCompanyId = 1;
                    areaWiseMinimumTax.CreatedBy = 1;
                    areaWiseMinimumTax.CreatedDate = DateTime.Now;
                    area.InsertAreaWiseMinimumTax(areaWiseMinimumTax);
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
        public ActionResult UpdateAreaWiseMinimumTax(AreaWiseMinimumTax areaWiseMinimumTax)
        {
            ResponseDto response = new ResponseDto();
            if (ModelState.IsValid)
            {
                try
                {
                    areaWiseMinimumTax.CmnCompanyId = 1;
                    areaWiseMinimumTax.ModifiedBy = 1;
                    areaWiseMinimumTax.ModifiedDate = DateTime.Now;
                    area.UpdateAreaWiseMinimumTax(areaWiseMinimumTax);
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
        public ActionResult DeleteAreaWiseMinimumTax(int areaWiseMinimumTaxId)
        {
            try
            {
                area.DeleteAreaWiseMinimumTax(areaWiseMinimumTaxId);
                return Json(area, JsonRequestBehavior.AllowGet);
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