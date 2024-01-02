using ChatBox.ViewModel;
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
using MongoDB.Driver;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ChatBox.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        private UserVM _userVM;

        public User()
        {
            InitializeComponent();
            _userVM = new UserVM();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Load user data once here
            Connection connection = new Connection("mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority", "AccountInfo");
            string email = Connection.GetLoggedInUserEmail();

            _userVM.LoadUserData(email);
            UpdateControls();

            await Task.Delay(140);

            // Trigger the button click
            Button button = this.FindName("btnUser") as Button;  // Replace "ButtonName" with the actual button name
            if (button != null)
            {
                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Connection connection = new Connection("mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority", "AccountInfo");
            string email = Connection.GetLoggedInUserEmail();
            _userVM.LoadUserData(email);
            UpdateControls();

        }

        private void UpdateControls()
        {
            if (_userVM != null)
            {

                Connection connection = new Connection("mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority", "AccountInfo");
                string pass = Connection.GetLoggedInUserPass();
                // In giá trị để kiểm tra
                txtUser.Text = _userVM.User;
                txtBirthday.Text = _userVM.Birthday;
                txtIntroduction.Text = _userVM.Introduce;
                txtPass.Text = pass;
            }
            else
            {
                MessageBox.Show("Failed to load user data.");
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            string User = txtUser.Text;
            string Birthday = txtBirthday.Text;
            string Introduce = txtIntroduction.Text;
            string Pass = txtPass.Text;
            string Email = Connection.GetLoggedInUserEmail(); // Lấy email đã đăng nhập từ biến global
           

            // Tạo một đối tượng Connection cho việc thao tác trên cơ sở dữ liệu "AccountInfo"
            Connection modify = new Connection(@"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/AccountInfo?retryWrites=true&w=majority", "AccountInfo");

            // Kiểm tra nếu thông tin người dùng đã tồn tại, thì ghi đè, ngược lại tạo mới
            modify.InsertOrUpdateAccountInfo(Email, Pass, User, Birthday, Introduce);
        }
    }
}
