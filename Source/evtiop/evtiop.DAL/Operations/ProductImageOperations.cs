using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class ProductImageOperations : IOperations<ProductImage>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from productImages where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<ProductImage> GetAll()
        {
            string commandText = "select * from productImages";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var productImages = new ObservableCollection<ProductImage>();
                while (dataReader.Read())
                {
                    var productImage = new ProductImage();
                    productImage.ID = Convert.ToInt64(dataReader["Id"]);
                    productImage.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    productImage.Name = dataReader["Name"].ToString();
                    productImage.ImageURL = dataReader["ImageURL"].ToString();
                    productImages.Add(productImage);
                }

                return productImages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataReader.Close();
                dbManager.CloseConnection(connection);
            }
        }

        public ProductImage GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from productImages where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var productImage = new ProductImage();
                while (dataReader.Read())
                {
                    productImage.ID = Convert.ToInt64(dataReader["Id"]);
                    productImage.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    productImage.Name = dataReader["Name"].ToString();
                    productImage.ImageURL = dataReader["ImageURL"].ToString();
                }

                return productImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataReader.Close();
                dbManager.CloseConnection(connection);
            }
        }

        public long GetScalarValue()
        {
            string commandText = ""; //must be scalar command
            object scalarValue = dbManager.GetScalarValue(commandText, CommandType.Text);
            return Convert.ToInt64(scalarValue);
        }

        public void Insert(ProductImage productImage)
        {
            string commandText = "insert into productImages (ProductId, Name, ImageURL)" +
                 "values (@ProductId, @Name, @ImageURL);";
            dbManager.Insert(commandText, CommandType.Text, Param(productImage).ToArray());
        }

        public ObservableCollection<ProductImage> SelectAll()
        {
            string commandText = "select * from productImages";

            var productDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var productImages = new ObservableCollection<ProductImage>();
            foreach (DataRow row in productDataTable.Rows)
            {
                var productImage = new ProductImage();
                productImage.ID = Convert.ToInt64(row["Id"]);
                productImage.ProductID = Convert.ToInt64(row["ProductId"]);
                productImage.Name = row["Name"].ToString();
                productImage.ImageURL = row["ImageURL"].ToString();
                productImages.Add(productImage);
            }

            return productImages;
        }

        public void Update(ProductImage productImage)
        {
            string commandText = "update productImages set ProductId = @ProductId, Name = @Name, " +
                "ImageURL = @ImageURL where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(productImage).ToArray());
        }

        public List<IDbDataParameter> Param(ProductImage productImage)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", productImage.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@ProductID", productImage.ProductID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Name", productImage.Name, DbType.String));
            parameters.Add(dbManager.CreateParameter("@ImageURL", productImage.ImageURL, DbType.String));

            return parameters;
        }
    }
}
