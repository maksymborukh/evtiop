using evtiop.BLL.Server;
using evtiop.BLL.Static;
using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

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

            StaticBasketInfo.SumUp = 0;
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
                    cartProduct.ImageURL = product.ImageURL;
                    StaticBasketInfo.SumUp += cartProduct.total;
                    l.Add(cartProduct);
                }
            }
            catch
            {

            }
            ServerHelper serverHelper = new ServerHelper();

            if (StaticServerInfo.IsEnableConnectionToServer)
            {
                foreach (CartProduct p in l)
                {
                    p.ImageSource = serverHelper.GetImageFromServer(p.ImageURL);
                }
            }
            else
            {
                foreach (CartProduct p in l)
                {
                    BitmapImage bimage = new BitmapImage();
                    bimage.BeginInit();
                    bimage.UriSource = new Uri("/UI;component/images/noserverconn.png", UriKind.Relative);
                    bimage.EndInit();
                    p.ImageSource = bimage;
                }
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
