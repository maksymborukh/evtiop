﻿using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class BasketProductOperations : IOperations<BasketProducts>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(BasketProducts basketProducts)
        {
            string commandText = "delete from basketProducts where basketId = @BasketId and ProductId = @ProductId;";
            dbManager.Delete(commandText, CommandType.Text, Param(basketProducts).ToArray());
        }

        public List<BasketProducts> GetAll()
        {
            string commandText = "select * from basketProducts";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var basketProducts = new List<BasketProducts>();
                while (dataReader.Read())
                {
                    var basketProduct = new BasketProducts();
                    basketProduct.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    basketProduct.BasketID = Convert.ToInt64(dataReader["BasketId"]);
                    basketProduct.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    basketProducts.Add(basketProduct);
                }

                return basketProducts;
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

        public List<BasketProducts> GetAllByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@BasketId", id, DbType.Int64));

            string commandText = "select * from basketProducts where BasketId = @BasketId";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var basketProducts = new List<BasketProducts>();
                while (dataReader.Read())
                {
                    var basketProduct = new BasketProducts();
                    basketProduct.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    basketProduct.BasketID = Convert.ToInt64(dataReader["BasketId"]);
                    basketProduct.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    basketProducts.Add(basketProduct);
                }

                return basketProducts;
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

        public BasketProducts GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@BasketId", id, DbType.Int64));

            string commandText = "select * from basketProducts where BasketId = @BasketId;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var basketProduct = new BasketProducts();
                while (dataReader.Read())
                {
                    basketProduct.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    basketProduct.BasketID = Convert.ToInt64(dataReader["BasketId"]);
                    basketProduct.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                }

                return basketProduct;
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

        public void Insert(BasketProducts basketProducts)
        {
            string commandText = "insert into basketProducts (ProductId, BasketId, Quantity)" +
                 "values (@ProductId, @BasketId, @Quantity);";
            dbManager.Insert(commandText, CommandType.Text, Param(basketProducts).ToArray());
        }

        public List<BasketProducts> SelectAll()
        {
            string commandText = "select * from basketProducts";

            var basketProductsDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var basketProducts = new List<BasketProducts>();
            foreach (DataRow row in basketProductsDataTable.Rows)
            {
                var basketProduct = new BasketProducts();
                basketProduct.ProductID = Convert.ToInt64(row["ProductId"]);
                basketProduct.BasketID = Convert.ToInt64(row["BasketId"]);
                basketProduct.Quantity = Convert.ToInt32(row["Quantity"]);
                basketProducts.Add(basketProduct);
            }

            return basketProducts;
        }

        public void Update(BasketProducts basketProducts)
        {
            string commandText = "update basketProducts set ProductId = @ProductId, BasketId = @BasketId, Quantity = @Quantity where basketId = @BasketId and ProductId = @ProductId;";
            dbManager.Update(commandText, CommandType.Text, Param(basketProducts).ToArray());
        }

        public List<IDbDataParameter> Param(BasketProducts basketProducts)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@ProductId", basketProducts.ProductID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@BasketId", basketProducts.BasketID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Quantity", basketProducts.Quantity, DbType.Int32));

            return parameters;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
