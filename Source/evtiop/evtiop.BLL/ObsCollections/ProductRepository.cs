using evtiop.BLL.DTO;
using evtiop.BLL.Server;
using evtiop.BLL.Static;
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
        public ObservableCollection<ProductDTO> products;
        public void LoadProducts()
        {
            ProductOperations productOperations = new ProductOperations();
            List<Product> list = productOperations.GetOnePage(StaticPageInfo.Limit, StaticPageInfo.CurrentOffset);

            ServerHelper serverHelper = new ServerHelper();

            if (StaticServerInfo.IsEnableConnectionToServer)
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

            List<ProductDTO> listD = new List<ProductDTO>();
            foreach (Product el in list)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ID = el.ID;
                productDTO.Name = el.Name;
                productDTO.Description = el.Description;
                productDTO.Quantity = el.Quantity;
                productDTO.Price = el.Price;
                productDTO.ManufacturerID = el.ManufacturerID;
                productDTO.ImageURL = el.ImageURL;
                productDTO.ProductImages = el.ProductImages;
                productDTO.ImageSource = el.ImageSource;
                listD.Add(productDTO);
            }

            products = new ObservableCollection<ProductDTO>(listD);
        }
        public ObservableCollection<ProductDTO> GetProducts()
        {
            return products;
        }
    }
}
