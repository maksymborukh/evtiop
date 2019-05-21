using evtiop.BLL.Static;
using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace evtiop.BLL.DTO
{
    public class CartPage
    {
        public ObservableCollection<CartProduct> cartProducts;

        public CartPage()
        {
            List<CartProduct> l = new List<CartProduct>();

            BasketOperations basketOperations = new BasketOperations();
            long basketId = basketOperations.GetByCustomerID(StaticUserInfo.CustomerId);

            BasketProductOperations basketProductOperations = new BasketProductOperations();
            List<BasketProducts> list = basketProductOperations.GetAllByID(basketId);

            ProductOperations productOperations = new ProductOperations();

            try
            {
                foreach (BasketProducts el in list)
                {
                    var product = new Product();
                    product = productOperations.GetByID(el.ProductID);

                    long amount = basketProductOperations.GetScalarValue($"select Quantity from basketProducts where BasketId = {basketId} and ProductId = {el.ProductID};");

                    var cartProduct = new CartProduct();
                    cartProduct.Description = product.Name + ": " + product.Description;
                    cartProduct.Price = product.Price;
                    cartProduct.quantity = Convert.ToInt32(amount);
                    cartProduct.total = cartProduct.Price * cartProduct.quantity;
                    cartProduct.Id = product.ID;
                    l.Add(cartProduct);
                    //if (!l.Any(item => item.Id == cartProduct.Id))
                    //{
                    //    l.Add(cartProduct);
                    //}
                    //else
                    //{
                    //    var index = l.FindIndex(c => c.Id == cartProduct.Id);
                    //    cartProduct.Quantity = l[index].Quantity + 1;
                    //    cartProduct.Total = cartProduct.Price * cartProduct.Quantity;
                    //    l[index] = cartProduct;
                    //}
                }
            }
            catch
            {

            }

            cartProducts = new ObservableCollection<CartProduct>(l);
            StaticBasketInfo.ProductsInBasket = cartProducts.Count;
        }

        public ObservableCollection<CartProduct> GetProducts()
        {
            return cartProducts;
        }
    }
}
