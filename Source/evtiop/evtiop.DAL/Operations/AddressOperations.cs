using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;


namespace evtiop.DAL.Operations
{
    public class AddressOperations : IOperations<Address>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from adresses where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Address> GetAll()
        {
            string commandText = "select * from addresses";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var addresses = new ObservableCollection<Address>();
                while (dataReader.Read())
                {
                    var address = new Address();
                    address.ID = Convert.ToInt64(dataReader["Id"]);
                    address.Street = dataReader["Street"].ToString();
                    address.City = dataReader["City"].ToString();
                    address.State = dataReader["State"].ToString();
                    address.Country = dataReader["Country"].ToString();
                    address.CustomerID = Convert.ToInt64(dataReader["CustomerID"]);
                    addresses.Add(address);
                }

                return addresses;
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

        public Address GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from adresses where Id = @Id;";
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var address = new Address();
                while (dataReader.Read())
                {
                    address.ID = Convert.ToInt64(dataReader["Id"]);
                    address.Street = dataReader["Street"].ToString();
                    address.City = dataReader["City"].ToString();
                    address.State = dataReader["State"].ToString();
                    address.Country = dataReader["Country"].ToString();
                    address.CustomerID = Convert.ToInt64(dataReader["CustomerID"]);
                }

                return address;
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

        public void Insert(Address address)
        {
            string commandText = "insert into addresses (Street, City, State, Country, CustomerId)" +
                 "values (@Street, @City, @State, @Country, @CustomerId);";
            dbManager.Insert(commandText, CommandType.Text, Param(address).ToArray());
        }

        public ObservableCollection<Address> SelectAll()
        {
            string commandText = "select * from addresses";

            var addressDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var addresses = new ObservableCollection<Address>();
            foreach (DataRow row in addressDataTable.Rows)
            {
                var address  = new Address();
                address.ID = Convert.ToInt64(row["Id"]);
                address.Street = row["Street"].ToString();
                address.City = row["City"].ToString();
                address.State = row["State"].ToString();
                address.Country = row["Country"].ToString();
                address.CustomerID = Convert.ToInt64(row["CustomerID"]);
                addresses.Add(address);
            }

            return addresses;
        }

        public void Update(Address address)
        {
            string commandText = "update addresses set Street = @Street, City = @City, State = @State, Country = @Country where Id = @Id;";
            dbManager.Update(commandText, CommandType.Text, Param(address).ToArray());
        }

        public List<IDbDataParameter> Param(Address address)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", address.ID, DbType.Int64));
            parameters.Add(dbManager.CreateParameter("@Street", 50, address.Street, DbType.String));
            parameters.Add(dbManager.CreateParameter("@City", address.City, DbType.String));
            parameters.Add(dbManager.CreateParameter("@State", address.State, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Country", address.Country, DbType.String));
            parameters.Add(dbManager.CreateParameter("@CustomerID", address.CustomerID, DbType.Int64));

            return parameters;
        }
    }
}
