using evtiop.DAL.Operations;
using System;
using Customer = evtiop.DAL.Entities.Customer;
using CustomerB = evtiop.BLL.DTO.Customer;
using Address = evtiop.DAL.Entities.Address;
using AddressB = evtiop.BLL.DTO.Address;

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

        public long GetId(string email)
        {
            CustomerOperations customerOperations = new CustomerOperations();
            try
            {
                Customer customer = customerOperations.GetByEmail(email);
                return customer.ID;
            }
            catch
            {
                return 0;
            }
        }

        public CustomerB GetUser(long id)
        {
            CustomerOperations customerOperations = new CustomerOperations();
            try
            {
                Customer customer = customerOperations.GetByID(id);

                return DintoB(customer);
            }
            catch
            {
                return null;
            }
        }

        public AddressB GetAddress(long id)
        {
            AddressOperations addressOperations = new AddressOperations();
            try
            {
                Address address = addressOperations.GetByCustID(id);

                return DintoB(address);
            }
            catch
            {
                return null;
            }
        }

        private CustomerB DintoB(Customer customer)
        {
            CustomerB customerB = new CustomerB();
            customerB.ID = customer.ID;
            customerB.FirstName = customer.FirstName;
            customerB.LastName = customer.LastName;
            customerB.EmailAddress = customer.EmailAddress;
            customerB.Password = customer.Password;
            customerB.Phone = customer.Phone.ToString();
            customerB.RegistrationDate = customer.RegistrationDate;
            customerB.ImageURL = customer.ImageURL;

            return customerB;
        }

        private AddressB DintoB(Address address)
        {
            AddressB addressB = new AddressB();
            addressB.ID = address.ID;
            addressB.Street = address.Street;
            addressB.City = address.City;
            addressB.State = address.State;
            addressB.Country = address.Country;
            addressB.CustomerID = address.CustomerID;

            return addressB;
        }
    }
}
