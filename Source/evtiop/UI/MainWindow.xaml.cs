using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.user_control;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GridForCloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Visibility = Visibility.Visible;
        }

        private void GridForCloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButton.Visibility = Visibility.Collapsed;
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            LoginUserControl loginUserControl = new LoginUserControl();
            loginUserControlContainer.Children.Add(loginUserControl);

            someAnimation("login");            
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpUserControl signUpUserControl = new SignUpUserControl();
            SignUpUserControlContainer.Children.Add(signUpUserControl);

            someAnimation("signUp");
        }

        private void SkipLogging_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void someAnimation(string tag) 
        {

            ((Storyboard)this.Resources["ButtonsAnimation"]).Begin(this);
            ((Storyboard)this.Resources["LogoAnimation"]).Begin(this);
            ((Storyboard)this.Resources["BackgroundAnimation"]).Begin(this);
            ((Storyboard)this.Resources["ForegroundAnimation"]).Begin(this);

            if (tag == "login")
                ((Storyboard)this.Resources["LoginUserControlAnimation"]).Begin(this);
            else if (tag == "signUp")
                ((Storyboard)this.Resources["SignUpUserControlAnimation"]).Begin(this);
        } //TODO REWRITE
    }
}
