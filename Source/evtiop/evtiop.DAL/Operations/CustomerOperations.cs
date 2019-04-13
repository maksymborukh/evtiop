using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using evtiop.DAL.Interfaces;
using evtiop.DAL.Entities;
using System.Collections.ObjectModel;
using System.Data;
using evtiop.DAL.Core;
using System.Data.Common;

namespace evtiop.DAL.Operations
{
    public class CustomerOperations : IOperations<Customer>
    {
        DBManager dbManager = new DBManager("shopdb");
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetByID(long id)
        {
            throw new NotImplementedException();
        }

        public long GetScalarValue()
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer customer)
        {
            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@FirstName", 50, customer.FirstName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@LastName", customer.LastName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@EmailAddress", customer.EmailAddress, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Password", customer.Password, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Phone", customer.Phone, DbType.Int32));
            parameters.Add(dbManager.CreateParameter("@ImageURL", customer.ImageURL, DbType.String));
            parameters.Add(dbManager.CreateParameter("@RegistrationDate", customer.RegistrationDate, DbType.DateTime));

            string commandText = "insert into customers (FirstName, LastName, EmailAddress, Password, Phone, Registration_date, ImageURL)" +
                 "values (@FirstName, @LastName, @EmailAddress, @Password, @Phone, @Registration_date, @ImageURL);";
            dbManager.Insert(commandText, CommandType.Text, parameters.ToArray());
        }

        public ObservableCollection<Customer> SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
