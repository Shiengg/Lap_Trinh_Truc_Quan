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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBox.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string User=txtUser.Text;
            string Birthday = txtBirthday.Text;
            string Introduce = IntroduceTextBox.Text;
            string Email = Connection.GetLoggedInUserEmail(); // Lấy email đã đăng nhập từ biến global
            string Pass = Connection.GetLoggedInUserPass();
            Connection modify = new Connection(@"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority", "AccountInfo");


            modify.InsertAccountInfo(Email, Pass, User, Birthday, Introduce);
        }

    }
}
