using evtiop.DAL.Core;
using evtiop.DAL.Entities;
using evtiop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace evtiop.DAL.Operations
{
    public class CustomerOperations : IOperations<Customer>
    {
        DBManager dbManager = new DBManager("shopdb");
        IDbConnection connection = null;
        public void Delete(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "delete from customer where Id = @Id;";
            dbManager.Delete(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Customer> GetAll()
        {
            string commandText = "select * from customer";
            //var connection = dbManager.GetDbConnection();
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, null, out connection);
            try
            {
                var customers = new ObservableCollection<Customer>();
                while (dataReader.Read())
                {
                    var customer = new Customer();
                    customer.FirstName = dataReader["FirstName"].ToString();
                    customer.LastName = dataReader["LastName"].ToString();
                    customer.EmailAddress = dataReader["EmailAddress"].ToString();
                    customer.Password = dataReader["Password"].ToString();
                    customer.Phone = Convert.ToInt32(dataReader["Phone"]);
                    customer.RegistrationDate = Convert.ToDateTime(dataReader["RegistrationDate"]);
                    customer.ImageURL = dataReader["ImageURL"].ToString();
                    customers.Add(customer);
                }

                return customers;
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

        public Customer GetByID(long id)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", id, DbType.Int64));

            string commandText = "select * from customer where Id = @Id;";
            //var connection = dbManager.GetDbConnection();
            var dataReader = dbManager.GetDataReader(commandText, CommandType.Text, parameters.ToArray(), out connection);
            try
            {
                var customer = new Customer();
                while (dataReader.Read())
                {
                    customer.FirstName = dataReader["FirstName"].ToString();
                    customer.LastName = dataReader["LastName"].ToString();
                    customer.EmailAddress = dataReader["EmailAddress"].ToString();
                    customer.Password = dataReader["Password"].ToString();
                    customer.Phone = Convert.ToInt32(dataReader["Phone"]);
                    customer.RegistrationDate = Convert.ToDateTime(dataReader["RegistrationDate"]);
                    customer.ImageURL = dataReader["ImageURL"].ToString();
                }

                return customer;
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

        public void Insert(Customer customer)
        {
            string commandText = "insert into customers (FirstName, LastName, EmailAddress, Password, Phone, Registration_date, ImageURL)" +
                 "values (@FirstName, @LastName, @EmailAddress, @Password, @Phone, @Registration_date, @ImageURL);";
            dbManager.Insert(commandText, CommandType.Text, Param(customer).ToArray());
        }

        public ObservableCollection<Customer> SelectAll()
        {
            string commandText = "select * from customer";

            var customerDataTable = dbManager.GetDataTable(commandText, CommandType.Text);
            var customers = new ObservableCollection<Customer>();
            foreach (DataRow row in customerDataTable.Rows)
            {
                var customer = new Customer();
                customer.FirstName = row["FirstName"].ToString();
                customer.LastName = row["LastName"].ToString();
                customer.EmailAddress = row["EmailAddress"].ToString();
                customer.Password = row["Password"].ToString();
                customer.Phone = Convert.ToInt32(row["Phone"]);
                customer.RegistrationDate = Convert.ToDateTime(row["RegistrationDate"]);
                customer.ImageURL = row["ImageURL"].ToString();
                customers.Add(customer);
            }

            return customers;
        }

        public void Update(Customer customer)
        {
            string commandText = "insert into customers (FirstName, LastName, EmailAddress, Password, Phone, Registration_date, ImageURL)" +
                 "values (@FirstName, @LastName, @EmailAddress, @Password, @Phone, @Registration_date, @ImageURL);";
            dbManager.Update(commandText, CommandType.Text, Param(customer).ToArray());
        }

        public List<IDbDataParameter> Param(Customer customer)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@FirstName", 50, customer.FirstName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@LastName", customer.LastName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@EmailAddress", customer.EmailAddress, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Password", customer.Password, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Phone", customer.Phone, DbType.Int32));
            parameters.Add(dbManager.CreateParameter("@ImageURL", customer.ImageURL, DbType.String));
            parameters.Add(dbManager.CreateParameter("@RegistrationDate", customer.RegistrationDate, DbType.DateTime));

            return parameters;
        }
    }
}
