﻿using evtiop.BLL.DTO;
using evtiop.BLL.ObsCollections;
using evtiop.BLL.Server;
using evtiop.BLL.Static;
using evtiop.BLL.Transfer;
using evtiop.BLL.User;
using System.Collections.ObjectModel;
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
        public ObservableCollection<ProductDTO> products = new ObservableCollection<ProductDTO>();

        public StoreWindow()
        {
            StaticUserInfo.CustomerId = 0;

            LoadMainInfo();

            InitializeComponent();

            AmountInCart.Text = StaticBasketInfo.ProductsInBasket.ToString();

            AccountUserButton.IsEnabled = false;
            SettingUserButton.IsEnabled = false;
        }

        public StoreWindow(long Id)
        {
            StaticUserInfo.CustomerId = Id;

            LoadMainInfo();

            InitializeComponent();

            AmountInCart.Text = StaticBasketInfo.ProductsInBasket.ToString();

            userHelper = new UserHelper();

            if (!StaticServerInfo.IsEnableConnectionToServer)
            {
                MessageBox.Show("Cannot connect to server");
            }
            else
            {
                LoadImage();
            }
        }

        private void LoadMainInfo()
        {
            //set page info
            PageInfo pageInfo = new PageInfo();
            StaticPageInfo.Page = 1;
            StaticPageInfo.Offset = pageInfo.GetOffest();
            StaticPageInfo.CurrentOffset = 0;
            StaticPageInfo.Limit = 20;

            //check server connection
            ServerHelper serverHelper = new ServerHelper();
            StaticServerInfo.IsEnableConnectionToServer = serverHelper.CheckFtpConnection();

            //basket
            CartPage cartPage = new CartPage();

            ProductRepository productRepository = new ProductRepository();
            productRepository.LoadProducts();

            products = productRepository.GetProducts();
            this.DataContext = products;
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

        private void Basket_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create cart user control
            Cart basket = new Cart();

            //add cart user control to grid
            UserContorlContainer.Children.Add(basket);

            //show cart page
            ShowUserControlPage();
        }

        private void HeartIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create wishlist user control
            WishList wishList = new WishList();

            //add wishlist user control to grid
            UserContorlContainer.Children.Add(wishList);

            //show wishlist page
            ShowUserControlPage();
        }

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

            AmountInCart.Text = StaticBasketInfo.ProductsInBasket.ToString();
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

        private void CloseUserMenu()
        {
            userMenuButtons.Visibility = Visibility.Collapsed;
            userMenuButtons.Tag = "closed";
        }

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
            StaticPageInfo.CurrentOffset = 0;
            StaticPageInfo.Page = 1;

            var item = CategoryList.SelectedItem as dynamic;
            if (item != null)
            {
                long categId = item.ID;

                ProductRepository productRepository = new ProductRepository();
                products.Clear();

                if (categId == 4294967300)
                {
                    PageInfo pageInfo = new PageInfo();
                    StaticPageInfo.Page = 1;
                    StaticPageInfo.Offset = pageInfo.GetOffest();
                    productRepository.LoadProducts();
                }
                else
                {
                    productRepository.LoadProducts(categId);
                }

                products = productRepository.GetProducts();
                this.DataContext = products;
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            CartHelper cartHelper = new CartHelper();

            dynamic item = ProductList.SelectedItem as dynamic;
            long prodId = item.ID;

            if (!cartHelper.Add(prodId))
            {
                MessageBox.Show("Error. Try again later.");
            }
            else
            {
                AmountInCart.Text = (StaticBasketInfo.ProductsInBasket += 1).ToString();
                MessageBox.Show("Added to cart.");
            }
        }

        private void ProductList_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalOffset + e.ViewportHeight == e.ExtentHeight)
            {
                Pagination.Visibility = Visibility.Visible;
            }
            else if (e.ViewportHeight > e.ExtentHeight)
            {
                Pagination.Visibility = Visibility.Visible;
                PageN.IsEnabled = false;
            }
            else
            {
                Pagination.Visibility = Visibility.Collapsed;
                PageN.IsEnabled = true;
                PageP.IsEnabled = true;
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (StaticPageInfo.Page > 1)
            {
                StaticPageInfo.CurrentOffset -= StaticPageInfo.Limit;
                StaticPageInfo.Page -= 1;
                ProductRepository productRepository = new ProductRepository();
                products.Clear();

                if (CategoryList.SelectedItem == null)
                {
                    productRepository.LoadProducts();
                }
                else
                {
                    var item = CategoryList.SelectedItem as dynamic;
                    long categId = item.ID;
                    productRepository.LoadProducts(categId);
                }

                products = productRepository.GetProducts();
                this.DataContext = products;
            }

            ProductList.SelectedIndex = 0;
            ProductList.ScrollIntoView(ProductList.SelectedItem);
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            StaticPageInfo.CurrentOffset += StaticPageInfo.Limit;
            StaticPageInfo.Page += 1;

            if (StaticPageInfo.CurrentOffset < StaticPageInfo.Offset)
            {
                ProductRepository productRepository = new ProductRepository();
                products.Clear();

                if (CategoryList.SelectedItem == null)
                {
                    productRepository.LoadProducts();
                }
                else
                {
                    var item = CategoryList.SelectedItem as dynamic;
                    long categId = item.ID;
                    if (categId == 4294967300)
                    {
                        productRepository.LoadProducts();
                    }
                    else
                    {
                        productRepository.LoadProducts(categId);
                    }
                }

                products = productRepository.GetProducts();
                this.DataContext = products;              
            }
            else
            {
                StaticPageInfo.CurrentOffset -= StaticPageInfo.Limit;
                StaticPageInfo.Page -= 1;
            }

            ProductList.SelectedIndex = 0;
            ProductList.ScrollIntoView(ProductList.SelectedItem);
        }
    }
}
