using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : UserControl
    {
        public Account()
        {
            InitializeComponent();
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditPage.Visibility = Visibility.Visible;
            PersonalDataPage.IsEnabled = false;
            Image image = (Image)sender;
            EditMessageTextBlock.Text = image.Tag.ToString();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditImagePage.Visibility = Visibility.Visible;
            PersonalDataPage.IsEnabled = false;
        }

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            //todo save button action in account page
        }

        private void CancelChange_Click(object sender, RoutedEventArgs e)
        {
            EditImagePage.Visibility = Visibility.Collapsed;
            EditPage.Visibility = Visibility.Collapsed;
            PersonalDataPage.IsEnabled = true;
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
