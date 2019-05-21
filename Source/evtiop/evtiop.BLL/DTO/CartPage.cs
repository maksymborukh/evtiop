using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace evtiop.BLL.DTO
{
    public class CartPage
    {
        public ObservableCollection<CartProduct> cartProducts;

        public static long ID = 0;

        public CartPage(long id)
        {
            ID = id;
        }
        public CartPage()
        {
            cartProducts = new ObservableCollection<CartProduct>();

            BasketOperations basketOperations = new BasketOperations();
            long basketId = basketOperations.GetByCustomerID(ID);

            BasketProductOperations basketProductOperations = new BasketProductOperations();
            List<BasketProducts> list = basketProductOperations.GetAllByID(basketId);

            ProductOperations productOperations = new ProductOperations();

            try
            {
                foreach (BasketProducts el in list)
                {
                    var product = new Product();
                    product = productOperations.GetByID(el.ProductID);

                    var cartProduct = new CartProduct();
                    cartProduct.Description = product.Name + " " + product.Description;
                    cartProduct.Price = product.Price;
                    cartProduct.Quantity = product.Quantity;
                    cartProduct.Total = cartProduct.Price;

                    cartProducts.Add(cartProduct);
                }
            }
            catch
            {

            }
        }
        
        //public void Add() { }

        public ObservableCollection<CartProduct> GetProducts()
        {
            return cartProducts;
        }
    }
}
