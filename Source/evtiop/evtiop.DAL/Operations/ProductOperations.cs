using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class ProductOperations : IOperations<Product>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from products where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Product> GetAll()
        {
            string commandText = "select * from products";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var products = new ObservableCollection<Product>();
                while (dataReader.Read())
                {
                    var product = new Product();
                    product.ID = Convert.ToInt64(dataReader["Id"]);
                    product.Name = dataReader["Name"].ToString();
                    product.Description = dataReader["Description"].ToString();
                    product.Price = Convert.ToInt32(dataReader["Price"]);
                    product.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    product.ManufacturerID = Convert.ToInt64(dataReader["ManufacturerId"]);
                    products.Add(product);
                }

                return products;
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

        public Product GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from products where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var product = new Product();
                while (dataReader.Read())
                {
                    product.ID = Convert.ToInt64(dataReader["Id"]);
                    product.Name = dataReader["Name"].ToString();
                    product.Description = dataReader["Description"].ToString();
                    product.Price = Convert.ToInt32(dataReader["Price"]);
                    product.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    product.ManufacturerID = Convert.ToInt64(dataReader["ManufacturerId"]);
                }

                return product;
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

        public void Insert(Product product)
        {
            string commandText = "insert into products (Name, Description, Price, Quantity, ManufacturerId)" +
                 "values (@Name, @Description, @Price, @Quantity, @ManufacturerId);";
            dbManager.Insert(commandText, CommandType.Text, Param(product).ToArray());
        }

        public ObservableCollection<Product> SelectAll()
        {
            string commandText = "select * from products";

            var productDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var products = new ObservableCollection<Product>();
            foreach (DataRow row in productDataTable.Rows)
            {
                var product = new Product();
                product.ID = Convert.ToInt64(row["Id"]);
                product.Name = row["Name"].ToString();
                product.Description = row["Description"].ToString();
                product.Price = Convert.ToInt32(row["Price"]);
                product.Quantity = Convert.ToInt32(row["Quantity"]);
                product.ManufacturerID = Convert.ToInt64(row["ManufacturerId"]);
                products.Add(product);
            }

            return products;
        }

        public void Update(Product product)
        {
            string commandText = "update products set Name = @Name, Description = @Description, " +
                "Price = @Price, Quantity = @Quantity, ManufacturerId = @ManufacturerId where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(product).ToArray());
        }

        public List<IDbDataParameter> Param(Product product)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", product.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Name", product.Name, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Description", product.Description, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Price", product.Price, DbType.Int32));
            parameters.Add(dbManager.CreateParameter("@Quantity", product.Quantity, DbType.Int32));
            parameters.Add(dbManager.CreateParameter("@ManufacturerId", product.ManufacturerID, DbType.Int64));

            return parameters;
        }
    }
}
