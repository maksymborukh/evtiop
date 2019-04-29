using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using evtiop.BLL.DTO;
using evtiop.BLL.User;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : UserControl
    {
        public UserInfo UserInfo { get; set; }
        private long UserId;

        public Account()
        {
            InitializeComponent();
        }

        public Account(long Id)
        {
            InitializeComponent();
            UserInfo = new UserInfo(Id);
            UserId = Id;
            this.DataContext = UserInfo;
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditPage.Visibility = Visibility.Visible;
            PersonalDataPage.IsEnabled = false;
            Image image = (Image)sender;
            EditMessageTextBlock.Text = image.Tag.ToString();
            string detect = EditMessageTextBlock.Text.ToString();
            detect = detect.ToLower();
            detect = detect.Replace(" ", string.Empty);
            EditInfo.Text = detectField(detect);           
        }

        private string detectField(string d)
        {
            switch (d)
            {
                case "firstname":
                    return UserInfo.firstname;
                case "lastname":
                    return UserInfo.lastname;
                case "password":
                    return UserInfo.pass;
                case "phone":
                    return UserInfo.phone;
                case "country":
                    return UserInfo.country;
                case "region":
                    return UserInfo.state;
                case "city":
                    return UserInfo.city;
                case "street":
                    return UserInfo.street;
                default:
                    return null;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditImagePage.Visibility = Visibility.Visible;
            PersonalDataPage.IsEnabled = false;
        }

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            string detect = EditMessageTextBlock.Text.ToString();
            detect = detect.ToLower();
            detect = detect.Replace(" ", string.Empty);
            setField(detect);
            UserHelper userHelper = new UserHelper();
            if (userHelper.UpdateUser(UserInfo, UserId))
            {
                MessageBox.Show("Successfully updated.");
            }
            else
            {
                MessageBox.Show("Error. Try again later.");
                EditInfo.Text = detectField(detect);
            }
            hideEditPage();
        }

        private void setField(string d)
        {
            switch (d)
            {
                case "firstname":
                    UserInfo.firstname = EditInfo.Text;
                    break;
                case "lastname":
                    UserInfo.lastname = EditInfo.Text;
                    break;
                case "password":
                    UserInfo.pass = EditInfo.Text;
                    break;
                case "phone":
                    UserInfo.phone = EditInfo.Text;
                    break;
                case "country":
                    UserInfo.country = EditInfo.Text;
                    break;
                case "region":
                    UserInfo.state = EditInfo.Text;
                    break;
                case "city":
                    UserInfo.city = EditInfo.Text;
                    break;
                case "street":
                    UserInfo.street = EditInfo.Text;
                    break;
                default:
                    break;
            }
        }

        private void CancelChange_Click(object sender, RoutedEventArgs e)
        {
            hideEditPage();
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hideEditPage()
        {
            EditImagePage.Visibility = Visibility.Collapsed;
            EditPage.Visibility = Visibility.Collapsed;
            PersonalDataPage.IsEnabled = true;
        }
    }
}
