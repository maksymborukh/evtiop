using evtiop.BLL.Server;
using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace evtiop.BLL.ObsCollections
{
    public class ProductRepository
    {
        public ObservableCollection<Product> products;
        public ProductRepository()
        {
            ProductOperations productOperations = new ProductOperations();
            List<Product> list = productOperations.GetAll();

            ServerHelper serverHelper = new ServerHelper();
            bool conn = serverHelper.CheckFtpConnection();

            if (conn)
            {
                foreach (Product p in list)
                {
                    p.ImageSource = serverHelper.GetImageFromServer(p.ImageURL);
                }
            }
            else
            {
                foreach (Product p in list)
                {
                    BitmapImage bimage = new BitmapImage();
                    bimage.BeginInit();
                    bimage.UriSource = new Uri("/UI;component/images/noserverconn.png", UriKind.Relative);
                    bimage.EndInit();
                    p.ImageSource = bimage;
                }
            }
            products = new ObservableCollection<Product>(list);
        }
        public ObservableCollection<Product> GetProducts()
        {
            return products;
        }
    }
}
