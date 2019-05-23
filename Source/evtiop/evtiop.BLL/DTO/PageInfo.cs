using evtiop.BLL.Static;
using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;
using System.Collections.Generic;

namespace evtiop.BLL.DTO
{
    public class PageInfo
    {
        public int GetOffest()
        {
            long amount;
            try
            {
                ProductOperations productOperations = new ProductOperations();
                amount = productOperations.GetScalarValue($"select Count(id) from Products");
            }
            catch
            {
                amount = 20;
            }
            return Convert.ToInt32(amount);
        }

        public int GetOffest(long categId)
        {
            long amount;
            try
            {
                CategoryProductOperations categoryProductOperations = new CategoryProductOperations();
                amount = categoryProductOperations.GetScalarValue($"select Count(productid) from CategoryProducts where CategoryId = {categId}");
            }
            catch
            {
                amount = 20;
            }
            return Convert.ToInt32(amount);
        }

        public bool LoadPage()
        {
            ProductOperations productOperations = new ProductOperations();
            try
            {
                List<Product> list = productOperations.GetOnePage(StaticPageInfo.Limit, StaticPageInfo.CurrentOffset);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
