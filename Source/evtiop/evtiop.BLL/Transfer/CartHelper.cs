using evtiop.BLL.Static;
using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;

namespace evtiop.BLL.Transfer
{
    public class CartHelper
    {
        public bool Add(long productId, int quant)
        {
            BasketOperations basketOperations = new BasketOperations();
            Basket basket = new Basket();

            basket.CustomerID = StaticUserInfo.CustomerId;

            BasketProducts basketProducts = new BasketProducts();
            
            basketProducts.ProductID = productId;
            basketProducts.Quantity = quant;
            basket.Added = DateTime.Now.ToUniversalTime();
            try
            {
                basketOperations.Insert(basket);
                basketProducts.BasketID = basketOperations.GetByCustomerID(StaticUserInfo.CustomerId);

                BasketProductOperations basketProductOperations = new BasketProductOperations();
                basketProductOperations.Insert(basketProducts);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
