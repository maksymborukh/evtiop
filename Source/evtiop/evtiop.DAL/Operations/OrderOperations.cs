using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class OrderOperations : IOperations<Order>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from orders where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Order> GetAll()
        {
            string commandText = "select * from orders";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var orders = new ObservableCollection<Order>();
                while (dataReader.Read())
                {
                    var order = new Order();
                    order.ID = Convert.ToInt64(dataReader["Id"]);
                    order.AddressID = Convert.ToInt64(dataReader["AddressId"]);
                    order.CustomerID = Convert.ToInt64(dataReader["CustomerID"]);
                    order.EmailAddress = dataReader["EmailAddress"].ToString();
                    order.DeliveryDate = Convert.ToDateTime(dataReader["DeliveryDate"]);
                    order.PurchaseDate = Convert.ToDateTime(dataReader["PurchaseDate"]);
                    order.Status = dataReader["Status"].ToString();
                    orders.Add(order);
                }

                return orders;
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

        public Order GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from orders where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var order = new Order();
                while (dataReader.Read())
                {
                    order.ID = Convert.ToInt64(dataReader["Id"]);
                    order.AddressID = Convert.ToInt64(dataReader["AddressId"]);
                    order.CustomerID = Convert.ToInt64(dataReader["CustomerID"]);
                    order.EmailAddress = dataReader["EmailAddress"].ToString();
                    order.DeliveryDate = Convert.ToDateTime(dataReader["DeliveryDate"]);
                    order.PurchaseDate = Convert.ToDateTime(dataReader["PurchaseDate"]);
                    order.Status = dataReader["Status"].ToString();
                }

                return order;
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

        public void Insert(Order order)
        {
            string commandText = "insert into orders (CustomerId, AddressId, EmailAddress, DeliveryDate, PurchaseDate, Status)" +
                 "values (@CustomerId, @AddressId, @EmailAddress, @DeliveryDate, @PurchaseDate, @Status);";
            dbManager.Insert(commandText, CommandType.Text, Param(order).ToArray());
        }

        public ObservableCollection<Order> SelectAll()
        {
            string commandText = "select * from orders";

            var orderDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var orders = new ObservableCollection<Order>();
            foreach (DataRow row in orderDataTable.Rows)
            {
                var order = new Order();
                order.ID = Convert.ToInt64(row["Id"]);
                order.AddressID = Convert.ToInt64(row["AddressId"]);
                order.CustomerID = Convert.ToInt64(row["CustomerID"]);
                order.EmailAddress = row["EmailAddress"].ToString();
                order.DeliveryDate = Convert.ToDateTime(row["DeliveryDate"]);
                order.PurchaseDate = Convert.ToDateTime(row["PurchaseDate"]);
                order.Status = row["Status"].ToString();
                orders.Add(order);
            }

            return orders;
        }

        public void Update(Order order)
        {
            string commandText = "update orders set CustomerId = @CustomerId, AddressId = @AddressId, EmailAddress = @EmailAddress, " +
                "DeliveryDate = @DeliveryDate, PurchaseDate = @PurchaseDate, Status = @Status where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(order).ToArray());
        }

        public List<IDbDataParameter> Param(Order order)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", order.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@AddressId", order.AddressID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@CustomerId", order.CustomerID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@EmailAddress", order.EmailAddress, DbType.String));
            parameters.Add(dbManager.CreateParameter("@DeliveryDate", order.DeliveryDate, DbType.DateTime));
            parameters.Add(dbManager.CreateParameter("@PurchaseDate", order.PurchaseDate, DbType.DateTime));
            parameters.Add(dbManager.CreateParameter("@Status", order.Status, DbType.String));

            return parameters;
        }
    }
}
