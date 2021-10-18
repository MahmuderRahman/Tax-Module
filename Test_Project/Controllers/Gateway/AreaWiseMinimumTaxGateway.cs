using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using Test_Project.Models;

namespace Test_Project.Controllers.Gateway
{
    public class AreaWiseMinimumTaxGateway
    {
        ConnectionDatabase db = new ConnectionDatabase();
        public int InsertAreaWiseMinimumTax(AreaWiseMinimumTax areaWiseMinimumTax)
        {
            try
            {
                db.DbSetAreaWiseMinimumTaxes.Add(areaWiseMinimumTax);
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int UpdateAreaWiseMinimumTax(AreaWiseMinimumTax areaWiseMinimumTax)
        {
            try
            {
                db.Entry(areaWiseMinimumTax).State = EntityState.Modified;
                db.SaveChanges();
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int DeleteAreaWiseMinimumTax(int areaWiseMinimumTaxId)
        {
            AreaWiseMinimumTax areaWiseMinimumTax = new AreaWiseMinimumTax { Id = areaWiseMinimumTaxId };
            db.Entry(areaWiseMinimumTax).State = EntityState.Deleted;
            return db.SaveChanges();

        }
    }
}