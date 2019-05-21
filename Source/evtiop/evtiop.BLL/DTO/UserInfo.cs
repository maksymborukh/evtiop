using evtiop.BLL.User;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace evtiop.BLL.DTO
{
    public class UserInfo : INotifyPropertyChanged
    {
        private string FirstName;
        private string LastName;
        public string EmailAddress { get; private set; }
        private string Password;
        private string Phone;
        public DateTime RegistrationDate { get; private set; }
        private string ImageURL;
        private string Street;
        private string City;
        private string State;
        private string Country;

        public UserInfo(long Id)
        {
            UserHelper userHelper = new UserHelper();
            Customer customer =  userHelper.GetUser(Id);
            Address address = userHelper.GetAddress(Id);
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            EmailAddress = customer.EmailAddress;
            Password = customer.Password;
            Phone = customer.Phone;
            RegistrationDate = customer.RegistrationDate;
            ImageURL = customer.ImageURL;
            Street = address.Street;
            City = address.City;
            State = address.State;
            Country = address.Country;
        }

        public string firstname
        {
            get { return FirstName; }
            set
            {
                FirstName = value;
                OnPropertyChanged("firstname");
            }
        }
        public string lastname
        {
            get { return LastName; }
            set
            {
                LastName = value;
                OnPropertyChanged("lastname");
            }
        }
        public string email
        {
            get { return EmailAddress; }
            set
            {
                EmailAddress = value;
                OnPropertyChanged("email");
            }
        }
        public string pass
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged("pass");
            }
        }
        public string phone
        {
            get { return Phone; }
            set
            {
                Phone = value;
                OnPropertyChanged("phone");
            }
        }
        public string image
        {
            get { return ImageURL; }
            set
            {
                ImageURL = value;
                OnPropertyChanged("image");
            }
        }
        public string street
        {
            get { return Street; }
            set
            {
                Street = value;
                OnPropertyChanged("street");
            }
        }
        public string city
        {
            get { return City; }
            set
            {
                City = value;
                OnPropertyChanged("city");
            }
        }
        public string state
        {
            get { return State; }
            set
            {
                State = value;
                OnPropertyChanged("state");
            }
        }
        public string country
        {
            get { return Country; }
            set
            {
                Country = value;
                OnPropertyChanged("country");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
