using evtiop.BLL.DTO;
using evtiop.BLL.Transfer;
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
        public Cart()
        {
            InitializeComponent();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Minus_MouseDown(object sender, RoutedEventArgs e)
        {
            dynamic item = CartList.SelectedItem as dynamic;
            long prodId = item.Id;
            if (item.quantity > 1)
            {
                CartHelper cartHelper = new CartHelper();
                cartHelper.Minus(prodId);
                (CartList.SelectedItem as CartProduct).quantity = item.quantity - 1;
                (CartList.SelectedItem as CartProduct).total = item.total - item.Price;
            }            
        }

        private void Plus_MouseDown(object sender, RoutedEventArgs e)
        {
            dynamic item = CartList.SelectedItem as dynamic;
            long prodId = item.Id;
            CartHelper cartHelper = new CartHelper();
            cartHelper.Add(prodId);
            (CartList.SelectedItem as CartProduct).quantity = item.quantity + 1;
            (CartList.SelectedItem as CartProduct).total = item.total + item.Price;
        }

        private void RemoveFromCart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
