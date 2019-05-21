using evtiop.BLL.Server;
using evtiop.BLL.Static;
using evtiop.BLL.Transfer;
using evtiop.BLL.User;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.animation;
using UI.user_control;

namespace UI.window
{
    /// <summary>
    /// Interaction logic for StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Window
    {
        private readonly UserHelper userHelper;

        public StoreWindow()
        {
            InitializeComponent();

            ServerHelper serverHelper = new ServerHelper();
            StaticServerInfo.IsEnableConnectionToServer = serverHelper.CheckFtpConnection();

            AccountUserButton.IsEnabled = false;
            SettingUserButton.IsEnabled = false;

            StaticUserInfo.CustomerId = 0;
        }

        public StoreWindow(long Id)
        {
            InitializeComponent();

            StaticUserInfo.CustomerId = Id;

            userHelper = new UserHelper();

            ServerHelper serverHelper = new ServerHelper();
            StaticServerInfo.IsEnableConnectionToServer = serverHelper.CheckFtpConnection();

            if (!StaticServerInfo.IsEnableConnectionToServer)
            {
                MessageBox.Show("Cannot connect to server");
            }
            else
            {
                LoadImage();
            }
        }

        private void LoadImage()
        {
            if (StaticServerInfo.IsEnableConnectionToServer)
            {
                ServerHelper serverHelper = new ServerHelper();
                try
                {
                    UserIcon.Source = serverHelper.GetImageFromServer(userHelper.GetUser(StaticUserInfo.CustomerId).ImageURL);
                }
                catch
                {
                    MessageBox.Show("Cannot connect to server.");
                }
            }
        }

        //set the minimum size of window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinWidth = 800;
            this.MinHeight = 600;
        }

        private void SearchIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        //click on cart icon 
        private void Basket_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create cart user control
            Cart basket = new Cart();

            //add cart user control to grid
            UserContorlContainer.Children.Add(basket);

            //show cart page
            ShowUserControlPage();
        }

        //click on heart icon
        private void HeartIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create wishlist user control
            WishList wishList = new WishList();

            //add wishlist user control to grid
            UserContorlContainer.Children.Add(wishList);

            //show wishlist page
            ShowUserControlPage();
        }

        //click on menu icon
        private void MenuIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //animate bars icon
            if (menuIcon.Tag.ToString() == "closed")
            {
                Animation.Rotate(0, 90, 300, menuIcon);
                menuIcon.Tag = "opened";
                categories.Visibility = Visibility.Visible;
            }
            else if (menuIcon.Tag.ToString() == "opened")
            {
                Animation.Rotate(90, 0, 300, menuIcon);
                menuIcon.Tag = "closed";
                categories.Visibility = Visibility.Collapsed;
            }
        }

        //click on user icon 
        private void UserIcon_MouseDown(object sender, MouseButtonEventArgs e)
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
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
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
            ProductList.IsEnabled = true;

            //update user icon
            if (StaticUserInfo.CustomerId != 0)
            {
                LoadImage();
            }
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
            UserHelp userHelp;
            if (StaticUserInfo.CustomerId != 0)
            {
                userHelp = new UserHelp(StaticUserInfo.CustomerId);
            }
            else
            {
                userHelp = new UserHelp();
            }
            UserContorlContainer.Children.Add(userHelp);

            //show help page
            ShowUserControlPage();

            //close user menu
            CloseUserMenu();
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
            ProductList.IsEnabled = false;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LeftArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RightArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO category change
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            CartHelper cartHelper = new CartHelper();

            dynamic item = ProductList.SelectedItem as dynamic;
            long prodId = item.ID;
            int q = item.Quantity;

            if (!cartHelper.Add(prodId, q))
            {
                MessageBox.Show("Error. Try again later.");
            }
            else
            {
                MessageBox.Show("Added to cart.");
            }
            //todo check if basket exist
        }
    }
}
