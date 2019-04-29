//version 1.0
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UI.user_control;
using UI.animation;
using UI.window;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StoreWindow storeWindow;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            LoginUserControl loginUserControl = new LoginUserControl();
            loginUserControlContainer.Children.Add(loginUserControl);

            SignUpUserControl signUpUserControl = new SignUpUserControl();
            SignUpUserControlContainer.Children.Add(signUpUserControl);
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
            

            someAnimation("login");            
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            

            someAnimation("signUp");
        }

        private void SkipLogging_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            storeWindow = new StoreWindow();
            this.Close();
            storeWindow.ShowDialog();
            
        }

        public void someAnimation(string tag) 
        {
            Animation.TranslateY(0, -60, 500, Logo);
            Animation.TranslateY(0, -600, 500, WindowBackgound);
            Animation.TranslateX(0, -310, 500, containerOfBaseButton);
            Animation.ForegroundColor(Colors.White, Colors.Teal, 500, Logo); //work only for textblock

            if (tag == "login")
                Animation.TranslateX(340, 0, 500, loginUserControlContainer);
            else if (tag == "signUp")
                Animation.TranslateX(340, 0, 500, SignUpUserControlContainer);
        } //TODO REWRITE

        private void GoToLoginPage_Click(object sender, MouseButtonEventArgs e)
        {
            Animation.TranslateX(0, -340, 500, SignUpUserControlContainer);
            Animation.TranslateX(340, 0, 500, loginUserControlContainer);
        }

        private void GoToCreateAccPage_Click(object sender, MouseButtonEventArgs e)
        {
            Animation.TranslateX(0, 340, 500, loginUserControlContainer);
            Animation.TranslateX(-340, 0, 500, SignUpUserControlContainer);
        }
    }
}
