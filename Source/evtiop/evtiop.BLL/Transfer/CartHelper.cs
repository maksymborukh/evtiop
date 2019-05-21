using evtiop.BLL.Static;
using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;

namespace evtiop.BLL.Transfer
{
    public class CartHelper
    {
        public bool Add(long productId)
        {
            BasketOperations basketOperations = new BasketOperations();

            bool Create;

            Basket basket = new Basket();
            basket.CustomerID = StaticUserInfo.CustomerId;
            if (basketOperations.GetByCustomerID(StaticUserInfo.CustomerId) == 0)
            {
                Create = true;
            }
            else
            {
                Create = false;
            }

            BasketProducts basketProducts = new BasketProducts();
            
            basketProducts.ProductID = productId;
            basketProducts.Quantity = 1;
            basket.Added = DateTime.Now.ToUniversalTime();
            try
            {
                if (Create)
                {
                    basketOperations.Insert(basket);
                }
                
                basketProducts.BasketID = basketOperations.GetByCustomerID(StaticUserInfo.CustomerId);

                BasketProductOperations basketProductOperations = new BasketProductOperations();
                long amount = basketProductOperations.GetScalarValue($"select Quantity from basketProducts where BasketId = {basketProducts.BasketID} and ProductId = {basketProducts.ProductID};");
                if (amount >= 1)
                {
                    basketProducts.Quantity = Convert.ToInt32(amount) + 1;
                    basketProductOperations.Update(basketProducts);
                }
                else
                {
                    basketProductOperations.Insert(basketProducts);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Minus(long productId)
        {
            BasketOperations basketOperations = new BasketOperations();
            BasketProducts basketProducts = new BasketProducts();

            basketProducts.ProductID = productId;
            try
            {
                basketProducts.BasketID = basketOperations.GetByCustomerID(StaticUserInfo.CustomerId);

                BasketProductOperations basketProductOperations = new BasketProductOperations();
                long amount = basketProductOperations.GetScalarValue($"select Quantity from basketProducts where BasketId = {basketProducts.BasketID} and ProductId = {basketProducts.ProductID};");

                basketProducts.Quantity = Convert.ToInt32(amount) - 1;
                basketProductOperations.Update(basketProducts);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
