using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.animation;
using UI.user_control;

namespace UI.window
{
    /// <summary>
    /// Interaction logic for StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Window
    {

        public StoreWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinWidth = 800;
            this.MinHeight = 600;           
        }

        private void searchIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Basket_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void heartIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void menuIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (menuIcon.Tag.ToString() == "closed")
            {
                Animation.Rotate(0, 90, 500, menuIcon);
                menuIcon.Tag = "opened";
            }
            else if (menuIcon.Tag.ToString() == "opened")
            {
                Animation.Rotate(90, 0, 500, menuIcon);
                menuIcon.Tag = "closed";
            }
        }

        private void userIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (userMenuButtons.Tag.ToString() == "closed")
            {
                userMenuButtons.Visibility = Visibility.Visible;
                userMenuButtons.Tag = "opened";
            }
            else if (userMenuButtons.Tag.ToString() == "opened")
            {
                userMenuButtons.Visibility = Visibility.Collapsed;
                userMenuButtons.Tag = "closed";
            }
        }

        private void AccountPage_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            UserContorlContainer.Children.Add(account);

            //todo move to business logic
            GridForUserContorlContainer.Visibility = Visibility.Visible;
            userMenuButtons.Visibility = Visibility.Collapsed;
            userMenuButtons.Tag = "closed";
            header.IsEnabled = false;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseUserControlContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserContorlContainer.Children.Clear();
            GridForUserContorlContainer.Visibility = Visibility.Collapsed;
            //in new funct
            userMenuButtons.Visibility = Visibility.Collapsed;
            userMenuButtons.Tag = "closed";
            header.IsEnabled = true;
        }

        private void SettingPage_Click(object sender, RoutedEventArgs e)
        {
            UserSetting userSetting = new UserSetting();
            UserContorlContainer.Children.Add(userSetting);

            //todo move to business logic
            GridForUserContorlContainer.Visibility = Visibility.Visible;
            userMenuButtons.Visibility = Visibility.Collapsed;
            userMenuButtons.Tag = "closed";
            header.IsEnabled = false;
        }
    }
}
