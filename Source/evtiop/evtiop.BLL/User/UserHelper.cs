using evtiop.DAL.Entities;
using evtiop.DAL.Operations;
using System;

namespace evtiop.BLL.User
{
    public class UserHelper
    {
        public bool Authentication(string email, string password, ref string ex)
        {
            CustomerOperations customer = new CustomerOperations();
            Customer customers = customer.GetByEmail(email);

            if (customers.EmailAddress != null)
            {
                if (customers.Password == password)
                {
                    ex = "OK";
                    return true;
                }
                else
                {
                    ex = "Invalid password";
                    return false;
                }
            }
            else
            {
                ex = "User does not exist";
                return false;
            }
        }

        public bool CreateAccount(string firstName, string lastName, string email, string password)
        {
            CustomerOperations customerOperations = new CustomerOperations();
            Customer customer = new Customer();
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.EmailAddress = email;
            customer.Password = password;
            customer.RegistrationDate = DateTime.Now.ToUniversalTime();
            customer.ImageURL = "";

            try
            {
                customerOperations.Insert(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
