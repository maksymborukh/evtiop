using evtiop.BLL.Validation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                //TODO LOGGING
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
