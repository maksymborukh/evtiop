using evtiop.BLL.Validation;
using evtiop.BLL.User;
using System.Windows;
using System.Windows.Controls;
using UI.window;
using evtiop.BLL.DTO;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerValidation customerValidation;
            customerValidation = new CustomerValidation();
            DataContext = customerValidation.Customer;
        }

        private void loginAction_Click(object sender, RoutedEventArgs e)
        {
            ForceValidation();
            if (!Validation.GetHasError(emailTextBox) && !Validation.GetHasError(passwordTextBox))
            {
                string ex = null;
                UserHelper userHelper = new UserHelper();
                if (userHelper.Authentication(emailTextBox.Text, passwordTextBox.Text, ref ex))
                {
                    StoreWindow storeWindow = new StoreWindow(userHelper.GetId(emailTextBox.Text));
                    var myWindow = Window.GetWindow(this);
                    myWindow.Close();                 
                    storeWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show(ex);
                }
            }
            else
                MessageBox.Show("Incorrect input!");
        }

        private void ForceValidation()
        {
            emailTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            passwordTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
