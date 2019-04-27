using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class ProductSpecialOperations : IOperations<ProductSpecial>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from productSpecials where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public List<ProductSpecial> GetAll()
        {
            string commandText = "select * from productSpecials";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var productSpecials = new List<ProductSpecial>();
                while (dataReader.Read())
                {
                    var productSpecial = new ProductSpecial();
                    productSpecial.ID = Convert.ToInt64(dataReader["Id"]);
                    productSpecial.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    productSpecial.ExpireOn = Convert.ToDateTime(dataReader["ExpireOn"]);
                    productSpecial.Available = Convert.ToDateTime(dataReader["Available"]);
                    productSpecial.Discount = Convert.ToInt32(dataReader["Discount"]);
                    productSpecials.Add(productSpecial);
                }

                return productSpecials;
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

        public ProductSpecial GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from productSpecials where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var productSpecial = new ProductSpecial();
                while (dataReader.Read())
                {
                    productSpecial.ID = Convert.ToInt64(dataReader["Id"]);
                    productSpecial.ProductID = Convert.ToInt64(dataReader["ProductId"]);
                    productSpecial.ExpireOn = Convert.ToDateTime(dataReader["ExpireOn"]);
                    productSpecial.Available = Convert.ToDateTime(dataReader["Available"]);
                    productSpecial.Discount = Convert.ToInt32(dataReader["Discount"]);
                }

                return productSpecial;
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

        public void Insert(ProductSpecial productSpecial)
        {
            string commandText = "insert into customers (ProductId, ExpireOn, Available, Discount)" +
                 "values (@ProductId, @ExpireOn, @Available, @Discount);";
            dbManager.Insert(commandText, CommandType.Text, Param(productSpecial).ToArray());
        }

        public List<ProductSpecial> SelectAll()
        {
            string commandText = "select * from productSpecials";

            var productSpecialDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var productSpecials = new List<ProductSpecial>();
            foreach (DataRow row in productSpecialDataTable.Rows)
            {
                var productSpecial = new ProductSpecial();
                productSpecial.ID = Convert.ToInt64(row["Id"]);
                productSpecial.ProductID = Convert.ToInt64(row["ProductId"]);
                productSpecial.ExpireOn = Convert.ToDateTime(row["ExpireOn"]);
                productSpecial.Available = Convert.ToDateTime(row["Available"]);
                productSpecial.Discount = Convert.ToInt32(row["Discount"]);
                productSpecials.Add(productSpecial);
            }

            return productSpecials;
        }

        public void Update(ProductSpecial productSpecial)
        {
            string commandText = "update productSpecial set ProductId = @ProductId, ExpireOn = @ExpireOn, " +
                "Available = @Available, Discount = @Discount where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(productSpecial).ToArray());
        }

        public List<IDbDataParameter> Param(ProductSpecial productSpecial)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", productSpecial.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@ProductId", productSpecial.ProductID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@ExpireOn", productSpecial.ExpireOn, DbType.DateTime));
            parameters.Add(dbManager.CreateParameter("@Available", productSpecial.Available, DbType.DateTime));
            parameters.Add(dbManager.CreateParameter("@Discount", productSpecial.Discount, DbType.UInt32));

            return parameters;
        }
    }
}
