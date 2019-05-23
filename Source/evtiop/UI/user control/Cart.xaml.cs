using evtiop.BLL.DTO;
using evtiop.BLL.Static;
using evtiop.BLL.Transfer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Cart : UserControl
    {
        public ObservableCollection<CartProduct> cartProducts = new ObservableCollection<CartProduct>();

        public Cart()
        {
            CartPage cartPage = new CartPage();
            cartProducts = cartPage.GetProducts();
            this.DataContext = cartProducts;
            InitializeComponent();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            //todo make order
        }

        private void Minus_MouseDown(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic item = CartList.SelectedItem as dynamic;
                long prodId = item.Id;
                if (item.quantity > 1)
                {
                    CartHelper cartHelper = new CartHelper();
                    cartHelper.Minus(prodId);
                    (CartList.SelectedItem as CartProduct).quantity = item.quantity - 1;
                    (CartList.SelectedItem as CartProduct).total = item.total - item.Price;
                    TotalPrice totalPrice = (TotalPrice)this.Resources["TotalSum"];
                    totalPrice.sumUp -= item.Price;
                }
            }
            catch
            {
                MessageBox.Show("Error. Try again later");
            }
        }

        private void Plus_MouseDown(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic item = CartList.SelectedItem as dynamic;
                long prodId = item.Id;
                CartHelper cartHelper = new CartHelper();
                cartHelper.Add(prodId);
                (CartList.SelectedItem as CartProduct).quantity = item.quantity + 1;
                (CartList.SelectedItem as CartProduct).total = item.total + item.Price;
                TotalPrice totalPrice = (TotalPrice)this.Resources["TotalSum"];
                totalPrice.sumUp += item.Price;
            }
            catch
            {
                MessageBox.Show("Error. Try again later");
            }
        }

        private void RemoveFromCart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic item = CartList.SelectedItem as dynamic;
                long prodId = item.Id;
                CartHelper cartHelper = new CartHelper();

                if (cartHelper.Delete(prodId))
                {
                    cartProducts.Remove(item);
                    TotalPrice totalPrice = (TotalPrice)this.Resources["TotalSum"];
                    totalPrice.sumUp -= item.total;
                    totalPrice.amount -= 1;
                    StaticBasketInfo.ProductsInBasket -= 1;
                }
                else
                {
                    MessageBox.Show("Error. Try again later.");
                }
            }
            catch
            {
                MessageBox.Show("Error. Try again later");
            }
        }
    }
}
