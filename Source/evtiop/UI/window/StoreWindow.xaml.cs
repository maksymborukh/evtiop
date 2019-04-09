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

        //set the minimum size of window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinWidth = 800;
            this.MinHeight = 600;           
        }

        private void searchIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        //click on basket icon 
        private void Basket_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create basket user control
            Basket basket = new Basket();

            //add basket user control to grid
            UserContorlContainer.Children.Add(basket);

            //show basket page
            ShowUserControlPage();
        }

        //click on heart icon
        private void heartIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create wishlist user control
            WishList wishList = new WishList();

            //add wishlist user control to grid
            UserContorlContainer.Children.Add(wishList);

            //show wishlist page
            ShowUserControlPage();
        }

        //click on menu icon
        private void menuIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //animate bars icon
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

        //click on user icon 
        private void userIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //open or close menu
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

        //click account item in user menu
        private void AccountPage_Click(object sender, RoutedEventArgs e)
        {
            //create account user control and add it to grid
            Account account = new Account();
            UserContorlContainer.Children.Add(account);

            //show account page
            ShowUserControlPage();

            //close user menu
            CloseUserMenu();
        }

        //click on logout in user menu
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            //close app
            this.Close();
        }

        //click on close icon in page
        private void CloseUserControlContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //clear user control grid
            UserContorlContainer.Children.Clear();

            //hide user control grid
            GridForUserContorlContainer.Visibility = Visibility.Collapsed;

            //enable header
            header.IsEnabled = true;
        }

        private void SettingPage_Click(object sender, RoutedEventArgs e)
        {
            //create setting user control and add it to grid
            UserSetting userSetting = new UserSetting();
            UserContorlContainer.Children.Add(userSetting);

            //show setting page
            ShowUserControlPage();

            //close user menu
            CloseUserMenu();
        }

        private void HelpPage_Click(object sender, RoutedEventArgs e)
        {
            //create help user control and add it to grid
            UserHelp userHelp = new UserHelp();
            UserContorlContainer.Children.Add(userHelp);

            //close user menu
            CloseUserMenu();

            //show help page
            ShowUserControlPage();
        }

        //close user menu
        private void CloseUserMenu()
        {            
            userMenuButtons.Visibility = Visibility.Collapsed;
            userMenuButtons.Tag = "closed";            
        }

        //show user control page
        private void ShowUserControlPage()
        {
            GridForUserContorlContainer.Visibility = Visibility.Visible;
            header.IsEnabled = false;
        }
    }
}
