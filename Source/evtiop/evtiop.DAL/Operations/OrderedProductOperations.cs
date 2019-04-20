using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class OrderedProductOperations : IOperations<OrderedProduct>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from orderedProducts where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<OrderedProduct> GetAll()
        {
            string commandText = "select * from orderedOperations";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var orderedProducts = new ObservableCollection<OrderedProduct>();
                while (dataReader.Read())
                {
                    var orderedProduct = new OrderedProduct();
                    orderedProduct.ID = Convert.ToInt64(dataReader["Id"]);
                    orderedProduct.OrderID = Convert.ToInt64(dataReader["OrderId"]);
                    orderedProduct.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    orderedProduct.Name = dataReader["Name"].ToString();
                    orderedProduct.Price = Convert.ToInt32(dataReader["Price"]);
                    orderedProduct.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    orderedProducts.Add(orderedProduct);
                }

                return orderedProducts;
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

        public OrderedProduct GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from orderedProducts where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var orderedProduct = new OrderedProduct();
                while (dataReader.Read())
                {
                    orderedProduct.ID = Convert.ToInt64(dataReader["Id"]);
                    orderedProduct.OrderID = Convert.ToInt64(dataReader["OrderId"]);
                    orderedProduct.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    orderedProduct.Name = dataReader["Name"].ToString();
                    orderedProduct.Price = Convert.ToInt32(dataReader["Price"]);
                    orderedProduct.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                }

                return orderedProduct;
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

        public long GetScalarValue(string commandText)
        {
            object scalarValue = dbManager.GetScalarValue(commandText, CommandType.Text);
            return Convert.ToInt64(scalarValue);
        }

        public void Insert(OrderedProduct orderedProduct)
        {
            string commandText = "insert into orderedProducts (OrderId, ProductId, Name, Price, Quantity)" +
                 "values (@OrderId, @ProductId, @Name, @Price, @Quantity);";
            dbManager.Insert(commandText, CommandType.Text, Param(orderedProduct).ToArray());
        }

        public ObservableCollection<OrderedProduct> SelectAll()
        {
            string commandText = "select * from orderedProducts";

            var orderedProductDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var orderedProducts = new ObservableCollection<OrderedProduct>();
            foreach (DataRow row in orderedProductDataTable.Rows)
            {
                var orderedProduct = new OrderedProduct();
                orderedProduct.ID = Convert.ToInt64(row["Id"]);
                orderedProduct.OrderID = Convert.ToInt64(row["OrderId"]);
                orderedProduct.ProductID = Convert.ToInt64(row["ProductId"]);
                orderedProduct.Name = row["Name"].ToString();
                orderedProduct.Price = Convert.ToInt32(row["Price"]);
                orderedProduct.Quantity = Convert.ToInt32(row["Quantity"]);
                orderedProducts.Add(orderedProduct);
            }

            return orderedProducts;
        }

        public void Update(OrderedProduct orderedProduct)
        {
            string commandText = "update orderedProducts set OrderId = @OrderId, ProductId = @ProductId, " +
                "Name = @Name, Price = @Price, Quantity = @Quantity where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(orderedProduct).ToArray());
        }

        public List<IDbDataParameter> Param(OrderedProduct orderedProduct)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", orderedProduct.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@OrderId", orderedProduct.OrderID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@ProductId", orderedProduct.ProductID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Name", orderedProduct.Name, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Price", orderedProduct.Price, DbType.Int32));
            parameters.Add(dbManager.CreateParameter("@Quantity", orderedProduct.Quantity, DbType.Int32));

            return parameters;
        }
    }
}
