using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.Models;

namespace Test_Project.Controllers.Gateway
{
    public class TaxEmployeeInvestmentGateway
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public int InsertTaxEmployeeInvestment(List<TaxEmployeeInvestment> employeeInvestments)
        {
            try
            {
                foreach (var empInvestment in employeeInvestments)
                {


                    if (empInvestment.Id > 0)
                    {
                        empInvestment.CmnCompanyId = 1;
                        empInvestment.ModifiedBy = 1;
                        empInvestment.ModifiedDate = DateTime.Now;
                        db.Entry(empInvestment).State = EntityState.Modified;
                    }
                    else
                    {
                        var list = (from empInv in db.DbSetTaxEmployeeInvestments
                                    where empInvestment.HrmFinancialYearId == empInv.HrmFinancialYearId &&
                                          empInvestment.HrmEmployeeId == empInv.HrmEmployeeId && empInvestment.TaxInvestmentRebateAreaId == empInv.TaxInvestmentRebateAreaId
                                    select new
                                    {
                                        empInv.Id,
                                        empInv.TaxInvestmentRebateAreaId,
                                        empInv.TotalInvestment
                                    }).ToList();

                        if (list.Count > 0)
                        {
                            var totalAmount = list[0].TotalInvestment + empInvestment.TotalInvestment;
                            empInvestment.TotalInvestment = totalAmount;
                            empInvestment.Id = list[0].Id;
                            empInvestment.CmnCompanyId = 1;
                            empInvestment.ModifiedBy = 1;
                            empInvestment.ModifiedDate = DateTime.Now;
                            db.Entry(empInvestment).State = EntityState.Modified;
                        }
                        else
                        {
                            empInvestment.CmnCompanyId = 1;
                            empInvestment.CreatedBy = 1;
                            empInvestment.CreatedDate = DateTime.Now;
                            db.DbSetTaxEmployeeInvestments.Add(empInvestment);
                        }
                    }
                }
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                string message = ex.Message;
                throw new Exception(message);
            }
        }
    }
}