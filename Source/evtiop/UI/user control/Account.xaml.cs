using evtiop.BLL.DTO;
using evtiop.BLL.User;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : UserControl
    {
        public UserInfo UserInfo { get; set; }
        private long UserId;
        private string ImageName { get; set; }
        private string ImagePath { get; set; }
        public Account()
        {
            InitializeComponent();
        }

        public Account(long Id)
        {
            InitializeComponent();
            UserInfo = new UserInfo(Id);
            UserId = Id;
            ImageName = UserInfo.image;
            LoadImage();
            this.DataContext = UserInfo;
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditPage.Visibility = Visibility.Visible;
            PersonalDataPage.IsEnabled = false;
            Image image = (Image)sender;
            EditMessageTextBlock.Text = image.Tag.ToString();
            string detect = EditMessageTextBlock.Text.ToString();
            detect = detect.ToLower();
            detect = detect.Replace(" ", string.Empty);
            EditInfo.Text = detectField(detect);
        }

        private string detectField(string d)
        {
            switch (d)
            {
                case "firstname":
                    return UserInfo.firstname;
                case "lastname":
                    return UserInfo.lastname;
                case "password":
                    return UserInfo.pass;
                case "phone":
                    return UserInfo.phone;
                case "country":
                    return UserInfo.country;
                case "region":
                    return UserInfo.state;
                case "city":
                    return UserInfo.city;
                case "street":
                    return UserInfo.street;
                default:
                    return null;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditImagePage.Visibility = Visibility.Visible;
            PersonalDataPage.IsEnabled = false;
        }

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            string detect = EditMessageTextBlock.Text.ToString();
            detect = detect.ToLower();
            detect = detect.Replace(" ", string.Empty);
            setField(detect);
            UserHelper userHelper = new UserHelper();
            if (userHelper.UpdateUser(UserInfo, UserId))
            {
                MessageBox.Show("Successfully updated.");
            }
            else
            {
                MessageBox.Show("Error. Try again later.");
                EditInfo.Text = detectField(detect);
            }
            hideEditPage();
        }

        private void setField(string d)
        {
            switch (d)
            {
                case "firstname":
                    UserInfo.firstname = EditInfo.Text;
                    break;
                case "lastname":
                    UserInfo.lastname = EditInfo.Text;
                    break;
                case "password":
                    UserInfo.pass = EditInfo.Text;
                    break;
                case "phone":
                    UserInfo.phone = EditInfo.Text;
                    break;
                case "country":
                    UserInfo.country = EditInfo.Text;
                    break;
                case "region":
                    UserInfo.state = EditInfo.Text;
                    break;
                case "city":
                    UserInfo.city = EditInfo.Text;
                    break;
                case "street":
                    UserInfo.street = EditInfo.Text;
                    break;
                default:
                    break;
            }
        }

        private void CancelChange_Click(object sender, RoutedEventArgs e)
        {
            hideEditPage();
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                UserImage.Source = new BitmapImage(new Uri(op.FileName));
                UserImageSmall.Source = new BitmapImage(new Uri(op.FileName));
            }
            ImageName = Path.GetFileName(op.FileName);
            ImagePath = op.FileName;
        }

        private void hideEditPage()
        {
            EditImagePage.Visibility = Visibility.Collapsed;
            EditPage.Visibility = Visibility.Collapsed;
            PersonalDataPage.IsEnabled = true;
        }

        private void SaveImageChange_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("admin", "admin");
                client.UploadFile($"ftp://192.168.0.117/ {ImageName}", WebRequestMethods.Ftp.UploadFile, ImagePath);
            }

            string detect = EditMessageTextBlock.Text.ToString();
            detect = detect.ToLower();
            detect = detect.Replace(" ", string.Empty);
            setField(detect);
            
            UserHelper userHelper = new UserHelper();
            UserInfo.image = Path.GetFileName(UserImage.Source.ToString());
            //    ((BitmapFrame)UserImage.Source).Decoder.ToString();
            if (userHelper.UpdateUser(UserInfo, UserId))
            {
                MessageBox.Show("Successfully updated.");
            }
            else
            {
                MessageBox.Show("Error. Try again later.");
                setField(detect);
            }
            hideEditPage();

            
        }

        private void LoadImage()
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("admin", "admin");
                using (MemoryStream stream = new MemoryStream(client.DownloadData($"ftp://192.168.0.117/ {ImageName}")))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = stream;
                    image.EndInit();

                    UserImage.Source = image;
                    UserImageSmall.Source = image;
                }
            }           
        }
    }
}
