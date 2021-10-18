using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.Models;

namespace Test_Project.Controllers.Gateway
{
    public class TaxSlabTypeGateway
    {
        ConnectionDatabase db = new ConnectionDatabase();

        public List<TaxSlabType> GetAll()
        {
            return db.DbSetTaxSlabTypes.ToList();
        }
        public int InsertTaxSlabType(TaxSlabType taxSlabType)
        {
            try
            {
                db.DbSetTaxSlabTypes.Add(taxSlabType);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                string message = ex.Message;
                if (message.Contains("UK_Name"))
                    message = string.Format("Tax slab type name({0}) already exists!", taxSlabType.Name);

                throw new Exception(message);
            }
        }
        public int UpdateTaxSlabType(TaxSlabType taxSlabType)
        {
            try
            {
                db.Entry(taxSlabType).State = EntityState.Modified;
                db.SaveChanges();
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                string message = ex.Message;
                if (message.Contains("UK_Name"))
                    message = string.Format("Tax slab type name({0}) already exists!", taxSlabType.Name);

                throw new Exception(message);
            }
        }
        public int DeleteTaxSlabType(int taxSlabTypeId)
        {
            TaxSlabType taxSlabType = new TaxSlabType { Id = taxSlabTypeId };
            db.Entry(taxSlabType).State = EntityState.Deleted;
            return db.SaveChanges();

        }
    }
}