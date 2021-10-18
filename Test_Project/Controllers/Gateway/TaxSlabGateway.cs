using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.Models;

namespace Test_Project.Controllers.Gateway
{
    public class TaxSlabGateway
    {
        ConnectionDatabase db = new ConnectionDatabase();

        public IEnumerable<dynamic> GetAll()
        {
            using (ConnectionDatabase dbContext = new ConnectionDatabase())
            {
                var list = (from st in dbContext.DbSetTaxSlabs

                    select new
                    {
                        st.Id,
                        st.TaxSlabTypeId,
                        SlabType = st.TaxSlabType.Name,
                        st.TaxAmount,
                        st.TaxRate,
                        st.Order,
                        st.LimitAbove,
                        st.ValidFromHrmFinancialYearId,
                        YearName=st.HrmFinancialYear.Name,
                        st.IsActive

                    }).ToList();
               
                return list;
            }
        }

        public int GetTaxSlabNextOrderNo()
        {
    
            var maxOrder =  db.DbSetTaxSlabs
                .Select(x => x.Order)
                .DefaultIfEmpty(0)
                .Max();
            return maxOrder+1;
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
        public int InsertTaxslab(TaxSlab taxSlab)
        {
            try
            {
                db.DbSetTaxSlabs.Add(taxSlab);
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int UpdateTaxslab(TaxSlab taxSlab)
        {
            try
            {
                db.Entry(taxSlab).State = EntityState.Modified;
                db.SaveChanges();
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int DeleteTaxslab(int taxSlabId)
        {
            TaxSlab taxSlab = new TaxSlab { Id = taxSlabId };
            db.Entry(taxSlab).State = EntityState.Deleted;
            return db.SaveChanges();

        }
        public List<TaxSlab> GetNextOrderNo(int id)
        {
            var nextOrderno = db.DbSetTaxSlabs
                .Where(r => r.Id == id)
                .ToList();
            return nextOrderno;
        }
    }
}