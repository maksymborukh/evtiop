using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class BasketOperations : IOperations<Basket>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from baskets where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Basket> GetAll()
        {
            string commandText = "select * from baskets";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var baskets = new ObservableCollection<Basket>();
                while (dataReader.Read())
                {
                    var basket = new Basket();
                    basket.ID = Convert.ToInt64(dataReader["Id"]);
                    basket.CustomerID = Convert.ToInt64(dataReader["CustomerId"]);
                    basket.Added = Convert.ToDateTime(dataReader["Added"]);
                    baskets.Add(basket);
                }

                return baskets;
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

        public Basket GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from baskets where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var basket = new Basket();
                while (dataReader.Read())
                {
                    basket.ID = Convert.ToInt64(dataReader["Id"]);
                    basket.CustomerID = Convert.ToInt64(dataReader["CustomerId"]);
                    basket.Added = Convert.ToDateTime(dataReader["Added"]);
                }

                return basket;
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

        public void Insert(Basket basket)
        {
            string commandText = "insert into baskets (CustomerId, Added)" +
                 "values (@CustomerId, @Added);";
            dbManager.Insert(commandText, CommandType.Text, Param(basket).ToArray());
        }

        public ObservableCollection<Basket> SelectAll()
        {
            string commandText = "select * from baskets";

            var basketDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var baskets = new ObservableCollection<Basket>();
            foreach (DataRow row in basketDataTable.Rows)
            {
                var basket = new Basket();
                basket.ID = Convert.ToInt64(row["Id"]);
                basket.CustomerID = Convert.ToInt64(row["CustomerId"]);
                basket.Added = Convert.ToDateTime(row["Added"]);
                baskets.Add(basket);
            }

            return baskets;
        }

        public void Update(Basket basket)
        {
            string commandText = "update baksers set CustomerID = @CustomerID, Added = @Added where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(basket).ToArray());
        }

        public List<IDbDataParameter> Param(Basket basket)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", basket.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@CustomerID", 50, basket.CustomerID, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Added", basket.Added, DbType.DateTime));

            return parameters;
        }
    }
}
