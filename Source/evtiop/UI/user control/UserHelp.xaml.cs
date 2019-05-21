using evtiop.BLL.DTO;
using evtiop.BLL.User;
using System.Windows;
using System.Windows.Controls;

namespace UI.user_control
{
    /// <summary>
    /// Interaction logic for UserHelp.xaml
    /// </summary>
    public partial class UserHelp : UserControl
    {
        public UserInfo UserInfo { get; set; }
        private long UserId;
        public UserHelp()
        {
            InitializeComponent();
        }

        public UserHelp(long Id)
        {
            InitializeComponent();
            UserInfo = new UserInfo(Id);
            UserId = Id;
            this.DataContext = UserInfo;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            UserHelper userHelper = new UserHelper();
            if (!userHelper.SendMessage(email.Text, firstName.Text, lastName.Text, Subject.Text, message.Text, UserId))
            {
                MessageBox.Show("Error. Try again later.");
            }
            else
            {
                MessageBox.Show("Successfully sent.");
            }
        }
    }
}
