using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test_Project.Models;

namespace Test_Project.Controllers.Gateway
{
    public class TaxEmployeeAssetGateway
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public int InsertTaxEmployeeAsset(List<TaxEmployeeAsset> employeeAssets)
        {
            try
            {
                foreach (var asset in employeeAssets)
                {
                   
                    asset.CmnCompanyId = 1;
                    asset.CreatedBy = 1;
                    asset.CreatedDate = DateTime.Now;
                    db.DbSetTaxEmployeeAssets.Add(asset);
                }
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}