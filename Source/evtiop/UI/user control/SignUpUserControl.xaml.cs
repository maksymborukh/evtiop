using System.Windows;
using System.Windows.Controls;
using evtiop.BLL.Validation;
using evtiop.BLL.User;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for SignUpUserControl.xaml
    /// </summary>
    public partial class SignUpUserControl : UserControl
    {
        public SignUpUserControl()
        {
            InitializeComponent();            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerValidation customerValidation;
            customerValidation = new CustomerValidation();
            DataContext = customerValidation.Customer;
        }

        private void createAccAction_Click(object sender, RoutedEventArgs e)
        {
            ForceValidation();
            if (!Validation.GetHasError(emailTextBox) && !Validation.GetHasError(passwordTextBox) &&
                !Validation.GetHasError(firstNameTextBox) && !Validation.GetHasError(lastNameTextBox))
            {
                UserHelper userHelper = new UserHelper();

                if (userHelper.CreateAccount(firstNameTextBox.Text, lastNameTextBox.Text, emailTextBox.Text, passwordTextBox.Text))
                {
                    MessageBox.Show("Successfully created.");
                }
                else
                {
                    MessageBox.Show("Error! Try again later.");
                }
            }
            else
                MessageBox.Show("Incorrect input!");
        }

        private void ForceValidation()
        {
            emailTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            passwordTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            firstNameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            lastNameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
